using System.IO;

namespace FileBrowser.Core.Editor.FileExplorer {
    public class ExplorerIOFileItemViewModel : BaseIOExplorerItemViewModel {
        public long FileSize { get; private set; }

        public ExplorerIOFileItemViewModel() {
        }

        protected override void OnFilePathChanged(string path) {
            base.OnFilePathChanged(path);

            try {
                FileInfo info = new FileInfo(path);
                this.FileSize = info.Length;
                this.IsHidden = (info.Attributes & FileAttributes.Hidden) != 0;
            }
            catch {
                this.FileSize = 0;
                this.IsHidden = false;
            }

            this.RaisePropertyChanged(nameof(this.FileSize));
        }
    }
}