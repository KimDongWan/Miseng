using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Miseng.ViewModel;
using Miseng.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Miseng.ViewModel.UICanvas;


namespace Miseng.ViewModel
{
    public class SrcCodeViewModel : ViewModelBase
    {
        public ICSharpCode.AvalonEdit.TextEditor avalonEdit;
        private FileSrc _fileSrcCode;
        private ControlViewModel _evm;
        public ICSharpCode.AvalonEdit.TextEditor avalonEditJS;
        private FileSrc _fileSrcCodeJS;

        public FileSrc FileSrcCode
        {
            get { return _fileSrcCode; }
            set
            {
                _fileSrcCode = value;
                OnPropertyChanged("FileSrcCode");
            }
        }

        public FileSrc FileSrcCodeJS
        {
            get { return _fileSrcCodeJS; }
            set
            {
                _fileSrcCodeJS = value;
                OnPropertyChanged("FileSrcCodeJS");
            }
        }
        public string FileSrcCodeString
        {
            get { return _fileSrcCode.Src; }
            set
            {
                _fileSrcCode.Src = value;
                avalonEdit.Load(_fileSrcCode.Path);
                avalonEdit.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.GetDefinitionByExtension(Path.GetExtension(_fileSrcCode.Path));
                OnPropertyChanged("FileSrcCodeString");
            }
        }

        public string FileSrcCodeStringJS
        {
            get { return _fileSrcCodeJS.Src; }
            set
            {
                _fileSrcCodeJS.Src = value;
                //요기
                avalonEditJS.Load(_fileSrcCodeJS.Path);
                avalonEditJS.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.GetDefinitionByExtension(Path.GetExtension(_fileSrcCodeJS.Path));
                OnPropertyChanged("FileSrcCodeStringJS");
            }
        }

        public SrcCodeViewModel(ControlViewModel evm)
        {
            _evm = evm;
            _evm.SrcCodeVM = this;
            FileSrcCode = new FileSrc();
            FileSrcCodeJS = new FileSrc();
        }

        public void GetSrcOfPath(string path)
        {
            //이쪽을 바꿔야됨.
            _fileSrcCode.Path = path;
            FileSrcCodeString = System.IO.File.ReadAllText(path);
        }

        public void GetSrcOfPathJS(string path)
        {
            //이쪽을 바꿔야됨.
            _fileSrcCodeJS.Path = path;
            if (!File.Exists(path))
            {
                File.Create(path).Close();

            }
            FileSrcCodeStringJS = System.IO.File.ReadAllText(path);
        }

    }
}
