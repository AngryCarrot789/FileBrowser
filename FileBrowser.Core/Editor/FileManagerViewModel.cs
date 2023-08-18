using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileBrowser.Core.Drop;
using FileBrowser.Core.Editor.FileExplorer;

namespace FileBrowser.Core.Editor {
    /// <summary>
    /// A view model for managing multiple explorer views
    /// </summary>
    public class FileManagerViewModel : BaseViewModel, IDropHandler {
        private readonly ObservableCollection<FileExplorerViewModel> explorers;
        private FileExplorerViewModel activeExplorer;

        public ReadOnlyObservableCollection<FileExplorerViewModel> Explorers { get; set; }

        /// <summary>
        /// The active file explorer that the UI is focused on
        /// </summary>
        public FileExplorerViewModel ActiveExplorer {
            get => this.activeExplorer;
            set => this.RaisePropertyChanged(ref this.activeExplorer, value);
        }

        public FileManagerViewModel() {
            this.explorers = new ObservableCollection<FileExplorerViewModel>();
            this.Explorers = new ReadOnlyObservableCollection<FileExplorerViewModel>(this.explorers);
        }

        public async Task<FileExplorerViewModel> OpenExplorerAt(string path) {
            FileExplorerViewModel explorer = new FileExplorerViewModel();
            this.explorers.Add(explorer);

            await explorer.NavigateAction(path);
            return explorer;
        }

        public FileExplorerViewModel OpenExplorerAtInternal(string path) {
            FileExplorerViewModel explorer = new FileExplorerViewModel();
            this.explorers.Add(explorer);
            explorer.NavigateInternal(path);
            return explorer;
        }

        public DropType OnDropEnter(string[] paths) {
            return DropType.Copy;
        }

        public void OnDropLeave(bool isCancelled) {

        }

        public async Task OnFilesDropped(string[] paths) {
            HashSet<string> existing = new HashSet<string>(this.explorers.Select(x => x.CurrentFolder));
            foreach (string path in paths) {
                if (!existing.Contains(path) && Directory.Exists(path)) {
                    await this.OpenExplorerAt(path);
                }
            }
        }

        public async Task NavigateToFolder(string path)
        {
            if (this.ActiveExplorer == null)
            {
                if (this.explorers.Count < 1)
                {
                    FileExplorerViewModel explorer = new FileExplorerViewModel();
                    this.explorers.Add(explorer);
                    this.ActiveExplorer = explorer;
                }
                else
                {
                    FileExplorerViewModel first = this.explorers.FirstOrDefault(x => x.CurrentFolder == path);
                    if (first != null)
                    {
                        this.ActiveExplorer = first;
                    }
                    else
                    {
                        await this.OpenExplorerAt(path);
                    }

                    return;
                }
            }

            if (this.ActiveExplorer.CurrentFolder == path)
                return;

            await this.ActiveExplorer.NavigateAction(path);
        }
    }
}