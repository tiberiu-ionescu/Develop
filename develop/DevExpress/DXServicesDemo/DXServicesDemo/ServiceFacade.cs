using DevExpress.Mvvm;
using System;
using System.Linq;
using System.Windows;

namespace DXServicesDemo
{
    public interface IServiceFacade
    {
        void ShowDocument(object viewModel, string title, string view);

        bool Dialog(dynamic viewModel, string title, string view, params string[] buttons);

        MessageBoxResult Message(
            string caption,
            string body, MessageBoxButton button = MessageBoxButton.OKCancel,
            MessageBoxImage image = MessageBoxImage.Exclamation, bool winUiMode = false);

        void Notify(string caption, object body = null);
    }

    public class ServiceFacade : IServiceFacade
    {
        #region | Services |

        private readonly IMessageBoxService _messageBoxService;
        private readonly IMessageBoxService _winUiMessageBoxService;
        private readonly IDialogService _dialogService;
        private readonly IDocumentManagerService _documentService;
        private readonly INotificationService _notificatorService;
        #endregion

        public ServiceFacade(IServiceContainer container)
        {
            _messageBoxService = container.GetService<IMessageBoxService>("MessageBoxService");

            _winUiMessageBoxService = container.GetService<IMessageBoxService>("WinUIMessageBoxService");

            _dialogService = container.GetService<IDialogService>("DialogService");

            _documentService = container.GetService<IDocumentManagerService>("DocumentService");

            _notificatorService = container.GetService<INotificationService>("NotificationService");
        }

        public void ShowDocument(object viewModel, string title, string view)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var doc = _documentService.FindDocument(viewModel);

                if (doc == null)
                {
                    doc = _documentService.CreateDocument(view, viewModel);
                    doc.Id = _documentService.Documents.Count();
                    doc.Title = title;
                }

                doc.Show();
            });
        }

        public bool Dialog(object viewModel, string title, string view, params string[] buttons)
        {
            if (buttons?.Any() != true)
            {
                var result = MessageResult.None;

                Application.Current.Dispatcher.Invoke(() => result = _dialogService.ShowDialog(MessageButton.OKCancel, title, view, viewModel));

                switch (result)
                {
                    case MessageResult.None:
                    case MessageResult.Cancel:
                    case MessageResult.No:
                        return false;

                    case MessageResult.OK:
                    case MessageResult.Yes:
                        return true;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            var commands = buttons.Select(t => new UICommand { Caption = t }).ToList();
            commands[0].IsDefault = true;
            commands[0].IsCancel = false;

            UICommand command = null;

            Application.Current.Dispatcher.Invoke(() => command = _dialogService.ShowDialog(commands, title, view, viewModel));

            return command?.IsDefault == true;
        }

        public MessageBoxResult Message(string caption, string body, MessageBoxButton button, MessageBoxImage image, bool winUiMode = false)
        {
            MessageBoxResult result = MessageBoxResult.None;

            Application.Current.Dispatcher.Invoke(() =>
            {
                result = winUiMode 
                    ? _winUiMessageBoxService.Show(body, caption, button, image) 
                    : _messageBoxService.Show(body, caption, button, image);
            });

            return result;
        }

        public void Notify(string caption_
            , object body = null)
        {
            Application.Current.Dispatcher.Invoke(() => _notificatorService.CreatePredefinedNotification(caption_, body?.ToString(), "").ShowAsync());
        }
    }
}