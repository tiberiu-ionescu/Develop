using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DXServicesDemo.ViewModels;
using System.Windows;

namespace DXServicesDemo
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IoC.Instance.RegisterInstance(new ServiceFacade(ServiceContainer.Default) as IServiceFacade);

            IoC.Instance.Register(typeof(ChildVM), ViewModelSource.GetPOCOType(typeof(ChildVM)));
        }
    }
}