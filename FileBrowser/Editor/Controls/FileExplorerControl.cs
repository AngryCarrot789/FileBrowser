using System.Windows;
using System.Windows.Controls;

namespace FileBrowser.Editor.Controls {
    public class FileExplorerControl : ItemsControl {
        public static readonly DependencyProperty ExplorerModeProperty =
            DependencyProperty.Register(
                "ExplorerMode",
                typeof(ExplorerMode),
                typeof(FileExplorerControl),
                new FrameworkPropertyMetadata(ExplorerMode.Wrap, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public ExplorerMode ExplorerMode {
            get => (ExplorerMode) this.GetValue(ExplorerModeProperty);
            set => this.SetValue(ExplorerModeProperty, value);
        }
    }
}