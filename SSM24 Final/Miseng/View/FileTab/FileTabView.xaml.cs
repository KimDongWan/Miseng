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
using Miseng.View.UICanvas;
using Miseng.ViewModel.UICanvas;
using Miseng.View.Script;
namespace Miseng.View.FileTab
{
    public partial class FileTabView : UserControl
    {
        private List<TabItem> _tabItems;
        private TabItem _tabAdd;
        private PreviewCanvasView previewCanvasV;
        private UICanvasView uiCanvasV;
        public ScriptView scriptV;
        public static int fileName_cnt = 0;
        public string file_path;
        public ControlViewModel ctrVM;
        public string project_name;

        //public ControlViewModel ctrVM;
        //UICanvasViewModel 


        public FileTabView()
        {
            try
            {
                InitializeComponent();
                this.Loaded += new RoutedEventHandler(ViewLoded);
                // initialize tabItem array
                _tabItems = new List<TabItem>();

                // add a tabItem with + in header 
                _tabAdd = new TabItem();
                _tabAdd.Header = "+";
                _tabAdd.MouseLeftButtonUp += new MouseButtonEventHandler(tabAdd_MouseLeftButtonUp);

                _tabItems.Add(_tabAdd);

                // add first tab
                //this.AddTabItem();

                // bind tab control
                tabDynamic.DataContext = _tabItems;

                tabDynamic.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ViewLoded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow != null)
            {
                Window window = Application.Current.MainWindow;
                ctrVM = window.DataContext as ControlViewModel;
            }
        }
        private TabItem AddTabItem(string tab_Name, string filepath)
        {
            project_name = ctrVM.current_projectName;
            fileName_cnt++;

            // create new tab item
            TabItem tab = new TabItem();

            tab.Header = tab_Name;
            tab.Name = tab_Name;
            tab.HeaderTemplate = tabDynamic.FindResource("TabHeader") as DataTemplate;
            tab.MouseDoubleClick += new MouseButtonEventHandler(tab_MouseDoubleClick);

            Grid dynamicGrid = new Grid();
            ColumnDefinition previewGrid = new ColumnDefinition();
            ColumnDefinition uiCanvasGrid = new ColumnDefinition();
            ColumnDefinition fucntionCanvasGrid = new ColumnDefinition();
            previewGrid.MaxWidth = 130;
            previewGrid.MinWidth = 50;
            uiCanvasGrid.MinWidth = 100;
            fucntionCanvasGrid.MinWidth = 200;

            dynamicGrid.ColumnDefinitions.Add(previewGrid);
            //    previewGrid.Width = new GridLength(100);
            dynamicGrid.ColumnDefinitions.Add(uiCanvasGrid);
            dynamicGrid.ColumnDefinitions.Add(fucntionCanvasGrid);

            file_path = filepath;
            uiCanvasV = new UICanvasView();
            Canvas _ca = uiCanvasV.MyCanvas;
            previewCanvasV = new PreviewCanvasView(file_path, _ca);
            scriptV = new ScriptView();
            //20150228 start
            tab.DataContext = scriptV;
            //20150228 end
            GridSplitter PandUISpli = new GridSplitter();
            PandUISpli.Background = new SolidColorBrush(Color.FromArgb(255, 45, 45, 48));
            PandUISpli.Width = 3;
            GridSplitter UIandFUNCTIONSpli = new GridSplitter();
            UIandFUNCTIONSpli.Background = new SolidColorBrush(Color.FromArgb(255, 45, 45, 48));
            UIandFUNCTIONSpli.Width = 10;

            Grid.SetColumn(previewCanvasV, 0);
            Grid.SetColumn(PandUISpli, 0);
            Grid.SetColumn(uiCanvasV, 1);
            Grid.SetColumn(UIandFUNCTIONSpli, 1);
            Grid.SetColumn(scriptV, 2);

            dynamicGrid.Children.Add(previewCanvasV);
            dynamicGrid.Children.Add(PandUISpli);
            dynamicGrid.Children.Add(uiCanvasV);
            dynamicGrid.Children.Add(UIandFUNCTIONSpli);
            dynamicGrid.Children.Add(scriptV);
            tab.Content = dynamicGrid;

            // insert tab item right before the last (+) tab item
            _tabItems.Insert(_tabItems.Count - 1, tab);

            return tab;
        }


