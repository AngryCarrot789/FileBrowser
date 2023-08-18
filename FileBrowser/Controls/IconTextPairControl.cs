using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FileBrowser.Editor.Icons;
using FileBrowser.Utils;

namespace FileBrowser.Controls {
    public class IconTextPairControl : Control, IImageable {
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(
                "Source",
                typeof(ImageSource),
                typeof(IconTextPairControl),
                new PropertyMetadata(null));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(IconTextPairControl),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.None, (d, e) => { }, (a, b) => b == null ? "" : b.ToString()));

        public static readonly DependencyProperty TargetFilePathProperty =
            DependencyProperty.Register(
                "TargetFilePath",
                typeof(string),
                typeof(IconTextPairControl),
                new PropertyMetadata(null, OnTargetFilePathPropertyChanged));

        public static readonly DependencyProperty ShellIconSizeProperty =
            DependencyProperty.Register(
                "ShellIconSize",
                typeof(ShellIconSize),
                typeof(IconTextPairControl),
                new PropertyMetadata(ShellIconSize.Normal));

        public ShellIconSize ShellIconSize {
            get => (ShellIconSize) this.GetValue(ShellIconSizeProperty);
            set => this.SetValue(ShellIconSizeProperty, value);
        }

        private static void OnTargetFilePathPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((IconTextPairControl) d).OnTargetFileChanged();
        }

        public string TargetFilePath {
            get => (string) this.GetValue(TargetFilePathProperty);
            set => this.SetValue(TargetFilePathProperty, value);
        }

        public ImageSource Source {
            get => (ImageSource) this.GetValue(SourceProperty);
            set => this.SetValue(SourceProperty, value);
        }

        public string Text {
            get => (string) this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        private bool triggerUpdateOnLoad;

        public IconTextPairControl() {
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