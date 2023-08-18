using System.Threading.Tasks;
using FileBrowser.Core.Shortcuts.Managing;

namespace FileBrowser.Shortcuts {
    public delegate Task<bool> ShortcutActivateHandler(ShortcutProcessor processor, GroupedShortcut shortcut);
}