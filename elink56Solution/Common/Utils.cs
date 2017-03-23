using System;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using Microsoft.VisualBasic;
using System.Collections;
using System.Net;
using System.Xml;

public class Utils
{
    public Utils()
    {
    }

    #region 获得当前绝对路径
    /// <summary>
    /// 获得当前绝对路径
    /// </summary>
    /// <param name="strPath">指定的路径</param>
    /// <returns>绝对路径</returns>
    public static string GetMapPath(string strPath)
    {
        if (HttpContext.Current != null)
        {
            return HttpContext.Current.Server.MapPath(strPath);
        }
        else //非web程序引用
        {
            strPath = strPath.Replace("/", "\\");
            if (strPath.StartsWith("\\"))
            {
                strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
            }
            return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
        }
    }
    #endregion



    #region 检测是否是正确的
    /// <summary>
    /// 检测是否是正确的Url
    /// </summary>
    /// <param name="strUrl">要验证的Url</param>
    /// <returns>判断结果</returns>
    public static bool IsURL(string strUrl)
    {
        return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
    }
    #endregion

    #region 时间工具
    /// <summary>
    /// 返回标准日期格式string
    /// </summary>
    public static string GetDate()
    {
        return DateTime.Now.ToString("yyyy-MM-dd");
    }

    /// <summary>
    /// 返回指定日期格式
    /// </summary>
    public static string GetDate(string datetimestr, string replacestr)
    {
        if (datetimestr == null)
            return replacestr;

        if (datetimestr.Equals(""))
            return replacestr;

        try
        {
            datetimestr = Convert.ToDateTime(datetimestr).ToString("yyyy-MM-dd").Replace("1900-01-01", replacestr);
        }
        catch
        {
            return replacestr;
        }
        return datetimestr;
    }


    /// <summary>
    /// 返回标准时间格式string
    /// </summary>
    public static string GetTime()
    {
        return DateTime.Now.ToString("HH:mm:ss");
    }

