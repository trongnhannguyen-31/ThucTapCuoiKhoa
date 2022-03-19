namespace Phoenix.Mobile.Core.Models.Setting
{
    public class CacheSettings
    {
        /// <summary>
        /// Thoi gian cache toi da cho tat ca cac loai cache
        /// </summary>
        public int LongCacheMinutes { get; set; }
        public int DefaultCacheMinutes { get; set; }
        public int ShortCacheMinutes { get; set; }

    }
}
