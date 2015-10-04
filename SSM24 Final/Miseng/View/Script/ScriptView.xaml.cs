using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Miseng.View.TabControls;
using Miseng.ViewModel.UICanvas;
using Miseng.View.FunctionMaking;
using Miseng.ViewModel.Script;
namespace Miseng.View.Script
{
    public partial class ScriptView : UserControl
    {
        public ControlViewModel ctrVM;
        private UICanvasViewModel ucaVM;
        private TabControlsView _tabControls;
        private mshtml.HTMLDocument doc;
        private string current_select_block;
        mshtml.HTMLDocumentEvents2_onmousemoveEventHandler _dwMouseMoveEventHandler;
        
        public ScriptView()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ViewLoded);
            current_select_block = "";
            string src = Environment.GetEnvironmentVariable("USERPROFILE") + "\\MisengWorkSpace\\hiddenFile\\blockly\\index.html";
            _webBrowser.Navigate(src);
            
            //DeleteCache.DeleteCache_main();
        }

        private void ViewLoded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow != null)
            {
                Window window = Application.Current.MainWindow;
                ctrVM = window.DataContext as ControlViewModel;
                ucaVM = ctrVM._ucaVM;
                _tabControls = ctrVM.tabControlsV;
                _webBrowser.LoadCompleted += webBrowser_LoadCompleted;

            }
            //doc = (mshtml.HTMLDocument)_webBrowser.Document;

        }
        private void webBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            mshtml.HTMLDocument doc;
            doc = (mshtml.HTMLDocument)_webBrowser.Document;

            mshtml.HTMLDocumentEvents2_Event iEvent; //Event in the mshtml Document through which mouse events can be raised.

            iEvent = (mshtml.HTMLDocumentEvents2_Event)doc;
            //iEvent.onclick += new mshtml.HTMLDocumentEvents2_onclickEventHandler(ClickEventHandler);
            //iEvent.onmousemove = 
            if (_dwMouseMoveEventHandler != null)
                iEvent.onmousemove -= _dwMouseMoveEventHandler;
            _dwMouseMoveEventHandler = new mshtml.HTMLDocumentEvents2_onmousemoveEventHandler(MouseMoveEventHandler);
            iEvent.onmousemove += _dwMouseMoveEventHandler;
        }

        private void MouseMoveEventHandler(mshtml.IHTMLEventObj e)
        {
            string preBlock = current_select_block;
            current_select_block = _webBrowser.InvokeScript("getSelectBlockName") as string;

            if (preBlock != current_select_block && current_select_block != " ")
            {
                ctrVM.functionMV.contain.Children.Clear();
                _tabControls.TabContols.SelectedIndex = 1;
                ctrVM.functionMV.initModel(current_select_block);
                //3D객체에 string 전송
                AddMouseMoveEvnet();
            }


        }

        public void AddMouseMoveEvnet()
        {
            mshtml.HTMLDocument doc;
            doc = (mshtml.HTMLDocument)_webBrowser.Document;

            mshtml.HTMLDocumentEvents2_Event iEvent; //Event in the mshtml Document through which mouse events can be raised.

            iEvent = (mshtml.HTMLDocumentEvents2_Event)doc;
            //iEvent.onclick += new mshtml.HTMLDocumentEvents2_onclickEventHandler(ClickEventHandler);
            //iEvent.onmousemove = 
            if (_dwMouseMoveEventHandler != null)
                iEvent.onmousemove -= _dwMouseMoveEventHandler;
            _dwMouseMoveEventHandler = new mshtml.HTMLDocumentEvents2_onmousemoveEventHandler(MouseMoveEventHandler);
            iEvent.onmousemove += _dwMouseMoveEventHandler;
        }

        public void HideScriptErrors(WebBrowser wb, bool Hide)
        {
            System.Reflection.FieldInfo fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;
            object objComWebBrowser = fiComWebBrowser.GetValue(wb);
            if (objComWebBrowser == null) return;
            objComWebBrowser.GetType().InvokeMember("Silent", System.Reflection.BindingFlags.SetProperty, null, objComWebBrowser, new object[] { Hide });
        }

        public string getJSCode()
        {
            return _webBrowser.InvokeScript("getJSCode") as string;
        }
        public string getBlocks()
        {
            //return _webBrowser.InvokeScript("getJSCode") as string;
            var result = _webBrowser.InvokeScript("saveBlock");
            if (result is string) return result as string;
            else return null;
        }

    }



}