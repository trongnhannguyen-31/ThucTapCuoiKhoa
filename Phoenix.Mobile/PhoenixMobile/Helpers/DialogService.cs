using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Phoenix.Mobile.Core.Infrastructure;

namespace Phoenix.Mobile.Helpers
{
    public class DialogService : IDialogService
    {
        private readonly IUserDialogs _userDialogs;

        public DialogService(IUserDialogs userDialogs)
        {
            _userDialogs = userDialogs;
        }

        public IDisposable Alert(string message, string title = "Thông báo", string okText = "Yes")
        {
            //if (okText == null) okText = StringResource.Dialog.Ok.Translate();
            return _userDialogs.Alert(message, title, okText);
        }

        public Task AlertAsync(string message, string title = "Thông báo",  string okText = "Yes", CancellationToken? cancelToken = null)
        {
            //if (okText == null) okText = StringResource.Dialog.Ok.Translate();
            return _userDialogs.AlertAsync(message, title, okText, cancelToken);
        }

        public Task<string> ActionSheetAsync(string title, string cancel, string destructive, CancellationToken? cancelToken = null,
            params string[] buttons)
        {
            return _userDialogs.ActionSheetAsync(title, cancel, destructive, cancelToken, buttons);
        }

        public async Task<bool> ConfirmAsync(string message, string title = null, string okText = null, string cancelText = null,
            CancellationToken? cancelToken = null)
        {
            return (await _userDialogs.ConfirmAsync(message, title, okText, cancelText, cancelToken));
        }

        public void ShowLoading(string title = null)
        {
            throw new NotImplementedException();
        }

        public void HideLoading()
        {
            throw new NotImplementedException();
        }

        public IDisposable Toast(string title, TimeSpan? dismissTimer = null)
        {
            return _userDialogs.Toast(title, dismissTimer);
        }
    }
}
