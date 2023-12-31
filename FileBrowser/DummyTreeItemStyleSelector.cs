using System.Windows;
using System.Windows.Controls;
using FileBrowser.Core.Editor.FileTree;

namespace FileBrowser {
    public class DummyTreeItemStyleSelector : StyleSelector {
        public Style WithDummyStyle { get; set; }

        public Style DefaultStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container) {
            if (item is IExplorerFolder)
                return this.WithDummyStyle;
            return this.DefaultStyle;
        }
    }
}