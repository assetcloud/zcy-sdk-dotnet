using System;
using System.Collections.Generic;
using System.Text;

namespace AssetCloud.AssetCloudSDK
{

    public class AssetCloudConfig
    {
        /// <summary>
        /// 应用Key
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// 应用Secret
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 请求基础URL
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// 是否在收到不成功的响应时自动抛出异常，默认true
        /// </summary>
        public bool ThrowsOnFailureResponseCode { get; set; } = true;
    }
}
