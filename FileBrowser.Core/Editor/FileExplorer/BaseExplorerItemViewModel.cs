namespace FileBrowser.Core.Editor.FileExplorer {
    public abstract class BaseExplorerItemViewModel : BaseViewModel {
        private bool isFileHidden;
        public bool IsFileHidden {
            get => this.isFileHidden;
            set => this.RaisePropertyChanged(ref this.isFileHidden, value);
        }

        public abstract string FileName { get; }
    }
}