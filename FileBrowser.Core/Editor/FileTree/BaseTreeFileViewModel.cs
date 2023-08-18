using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FileBrowser.Core.Utils;

namespace FileBrowser.Core.Editor.FileTree {
    /// <summary>
    /// The base class for all files in a file explorer tree
    /// </summary>
    public abstract class BaseTreeFileViewModel : BaseViewModel {
        internal BaseTreeFileViewModel parent;
        private FileTreeViewModel tree;

        /// <summary>
        /// The parent file
        /// </summary>
        public BaseTreeFileViewModel Parent => this.parent;

        private bool isExpanded;
        private bool isExpanding;

        /// <summary>
        /// Whether or not this item has been expanded at least once by the user
        /// </summary>
        public bool HasExpandedOnce { get; private set; }

        /// <summary>
        /// Whether or not this item is currently expanded
        /// </summary>
        public bool IsExpanded {
            get => this.isExpanded;
            set {
                if (this.isExpanded == value) {
                    return;
                }

                if (this.isExpanding) {
                    this.RaisePropertyChanged(ref this.isExpanded, false);
                    return;
                }

                if (value) {
                    this.OnExpandInternal();
                }
                else {
                    this.RaisePropertyChanged(ref this.isExpanded, false);
                }
            }
        }

        public FileTreeViewModel Tree {
            get => this.tree;
            private set => this.RaisePropertyChanged(ref this.tree, value);
        }

        // use static versions to save memory

        public static AsyncRelayCommand<BaseTreeFileViewModel> RefreshCommand { get; } = new AsyncRelayCommand<BaseTreeFileViewModel>(x => x?.RefreshAsync() ?? Task.CompletedTask);
        public static AsyncRelayCommand<BaseTreeFileViewModel> RemoveSelfCommand { get; } = new AsyncRelayCommand<BaseTreeFileViewModel>((x) => {
            if (x?.Parent is IExplorerFolder folder)
                folder.RemoveItem(x);
            return Task.CompletedTask;
        });

        protected BaseTreeFileViewModel() {

        }

        /// <summary>
        /// Sets this file's explorer. This should be overridden by files that contain child files to form a recursive set operation
        /// </summary>
        /// <param name="newTree"></param>
        public virtual void SetTreeExplorer(FileTreeViewModel newTree) {
            this.Tree = newTree;
        }

        private async void OnExpandInternal() {
            this.isExpanding = true;
            try {
                this.isExpanded = await this.OnExpandAsync();
            }
            finally {
                this.isExpanding = false;
            }

            this.RaisePropertyChanged(nameof(this.IsExpanded));
            if (!this.HasExpandedOnce) {
                this.HasExpandedOnce = true;
                this.RaisePropertyChanged(nameof(this.HasExpandedOnce));
            }
        }

        protected virtual Task<bool> OnExpandAsync() {
            return Task.FromResult(false);
        }

        public static void AddItem<T>(BaseTreeFileViewModel parent, IList<T> items, int index, T item) where T : BaseTreeFileViewModel {
            if (item == parent)
                throw new Exception("Cannot add a parent to itself");
            if (items.Contains(item))
                throw new Exception("List already contains the item");

            item.parent = parent;
            items.Insert(index, item);
            item.SetTreeExplorer(parent.Tree);
            item.RaisePropertyChanged(nameof(item.Parent));
        }

        public static void RemoveItemAt<T>(IList<T> items, int index) where T : BaseTreeFileViewModel {
            T item = items[index];
            item.OnRemovingFromParent();
            item.parent = null;
            item.Tree = null;
            items.RemoveAt(index);
            item.RaisePropertyChanged(nameof(item.Parent));
        }

        public static bool RemoveItem<T>(IList<T> items, T item) where T : BaseTreeFileViewModel {
            int index = items.IndexOf(item);
            if (index == -1)
                return false;
            RemoveItemAt(items, index);
            return true;
        }

        public static void ClearItems<T>(IList<T> items) where T : BaseTreeFileViewModel {
            Exception ex = null;
            foreach (T item in items) {
                try {
                    item.OnRemovingFromParent();
                    item.parent = null;
                    item.Tree = null;
                    item.RaisePropertyChanged(nameof(item.Parent));
                }
                catch (Exception e) {
                    ex = e;
                }
            }

            try {
                items.Clear();
            }
            catch (Exception e) {
                if (ex == null) {
                    ex = e;
                }
                else {
                    ex.AddSuppressed(e);
                }
            }

            if (ex != null) {
                throw ex;
            }
        }

        /// <summary>
        /// Refreshes this item, causing any data to be reloaded
        /// </summary>
        public virtual Task RefreshAsync() {
            return Task.CompletedTask;
        }

        public virtual void OnRemovingFromParent() {
            if (this is IExplorerFolder) {
                ((IExplorerFolder) this).Clear();
            }
        }
    }
}