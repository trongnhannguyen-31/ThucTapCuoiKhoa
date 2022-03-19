namespace CongDongBau.Server.Web.Areas.Admin.Models.Import
{
    /// <summary>
    /// Danh mục các template excel sử dụng trong hệ thống
    /// </summary>
    public static class TemplatesCollection
    {
        private static string TemplatePath = @"/Templates";
        public static string ImportMemberCard => $"{TemplatePath}/Template_ImportMemberCard.xlsx";
        public static string ImportRenewCode => $"{TemplatePath}/Template_ImportRenewCode.xlsx";
        public static string ImportHospital => $"{TemplatePath}/Template_ImportHospital.xlsx";
        public static string ImportPromotion => $"{TemplatePath}/Template_ImportPromotion.xlsx";
    }
}