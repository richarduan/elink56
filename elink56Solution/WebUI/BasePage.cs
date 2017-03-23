using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WebUI
{
    public class BasePage:System.Web.UI.Page
    {



        #region AntiForgery
        protected System.Web.HtmlString Token
        {
            get
            {
                return System.Web.Helpers.AntiForgery.GetHtml();
            }
        }
        public System.Web.HtmlString getToken
        {
            get
            {
                return System.Web.Helpers.AntiForgery.GetHtml();
            }
        }

        public void ProcessRequest()
        {
            string token = request.RequestPOST("token");
            var cookie = System.Web.HttpContext.Current.Request.Cookies["__RequestVerificationToken"];
            if (cookie != null)
            {
                try
                {
                    System.Web.Helpers.AntiForgery.Validate(cookie.Value, token);
                }
                catch (Exception e)
                {
                    logHelper.Write(String.Format("Err:{0},\r\nMessage:{1},\r\ntoken:{2}\r\ncookieValue:{3}",
                        "out post xsrf is Err",
                        e.Message.ToString(),
                        token,
                        Convert.ToString(cookie.Value)
                        ));
                    System.Web.HttpContext.Current.Response.Write("err is 808!");
                    System.Web.HttpContext.Current.Response.End();
                    return;
                }
            }
            else
            {
                logHelper.Write(String.Format("Err:{0},\r\token:{1}", "out post xsrf is Null", token));
                System.Web.HttpContext.Current.Response.Write("err is 808!");
                System.Web.HttpContext.Current.Response.End();
                return;
            }
        }



        #endregion

        #region GetConfig
        #region caterDomain
        /// <summary>
        /// 获取主域名
        /// </summary>
        public string caterDomain
        {
            get
            {
                return Utils.SetAppSettings("caterDomain");
            }
        }
        #endregion
        #region FilesServerDomain
        /// <summary>
        /// 调用文件存储地址，可为域名
        /// </summary>
        public string FilesServerDomain
        {
            get
            {
                return Utils.SetAppSettings("FilesServerDomain");
            }
        }
        #endregion
        #region saveServerPath
        /// <summary>
        /// 获取文件存储路径
        /// </summary>
        public string saveServerPath
        {
            get
            {
                return Utils.SetAppSettings("saveServerPath");
            }
        }
        #endregion


        #endregion

        #region style javascript
        #region javascript
        /// <summary>
        /// JS
        /// </summary>
        /// <param name="javascriptName">文件名称</param>
        /// <returns></returns>
        public string staticJavascript(string javascriptName)
        {
            return "<script type=\"text/javascript\" src=\"http://" + this.caterDomain + "" + javascriptName + "\"></script>";
        }
        #endregion

        #region style
        /// <summary>
        /// style
        /// </summary>
        /// <param name="cssName"></param>
        /// <returns></returns>
        public string staticSstyle(string cssName)
        {
            return "<link href=\"http://" + this.caterDomain + "" + cssName + "?rnd=" + TypeParse.RandomNum().ToString() + "\" rel=\"stylesheet\" />";
        }
        #endregion


        #endregion


        #region Request
        /// <summary>
        /// 获取页数
        /// </summary>
        public string RequestPageNo
        {
            get
            {
                string _PageNo = System.Web.HttpContext.Current.Request.QueryString["page"];
                return TypeParse.IsNumeric(_PageNo) ? _PageNo : "1";
            }
        }
        /// <summary>
        /// Request_Tsid
        /// </summary>
        public string RequestTsid
        {
            get
            {
                string _tsid = System.Web.HttpContext.Current.Request.QueryString["tsid"];
                return TypeParse.IsNumeric(_tsid) ? _tsid : "0";
            }
        }
        #endregion 


    }
}
