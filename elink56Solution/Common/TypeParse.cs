using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;



public class TypeParse
{

    #region 判断对象是否为Int32类型的数字
    /// <summary>
    /// 判断对象是否为Int32类型的数字
    /// </summary>
    /// <param name="Expression"></param>
    /// <returns></returns>
    public static bool IsNumeric(object Expression)
    {
        if (Expression != null)
        {
            try
            {
                string str = Expression.ToString();
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*[.]?[0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                    {
                        if (Convert.ToInt32(str) >= 0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        return false;

    }
    #endregion


    public static bool IsFloat(object Expression)
    {
        if (Expression == null) return false;

        string str = Convert.ToString(Expression);

        if (string.IsNullOrEmpty(str)) return false;
        


        Regex regex = new Regex(@"^\d*\.?\d*$");
        return regex.IsMatch(str.Trim());


    }


    public static bool IsDouble(object Expression)
    {
        if (Expression != null)
        {
            return Regex.IsMatch(Expression.ToString(), @"^([0-9])[0-9]*(\.\w*)?$");
        }
        return false;
    }

    public static bool isDate(string Expression)
    {
        if (Expression == null) return false;
        DateTime dtDate;
        return DateTime.TryParse(Expression, out dtDate);
    
    }

    #region DateDiff
    public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
    {

        string dateDiff = string.Empty;

        try
        {

            if (DateTime2 >= DateTime1)
            {

                TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);

                TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);

                TimeSpan ts = ts1.Subtract(ts2).Duration();

                dateDiff = string.Empty;
                dateDiff += ts.Days > 0 ? ts.Days.ToString() + "天" : string.Empty;

                dateDiff += ts.Hours > 0 ? ts.Hours.ToString() + "小时" : string.Empty;

                dateDiff += ts.Minutes > 0 ? ts.Minutes.ToString() + "分钟" : string.Empty;

                dateDiff += ts.Seconds.ToString() + "秒";
                return dateDiff;
            }
            return string.Empty;
        }

        catch
        {
            return string.Empty;
        }

     

    }
    #endregion




    /// <summary>  
    /// 使用正则表达式判断是否为日期  
    /// </summary>  
    /// <param name="str" type=string></param>  
    /// <returns name="isDateTime" type=bool></returns>  
    public static bool IsDateTime(string str)
    {
        bool isDateTime = false;
        // yyyy/MM/dd  
        if (Regex.IsMatch(str, "^(?<year>\\d{2,4})/(?<month>\\d{1,2})/(?<day>\\d{1,2})$"))
            isDateTime = true;
        // yyyy-MM-dd   
        else if (Regex.IsMatch(str, "^(?<year>\\d{2,4})-(?<month>\\d{1,2})-(?<day>\\d{1,2})$"))
            isDateTime = true;
        // yyyy.MM.dd   
        else if (Regex.IsMatch(str, "^(?<year>\\d{2,4})[.](?<month>\\d{1,2})[.](?<day>\\d{1,2})$"))
            isDateTime = true;
        // yyyy年MM月dd日  
        else if (Regex.IsMatch(str, "^((?<year>\\d{2,4})年)?(?<month>\\d{1,2})月((?<day>\\d{1,2})日)?$"))
            isDateTime = true;
        // yyyy年MM月dd日  
        else if (Regex.IsMatch(str, "^((?<year>\\d{2,4})年)?(正|一|二|三|四|五|六|七|八|九|十|十一|十二)月((一|二|三|四|五|六|七|八|九|十){1,3}日)?$"))
            isDateTime = true;

        // yyyy年MM月dd日  
        else if (Regex.IsMatch(str, "^(零|〇|一|二|三|四|五|六|七|八|九|十){2,4}年((正|一|二|三|四|五|六|七|八|九|十|十一|十二)月((一|二|三|四|五|六|七|八|九|十){1,3}(日)?)?)?$"))
            isDateTime = true;
        // yyyy年  
        //else if (Regex.IsMatch(str, "^(?<year>\\d{2,4})年$"))  
        //    isDateTime = true;  

        // 农历1  
        else if (Regex.IsMatch(str, "^(甲|乙|丙|丁|戊|己|庚|辛|壬|癸)(子|丑|寅|卯|辰|巳|午|未|申|酉|戌|亥)年((正|一|二|三|四|五|六|七|八|九|十|十一|十二)月((一|二|三|四|五|六|七|八|九|十){1,3}(日)?)?)?$"))
            isDateTime = true;
        // 农历2  
        else if (Regex.IsMatch(str, "^((甲|乙|丙|丁|戊|己|庚|辛|壬|癸)(子|丑|寅|卯|辰|巳|午|未|申|酉|戌|亥)年)?(正|一|二|三|四|五|六|七|八|九|十|十一|十二)月初(一|二|三|四|五|六|七|八|九|十)$"))
            isDateTime = true;

        // XX时XX分XX秒  
        else if (Regex.IsMatch(str, "^(?<hour>\\d{1,2})(时|点)(?<minute>\\d{1,2})分((?<second>\\d{1,2})秒)?$"))
            isDateTime = true;
        // XX时XX分XX秒  
        else if (Regex.IsMatch(str, "^((零|一|二|三|四|五|六|七|八|九|十){1,3})(时|点)((零|一|二|三|四|五|六|七|八|九|十){1,3})分(((零|一|二|三|四|五|六|七|八|九|十){1,3})秒)?$"))
            isDateTime = true;
        // XX分XX秒  
        else if (Regex.IsMatch(str, "^(?<minute>\\d{1,2})分(?<second>\\d{1,2})秒$"))
            isDateTime = true;
        // XX分XX秒  
        else if (Regex.IsMatch(str, "^((零|一|二|三|四|五|六|七|八|九|十){1,3})分((零|一|二|三|四|五|六|七|八|九|十){1,3})秒$"))
            isDateTime = true;

        // XX时  
        else if (Regex.IsMatch(str, "\\b(?<hour>\\d{1,2})(时|点钟)\\b"))
            isDateTime = true;
        else
            isDateTime = false;

        return isDateTime;
    }  

    #region string型转换为bool型
    /// <summary>
    /// string型转换为bool型
    /// </summary>
    /// <param name="strValue">要转换的字符串</param>
    /// <param name="defValue">缺省值</param>
    /// <returns>转换后的bool类型结果</returns>
    public static bool StrToBool(object Expression, bool defValue)
    {
        if (Expression != null)
        {
            if (string.Compare(Expression.ToString(), "true", true) == 0)
            {
                return true;
            }
            else if (string.Compare(Expression.ToString(), "false", true) == 0)
            {
                return false;
            }
        }
        return defValue;
    }
    #endregion

    #region 将对象转换为Int32类型
    /// <summary>
    /// 将对象转换为Int32类型
    /// </summary>
    /// <param name="strValue">要转换的字符串</param>
    /// <param name="defValue">缺省值</param>
    /// <returns>转换后的int类型结果</returns>
    public static int StrToInt(object Expression, int defValue)
    {

        if (Expression != null)
        {
            string str = Expression.ToString();
            if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*$"))
            {
                if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                {
                    return Convert.ToInt32(str);
                }
            }
        }
        return defValue;
    }
    #endregion

    #region string型转换为float型
    /// <summary>
    /// string型转换为float型
    /// </summary>
    /// <param name="strValue">要转换的字符串</param>
    /// <param name="defValue">缺省值</param>
    /// <returns>转换后的int类型结果</returns>
    public static float StrToFloat(object strValue, float defValue)
    {
        if ((strValue == null) || (strValue.ToString().Length > 10))
        {
            return defValue;
        }

        float intValue = defValue;
        if (strValue != null)
        {
            bool IsFloat = Regex.IsMatch(strValue.ToString(), @"^([-]|[0-9])[0-9]*(\.\w*)?$");
            if (IsFloat)
            {
                intValue = Convert.ToSingle(strValue);
            }
        }
        return intValue;
    }
    #endregion

    #region 判断给定的字符串数组(strNumber)中的数据是不是都为数值型
    /// <summary>
    /// 判断给定的字符串数组(strNumber)中的数据是不是都为数值型
    /// </summary>
    /// <param name="strNumber">要确认的字符串数组</param>
    /// <returns>是则返加true 不是则返回 false</returns>
    public static bool IsNumericArray(string[] strNumber)
    {
        if (strNumber == null)
        {
            return false;
        }
        if (strNumber.Length < 1)
        {
            return false;
        }
        foreach (string id in strNumber)
        {
            if (!IsNumeric(id))
            {
                return false;
            }
        }
        return true;

    }
    #endregion

    #region 泛型处理为DATASET
    /// <summary>
    /// 泛型处理为DATASET
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <param name="list"></param>
    /// <returns>DateSet</returns>
    public static DataSet ConvertToDataSet<T>(IList<T> list)
    {
        if (list == null || list.Count <= 0)
        {
            return null;
        }

        DataSet ds = new DataSet();
        DataTable dt = new DataTable(typeof(T).Name);
        DataColumn column;
        DataRow row;

        System.Reflection.PropertyInfo[] myPropertyInfo = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

        foreach (T t in list)
        {
            if (t == null)
            {
                continue;
            }

            row = dt.NewRow();

            for (int i = 0, j = myPropertyInfo.Length; i < j; i++)
            {
                System.Reflection.PropertyInfo pi = myPropertyInfo[i];

                string name = pi.Name;

                if (dt.Columns[name] == null)
                {
                    column = new DataColumn(name, pi.PropertyType);
                    dt.Columns.Add(column);
                }

                row[name] = pi.GetValue(t, null);
            }

            dt.Rows.Add(row);
        }

        ds.Tables.Add(dt);

        return ds;
    }
    #endregion

    #region 字符串转换数组
    /// <summary>
    /// 字符串转换数组
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string[] SplitToArray(string str)
    {
        char[] chs = str.ToCharArray();
        string[] sArr = new string[chs.Length];
        for (int i = 0; i < chs.Length; i++)
        {
            //sArr[i] = chs[i].ToString();   
            sArr[i] = new string(chs[i], 1);
        }
        return sArr;
    }
    #endregion

    #region 特殊字符串过滤
    /// <summary>
    /// 特殊字符串过滤
    /// </summary>
    /// <param name="temp">传入字符串</param>
    /// <returns>过滤输出</returns>
    public static string checkStr(string temp)
    {
        if (temp != null && temp != "")
        {
            string replace = temp.Replace("'", "’");
            return replace;
        }
        return temp;
    }

    #endregion


    #region 字符串截取（中英混合）
    /// <summary>
    /// c#的中英文混合字符串截取指定长度,startidx从0开始 
    /// </summary>
    /// <param name="str"></param>
    /// <param name="startidx"></param>
    /// <param name="len"></param>
    /// <returns></returns>
    public static string getStrLenB(string str, int startidx, int len)
    {
        int Lengthb = getLengthb(str);



        if (startidx + 1 > Lengthb)
        {
            return "";
        }
        int j = 0;
        int l = 0;
        int strw = 0;//字符的宽度   
        bool b = false;
        string rstr = "";
        for (int i = 0; i < str.Length; i++)
        {
            char c = str[i];
            if (j >= startidx)
            {
                rstr = rstr + c;
                b = true;
            }
            if (IsChinese(c))
            {
                strw = 2;
            }
            else
            {
                strw = 1;
            }
            j = j + strw;
            if (b)
            {
                l = l + strw;
                if ((l + 1) >= len) break;
            }


        }
        if (Lengthb > getLengthb(rstr))
        {
            rstr = rstr + "..";
        }

        return rstr;

    }

    public static string SearchString(string allStr, string starStr, string starEnd)
    {

        string start = starStr.Trim();
        string endstr = starEnd.Trim();

        Regex rg = new Regex("(?<=(" + starStr + "))[.\\s\\S]*?(?=(" + starEnd + "))", RegexOptions.Multiline | RegexOptions.Singleline);
        string str = rg.Match(allStr.Trim()).Value;
        return str;

    }

    #endregion

    #region 获得字节长度
    /// <summary>
    /// 获得字节长度
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>

    private static int getLengthb(string str)
    {
        return System.Text.Encoding.Default.GetByteCount(str);
    }
    #endregion

    #region 字符是否为汉字
    /// <summary>
    /// 字符是否为汉字
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static bool IsChinese(char c)
    {
        return (int)c >= 0x4E00 && (int)c <= 0x9FA5;
    }
    #endregion

    #region HTML过滤
    /// <summary>
    /// HTML过滤
    /// </summary>
    /// <param name="html"></param>
    /// <returns></returns>
    public static string checkHTML(string html)
    {
        System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" no[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        html = regex1.Replace(html, ""); //过滤<script></script>标记 
        html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性 
        html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件 
        html = regex4.Replace(html, ""); //过滤iframe 
        html = regex5.Replace(html, ""); //过滤frameset 
        html = regex6.Replace(html, ""); //过滤frameset 
        html = regex7.Replace(html, ""); //过滤frameset 
        html = regex8.Replace(html, ""); //过滤frameset 
        html = regex9.Replace(html, "");
        html = html.Replace(" ", "");
        html = html.Replace("</strong>", "");
        html = html.Replace("<strong>", "");
        return html;
    }
    #endregion

    #region 字符过滤
    /// <summary>
    /// 字符过滤
    /// </summary>
    /// <param name="temp">字符串</param>
    /// <returns></returns>
    public static string HTMLEncode(string temp)
    {
        string str = temp;
        str = str.Replace("\r", "<br />");
        str = str.Replace("\n", "");

        if (string.IsNullOrEmpty(str)) return string.Empty;
        return str.ToString();
    }
    #endregion

    #region 随进函数长度参数
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
    #endregion

    #region 检测是否符合email格式
    /// <summary>
    /// 检测是否符合email格式
    /// </summary>
    /// <param name="strEmail">要判断的email字符串</param>
    /// <returns>判断结果</returns>
    public static bool IsValidEmail(string strEmail)
    {
        return Regex.IsMatch(strEmail, @"^[\w\.]+([-]\w+)*@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]");
    }

    public static bool IsValidDoEmail(string strEmail)
    {
        return Regex.IsMatch(strEmail, @"^@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
    }
    #endregion

    /// <summary>
    /// 判断手机号
    /// </summary>
    /// <param name="mobile"></param>
    /// <returns></returns>
    public static bool ValidateMobile(string mobile)
    {

        if (string.IsNullOrEmpty(mobile)) return false;
        return System.Text.RegularExpressions.Regex.IsMatch(mobile, @"^(13|14|15|16|18|19)\d{9}$");

    }




    #region 格式化显示时间为几个月,几天前,几小时前,几分钟前,或几秒前
    /// <summary>
    /// 格式化显示时间为几个月,几天前,几小时前,几分钟前,或几秒前
    /// </summary>
    /// <param name="dt">要格式化显示的时间</param>
    /// <returns>几个月,几天前,几小时前,几分钟前,或几秒前</returns>
    public static string DateStringFromNow(DateTime dt)
    {
        TimeSpan span = DateTime.Now - dt;
        if (span.TotalDays > 60)
        {
            return dt.ToShortDateString();
        }
        else if (span.TotalDays > 30)
        {
            return "1个月前";
        }
        else if (span.TotalDays > 14)
        {
            return "2周前";
        }
        else if (span.TotalDays > 7)
        {
            return "1周前";
        }
        else if (span.TotalDays > 1)
        {
            return string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
        }
        else if (span.TotalHours > 1)
        {
            return string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
        }
        else if (span.TotalMinutes > 1)
        {
            return string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
        }
        else if (span.TotalSeconds >= 1)
        {
            return string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
        }
        else
        {
            return "1秒前";
        }
    }
    #endregion

    public static string ConvertToDecimalPoint(double value)
    {
        if (Convert.ToDouble(value) <= 0) return string.Empty;
        return convertToDecimalPoint(Convert.ToDouble(value).ToString("0.00"));
    }
    public static string convertToDecimalPoint(string str)
    {
        int d = str.IndexOf(".");
        string z = str.Substring(0, d);
        string x = str.Substring(d, str.Length - z.Length).TrimEnd('0');
        return x == "." ? z : z + x;
    }
}





