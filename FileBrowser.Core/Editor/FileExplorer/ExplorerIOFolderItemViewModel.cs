using System.Collections.ObjectModel;
using System.IO;

namespace FileBrowser.Core.Editor.FileExplorer {
    public class ExplorerIOFolderItemViewModel : BaseIOExplorerItemViewModel {
        public ExplorerIOFolderItemViewModel() {

        }

        protected override void OnFilePathChanged(string path) {
            base.OnFilePathChanged(path);

            try {
                DirectoryInfo info = new DirectoryInfo(path);
                this.IsHidden = (info.Attributes & FileAttributes.Hidden) != 0;
            }
            catch {
                this.IsHidden = false;
            }
        }
    }
}