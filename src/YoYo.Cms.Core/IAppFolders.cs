namespace YoYo.Cms
{
    public interface IAppFolders
    {

        /// <summary>
        /// 临时文件下载地址
        /// </summary>
        string TempFileDownloadFolder { get; set; }
        /// <summary>
        /// 发布文件临时路径
        /// </summary>
        string TempFileInfoFolder { get; set; }
        /// <summary>
        /// 文件正式目录
        /// </summary>
        string OfficialFileInfoFolder { get; set; }
        /// <summary>
        /// 资源附件临时文件夹
        /// </summary>
        string TempResourceItemFolder { get; set; }
        /// <summary>
        /// 资源附件正式文件夹
        /// </summary>
        string OfficialResourceItemFolder { get; set; }

        /// <summary>
        /// 资源图片临时文件夹
        /// </summary>
        string TempResourceItemImg { get; set; }
        /// <summary>
        /// 资源图片正式文件夹
        /// </summary>
        string OfficialResourceItemImg { get; set; }

        /// <summary>
        /// 资源详情图片文件夹
        /// </summary>
        string CommodityDetailItemFolder { get; set; }

        /// <summary>
        /// 示例个人图片文件夹
        /// </summary>
        string SampleProfileImagesFolder { get; }
        /// <summary>
        /// web日志文件夹路径
        /// </summary>
        string WebLogsFolder { get; set; }

        /// <summary>
        /// 上传资源附件临时文件夹
        /// </summary>
        string TempSuppleyAndDemmandFolder { get; set; }
        /// <summary>
        /// 供需正式文件夹
        /// </summary>
        string OfficialSuppleyAndDemmandFolder { get; set; }

        /// <summary>
        /// 广告图正式文件夹
        /// </summary>
        string CarouselPicturePath { get; set; }
        /// <summary>
        /// 广告图临时文件夹
        /// </summary>
        string CarouselPictureTempPath { get; set; }
        /// <summary>
        /// 合作伙伴正式文件夹
        /// </summary>
        string PartnerPicturePath { get; set; }
    }
}