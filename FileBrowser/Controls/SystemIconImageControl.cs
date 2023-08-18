using System.Windows;
using System.Windows.Controls;
using FileBrowser.Explorer.Icons;

namespace FileBrowser.Controls {
    public class SystemIconImageControl : Control {
        public static readonly DependencyProperty SystemIconTypeProperty =
            DependencyProperty.Register(
                "SystemIconType",
                typeof(CSIDL),
                typeof(SystemIconImageControl),
                new FrameworkPropertyMetadata(CSIDL.CSIDL_DESKTOP, FrameworkPropertyMetadataOptions.AffectsRender));

        public CSIDL SystemIconType {
            get => (CSIDL) this.GetValue(SystemIconTypeProperty);
            set => this.SetValue(SystemIconTypeProperty, value);
        }
    }
}