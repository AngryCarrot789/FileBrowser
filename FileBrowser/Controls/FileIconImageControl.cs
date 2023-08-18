using System.Windows;
using FileBrowser.Editor.Icons;
using FileBrowser.Utils;
using Image = System.Windows.Controls.Image;

namespace FileBrowser.Controls {
    public class FileIconImageControl : Image, IImageable {
        public static readonly DependencyProperty TargetFilePathProperty =
            DependencyProperty.Register(
                "TargetFilePath",
                typeof(string),
                typeof(FileIconImageControl),
                new PropertyMetadata(null, OnTargetFilePathPropertyChanged));

        public static readonly DependencyProperty IconTypeProperty =
            DependencyProperty.Register(
                "ShellIconSize",
                typeof(ShellIconSize),
                typeof(FileIconImageControl),
                new FrameworkPropertyMetadata(ShellIconSize.Normal, PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        }

        public string TargetFilePath {
            get => (string) this.GetValue(TargetFilePathProperty);
            set => this.SetValue(TargetFilePathProperty, value);
        }

        public ShellIconSize ShellIconSize {
            get => (ShellIconSize) this.GetValue(IconTypeProperty);
            set => this.SetValue(IconTypeProperty, value);
        }

        private static void OnTargetFilePathPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((FileIconImageControl) d).OnTargetFileChanged();
        }

        private bool triggerUpdateOnLoad;

        public FileIconImageControl() {
            this.Loaded += this.OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e) {
            if (this.triggerUpdateOnLoad) {
                this.triggerUpdateOnLoad = false;
                this.OnTargetFileChanged();
            }
        }

        public void OnTargetFileChanged() {
            if (this.IsLoaded) {
                FileIconService.Instance.EnqueueForIconResolution(this.TargetFilePath, this, false, false, this.ShellIconSize);
                this.triggerUpdateOnLoad = false;
            }
            else {
                this.triggerUpdateOnLoad = true;
            }
        }
    }
}