    /// <summary>
    /// 返回标准时间格式string
    /// </summary>
    public static string GetDateTime()
    {
        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    /// <summary>
    /// 返回相对于当前时间的相对天数
    /// </summary>
    public static string GetDateTime(int relativeday)
    {
        return DateTime.Now.AddDays(relativeday).ToString("yyyy-MM-dd HH:mm:ss");
    }

    /// <summary>
    /// 返回标准时间格式string
    /// </summary>
    public static string GetDateTimeF()
    {
        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffffff");
    }

    /// <summary>
    /// 返回标准时间 
    /// </sumary>
    public static string GetStandardDateTime(string fDateTime, string formatStr)
    {
        if (fDateTime == "0000-0-0 0:00:00")
            return fDateTime;

        return Convert.ToDateTime(fDateTime).ToString(formatStr);
    }

    /// <summary>
    /// 返回标准时间 yyyy-MM-dd HH:mm:ss
    /// </sumary>
    public static string GetStandardDateTime(string fDateTime)
    {
        return GetStandardDateTime(fDateTime, "yyyy-MM-dd HH:mm:ss");
    }

    /// <summary>
    /// 返回标准时间 yyyy-MM-dd
    /// </sumary>
    public static string GetStandardDate(string fDate)
    {
        return GetStandardDateTime(fDate, "yyyy-MM-dd");
    }
    
    /// <summary>
    /// 是否为标准时间
    /// </sumary>
    public static bool isDateTime(string dateTime)
    {
        if (string.IsNullOrEmpty(dateTime)) return false;
        DateTime time;
        if (DateTime.TryParse(dateTime, out time)) return true;

        return false;
    }

    /// <summary>
    /// 日期比较
    /// </summary>
    /// <param name="today">当前日期</param>
    /// <param name="writeDate">输入日期</param>
    /// <param name="n">比较天数</param>
    /// <returns>大于天数返回true，小于返回false</returns>
    public static bool CompareDate(string today, string writeDate, int n)
    {
        DateTime Today = Convert.ToDateTime(today);
        DateTime WriteDate = Convert.ToDateTime(writeDate);
        WriteDate = WriteDate.AddDays(n);
        if (Today >= WriteDate)
            return false;
        else
            return true;
    }

    public string FormatValue(decimal value)
    {
        StringBuilder sb = new StringBuilder();

        string sValue = value.ToString();

        System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();

        string temp = sValue;

        bool includeFloat = sValue.LastIndexOf(".") != -1;

        if (includeFloat)
        {
            //temp = temp.Substring(0, sP);
            temp = ((int)value).ToString();
        }

        int templength = temp.Length;

        if (temp.Length > 3)
        {
            while (templength > 3)
            {
                list.Add(temp.Substring(templength - 3, 3));
                templength -= 3;
            }

            //最前面的添加进来
            list.Add(temp.Substring(0, temp.Length - list.Count * 3));

            for (int i = list.Count - 1; i > 0; i--)
            {
                sb.Append(list[i] + ",");
            }
            sb.Append(list[0]);

            if (includeFloat)
            {
                sb.Append(sValue.Substring(sValue.LastIndexOf(".")));
            }
        }
        else
        {
            return sValue;
        }
        return sb.ToString();
    }


    #endregion


    #region 得到站点用户IP,
    /// <summary>
    /// 得到站点用户IP, IpSTR = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString()
    /// </summary>
    /// <returns></returns>
    public static string getUserIP()
    {
        return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
    }
    #endregion

    /*
    /// <summary>
    /// 读取web.config相关数据信息
    /// </summary>
    /// <param name="xmlTargetElement">相关字节</param>
    /// <returns></returns>
    /// 编写时间2007-03-08  y.xiaobin(著)
    public static string getXmlElementValue(string xmlTargetElement)
    {
        return System.Configuration.ConfigurationManager.AppSettings[xmlTargetElement];
    }
     * */


    #region SetConfiguration
    /// <summary>
    /// 设置appSetting的值
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    public static string SetAppSettings(string key)
    {
        string conString = System.Configuration.ConfigurationManager.AppSettings[key];
        return conString;

    }
    #endregion

    #region  web.config相关文件操作
    /// <summary>
    /// web.config相关文件操作
    /// 0检测是web.config是否为只读或可写;返回值为:true或false,1把web.config改写为只读;2把web.config改写为可写
    /// 在此函数中自动去根目下寻找web.config
    /// </summary>
    /// <param name="flg">0检测是web.config是否为只读或可写;返回值为:true或false,1把web.config改写为只读;2把web.config改写为可写</param>
    /// 2007-5-9 y.xiaobin
    /// <returns></returns>
    public static bool constReadOnly(int num)
    {
        bool _readonly = false;
        string _config = HttpContext.Current.Server.MapPath(@"~/Web.config");
        FileInfo fi = new FileInfo(_config);
        switch (num)
        {
            case 0: _readonly = fi.IsReadOnly; break;
            case 1:
                fi.IsReadOnly = true;
                _readonly = true;
                break;
            case 2:
                {
                    fi.IsReadOnly = false;
                    _readonly = false;
                }
                break;
            default: throw new Exception("错误参数!");
        }

        return _readonly;

    }

    /// <summary>
    /// web.config相关文件操作
    /// 0检测是web.config是否为只读或可写;返回值为:true或false,1把config改写为只读;2把web.config改写为可写
    /// 在此函数中自动去根目下寻找web.config
    /// </summary>
    /// <param name="flg">0检测是web.config是否为只读或可写;返回值为:true或false,1把web.config改写为只读;2把web.config改写为可写</param>
    /// 2007-5-9 y.xiaobin
    /// <returns></returns>
    public static bool constReadOnly(int num, string strSource)
    {
        bool _readonly = false;
        string _config = HttpContext.Current.Server.MapPath(@"~/" + strSource);
        FileInfo fi = new FileInfo(_config);
        switch (num)
        {
            case 0: _readonly = fi.IsReadOnly; break;
            case 1:
                fi.IsReadOnly = true;
                _readonly = true;
                break;
            case 2:
                {
                    fi.IsReadOnly = false;
                    _readonly = false;
                }
                break;
            default: throw new Exception("错误参数!");
        }

        return _readonly;

    }
    /// <summary>
    /// 保存web.config设置
    /// </summary>
    /// <param name="xmlTargetElement">关键字</param>
    /// <param name="xmlText">value</param>
    /// 2007.05.09 修改 y.xiaobin
    public static void SaveXmlElementValue(string xmlTargetElement, string xmlText)
    {
        string returnInt = null;
        string filename = HttpContext.Current.Server.MapPath("~") + @"/Web.config";
        System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
        xmldoc.Load(filename);
        System.Xml.XmlNodeList topM = xmldoc.DocumentElement.ChildNodes;
        foreach (System.Xml.XmlNode element in topM)
        {
            if (element.Name == "appSettings")
            {
                System.Xml.XmlNodeList node = element.ChildNodes;
                if (node.Count > 0)
                {
                    foreach (System.Xml.XmlNode el in node)
                    {
                        if (el.Name == "add")
                        {
                            if (el.Attributes["key"].InnerXml == xmlTargetElement)
                            {
                                //保存web.config数据
                                el.Attributes["value"].Value = xmlText;
                                xmldoc.Save(HttpContext.Current.Server.MapPath(@"~/Web.config"));
                                return;
                            }
                        }
                    }
                }
                else
                {
                    returnInt = "Web.Config配置文件未配置";
                }
                break;
            }
            else
            {
                returnInt = "Web.Config配置文件未配置";
            }
        }

        if (returnInt != null)
            throw new Exception(returnInt);
    }
    #endregion

    /// <summary>
    /// 是否为ip
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public static bool IsIP(string ip)
    {
        return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
    }

    public static bool IsIPSect(string ip)
    {
        return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){2}((2[0-4]\d|25[0-5]|[01]?\d\d?|\*)\.)(2[0-4]\d|25[0-5]|[01]?\d\d?|\*)$");
    }


    public static string PasswordMD5(string password)
    {
        return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password.ToString().Trim(), "MD5");
    }

}
