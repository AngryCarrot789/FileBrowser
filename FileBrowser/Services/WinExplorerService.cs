using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using FileBrowser.Core;
using FileBrowser.Core.Services;

namespace FileBrowser.Services {
    [ServiceImplementation(typeof(IExplorerService))]
    public class WinExplorerService : IExplorerService {
        public void OpenFileInExplorer(string filePath) {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && File.Exists(filePath)) {
                Process.Start("explorer.exe", $"/select, \"{filePath.Replace('/', '\\')}\"");
            }
        }
    }
}