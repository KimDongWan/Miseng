using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;
using Miseng.Properties;
using System.Windows;
using System.Collections;


namespace Miseng.ViewModel
{
    /// <summary>
    /// Enum to hold the Types of different file objects
    /// </summary>
    public enum ObjectType
    {
        MyComputer = 0,
        DiskDrive = 1,
        Directory = 2,
        File = 3
    }

    /// <summary>
    /// Class for containing the information about a Directory/File
    /// </summary>
    public class DirInfo : DependencyObject
    {
        #region // Public Properties
        public string Name { get; set; }
        public string Path { get; set; }
        public string Root { get; set; }
        public string Size { get; set; }
        public string Ext { get; set; }
        public int DirType { get; set; }
        #endregion

        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            try
            {
                foreach (FileInfo file in files)
                {
                    string temppath = System.IO.Path.Combine(destDirName, file.Name);


                    file.CopyTo(temppath, true);


                }
            }
            catch (Exception e)
            {
                MessageBoxResult result;
                result = MessageBox.Show(e.Message, "에러");
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = System.IO.Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
        #region // Dependency Properties
        public static readonly DependencyProperty propertyChilds = DependencyProperty.Register("Childs", typeof(IList<DirInfo>), typeof(DirInfo));
        public IList<DirInfo> SubDirectories
        {
            get { return (IList<DirInfo>)GetValue(propertyChilds); }
            set { SetValue(propertyChilds, value); }
        }

        public static readonly DependencyProperty propertyIsExpanded = DependencyProperty.Register("IsExpanded", typeof(bool), typeof(DirInfo));
        public bool IsExpanded
        {
            get { return (bool)GetValue(propertyIsExpanded); }
            set { SetValue(propertyIsExpanded, value); }
        }

        public static readonly DependencyProperty propertyIsSelected = DependencyProperty.Register("IsSelected", typeof(bool), typeof(DirInfo));
        public bool IsSelected
        {
            get { return (bool)GetValue(propertyIsSelected); }
            set { SetValue(propertyIsSelected, value); }
        }
        #endregion

        #region // .ctor(s)
        public DirInfo()
        {
            SubDirectories = new List<DirInfo>();
            SubDirectories.Add(new DirInfo("TempDir"));
            //DirType = (int)ObjectType.Directory;//dwCH
        }

        public DirInfo(string directoryName)
        {
            Name = directoryName;
            //DirType = (int)ObjectType.Directory;//dwCH
        }

        public DirInfo(DirectoryInfo dir)
            : this()
        {
            Name = dir.Name;
            Root = dir.Root.Name;
            Path = dir.FullName;
            DirType = (int)ObjectType.Directory;
        }

        public DirInfo(FileInfo fileobj)
        {
            Name = fileobj.Name;
            Path = fileobj.FullName;
            DirType = (int)ObjectType.File;
            Size = (fileobj.Length / 1024).ToString() + " KB";
            Ext = fileobj.Extension + " File";
        }

        public DirInfo(DriveInfo driveobj)
            : this()
        {
            if (driveobj.Name.EndsWith(@"\"))
                Name = driveobj.Name.Substring(0, driveobj.Name.Length - 1);
            else
                Name = driveobj.Name;

            Path = driveobj.Name;
            DirType = (int)ObjectType.DiskDrive;
        }
        #endregion
    }
}
