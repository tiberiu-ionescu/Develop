namespace DXServicesDemo.ViewModels
{
    public class ChildVM
    {
        private readonly IServiceFacade _facade;

        public ChildVM(IServiceFacade facade) => _facade = facade;

        public void Message() => _facade.Notify("MessageBox result", _facade.Message("Message caption", "Message body"));

        public void Dialog() => _facade.Notify("Dialog result", _facade.Dialog(IoC.Get<ChildVM>(), "Dialog title", "DialogView", "OK", "Cancel"));

        public void Document() => _facade.ShowDocument(IoC.Get<ChildVM>(), "Document title", "DocumentView");
    }
}