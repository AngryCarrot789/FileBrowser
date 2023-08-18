namespace FileBrowser.Core.Editor.FileExplorer {
    public abstract class BaseExplorerItemViewModel : BaseViewModel {
        public abstract string FileName { get; }

        private FileExplorerViewModel explorer;
        public FileExplorerViewModel Explorer {
            get => this.explorer;
            set => this.RaisePropertyChanged(ref this.explorer, value);
        }

        protected BaseExplorerItemViewModel() {
        }
    }
}