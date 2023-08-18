using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using FileBrowser.Core.Editor.FileExplorer;
using FileBrowser.Core.Editor.FileTree.Physical;
using FileBrowser.Utils;

namespace FileBrowser.Converters {
    public class FileTypeConverter : IValueConverter {
        public static FileTypeConverter Instance { get; } = new FileTypeConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null || value == DependencyProperty.UnsetValue) {
                return value;
            }

            if (value is BaseIOExplorerItemViewModel file) {
                if (file is ExplorerIOFileItemViewModel) {
                    string description = ShellUtils.GetFileTypeDescription(file.FilePath);
                    if (description == null) {
                        return file;
                    }
                    else {
                        return description;
                    }
                }
                else if (file is ExplorerIOFolderItemViewModel) {
                    return "Directory";
                }
                else {
                    return "UNKNOWN";
                }
            }
            else {
                return $"[DEBUG_ERROR_NOT_FILE: {value.GetType()} -> {value}]";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}