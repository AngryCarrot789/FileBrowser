using System.Windows;
using System.Windows.Controls;
using FileBrowser.Core.Editor.FileExplorer;

namespace FileBrowser.Explorer {
    public class FileExplorerControl : ItemsControl {
        public static readonly DependencyProperty ExplorerViewModeProperty = DependencyProperty.Register("ExplorerViewMode", typeof(ExplorerViewMode), typeof(FileExplorerControl), new FrameworkPropertyMetadata(ExplorerViewMode.List, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static readonly DependencyProperty DirectoryTypeProperty = DependencyProperty.Register("DirectoryType", typeof(ExplorerDirectoryType), typeof(FileExplorerControl), new PropertyMetadata(ExplorerDirectoryType.Physical));

        public ExplorerViewMode ExplorerViewMode {
            get => (ExplorerViewMode) this.GetValue(ExplorerViewModeProperty);
            set => this.SetValue(ExplorerViewModeProperty, value);
        }

        public ExplorerDirectoryType DirectoryType {
            get => (ExplorerDirectoryType) this.GetValue(DirectoryTypeProperty);
            set => this.SetValue(DirectoryTypeProperty, value);
        }
    }
}