        private TabItem AddTabItem()
        {
            fileName_cnt++;
            string name = string.Format("Scene{0}", fileName_cnt);
            //string name = System.IO.Path.GetRandomFileName().Replace(" ","").Replace(".","");
            string path = Environment.GetEnvironmentVariable("USERPROFILE") + "\\" + Miseng.Properties.Resources.My_WorkSpace_Name;
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            if (System.IO.Directory.GetDirectories(path).Length < 2) return null;

            //20150228 start
            if (string.IsNullOrEmpty(ctrVM.current_projectName))
                project_name = System.IO.Directory.GetDirectories(path)[2].Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries).Last();
            else
                project_name = ctrVM.current_projectName;
            //20150228 end

            string new_file_path = path + "\\" + project_name + "\\Tizen\\" + project_name;
            while (System.IO.File.Exists(new_file_path + "\\" + name + ".html"))
            {
                fileName_cnt++;
                name = string.Format("Scene{0}", fileName_cnt);
            }
            ctrVM.current_Scene = name;
            ctrVM.projectAndfileName = project_name + "_" + name;
            ctrVM.current_projectName = project_name;

            Grid.SetZIndex(ctrVM._intro, 0);
            Grid.SetZIndex(ctrVM.fileTabV, 1);

            System.IO.File.Create(new_file_path + "\\" + name + ".html").Close();
            System.IO.File.WriteAllText(new_file_path + "\\" + name + ".html", Miseng.Properties.Resources.basicHtml);
            file_path = new_file_path + "\\" + name + ".html";
            //myViewModel.Update_ExplorerViewModel();
            //System.IO.File.cr
            // create new tab item
            TabItem tab = new TabItem();

            tab.Header = name;
            tab.Name = name;
            tab.HeaderTemplate = tabDynamic.FindResource("TabHeader") as DataTemplate;
            tab.MouseDoubleClick += new MouseButtonEventHandler(tab_MouseDoubleClick);

            Grid dynamicGrid = new Grid();
            ColumnDefinition previewGrid = new ColumnDefinition();
            ColumnDefinition uiCanvasGrid = new ColumnDefinition();
            ColumnDefinition fucntionCanvasGrid = new ColumnDefinition();
            previewGrid.MaxWidth = 130;
            previewGrid.MinWidth = 50;
            uiCanvasGrid.MinWidth = 100;
            fucntionCanvasGrid.MinWidth = 200;


            dynamicGrid.ColumnDefinitions.Add(previewGrid);
            //     previewGrid.Width = new GridLength(100);
            dynamicGrid.ColumnDefinitions.Add(uiCanvasGrid);
            dynamicGrid.ColumnDefinitions.Add(fucntionCanvasGrid);


            //+버튼을 눌렀을때 file을 생성하고 path를 추가하는 부분을 구현해야함.

            uiCanvasV = new UICanvasView();
            Canvas ca = uiCanvasV.MyCanvas;
            previewCanvasV = new PreviewCanvasView(file_path, ca);
            scriptV = new ScriptView();
            //20150228 start
            tab.DataContext = scriptV;
            //20150228 end
            GridSplitter PandUISpli = new GridSplitter();
            PandUISpli.Background = new SolidColorBrush(Color.FromArgb(255, 45, 45, 48));
            PandUISpli.Width = 3;
            GridSplitter UIandFUNCTIONSpli = new GridSplitter();
            UIandFUNCTIONSpli.Background = new SolidColorBrush(Color.FromArgb(255, 45, 45, 48));
            UIandFUNCTIONSpli.Width = 10;

            Grid.SetColumn(previewCanvasV, 0);
            Grid.SetColumn(PandUISpli, 0);
            Grid.SetColumn(uiCanvasV, 1);
            Grid.SetColumn(UIandFUNCTIONSpli, 1);
            Grid.SetColumn(scriptV, 2);

            dynamicGrid.Children.Add(previewCanvasV);
            dynamicGrid.Children.Add(PandUISpli);
            dynamicGrid.Children.Add(uiCanvasV);
            dynamicGrid.Children.Add(UIandFUNCTIONSpli);
            dynamicGrid.Children.Add(scriptV);
            tab.Content = dynamicGrid;

