using System.IO;
using System.Threading.Tasks;

namespace FileBrowser.Core.Editor.FileTree.Physical {
    /// <summary>
    /// A class for a regular file, such as a text document
    /// </summary>
    public class IOFileItemViewModel : BaseIOFileItemViewModel {
        public string Items { get; set; }

        public IOFileItemViewModel(string filePath) {
            this.FilePath = filePath;
        }

        protected override async Task<bool> OnExpandAsync() {
            if (this.Tree != null && Path.GetExtension(this.FilePath) == ".class") {
                await this.Tree.OpenFileAsync(this);
            }

            return false;
        }
    }
}