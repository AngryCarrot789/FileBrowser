using System.Windows.Media;

namespace FileBrowser.Explorer.Icons {
    public interface IImageable {
        ImageSource Source { get; set; }
    }
}