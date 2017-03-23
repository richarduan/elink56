using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class logHelper
{
    public string logName = string.Empty;


    public void WriteLog(string logContentr)
    {

        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(logName+ ".txt", true))
        {
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "|" + logContentr);
            sw.Close();
            sw.Dispose();
        }

    }

    public static void Write(string logContentr)
    {

        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString() + "log\\" + Utils.GetDate() + ".txt"), true))
        {
            StringBuilder str = new StringBuilder();
            str.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss "));
            str.Append("\r\n");
            str.Append(Utils.getUserIP());
            str.Append("\r\n");
            str.Append(logContentr);
            str.Append("\r\n");
            str.Append("------------------------------------------------------------------------");

            sw.WriteLine(Convert.ToString(str));
            sw.Close();
            sw.Dispose();
        }

    }

    public static void Write(string logContentr, string filename)
    {

        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(System.Web.HttpContext.Current.Server.MapPath("log/" + filename + ".txt"), true))
        {
            StringBuilder str = new StringBuilder();
            str.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss "));
            str.Append("\r\n");
            str.Append(Utils.getUserIP());
            str.Append("\r\n");
            str.Append(logContentr);
            str.Append("\r\n");
            str.Append("------------------------------------------------------------------------");

            sw.WriteLine(Convert.ToString(str));
            sw.Close();
            sw.Dispose();
        }

    }


    public void WriteLog(string logContentr, string fxt)
    {

        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(logName + fxt, true))
        {
            sw.WriteLine(logContentr);
            sw.Close();
            sw.Dispose();
        }

    }



}
