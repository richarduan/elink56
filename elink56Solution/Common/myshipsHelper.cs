using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;


public class myshipsHelper
{
    private static Encoding DEFAULT_ENCODING = Encoding.GetEncoding("UTF-8");
    //private static string ACCEPT = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
    //private static string CONTENT_TYPE = "application/x-www-form-urlencoded";
    private static string USERAGENT = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; 宝船网数据API19InfoPath.2; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; msn OptimizedIE8;ZHCN)";

    public myshipsHelper()
    {


    }

    #region 基本属性
    public string APIKEY
    {
        get
        {
            return ATools.SetAppSettings("myshipskey");
        }
    }

    public string APIURL
    {
        get
        {
            StringBuilder url = new StringBuilder();
            url.Append("http://");
            url.Append(ATools.SetAppSettings("myshipapiurl"));
            return Convert.ToString(url);
        }
    }
    #endregion

    #region 获取宝船网的加密种子
    /// <summary>
    /// 获取宝船网的加密种子
    /// </summary>
    public string GetComKey
    {
        get
        {
            StringBuilder url = new StringBuilder();
            url.Append(this.APIURL);
            url.Append("/getComKey");
            return GetHtmlContent(Convert.ToString(url));
        }
    }
    #endregion

    #region 调用getShipId接口获取结果
    //调用getShipId接口获取结果
    public string GetShipid(string ShipName, string md5Key)
    {
        string term = HttpUtility.UrlEncode(ShipName);
        StringBuilder getshipidurl = new StringBuilder();
        getshipidurl.Append(this.APIURL);
        getshipidurl.Append("/getShipId");

        getshipidurl.Append("?term=");
        getshipidurl.Append(term);
        getshipidurl.Append("&key=");
        getshipidurl.Append(MD5(md5Key));

        return GetHtmlContent(Convert.ToString(getshipidurl));
    }
    #endregion


    #region 船舶最新船位查询服务
    //船舶最新船位查询服务
    public string GetShipLatest(string shipid, string md5Key)
    {

        ////api.myships.com/DataApiServer/getShipLatest?shipid=420368290&key=5cdd7336161c4ce2a27a21d770b8aa53
        string term = HttpUtility.UrlEncode(shipid);
        StringBuilder getshipidurl = new StringBuilder();
        getshipidurl.Append(this.APIURL);
        getshipidurl.Append("/getShipLatest?shipid=");
        getshipidurl.Append(term);
        getshipidurl.Append("&key=");
        getshipidurl.Append(MD5(md5Key));
        return GetHtmlContent(Convert.ToString(getshipidurl));
    }
    #endregion


    #region 船舶最新船位查询服务
    //船舶最新船位查询服务
    public string getShipBasicInformation(string shipid, string md5Key)
    {

        ////api.myships.com/DataApiServer/getShipLatest?shipid=420368290&key=5cdd7336161c4ce2a27a21d770b8aa53
        string term = HttpUtility.UrlEncode(shipid);
        StringBuilder getshipidurl = new StringBuilder();
        getshipidurl.Append(this.APIURL);
        getshipidurl.Append("/getShipBasicInformation?shipid=");
        getshipidurl.Append(term);
        getshipidurl.Append("&key=");
        getshipidurl.Append(MD5(md5Key));
        return GetHtmlContent(Convert.ToString(getshipidurl));
    }
    #endregion


    #region 船舶历史轨迹查询服务
    //船舶历史轨迹查询服务
    public string getShipHistorTrack(string shipid, string md5Key, string startTime, string endTime)
    {

        ////api.myships.com/DataApiServer/getShipLatest?shipid=420368290&key=5cdd7336161c4ce2a27a21d770b8aa53
        string term = HttpUtility.UrlEncode(shipid);
        StringBuilder getshipidurl = new StringBuilder();
        getshipidurl.Append(this.APIURL);
        getshipidurl.Append("/getShipHistorTrack?shipid=");
        getshipidurl.Append(term);
        getshipidurl.Append("&key=");
        getshipidurl.Append(MD5(md5Key));
        getshipidurl.Append("&startTime=");
        getshipidurl.Append(startTime);
        getshipidurl.Append("&endTime=");
        getshipidurl.Append(endTime);
        return GetHtmlContent(Convert.ToString(getshipidurl));
    }
    #endregion

    #region 返回16进制的md5字符串
    public  string MD5(string myString)
    {
        byte[] textBytes = System.Text.Encoding.Default.GetBytes(myString);
        try
        {
            System.Security.Cryptography.MD5CryptoServiceProvider cryptHandler = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hash = cryptHandler.ComputeHash(textBytes);
            string ret = string.Empty;
            foreach (byte a in hash)
            {
                if (a < 16)
                    ret += "0" + a.ToString("x");
                else
                    ret += a.ToString("x");

            }
            return ret;
        }
        catch
        {
            throw;
        }

    }
    #endregion

    #region POST_PUSH
    public  string GetHtmlContent(string url)
    {
        return GetHtmlContent(url, DEFAULT_ENCODING);
    }

    public  string GetHtmlContent(string url, Encoding encoding)
    {
        try
        {
            string html = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = USERAGENT;

            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream, encoding))
                {
                    html = reader.ReadToEnd();
                    reader.Close();
                }
            }
            return html;
        }
        catch (Exception ex)
        {
            string err = Utils.getUserIP();
            err += "_" + Utils.GetTime();
            err += "_" + url;
            err += Convert.ToString(ex.Message);
            logHelper.Write(err);
            return string.Empty;

        }
    }
    #endregion
}