            // insert tab item right before the last (+) tab item
            _tabItems.Insert(_tabItems.Count - 1, tab);
            ctrVM.FileTreeVM.UpDateFileExplorerViewModel();
            return tab;
        }
        public void add_Tab_Item(string tab_name, string filePath)
        {
            // clear tab control binding
            foreach (TabItem tebitem in _tabItems)
            {
                if (tebitem.Name == tab_name) return;
            }
            tabDynamic.DataContext = null;

            TabItem tab = this.AddTabItem(tab_name, filePath);

            // bind tab control
            tabDynamic.DataContext = _tabItems;

            // select newly added tab item
            tabDynamic.SelectedItem = tab;
        }

        private void tabAdd_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // clear tab control binding
            tabDynamic.DataContext = null;

            TabItem tab = this.AddTabItem();

            // bind tab control
            tabDynamic.DataContext = _tabItems;

            // select newly added tab item
            tabDynamic.SelectedItem = tab;
           
        }

        private void tab_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TabItem tab = sender as TabItem;

            TabProperty dlg = new TabProperty();

            // get existing header text
            dlg.txtTitle.Text = tab.Header.ToString();

            if (dlg.ShowDialog() == true)
            {
                // change header text
                tab.Header = dlg.txtTitle.Text.Trim();
            }
        }

        private void tabDynamic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_tabItems.Count == 1) return;
            TabItem tab = tabDynamic.SelectedItem as TabItem;
            if (tab == null) return;

            if (tab.Equals(_tabAdd))
            {

                // clear tab control binding
                tabDynamic.DataContext = null;

                TabItem newTab = this.AddTabItem();

                // bind tab control
                tabDynamic.DataContext = _tabItems;

                // select newly added tab item
                tabDynamic.SelectedItem = newTab;


                //ctrVM.fileTabV = newTab;
            }
            else
            {
                // your code here...
                //20150228 start
                if ((tabDynamic.SelectedItem as TabItem).DataContext != null)
                    scriptV = (tabDynamic.SelectedItem as TabItem).DataContext as ScriptView;
                //20150228 end
            }

            ctrVM.CURRENT_SCENE = (tabDynamic.SelectedItem as TabItem).Header.ToString() + ".html";
            ctrVM.current_Scene = (tabDynamic.SelectedItem as TabItem).Header.ToString();

            //20150228 start
            ctrVM.projectAndfileName = project_name + "_" + ctrVM.current_Scene;
            ctrVM.current_projectName = project_name;
            //20150228 end

            string path = Environment.GetEnvironmentVariable("USERPROFILE") + "\\MisengWorkSpace\\" + ctrVM.current_projectName + "\\Tizen\\" + ctrVM.current_projectName + "\\" + ctrVM.CURRENT_SCENE;
            ctrVM.SrcCodeVM.GetSrcOfPath(path);
        }

        private void btnRomove_Click(object sender, RoutedEventArgs e)
        {
            string tabName = (sender as Button).CommandParameter.ToString();

            var item = tabDynamic.Items.Cast<TabItem>().Where(i => i.Name.Equals(tabName)).SingleOrDefault();

            TabItem tab = item as TabItem;

            if (tab != null)
            {
                if (MessageBox.Show(string.Format("Are you sure you want to Save the Scene '{0}'?", tab.Header.ToString()),
                      "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // get selected tab
                    TabItem selectedTab = tabDynamic.SelectedItem as TabItem;
                    //System.IO.File.Delete(file_path);
                    // clear tab control binding                    
                    tabDynamic.DataContext = null;
                    previewCanvasV.ctrVM._ucaVM.DomtreeVM.saveHtml();
                    //ctrVM.hiddenfileSave( scriptV._webBrowser.InvokeScript("BlocklyStorage.customBackupBlocks_");
                    _tabItems.Remove(tab);
                    // bind tab control
                    tabDynamic.DataContext = _tabItems;

                    // select previously selected tab. if that is removed then select first tab
                    if (selectedTab == null || selectedTab.Equals(tab))
                    {
                        selectedTab = _tabItems[0];
                    }
                    tabDynamic.SelectedItem = selectedTab;
                }
            }
        }
    }
}