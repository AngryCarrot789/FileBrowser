using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileBrowser.Core.Editor.FileExplorer;
using FileBrowser.Core.Editor.FileTree;
using FileBrowser.Core.Editor.FileTree.Physical;

namespace FileBrowser.Core.Editor {
    public class MainViewModel : BaseViewModel {
        public AsyncRelayCommand OpenFolderCommand { get; }

        public FileManagerViewModel FileManager { get; }

        public FileTreeViewModel FileTree { get; }

        public MainViewModel() {
            this.OpenFolderCommand = new AsyncRelayCommand(this.OpenFolderAction);
            this.FileManager = new FileManagerViewModel();
            this.FileTree = new FileTreeViewModel();

            // just hoping these never throw...

            foreach (Environment.SpecialFolder folder in Enum.GetValues(typeof(Environment.SpecialFolder))) {
                string path = Environment.GetFolderPath(folder);
                if (!string.IsNullOrEmpty(path) && Directory.Exists(path)) {
                    this.FileTree.Root.AddFile(new IOFolderItemViewModel(path));
                }
            }

            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (!string.IsNullOrEmpty(desktop) && Directory.Exists(desktop)) {
                this.FileManager.OpenExplorerAtInternal(desktop);
            }

            this.FileManager.ActiveExplorer = this.FileManager.OpenExplorerAtInternal(null);
        }

        private async Task ExplorerOnOpenFile(IOFileItemViewModel file) {
            if (!File.Exists(file.FilePath)) {
                if (file.Parent != null) {
                    await file.Parent.RefreshAsync();
                }

                return;
            }

            // open process?
        }

        private async Task OpenFolderAction() {
            string path = await IoC.FilePicker.OpenFolder(null, "Select a folder to open");
            if (string.IsNullOrEmpty(path)) {
                return;
            }

            FileExplorerViewModel explorer = this.FileManager.Explorers.FirstOrDefault(x => x.CurrentFolder == path);
            if (explorer == null) {
                explorer = await this.FileManager.OpenExplorerAt(path);
            }

            this.FileManager.ActiveExplorer = explorer;
        }
    }
}