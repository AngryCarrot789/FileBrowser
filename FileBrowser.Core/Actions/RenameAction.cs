using System.Threading.Tasks;
using FileBrowser.Core.Utils;

namespace FileBrowser.Core.Actions {
    [ActionRegistration("actions.general.RenameItem")]
    public class RenameAction : AnAction {
        public override async Task<bool> ExecuteAsync(AnActionEventArgs e) {
            if (!e.DataContext.TryGetContext(out IRenameTarget renameable))
                return false;
            await renameable.RenameAsync();
            return true;
        }
    }
}