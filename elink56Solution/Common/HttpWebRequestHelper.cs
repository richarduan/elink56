using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;


public class HttpWebRequestHelper
{


    public static string PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
    {
        string ret = string.Empty;
        try
        {
            byte[] byteArray = dataEncode.GetBytes(paramData); //转化
            System.Net.HttpWebRequest webReq = (System.Net.HttpWebRequest)WebRequest.Create(new Uri(postUrl));
            webReq.Method = "POST";
            webReq.ContentType = "application/x-www-form-urlencoded";


            webReq.ContentLength = byteArray.Length;
            Stream newStream = webReq.GetRequestStream();
            newStream.Write(byteArray, 0, byteArray.Length);//写入参数
            newStream.Close();
            HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            ret = sr.ReadToEnd();
            sr.Close();
            response.Close();
            newStream.Close();
        }
        catch (Exception ex)
        {
            StringBuilder Err = new StringBuilder();
            Err.Append(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName.ToString());
            Err.Append(":" + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            Err.Append("\r\n");
            Err.Append(ex.Message);
            logHelper.Write(Err.ToString());
        }
        return ret;
    }


}

