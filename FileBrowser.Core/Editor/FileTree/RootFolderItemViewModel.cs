using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FileBrowser.Core.Editor.FileTree {
    public class RootFolderItemViewModel : BaseTreeFileViewModel, IExplorerFolder {
        private readonly ObservableCollection<BaseTreeFileViewModel> items;

        /// <summary>
        /// A collection of items in this .jar file
        /// </summary>
        public ReadOnlyObservableCollection<BaseTreeFileViewModel> Items { get; }

        IEnumerable<BaseTreeFileViewModel> IExplorerFolder.Items => this.Items;

        public RootFolderItemViewModel() {
            this.items = new ObservableCollection<BaseTreeFileViewModel>();
            this.Items = new ReadOnlyObservableCollection<BaseTreeFileViewModel>(this.items);
        }

        public override void SetExplorer(FileTreeViewModel newTree) {
            base.SetExplorer(newTree);
            foreach (BaseTreeFileViewModel item in this.items) {
                item.SetExplorer(newTree);
            }
        }

        public void AddFile(BaseTreeFileViewModel item) => AddItem(this, this.items, this.items.Count, item);

        public bool RemoveItem(BaseTreeFileViewModel item) => RemoveItem(this.items, item);

        public void RemoveItemAt(int index) => RemoveItemAt(this.items, index);

        public void Clear() => ClearItems(this.items);
    }
}