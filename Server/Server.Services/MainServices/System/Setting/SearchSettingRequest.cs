namespace Phoenix.Server.Services.MainServices.System.Setting
{
    public class SearchSettingRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
