using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

public class JsonHelper
{
    /// <summary>
    /// JsonString To T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsonString"></param>
    /// <returns></returns>
    public static T parse<T>(string jsonString)
    {

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (T)new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T)).ReadObject(ms);
            }

    }
    

    /// <summary>
    /// Object To Json
    /// </summary>
    /// <param name="jsonObject"></param>
    /// <returns></returns>
    public static string stringify(object jsonObject)
    {

        try
        {
            using (var ms = new MemoryStream())
            {
                new System.Runtime.Serialization.Json.DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);
                return Encoding.UTF8.GetString(ms.ToArray());
            }


        }
        catch(Exception ex)
        {
            logHelper.Write(Convert.ToString(ex.Message));
            return string.Empty;
        }
    }

    public static string restfulJSON(string relues, string errValue, string inputId)
    {


        System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        System.Collections.Generic.Dictionary<string, string> drow = new System.Collections.Generic.Dictionary<string, string>();

        drow.Add("status", relues);
        drow.Add("msg", errValue);
        drow.Add("relues", inputId);
        return jss.Serialize(drow);

    }



}
