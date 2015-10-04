using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Miseng.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Miseng.Properties;
using Miseng.ViewModel.UICanvas;

namespace Miseng.ViewModel
{
    public class FileExplorerViewModel : ViewModelBase
    {
        #region // Private fields
        private ControlViewModel _evm;
        private DirInfo _currentTreeItem;
        private IList<DirInfo> _sysDirSource;
        #endregion

        #region // Public properties
        /// <summary>
        /// list of the directories 
        /// </summary>
        public IList<DirInfo> SystemDirectorySource
        {
            get { return _sysDirSource; }
            set
            {
                _sysDirSource = value;
                OnPropertyChanged("SystemDirectorySource");
            }
        }

        /// <summary>
        /// Current selected item in the tree
        /// </summary>
        public DirInfo CurrentTreeItem
        {
            get { return _currentTreeItem; }
            set
            {
                _currentTreeItem = value;
                _evm.CurrentDirectory = _currentTreeItem;
            }
        }
        #endregion

        #region // .ctor
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="evm"></param>
        public FileExplorerViewModel(ControlViewModel evm)
        {
            _evm = evm;

            //create a node for "my computer"
            // this will be the root for the file system tree
            DirInfo rootNode = new DirInfo(Resources.My_WorkSpace);
            rootNode.Path = Environment.GetEnvironmentVariable("USERPROFILE") + "\\" + Miseng.Properties.Resources.My_WorkSpace_Name;
            _evm.CurrentDirectory = rootNode; //make root node as the current directory            
            _evm.UpdateDirectory = new DirInfo();
            SystemDirectorySource = new List<DirInfo> { rootNode };
        }

        public void UpDateFileExplorerViewModel()
        {

            DirInfo temp_CurrentDirectory = CurrentTreeItem;
            DirInfo rootNode = new DirInfo(Resources.My_WorkSpace);
            rootNode.Path = Environment.GetEnvironmentVariable("USERPROFILE") + "\\" + Miseng.Properties.Resources.My_WorkSpace_Name;
            SystemDirectorySource = new List<DirInfo> { rootNode };
            _evm.UpdateDirectory = temp_CurrentDirectory;
            ExpandToCurrentNode(temp_CurrentDirectory);
        }
        #endregion

        #region // public methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="curDir"></param>
        public void ExpandToCurrentNode(DirInfo curDir)
        {
            //expand the current selected node in tree 
            //if this is an ancestor of the directory we want to navigate or "My Computer" current node 
            if (CurrentTreeItem != null && (curDir.Path.Contains(CurrentTreeItem.Path) || CurrentTreeItem.Path == Environment.GetEnvironmentVariable("USERPROFILE")))
            {
                // expand the current node
                // If the current node is already expanded then first collapse it n then expand it                
                CurrentTreeItem.IsExpanded = false;
                CurrentTreeItem.IsExpanded = true;
            }
        }
        #endregion
    }
}
