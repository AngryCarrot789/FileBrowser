using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FileBrowser.Core.Editor.FileTree.Zip {
    public class ZipFolderEntryViewModel : BaseZipItemViewModel, IZipFolder {
        private readonly ObservableCollection<BaseZipItemViewModel> items;

        public ReadOnlyObservableCollection<BaseZipItemViewModel> Items { get; }

        IReadOnlyList<BaseZipItemViewModel> IZipFolder.ZipItems => this.Items;

        IEnumerable<BaseTreeFileViewModel> IExplorerFolder.Items => this.Items;

        public ZipFolderEntryViewModel(ZipFileViewModel ownerZip) : base(ownerZip) {
            this.items = new ObservableCollection<BaseZipItemViewModel>();
            this.Items = new ReadOnlyObservableCollection<BaseZipItemViewModel>(this.items);
        }

        public override void SetTreeExplorer(FileTreeViewModel newTree) {
            base.SetTreeExplorer(newTree);
            foreach (BaseZipItemViewModel item in this.items) {
                item.SetTreeExplorer(newTree);
            }
        }

        protected override Task<bool> OnExpandAsync() {
            return Task.FromResult(this.items.Count > 0);
        }

        public void AddZipItemSorted(BaseZipItemViewModel item) {
            this.InsertItem(ZipFileViewModel.BinarySearchInsertIndex(this, item), item);
        }

        public void InsertItem(int index, BaseZipItemViewModel item) {
            AddItem(this, this.items, index, item);
        }

        public bool RemoveItem(BaseTreeFileViewModel item) {
            if (!(item is BaseZipItemViewModel))
                return false;
            int index = this.items.IndexOf((BaseZipItemViewModel) item);
            if (index == -1)
                return false;
            RemoveItemAt(this.items, index);
            return true;
        }

        public void RemoveItemAt(int index) => RemoveItemAt(this.items, index);

        public void Clear() => ClearItems(this.items);
    }
}