using FreshMvvm;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Phoenix.Mobile.Helpers
{
    public class BasePageModel : FreshBasePageModel
    {
        public BasePageModel()
        {
        }
        public bool IsBusy { get; set; }
        public bool IsRefreshing { get; set; }
        public bool IsInvert => !IsBusy;
        public string LoadingAlert { get; set; }

        #region BackCommand

        private Command _backCommand;

        public Command BackCommand =>
            _backCommand ?? (_backCommand = new Command(async (p) => await BackExecute(), (p) => !IsBusy));

        private async Task BackExecute()
        {
            IsBusy = true;
            await CoreMethods.PopPageModel();
            IsBusy = false;
        }
        #endregion
        #region BackModalCommand

        private Command _backModalCommand;

        public Command BackModalCommand =>
            _backModalCommand ?? (_backModalCommand = new Command(async (p) => await ExecuteBackModalCommand(), (p) => !IsBusy));

        private async Task ExecuteBackModalCommand()
        {
            IsBusy = true;
            await CoreMethods.PopPageModel(modal:true, animate:true);
            IsBusy = false;
        }
        #endregion
        #region BackToRootCommand

        private Command _backToRootCommand;

        public Command BackToRootCommand =>
            _backToRootCommand ?? (_backToRootCommand = new Command(async (p) => await BackToRootExecute(), (p) => !IsBusy));

        private async Task BackToRootExecute()
        {
            IsBusy = true;
            await CoreMethods.PopToRoot(true);
            IsBusy = false;
        }
        #endregion
    }
}
