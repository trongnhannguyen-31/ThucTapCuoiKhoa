using Phoenix.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.PageModels.Common
{
    public class AccountPageModel : BasePageModel
    {



        #region ViewOrderCommand
        public Command ViewOrderCommand => new Command(async (p) => await ViewOrderExecute(), (p) => !IsBusy);
        private async Task ViewOrderExecute()
        {

            await CoreMethods.PushPageModel<AlertPageModel>();

        }
        #endregion
    }
}
