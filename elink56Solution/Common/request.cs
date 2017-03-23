using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


   public  class request
    {


        #region Request
        /// <summary>
        /// Request
        /// </summary>
        /// <param name="FromName">From名称</param>
        /// /// <param name="FromName">是否空处理</param>
        /// <returns>过滤输出</returns>
       public static string Request(string FromName)
       {
           string str = string.Empty;
           if (FromName != null && FromName != "")
           {
               str = System.Web.HttpContext.Current.Request[FromName];
               str = string.IsNullOrEmpty(str) ? string.Empty : TypeParse.checkStr(str);
           }
           return str;
       }


       public static string RequestPOST(string FromName)
       {
           string str = string.Empty;
           if (FromName != null && FromName != "")
           {
               str = System.Web.HttpContext.Current.Request.Form[FromName];
               str = string.IsNullOrEmpty(str) ? string.Empty : TypeParse.checkStr(str);
           }
           return str;
       }


        #endregion

        #region Request
        /// <summary>
        /// Request
        /// </summary>
        /// <param name="FromName">From名称</param>
        /// /// <param name="FromName">是否空处理</param>
        /// <returns>过滤输出</returns>
        public static string RequestCheckInt(string FromName)
        {
            if (FromName != null && FromName != "")
            {
                string str = System.Web.HttpContext.Current.Request[FromName];
                FromName = !TypeParse.IsNumeric(str) ? "0" : TypeParse.checkStr(str);
                
            }
            return FromName;
        }


        #endregion






        #region RequestAction
        /// <summary>
        /// Request
        /// </summary>
        /// <param name="FromName">RequestAction</param>
        /// <returns>过滤输出</returns>
        public static string RequestAction()
        {
            string str = System.Web.HttpContext.Current.Request["action"];
            string FromName = string.Empty;

            if (!string.IsNullOrEmpty(str))
            {
                ActionPush();
                FromName = TypeParse.checkStr(str);
            }

            return FromName;
        }

        #endregion

       public static void ResponseEnd(string state ,string text ,string other){
           System.Web.HttpContext.Current.Response.Write(JsonHelper.restfulJSON(state, text, other));
           System.Web.HttpContext.Current.Response.End();
           return;
       }

        #region ActionResult

        public static void ActionPush()
        {
            if (!ActionPushBool)
            {
                string logContents = string.Empty;
                logContents += System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                logContents +="|ActionPush";
                logHelper.Write(logContents);
                System.Web.HttpContext.Current.Response.Write("Server Is Err : 909 .out Post");
                System.Web.HttpContext.Current.Response.End();
                return;
            }
        }

        public static bool ActionPushBool
        {
            get
            {
                string ActionPushBool =Utils.SetAppSettings("ActionPushBool") ;
                if (ActionPushBool == "true")
                {
                    string HTTP_REFERER = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_REFERER"]);
                    string SERVER_NAME = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["SERVER_NAME"]);
                    int SERVER_NAME_LENGTH = SERVER_NAME.Length;
                    if (string.IsNullOrEmpty(HTTP_REFERER)) return false;
                    if (HTTP_REFERER.Length < SERVER_NAME_LENGTH) return false;
                    if (HTTP_REFERER.Substring(7, SERVER_NAME_LENGTH) != SERVER_NAME) return false;

                    return true;
                }
                return true;
            }
        }


        #endregion

    }

