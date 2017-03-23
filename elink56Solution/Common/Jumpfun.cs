using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    /// <summary>
    /// 跳转类
    /// </summary>
    public class Jumpfun
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public Jumpfun()
        {
        }

        /// <summary>
        /// JS纯提示
        /// </summary>
        /// <param name="temp">提示内容</param>
        /// <returns></returns>
        public static string alert(string temp)
        {
            return "<script language=\"JavaScript\">alert('" + temp.ToString() + "');</script>";
        }

        /// <summary>
        /// JS提示跳转
        /// </summary>
        /// <param name="temp">提示内容</param>
        /// <param name="url">跳转内容</param>
        /// <returns></returns>
        public static string alertGo(string temp, string url)
        {
            return "<script language=\"JavaScript\">alert('" + temp.ToString() + "'); window.location.href='" + url.ToString() + "'</script>";
        }


        /// <summary>
        /// 直接跳转至错误页面
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static string ErrGo(string temp)
        {
            return "<script language=\"JavaScript\">alert('" + temp.ToString() + "'); window.location.href='http://log.nutcn.com/WebErr/?ErrCome=DownGO&ErrValue=" + temp.ToString() + "'</script>";
        }

        /// <summary>
        /// 提示并返回
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static string aleReturn(string temp)
        {
            return "<script language=\"JavaScript\">alert('" + temp.ToString() + "');history.go(-1);</script>";
        }


        /// <summary>
        /// 操作提示 YES OR NO
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static string alertConfirm(string temp)
        {
            return "javascript:{if(confirm('System Aler:" + temp.ToString() + "?')){return true;}return false;} ;";
        }

        public static string alertClose(string temp)
        {
            return "<script language=\"JavaScript\">alert('" + temp.ToString() + "');window.close();</script>";
        }


        public static string confirm(string temp)
        {
            return "<script language=\"JavaScript\">$('" + temp + "').click(function(){if(!confirm(\"是否确定删除该数据？删除后将无法恢复！\")){return false}});</script>";
        }

        public static string confirm(string temp, string err)
        {
            return "<script language=\"JavaScript\">$('" + temp + "').click(function(){if(!confirm(\"" + err + "\")){return false}});</script>";
        }


        


    }

