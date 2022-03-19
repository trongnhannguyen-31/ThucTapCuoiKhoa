using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;
using FreshMvvm;
using Refit;
using Phoenix.Mobile.Core.Services;
using Phoenix.Mobile.Core.Infrastructure;
using Phoenix.Framework.Core;

namespace Phoenix.Mobile.Helpers
{
    public class ExceptionHandler : IExceptionHandler
    {
        private static bool _isBusy;
        private static readonly SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1, 1);
        public void Handle(Exception e, bool showWarningDialog = false)
        {
            if (e is NetworkException networkException)
            {
                if (_isBusy) return;
                SemaphoreSlim.WaitAsync();
                _isBusy = true;
                try
                {
                    var dialog = FreshIOC.Container.Resolve<IDialogService>();
                    
                    //handle 401
                    if (e.InnerException is ApiException refitException && refitException.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        if (showWarningDialog)
                        {
                            dialog.Toast("Đã hết phiên làm việc, vui lòng đăng nhập lại");
                        }
                        var nav = FreshIOC.Container.Resolve<IAuthService>();
                        nav.LogOut();
                        NavigationHelpers.ToLoginPage();
                        //var nav = FreshIOC.Container.Resolve<IAuthService>();
                        //nav.Logout(true);
                    }
                    //else
                    //{
                    //    if (showWarningDialog)
                    //        dialog.Toast(StringResource.Err.Network.Translate());
                    //}
                }
                finally
                {
                    _isBusy = false;
                    SemaphoreSlim.Release();
                }
                //don't log network error
                return;
            }
#if DEBUG
            Debug.WriteLine("------------------eRROr-------------------------");
            Debug.WriteLine(e.ToString());
            Debug.WriteLine("------------------end eRROr-------------------------");

#else
            // Crashes.TrackError(e);
#endif
        }
    }
}