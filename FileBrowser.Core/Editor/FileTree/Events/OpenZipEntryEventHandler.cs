using System.Threading.Tasks;
using FileBrowser.Core.Editor.FileTree.Zip;

namespace FileBrowser.Core.Editor.FileTree.Events {
    public delegate Task OpenZipEntryEventHandler(ZipFileEntryViewModel file);
}