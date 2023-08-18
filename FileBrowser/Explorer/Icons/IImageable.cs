using System.Windows.Media;

namespace FileBrowser.Editor.Icons {
    public interface IImageable {
        ImageSource Source { get; set; }
    }
}