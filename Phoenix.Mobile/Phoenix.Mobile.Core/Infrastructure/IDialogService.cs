using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Phoenix.Mobile.Core.Infrastructure
{
    public interface IDialogService
    {
        IDisposable Alert(string message, string title = "", string okText = null);
        Task AlertAsync(string message, string title = "", string okText = null, CancellationToken? cancelToken = null);

        Task<string> ActionSheetAsync(string title, string cancel, string destructive,
            CancellationToken? cancelToken = null, params string[] buttons);

        Task<bool> ConfirmAsync(string message, string title = null, string okText = null, string cancelText = null,
            CancellationToken? cancelToken = null);

        void ShowLoading(string title = null);
        void HideLoading();

        IDisposable Toast(string title, TimeSpan? dismissTimer = null);
    }
}