using System;
using Falcon.Web.Core.Settings;
using Phoenix.Shared.Common;

namespace Phoenix.Server.Services.Settings
{
    public class MetadataSettings : ISettings
    {
        //core
        public DateTimeOffset LastUpdatedLookup { get; set; }
		//
	    public string UploadingFolder { get; set; }
	    public string LuceneIndexesFolder { get; set; }

		/// <summary>
		/// 1 - 100%, default value 25% use for <see cref="ImageResizeModes.ByPercent"/>
		/// </summary>
		public int PhotoSizePercent { get; set; }
	    /// <summary>
	    /// Default value 1024px, use for <see cref="ImageResizeModes.ByMaxWidth"/> 
	    /// </summary>
	    public int PhoToMaxWidth { get; set; }
	    public ImageResizeModes ResizeMode { get; set; }
		//So ngay gan nhat de lay tin
	    public int NumberOfDayGetNewsLatest { get; set; }

	    public string GoogleMapApiKey { get; set; }
		/// <summary>
		/// Thoi gian toi da de thay doi dai ly nhan hang (phut)
		/// </summary>
	    public int NumberOfMinuteChangeAgency { get; set; }
        public string BannerUrl { get; set; }
        public string LogoUrl { get; set; }
        public string LogoExtendUrl { get; set; }
        public string NoteOtp { get; set; }
        //xem tong the sieu thi
        public double DistanceMapAll { get; set; }
        //xem chi tiet 1 sieu thi
        public double DistanceMapOnly { get; set; }
    }
}