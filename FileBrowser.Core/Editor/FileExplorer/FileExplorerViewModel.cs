using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileBrowser.Core.Utils;

namespace FileBrowser.Core.Editor.FileExplorer {
    /// <summary>
    /// A view model for handling a 'file explorer' view, which allows users to navigate, open files, etc.
    /// </summary>
    public class FileExplorerViewModel : BaseViewModel {
        private string currentFolder;

        /// <summary>
        /// The folder path that this explorer is currently viewing
        /// </summary>
        public string CurrentFolder {
            get => this.currentFolder;
            private set {
                this.RaisePropertyChanged(ref this.currentFolder, value);
                this.RaisePropertyChanged(nameof(this.CurrentFolderName));
            }
        }

        public string CurrentFolderName {
            get {
                if (string.IsNullOrEmpty(this.CurrentFolder))
                    return "";
                string path = Path.GetFileName(this.CurrentFolder);
                return string.IsNullOrEmpty(path) ? this.CurrentFolder : path;
            }
        }

        private bool isNavigating;
        public bool IsNavigating {
            get => this.isNavigating;
            set => this.RaisePropertyChanged(ref this.isNavigating, value);
        }

        private readonly ObservableCollection<BaseExplorerItemViewModel> items;
        public ReadOnlyObservableCollection<BaseExplorerItemViewModel> Items { get; }

        public FileExplorerViewModel() {
            this.items = new ObservableCollection<BaseExplorerItemViewModel>();
            this.Items = new ReadOnlyObservableCollection<BaseExplorerItemViewModel>(this.items);
        }

        public async Task NavigateAction(string path) {
            DirectoryInfo info = new DirectoryInfo(path);
            IEnumerator<FileSystemInfo> enumerator;
            try {
                enumerator = info.EnumerateFileSystemInfos().GetEnumerator();
            }
            catch (DirectoryNotFoundException e) {
                await IoC.MessageDialogs.ShowMessageExAsync("No such directory", $"'{path}' does not exist", e.GetToString());
                return;
            }
            catch (UnauthorizedAccessException e) {
                await IoC.MessageDialogs.ShowMessageExAsync("Unauthorized Access", "You don't have permission to view this folder", e.GetToString());
                return;
            }
            catch (Exception e) {
                await IoC.MessageDialogs.ShowMessageExAsync("Error", "Error while opening folder enumerator", e.GetToString());
                return;
            }

            this.IsNavigating = true;
            this.Clear();
            this.CurrentFolder = path;
            while (true) {
                bool moveNext = false;
                try {
                    moveNext = enumerator.MoveNext();
                }
                catch (DirectoryNotFoundException e) {
                    await IoC.MessageDialogs.ShowMessageExAsync("Directory no longer exists", $"'{path}' no longer exists", e.GetToString());
                    return;
                }
                catch (UnauthorizedAccessException e) {
                    await IoC.MessageDialogs.ShowMessageExAsync("Unauthorized Access", "You no longer have permission to view this folder", e.GetToString());
                    return;
                }
                catch (Exception e) {
                    await IoC.MessageDialogs.ShowMessageExAsync("Error", "Error while enumerating next file system entry", e.GetToString());
                    return;
                }

                if (!moveNext)
                    break;

#if DEBUG
                this.ProcessEntry(enumerator.Current);
#else
                try {
                    this.ProcessEntry(enumerator.Current);
                }
                catch (Exception e) {
                    await IoC.MessageDialogs.ShowMessageExAsync("Error", "Error processing file system entry: " + e, e.GetToString());
                }
#endif
            }

            enumerator.Dispose();
            this.IsNavigating = false;
        }

        public void Clear() {
            this.items.Clear();
        }

        private static readonly Comparison<BaseExplorerItemViewModel> SortComparer = (a, b) => {
            if (a is ExplorerIOFolderItemViewModel) {
                if (b is ExplorerIOFolderItemViewModel) {
                    return string.Compare(((ExplorerIOFolderItemViewModel) a).FileName, ((ExplorerIOFolderItemViewModel) b).FileName);
                }
                else {
                    return -1; // A comes before B
                }
            }
            else if (b is ExplorerIOFolderItemViewModel) {
                return 1; // A comes after B
            }
            else if (a is BaseIOExplorerItemViewModel && b is BaseIOExplorerItemViewModel) {
                return string.Compare(((BaseIOExplorerItemViewModel) a).FileName, ((BaseIOExplorerItemViewModel) b).FileName);
            }
            else {
                return 0;
            }
        };

        private void ProcessEntry(FileSystemInfo info) {
            BaseExplorerItemViewModel item;
            if (info is DirectoryInfo dir) {
                item = new ExplorerIOFolderItemViewModel() {
                    FilePath = dir.FullName
                };
            }
            else {
                FileInfo file = (FileInfo) info;
                item = new ExplorerIOFileItemViewModel() {
                    FilePath = file.FullName
                };
            }

            int index = CollectionUtils.GetSortInsertionIndex(this.items, item, SortComparer);
            this.items.Insert(index, item);
        }

        public void NavigateInternal(string path) {
            DirectoryInfo info = new DirectoryInfo(path);
            IEnumerable<FileSystemInfo> enumerator = info.EnumerateFileSystemInfos();
            this.Clear();
            this.CurrentFolder = path;

            foreach (FileSystemInfo entry in enumerator) {
                this.ProcessEntry(entry);
            }
        }
    }
}