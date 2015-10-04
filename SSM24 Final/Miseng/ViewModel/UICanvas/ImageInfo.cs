using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using Miseng.Common;

namespace Miseng.ViewModel.UICanvas
{
    public class ImageInfo : ViewModelBase
    {
        private string _name;
        private string _path;

        public ImageInfo()
        {
            ImageName = "";
            ImagePath = "";
        }

        public string ImageName
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("ImageName");
            }
        }
        public string ImagePath
        {
            get { return _path; }
            set
            {
                _path = value;
                OnPropertyChanged("ImagePath");
            }
        }
    }
}

