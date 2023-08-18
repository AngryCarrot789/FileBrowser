using FileBrowser.Core.Editor.FileExplorer;
using FileBrowser.Core.Editor.FileTree.Zip;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileBrowser.Core.Editor.FileTree.Events
{
    public delegate Task NavigateToItemEventHandler(BaseTreeFileViewModel file);
}
