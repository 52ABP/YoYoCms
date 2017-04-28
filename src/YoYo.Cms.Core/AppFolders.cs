using Abp.Dependency;

namespace YoYo.Cms
{
    public class AppFolders : IAppFolders, ISingletonDependency
    {
        public string TempFileDownloadFolder { get; set; }

        public string SampleProfileImagesFolder { get; set; }

        public string WebLogsFolder { get; set; }

        public string TempFileInfoFolder
        {
            get;
            set;
        }

        public string OfficialFileInfoFolder
        {
            get;
            set;
        }

        public string TempResourceItemFolder
        {
            get;

            set;
        }

        public string OfficialResourceItemFolder
        {
            get;

            set;
        }

        public string TempSuppleyAndDemmandFolder
        {
            get;

            set;
        }

        public string OfficialSuppleyAndDemmandFolder
        {
            get;

            set;
        }

        public string TempResourceItemImg
        {
            get;

            set;
        }

        public string OfficialResourceItemImg
        {
            get;

            set;
        }

        public string CarouselPicturePath
        {
            get;

            set;
        }

        public string CarouselPictureTempPath
        {
            get;

            set;
        }

        public string PartnerPicturePath
        {
            get;

            set;
        }

        /// <summary>
        /// 商品详情图片文件夹
        /// </summary>
        public string CommodityDetailItemFolder { get; set; }

        //测试文件夹
        // public string WebTest { get; set; }
    }
}