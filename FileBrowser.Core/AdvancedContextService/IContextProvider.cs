using System.Collections.Generic;

namespace FileBrowser.Core.AdvancedContextService {
    public interface IContextProvider {
        void GetContext(List<IContextEntry> list);
    }
}