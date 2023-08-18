using FileBrowser.Core.Editor.FileTree;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FileBrowser.Explorer
{
    internal class FileTreeItem : TreeViewItem
    {
        private bool isProcessingNavigation;

        public FileTreeItem()
        {
        }

        protected override bool IsItemItsOwnContainerOverride(object item) => item is FileTreeItem;

        protected override DependencyObject GetContainerForItemOverride() => new FileTreeItem();

        protected override async void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (this.isProcessingNavigation)
            {
                e.Handled = true;
                return;
            }

            if (!e.Handled && this.DataContext is BaseTreeFileViewModel file)
            {
                this.isProcessingNavigation = true;
                try
                {
                    await file.Tree.OnNavigate(file);
                }
                finally
                {
                    this.isProcessingNavigation = false;
                }
            }

            base.OnMouseLeftButtonDown(e);
        }
    }
}
