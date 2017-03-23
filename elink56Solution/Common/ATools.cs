using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Drawing;
//using System.Drawing.Imaging;
using System.IO;



    
    /// <summary>
    /// ATools工具操作类..如Session,Cookie...
    /// </summary>
    public class ATools
    {
        #region ATools工具操作类..如Session,Cookie...

       /// <summary>
       /// 返回Session信息
       /// </summary>
       /// <param name="strName">Session名称</param>
       /// <returns></returns>
        public static string GetSession(string strName)
        {
            if (System.Web.HttpContext.Current.Session[strName] == null)
            {
                return "";
            }
            return System.Web.HttpContext.Current.Session[strName].ToString();
        }
        #endregion

        #region 写Session信息
        /// <summary>
        /// 写Session信息
        /// </summary>
        /// <param name="strName">Session名称</param>
        /// <param name="strValue">值</param>
        public static void WriteSession(string strName,string strValue)
        {
            if (strName != "" && strValue!="")
            {
                HttpContext.Current.Session[strName] = strValue;
                HttpContext.Current.Session.Timeout = 60*20;
            }
        }
        #endregion 


        #region 写cookie值
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);

        }
        #endregion 

        #region 写cookie值
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="expires">过期时间(分钟)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);

        }

        #region 写cookie值
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="expires">过期时间(分钟)</param>
        public static void WriteCookie(string strName, List<System.Collections.ArrayList> strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }

            foreach (System.Collections.ArrayList Arr in strValue)
            {
                cookie.Values.Remove(Convert.ToString(Arr[0]));
                cookie.Values.Add(Convert.ToString(Arr[0]), Convert.ToString(Arr[1]));
            }
            

            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);

        }
        #endregion


        #endregion


        #region 读cookie值
        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (System.Web.HttpContext.Current.Request.Cookies != null && System.Web.HttpContext.Current.Request.Cookies[strName] != null)
            {
                return System.Web.HttpContext.Current.Request.Cookies[strName].Value.ToString();
            }

            return "";
        }

        #endregion 

        public static string CreateCheckCodeString()
        {
            //定义验证码的字符数组 
            char[] allCharArray =   {   '0','1','2','3','4','5','6','7','8','9', 
                        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O', 
                        'P','Q','R','S','T','U','V','W','X','Y','Z'};

            //定义验证码字符串 
            string randomCode = "";
            Random rand = new Random();

            //生成6位验证码字符串 
            for (int i = 0; i < 6; i++)
                randomCode += allCharArray[rand.Next(allCharArray.Length)];
            return randomCode;
        }

        /*
        /// <summary>
        /// 生成验证码图片 
        /// </summary>
        /// <param name="checkCode"></param>
        public static void CreateCheckCodeImage()
        {
            string checkCode = CreateCheckCodeString();

            //定义图片宽度 
            int iWidth = 80;
            //定义图片高度 
            int iHeight = 22;
            //定义大小为12pt的   Arial字体,用于绘制文字 
            Font font = new Font("Arial", 12, FontStyle.Bold);
            //定义黑色的单色画笔,用于绘制文字 
            SolidBrush brush = new SolidBrush(Color.Red);
            //定义钢笔,用户绘制干扰线 
            Pen pen1 = new Pen(Color.SeaGreen, 0);   //注意这里直接获得一个现有的Color对象 
            Pen pen2 = new Pen(Color.FromArgb(255, 255, 255), 0);   //注意这里根据Argb值获得了一个Color对象 

            //创建一个px*20px的图象 
            Bitmap image = new Bitmap(iWidth, iHeight);
            //从图象获得一个绘图面 
            Graphics g = Graphics.FromImage(image);
            //清除整个绘图画面并以指定颜色填充 
            g.Clear(ColorTranslator.FromHtml("#FFFFFF"));   //注意这里从html颜色代码获取Color对象 
            //定义文字的绘制矩形区域 
            RectangleF rect = new RectangleF(8, 3, iWidth, iHeight);
            //定义一个随机对象，用于绘制干扰线 
            Random rand = new Random();
            //生成两条横向干扰线 
            for (int i = 0; i < 3; i++)
            {
                //定义起点 
                Point p1 = new Point(0, rand.Next(iHeight));
                //定义终点 
                Point p2 = new Point(iWidth, rand.Next(iHeight));
                //绘制直线 
                g.DrawLine(pen1, p1, p2);
            }

            //生成4条纵向干扰线 
            for (int i = 0; i < 5; i++)
            {
                //定义起点 
                Point p1 = new Point(rand.Next(iHeight), 0);
                //定义终点 
                Point p2 = new Point(rand.Next(iHeight), iHeight);
                //绘制直线 
                g.DrawLine(pen2, p1, p2);
            }

            //绘制验证码文字 
            g.DrawString(checkCode, font, brush, rect);
            //保存图片为Jpeg格式 
             //ImageFormat.后面能更改保存图片的格式类型 
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Jpeg);
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ContentType = "image/Jpeg";
            HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            HttpContext.Current.Session["code"] = checkCode;
            //释放对象 
            g.Dispose();
            image.Dispose();
        }*/

        #region 背景色设置
        /// <summary>
        /// 背景色设置
        /// </summary>
        /// <param name="t"></param>
        /// <param name="classtr"></param>
        /// <returns></returns>
        public static string getColor(int t, string classtr)
        {
            if (t % 2 == 0)
            {
                return "class=\"bg\"";
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region image输出控制
        /// <summary>
        /// 输出控制
        /// </summary>
        /// <param name="imageName"></param>
        /// <param name="alt"></param>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string getImage(string imageName,string alt , string src,string style)
        {
            if (imageName == "0")
            {
                return "<div>暂无图片</div>";
            }
            return "<img " + style + "alt=\"" + alt + "\" src=\"" + src + imageName + "\" />";
        }
        #endregion


        /// <summary>
        /// 随进函数长度参数
        /// </summary>
        /// <returns></returns>
        public static int RandomNum(int length)
        {
            Random rand = new Random();
            string sNum = "1";
            for (int i = 1; i < length; i++)
            {
                sNum = sNum + rand.Next(0, 9).ToString();
            }
            return Convert.ToInt32(sNum);
        }
        /// <summary>
        /// 无长度参数
        /// </summary>
        /// <returns></returns>
        public static int RandomNum()
        {
            Random rand = new Random();
            string sNum = "1";
            for (int i = 1; i < 5; i++)
            {
                sNum = sNum + rand.Next(0, 9).ToString();
            }
            return Convert.ToInt32(sNum);
        }


        /// <summary>
        /// 字符过滤
        /// </summary>
        /// <param name="temp">字符串</param>
        /// <returns></returns>
        public static string HTMLEncode(string temp)
        {
            string str = temp;
            str = str.Replace("\r\n", "<br />");
            str = str.Replace("\r", "<br />");
            str = str.Replace("\n", "");
            return str.ToString();
        }

        public static string phoneReplace(string temp)
        {
            if (string.IsNullOrEmpty(temp) || temp.Length<11)  return string.Empty;
            string phoneNum = temp.Substring(0, 4);
            phoneNum += "****";
            phoneNum += temp.Substring(8, 3);

            return phoneNum;
        }


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





    }

