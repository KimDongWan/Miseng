using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Miseng.Model
{
    public class FileSystemExplorerService
    {
        /// <summary>
        /// Gets the list of files in the directory Name passed
        /// </summary>
        /// <param name="directory">The Directory to get the files from</param>
        /// <returns>Returns the List of File info for this directory.
        /// Return null if an exception is raised</returns>
        public static IList<FileInfo> GetChildFiles(string directory)
        {
            try
            {
                List<FileInfo> result = new List<FileInfo>();
                foreach (string path in Directory.GetFiles(directory))
                {
                    FileInfo file = new FileInfo(path);

                    //if ((file.Attributes & System.IO.FileAttributes.Hidden) != System.IO.FileAttributes.Hidden)
                    //{
                    result.Add(file);
                    //}
                }



                return result;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }

            return new List<FileInfo>();
        }


        /// <summary>
        /// Gets the list of directories 
        /// </summary>
        /// <param name="directory">The Directory to get the files from</param>
        /// <returns>Returns the List of directories info for this directory.
        /// Return null if an exception is raised</returns>
        /// 

        //디렉터리 안에 있는 디렉터리들을 받아옴.
        public static IList<DirectoryInfo> GetChildDirectories(string directory)
        {
            try
            {

                List<DirectoryInfo> result = new List<DirectoryInfo>();
                foreach (string path in Directory.GetDirectories(directory))
                {
                    DirectoryInfo dir = new DirectoryInfo(path);

                    if ((dir.Attributes & System.IO.FileAttributes.Hidden) != System.IO.FileAttributes.Hidden)
                    {
                        result.Add(dir);
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }

            return new List<DirectoryInfo>();
        }

        /// <summary>
        /// Gets the root directories of the system
        /// </summary>
        /// <returns>Return the list of root directories</returns>
        public static IList<DirectoryInfo> GetRootDirectories()
        {
            //루트디렉터리 설정
            string RootPath = Environment.GetEnvironmentVariable("USERPROFILE") + "\\" + Miseng.Properties.Resources.My_WorkSpace_Name;
            if (!System.IO.Directory.Exists(RootPath))
                System.IO.Directory.CreateDirectory(RootPath);

            List<DirectoryInfo> dirs = new List<DirectoryInfo>();
            foreach (string path in Directory.GetDirectories(RootPath))
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                if ((dir.Attributes & System.IO.FileAttributes.Hidden) != System.IO.FileAttributes.Hidden)
                {
                    dirs.Add(dir);
                }
            }
            return dirs;

        }
    }
}