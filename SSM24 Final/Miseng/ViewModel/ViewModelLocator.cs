/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:Miseng.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Miseng.Model;

namespace Miseng.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

             if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
            }
            else
            {
                SimpleIoc.Default.Register<IDataService, DataService>();
            }

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<Attribute.AttributeViewModel>();
            SimpleIoc.Default.Register<Code.CodeViewModel>();
            SimpleIoc.Default.Register<ExtendAttribute.ExtendAttributeViewModel>();
            SimpleIoc.Default.Register<ExtendFuntionMaking.ExtendFuntionMakingViewModel>();
            //SimpleIoc.Default.Register<ExtendUIMaking.ExtendUIMakingViewModel>();
            SimpleIoc.Default.Register<FileTab.FileTabViewModel>();
            SimpleIoc.Default.Register<FunctionMaking.FunctionMakingViewModel>();
            SimpleIoc.Default.Register<Intro.IntroViewModel>();
            SimpleIoc.Default.Register<Script.ScriptViewModel>();
            SimpleIoc.Default.Register<ScriptMaking.ScriptMakingViewModel>();
            SimpleIoc.Default.Register<SolutionSearch.SolutionSearchViewModel>();
            SimpleIoc.Default.Register<SourceTab.SourceTabViewModel>();
            SimpleIoc.Default.Register<UICanvas.UICanvasViewModel>();
            SimpleIoc.Default.Register<UIMaking.UIMakingViewModel>();
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}