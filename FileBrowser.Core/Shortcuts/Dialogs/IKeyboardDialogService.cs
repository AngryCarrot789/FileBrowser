using FileBrowser.Core.Shortcuts.Inputs;

namespace FileBrowser.Core.Shortcuts.Dialogs {
    public interface IKeyboardDialogService {
        KeyStroke? ShowGetKeyStrokeDialog();
    }
}