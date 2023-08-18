using System.Collections.Generic;

namespace FileBrowser.Core.Editor.FileTree {
    /// <summary>
    /// An interface for classes that store child files, and where files can be removed
    /// </summary>
    public interface IExplorerFolder {
        IEnumerable<BaseTreeFileViewModel> Items { get; }

        /// <summary>
        /// Tries to remove an item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool RemoveItem(BaseTreeFileViewModel item);

        /// <summary>
        /// Removes an item at the specific index
        /// </summary>
        /// <param name="index"></param>
        void RemoveItemAt(int index);

        /// <summary>
        /// Recursively clears this folder
        /// </summary>
        void Clear();
    }
}