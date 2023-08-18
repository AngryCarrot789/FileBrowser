using System.IO;
using System.Threading.Tasks;

namespace FileBrowser.Core.Editor.FileTree.Zip {
    public class ZipFileEntryViewModel : BaseZipItemViewModel {
        public ZipFileEntryViewModel(ZipFileViewModel ownerZip) : base(ownerZip) {

        }

        protected override async Task<bool> OnExpandAsync() {
            if (this.Tree != null && Path.GetExtension(this.ZipFileName) == ".class") {
                await this.Tree.OpenFileAsync(this);
            }

            return false;
        }
    }
}