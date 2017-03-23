using System;
using System.Data;
using System.Configuration;
using System.Web;



/// <summary>
/// WebAsPagesize翻页类
/// </summary>
public class WebAsPagesize
{

    /// <summary>
    /// WebAsPagesize翻页类
    /// </summary>
    public WebAsPagesize()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //

    }
    private int PageLen = 5;



    #region 控件属性
    private int Totalmsg;
    /// <summary>
    /// TotalPage 总条数
    /// </summary>
    public int strTotalmsg
    {
        set
        {
            Totalmsg = value;
        }
        get
        {
            return strTotalmsg;
        }
    }
    private string CurrentPage;
    /// <summary>
    /// CurrentPage 当前页面
    /// </summary>
    public string sCurrentPage
    {
        set
        {
            CurrentPage = value;
        }
        get
        {
            return sCurrentPage;
        }
    }
    private string QueryStr;
    /// <summary>
    /// QueryStr GET字符
    /// </summary>
    public string strQueryStr
    {
        set
        {
            QueryStr = value;
        }
        get
        {
            return strQueryStr;
        }
    }

    private string PageName;
    public string strPageName
    {
        set
        {
            PageName = value;
        }
        get
        {
            return PageName;
        }
    }



    private int PageNum;
    /// <summary>
    /// 
    /// </summary>
    public int strPageNum
    {
        set
        {
            PageNum = value;
        }
        get
        {
            return strPageNum;
        }
    }

    private string ClassName;
    /// <summary>
    /// 
    /// </summary>
    public string strClassName
    {
        set
        {
            ClassName = value;
        }
        get
        {
            return ClassName;
        }
    }




    private string UrlStr;
    /// <summary>
    /// 翻页条件
    /// </summary>
    public string strUrlstr
    {
        set
        {
            UrlStr = value;
        }
        get
        {
            return UrlStr;
        }
    }


    #endregion

    /// <summary>
    /// indexPage 当前页面判断
    /// </summary>
    private int indexPage()
    {
        try
        {
            int CurrPage;

            if (CurrentPage == "" || CurrentPage == null)
            {
                CurrPage = 1;
            }
            else
            {
                CurrPage = int.Parse(CurrentPage);
            }


                if (CurrPage < 1)
                {
                    return 1;
                }
                else
                {
                    return CurrPage;
                }
        }
        catch
        {
            return 1;
        }
    }

    /// <summary>
    /// 计算总页数
    /// </summary>
    private int TotaPage()
    {
        int Pages = Totalmsg % PageNum;

        if (Pages == 0)
        {
            return Totalmsg / PageNum;
        }
        else
        {
            return Totalmsg / PageNum + 1;
        }
    }


    /// <summary>
    /// 数字翻页方法
    /// </summary>
    private string NextPage()
    {
        try
        {

            int idp = indexPage();
            string PageStr = "";

            if (idp > 0)
            {
                for (int i = idp - 5; i <= indexPage() + PageLen; i++)
                {
                    if (i > TotaPage())
                        break;

                    if (i < 1)
                        continue;

                    if (i - indexPage() == 0)
                    {
                        PageStr += "<li class=\"active\"><a >" + i + "</a></li>";
                    }
                    else
                    {
                        PageStr += "<li>";
                        PageStr += "<a class=\"" + ClassName + "\" href=\"" + PageName + "?page=" + i + UrlStr + "\">" + i + "</a>";
                        PageStr += "</li>";
                    }
                }
            }

            return PageStr;
        }
        catch( Exception ex)
        {
            return "err:"+ex.Message;
        }

    }

    private string onePage()
    {
        return "<li><a class=\"" + ClassName + "\" href=\"" + PageName + "?page=1" + UrlStr + "\">第一页</a></li>";
    }

    private string endPage()
    {
        return "<li><a class=\"" + ClassName + "\" href=\"" + PageName + "?page=" + TotaPage().ToString() + UrlStr + "\">最后一页</a></li>";
    }

    private string onNext()
    {
        string str = "";
        int intPage = indexPage() + 1;

        if (intPage <= TotaPage())
            str += "<li><a class=\"" + ClassName + "\" href=\"" + PageName + "?page=" + intPage + UrlStr + "\">下一页</a></li>";
        return str;
    }
    private string onPir()
    {
        string str = "";
        int intPage = indexPage() - 1;

        if (intPage > 0)
            str += "<li><a class=\"" + ClassName + "\" href=\"" + PageName + "?page=" + intPage + UrlStr + "\">上一页</a></li>";
        return str;
    }


    public string PageSize()
    {

        string div = "";

        div += "<ul id=\"PageSize\" class=\"PageSize\">";
        div += onePage();
        div += onPir();
        div += NextPage();
        div += onNext();
        div += endPage();
        div += "</ul>";

        return div;
    }









}
