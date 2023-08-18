using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FileBrowser.Explorer
{
    internal class FileTreeControl : TreeView
    {
        public FileTreeControl()
        {

        }

        protected override bool IsItemItsOwnContainerOverride(object item) => item is FileTreeItem;

        protected override DependencyObject GetContainerForItemOverride() => new FileTreeItem();
    }
}
