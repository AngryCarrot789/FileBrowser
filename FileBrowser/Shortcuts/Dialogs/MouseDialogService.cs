using FileBrowser.Core;
using FileBrowser.Core.Shortcuts.Dialogs;
using FileBrowser.Core.Shortcuts.Inputs;

namespace FileBrowser.Shortcuts.Dialogs {
    [ServiceImplementation(typeof(IMouseDialogService))]
    public class MouseDialogService : IMouseDialogService {
        public MouseStroke? ShowGetMouseStrokeDialog() {
            MouseStrokeInputWindow window = new MouseStrokeInputWindow();
            if (window.ShowDialog() != true || window.Stroke.Equals(default)) {
                return null;
            }
            else {
                return window.Stroke;
            }
        }
    }
}