using System.IO;

namespace FileBrowser.Core.Editor.FileExplorer {
    public class ExplorerIODriveItemViewModel : BaseIOExplorerItemViewModel {
        private string formatType;
        public string FormatType {
            get => this.formatType;
            set => this.RaisePropertyChanged(ref this.formatType, value);
        }

        private string volumeLabel;
        public string VolumeLabel {
            get => this.volumeLabel;
            set => this.RaisePropertyChanged(ref this.volumeLabel, value);
        }

        private long totalSpace;
        public long TotalSpace {
            get => this.totalSpace;
            set => this.RaisePropertyChanged(ref this.totalSpace, value);
        }

        private long usedSpace;
        public long UsedSpace {
            get => this.usedSpace;
            set => this.RaisePropertyChanged(ref this.usedSpace, value);
        }

        public long RemainingSpace => this.totalSpace - this.usedSpace;

        public ExplorerIODriveItemViewModel() {

        }

        public ExplorerIODriveItemViewModel(DriveInfo info) {
            this.FilePath = info.Name;
            this.VolumeLabel = info.VolumeLabel;
            this.FormatType = info.DriveFormat; // NTFS most of the time; for drives at least
            if (info.IsReady) {
                this.TotalSpace = info.TotalSize;
                this.UsedSpace = this.TotalSpace - info.TotalFreeSpace;
            }
        }
    }
}