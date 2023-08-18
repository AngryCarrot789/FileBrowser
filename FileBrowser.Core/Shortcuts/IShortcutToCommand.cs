using System.Windows.Input;

namespace FileBrowser.Core.Shortcuts {
    public interface IShortcutToCommand {
        ICommand GetCommandForShortcut(string shortcutId);
    }
}