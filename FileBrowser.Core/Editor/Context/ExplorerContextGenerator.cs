using System.Collections.Generic;
using FileBrowser.Core.Actions.Contexts;
using FileBrowser.Core.AdvancedContextService;
using FileBrowser.Core.Editor.FileExplorer;
using FileBrowser.Core.Editor.FileTree;
using FileBrowser.Core.Editor.FileTree.Physical;
using FileBrowser.Core.Editor.FileTree.Zip;

namespace FileBrowser.Core.Editor.Context {
    public class ExplorerContextGenerator : IContextGenerator {
        public static ExplorerContextGenerator Instance { get; } = new ExplorerContextGenerator();

        public RelayCommand<string> OpenInExplorerCommand { get; }

        public RelayCommand<string> CopyStringCommand { get; }

        public ExplorerContextGenerator() {
            this.OpenInExplorerCommand = new RelayCommand<string>((x) => {
                if (!string.IsNullOrEmpty(x))
                    IoC.ExplorerService.OpenFileInExplorer(x);
            });

            this.CopyStringCommand = new RelayCommand<string>((x) => {
                if (!string.IsNullOrEmpty(x))
                    IoC.Clipboard.SetText(x);
            });
        }

        public void Generate(List<IContextEntry> list, IDataContext context) {
            if (!context.TryGetContext(out BaseTreeFileViewModel item)) {
                return;
            }

            FileTreeViewModel explorer = item.Tree;
            if (explorer != null || context.TryGetContext(out explorer)) {
                if (item is IOFileItemViewModel || item is ZipFileEntryViewModel)
                    list.Add(new CommandContextEntry("Open", explorer.OpenItemCommand, item));
            }

            if (item is IOFolderItemViewModel)
                list.Add(new CommandContextEntry("Refresh", BaseTreeFileViewModel.RefreshCommand, item));
            if (item is BaseIOFileItemViewModel) {
                string path = ((BaseIOFileItemViewModel) item).FilePath;
                if (!string.IsNullOrWhiteSpace(path)) {
                    list.Add(new CommandContextEntry("Open in Explorer", this.OpenInExplorerCommand, path));
                    list.Add(new CommandContextEntry("Copy Path", this.CopyStringCommand, path));
                }
            }

            list.Add(SeparatorEntry.Instance);
            list.Add(new CommandContextEntry("Remove", BaseTreeFileViewModel.RemoveSelfCommand, item));
        }
    }
}