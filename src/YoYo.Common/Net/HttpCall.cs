// ------------------------------------------------------------------------------
// Copyright   版权所有。 
// 项目名：Galaxy.Common 
// 文件名：HttpCall.cs
// 创建标识：梁桐铭  2017-03-20 11:13
// 创建描述：我们是OurGalaxy团队
// 
// 修改标识：
// 修改描述：
//  ------------------------------------------------------------------------------

#region 命名空间

using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

#endregion

namespace YoYo.Common.Net
{
    public class HttpCall
    {
        private static readonly Hashtable XmlNamespaces = new Hashtable(); //缓存xmlNamespace，避免重复调用GetNamespace

        /// <summary>
        ///     需要WebService支持Post调用
        /// </summary>
        public static XmlDocument QueryPostWebService(string url, string methodName, Hashtable pars)
        {
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            SetWebRequest(request);
            var data = EncodePars(pars);
            WriteRequestData(request, data);
            return ReadXmlResponse(request.GetResponse());
        }

        /// <summary>
        ///     需要WebService支持Get调用
        /// </summary>
        public static XmlDocument QueryGetWebService(string url, string methodName, Hashtable pars)
        {
            var request = (HttpWebRequest) WebRequest.Create(url + "/" + methodName + "?" + ParsToString(pars));
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            SetWebRequest(request);
            return ReadXmlResponse(request.GetResponse());
        }

        #region 额外的私有方法

        private static void WriteRequestData(HttpWebRequest request, byte[] data)
        {
            request.ContentLength = data.Length;
            var writer = request.GetRequestStream();
            writer.Write(data, 0, data.Length);
            writer.Close();
        }

        private static byte[] EncodePars(Hashtable Pars)
        {
            return Encoding.UTF8.GetBytes(ParsToString(Pars));
        }

        private static string ParsToString(Hashtable Pars)
        {
            var sb = new StringBuilder();
            foreach (string k in Pars.Keys)
            {
                if (sb.Length > 0)
                    sb.Append("&");
                sb.Append(HttpUtility.UrlEncode(k) + "=" + HttpUtility.UrlEncode(Pars[k].ToString()));
            }
            return sb.ToString();
        }

        /// <summary>
        ///     设置凭证与超时时间
        /// </summary>
        /// <param name="request"></param>
        private static void SetWebRequest(HttpWebRequest request)
        {
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Timeout = 10000;
        }

        /// <summary>
        ///     解析结果，返回xml
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static XmlDocument ReadXmlResponse(WebResponse response)
        {
            var sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            var retXml = sr.ReadToEnd();
            sr.Close();
            var doc = new XmlDocument();
            doc.LoadXml(retXml);
            return doc;
        }

        #endregion
    }
}