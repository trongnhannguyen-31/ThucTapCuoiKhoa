using Falcon.Web.Core.Message;
using Falcon.Web.Core.Settings;

namespace CongDongBau.Server.Services.Settings
{
	public class EmailSettings: ISettings
	{
		/// <summary>
		/// Gets or sets a store default email account identifier
		/// </summary>
		//public int DefaultEmailAccountId { get; set; }
		public EmailAccount SystemEmailAccount { get; set; }
		public string EmailWelcomeTemplate { get; set; }
		public string EmailWelcomeTitle { get; set; }
		public string EmailResetPasswordTemplate { get; set; }
		public string EmailResetPasswordTitle { get; set; }

		public string EmailDiaryTitle { get; set; }
		public string EmailDiaryTemplate { get; set; }
	}
}
