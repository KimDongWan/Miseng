using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Miseng.ViewModel;
using System.IO;
using System.Collections.Specialized;
using Miseng.Properties;
using System.Windows.Input;

namespace Miseng.ViewModel
{
    public class ExplorerWindowViewModel : ViewModelBase
    {
        #region // Private Members
        private DirInfo _currentDirectory;
        private DirInfo _updateDirectory;

        private FileExplorerViewModel _fileTreeVM;
        private SrcCodeViewModel _srcCodeVM;
        private ICommand _showTreeCommand;
        #endregion

        #region // .ctor
        public ExplorerWindowViewModel()
        {
            //FileTreeVM = new FileExplorerViewModel(this);
            //SrcCodeVM = new SrcCodeViewModel(this);

        }
        #endregion

        #region // Public Properties
        /// <summary>
        /// Name of the current directory user is in
        /// </summary>
        public DirInfo CurrentDirectory
        {
            get { return _currentDirectory; }
            set
            {
                _currentDirectory = value;
                OnPropertyChanged("CurrentDirectory");
            }
        }
        public DirInfo UpdateDirectory
        {
            get { return _updateDirectory; }
            set { _updateDirectory = value; }
        }


        /// <summary>
        /// Tree View model
        /// </summary>
        public FileExplorerViewModel FileTreeVM
        {
            get { return _fileTreeVM; }
            set
            {
                _fileTreeVM = value;
                OnPropertyChanged("FileTreeVM");
            }
        }

        public SrcCodeViewModel SrcCodeVM
        {
            get { return _srcCodeVM; }
            set
            {
                _srcCodeVM = value;
                OnPropertyChanged("SrcCodeVM");
            }
        }




        /// <summary>
        /// 
        /// </summary>
        public ICommand ShowTreeCommand
        {
            get { return _showTreeCommand; }
            set
            {
                _showTreeCommand = value;
                OnPropertyChanged("ShowTreeCommand");
            }
        }
        public void Update_ExplorerViewModel()
        {
            FileTreeVM.UpDateFileExplorerViewModel();
        }



        #endregion


    }
}
