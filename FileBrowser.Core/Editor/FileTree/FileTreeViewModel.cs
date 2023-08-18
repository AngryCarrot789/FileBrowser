using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using FileBrowser.Core.Drop;
using FileBrowser.Core.Editor.FileTree.Events;
using FileBrowser.Core.Editor.FileTree.Physical;
using FileBrowser.Core.Editor.FileTree.Zip;

namespace FileBrowser.Core.Editor.FileTree {
    /// <summary>
    /// A view model that contains a tree of files
    /// </summary>
    public class FileTreeViewModel : BaseViewModel, IDropHandler {
        public RootFolderItemViewModel Root { get; }

        public AsyncRelayCommand<BaseTreeFileViewModel> OpenItemCommand { get; }

        public event OpenFileEventHandler OpenFile;
        public event OpenZipEntryEventHandler OpenZipFile;

        public FileTreeViewModel() {
            this.OpenItemCommand = new AsyncRelayCommand<BaseTreeFileViewModel>(this.OpenFileAction);
            this.Root = new RootFolderItemViewModel();
            this.Root.SetExplorer(this);
        }

        /// <summary>
        /// Returns either a <see cref="IOFolderItemViewModel"/>, <see cref="ZipFileViewModel"/> or <see cref="IOFileItemViewModel"/>
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static BaseTreeFileViewModel ForPath(string path) {
            if (Directory.Exists(path)) {
                return new IOFolderItemViewModel(path);
            }
            else {
                string extension = Path.GetExtension(path);
                if (extension == ".jar" || extension == ".zip") {
                    return new ZipFileViewModel(path);
                }
                else {
                    return new IOFileItemViewModel(path);
                }
            }
        }

        public async Task OpenFileAction(BaseTreeFileViewModel item) {
            if (item is IOFileItemViewModel) {
                await this.OpenFileAsync((IOFileItemViewModel) item);
            }
            else if (item is ZipFileEntryViewModel) {
                await this.OpenFileAsync((ZipFileEntryViewModel) item);
            }
            else {
                await IoC.MessageDialogs.ShowDialogAsync("Cannot open", "This file cannot be opened. Must be a physical file or zip file");
            }
        }

        // Are task events bad?

        /// <summary>
        /// Called by IO files when they are trying to be opened
        /// </summary>
        /// <param name="file"></param>
        /// <exception cref="NotImplementedException"></exception>
        public async Task OpenFileAsync(IOFileItemViewModel file) {
            OpenFileEventHandler x = this.OpenFile;
            if (x != null)
                await x(file);
        }

        public async Task OpenFileAsync(ZipFileEntryViewModel file) {
            OpenZipEntryEventHandler x = this.OpenZipFile;
            if (x != null)
                await x(file);
        }

        public DropType OnDropEnter(string[] paths) {
            Debug.WriteLine("Drop Enter! " + string.Join(", ", paths));
            return DropType.Link;
        }

        public void OnDropLeave(bool isCancelled) {
            Debug.WriteLine("Drop Leave! " + (isCancelled ? "Cancelled" : "Not cancelled"));
        }

        public Task OnFilesDropped(string[] paths) {
            foreach (string path in paths) {
                this.Root.AddFile(ForPath(path));
            }

            Debug.WriteLine("Dropped! " + string.Join(", ", paths));
            return Task.CompletedTask;
        }
    }
}