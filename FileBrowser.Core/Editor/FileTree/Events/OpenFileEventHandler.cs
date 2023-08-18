using System.Threading.Tasks;
using FileBrowser.Core.Editor.FileTree.Physical;

namespace FileBrowser.Core.Editor.FileTree.Events {
    public delegate Task OpenFileEventHandler(IOFileItemViewModel file);
}