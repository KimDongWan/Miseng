using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.IO;
using Miseng.ViewModel;
using Miseng.Model;
using System.Windows;

namespace Miseng.ViewModel
{
    public class GetFileSysemInformationConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)//ok
        {
            try
            {
                DirInfo nodeToExpand = value as DirInfo;
                if (nodeToExpand == null)
                    return null;

                //return the subdirectories of the Current Node
                if ((ObjectType)nodeToExpand.DirType == ObjectType.MyComputer)
                {
                    return (from sd in FileSystemExplorerService.GetRootDirectories()
                            select new DirInfo(sd)).ToList();
                }
                else
                {
                    IList<DirInfo> childDirList = new List<DirInfo>();
                    IList<DirInfo> childFileList = new List<DirInfo>();
                    System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(nodeToExpand.Path);
                    System.IO.FileAttributes isHidden = (dirInfo.Attributes & System.IO.FileAttributes.Hidden);
                    if (nodeToExpand.DirType == (int)ObjectType.Directory && isHidden != System.IO.FileAttributes.Hidden)
                        childDirList = (from dirs in FileSystemExplorerService.GetChildDirectories(nodeToExpand.Path)
                                        select new DirInfo(dirs)).ToList();
                    childFileList = (from fobj in FileSystemExplorerService.GetChildFiles(nodeToExpand.Path)
                                     select new DirInfo(fobj)).ToList();

                    childDirList = childDirList.Concat(childFileList).ToList();

                    return childDirList;
                }

            }
            catch { return null; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
