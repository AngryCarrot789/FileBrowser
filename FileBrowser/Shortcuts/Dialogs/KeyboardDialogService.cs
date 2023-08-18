using FileBrowser.Core;
using FileBrowser.Core.Shortcuts.Dialogs;
using FileBrowser.Core.Shortcuts.Inputs;

namespace FileBrowser.Shortcuts.Dialogs {
    [ServiceImplementation(typeof(IKeyboardDialogService))]
    public class KeyboardDialogService : IKeyboardDialogService {
        public KeyStroke? ShowGetKeyStrokeDialog() {
            KeyStrokeInputWindow window = new KeyStrokeInputWindow();
            if (window.ShowDialog() != true || window.Stroke.Equals(default)) {
                return null;
            }
            else {
                return window.Stroke;
            }
        }
    }
}