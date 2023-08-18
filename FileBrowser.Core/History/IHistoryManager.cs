namespace FileBrowser.Core.History {
    public interface IHistoryManager {
        void AddAction(IHistoryAction action, string information = null);
    }
}