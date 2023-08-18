using System.Windows.Controls;
using System.Windows.Input;
using FileBrowser.Core.Editor.FileExplorer;

namespace FileBrowser.Explorer {
    public class BaseFileItemControl : Control {
        protected BaseFileItemControl() {

        }

        protected override async void OnMouseDoubleClick(MouseButtonEventArgs e) {
            if (this.DataContext is BaseIOExplorerItemViewModel file && file.Explorer != null) {
                e.Handled = true;
                await file.Explorer.NavigateAction(file.FilePath);
            }
            else {
                base.OnMouseDoubleClick(e);
            }
        }
    }

    public class DriveWrapItemControl : BaseFileItemControl { }

    public class DirectoryWrapItemControl : BaseFileItemControl { }
    public class ZipDirectoryWrapItemControl : BaseFileItemControl { }

    public class FileWrapItemControl : BaseFileItemControl { }
    public class ZipFileWrapItemControl : BaseFileItemControl { }
}