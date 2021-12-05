using DXServicesDemo.ViewModels;

namespace DXServicesDemo
{
    public class MainVM
    {
        private readonly IServiceFacade _facade;

        public MainVM() => _facade = IoC.Get<IServiceFacade>();

        public void Message() => _facade.Notify("MessageBox result", _facade.Message("Message caption", "Message body"));

        public void Dialog() => _facade.Notify("Dialog result", _facade.Dialog(IoC.Get<ChildVM>(), "Dialog title", "DialogView", "OK", "Cancel"));

        public void Document() => _facade.ShowDocument(IoC.Get<ChildVM>(), "Document title", "DocumentView");
    }
}