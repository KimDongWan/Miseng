using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Collections;
using System.ComponentModel;

namespace Miseng.Model
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class UIModel : ViewModelBase, INotifyPropertyChanged
    {
        private static UIModel self;
        private Dictionary<int, UINode> list = new Dictionary<int, UINode>();
        /// <summary>
        /// Initializes a new instance of the UIModel class.
        /// </summary>
        private UIModel()
        {
            list.Add(1, new UINode { Id = 1, ControlName = "Button" });

        }

        internal static UIModel instance()
        {
            if (self == null)
                self = new UIModel();
            return self;
        }

        internal IEnumerable<UINode> GetList()
        {
            return list.Values.ToList();
        } 

        /*

        ObservableCollection<string> zoneList;
        ListBox dragSource;

        public ListBox DragSource
        {
            get { return dragSource; }
            set { dragSource = value;
            NotifyPropertyChanged("DragSource");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string info)
        {
            if(PropertyChanged != null){
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        */
    }
}