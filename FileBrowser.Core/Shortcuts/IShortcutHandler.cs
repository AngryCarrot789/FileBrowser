using System.Threading.Tasks;
using FileBrowser.Core.Shortcuts.Managing;

namespace FileBrowser.Core.Shortcuts {
    public interface IShortcutHandler {
        Task<bool> OnShortcutActivated(ShortcutProcessor processor, GroupedShortcut shortcut);
    }
}