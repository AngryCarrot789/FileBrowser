using System.IO;

namespace FileBrowser.Core.Editor.FileExplorer {
    public class BaseIOExplorerItemViewModel : BaseExplorerItemViewModel {
        private string filePath;

        /// <summary>
        /// The file path of this item
        /// </summary>
        public string FilePath {
            get => this.filePath;
            set {
                this.filePath = value;
                this.OnFilePathChanged(value);
                this.RaisePropertyChanged();
            }
        }

        public override string FileName {
            get {
                if (string.IsNullOrEmpty(this.FilePath))
                    return "";
                string path = Path.GetFileName(this.FilePath);
                return string.IsNullOrEmpty(path) ? this.FilePath : path;
            }
        }

        public BaseIOExplorerItemViewModel() {
        }

        protected virtual void OnFilePathChanged(string path) {

        }
    }
}