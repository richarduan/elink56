using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUI
{
    public class officeBasePages:BasePage
    {


        #region 读取菜单XML
        /*-----------读取菜单-----------*/
        #region 读取菜单
        /// <summary>
        /// 读取菜单
        /// </summary>
        /// <returns></returns>
        public string theMenus()
        {

            string str = string.Empty;







            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            string filen = Server.MapPath("config/mainmenu.xml");
            xmlDoc.Load(filen);

            System.Xml.XmlNodeList menulist = xmlDoc.SelectSingleNode("menulist").ChildNodes;


            str += "<ul class=\"sidebar-menu\">\r\n";



            foreach (System.Xml.XmlElement menuInfo in menulist)
            {
                str += "<li class=\"header\">" + menuInfo.Attributes["name"].Value + "</li>\r\n";

                System.Xml.XmlNodeList headerlist = menuInfo.ChildNodes;
                foreach (System.Xml.XmlElement headerInfo in headerlist)
                {

                    System.Xml.XmlNodeList menus = headerInfo.ChildNodes;
                    if (menus.Count > 0)
                    {


                        str += "<li class=\"treeview\">\r\n";
                        str += "<a href=\"" + headerInfo.Attributes["url"].Value + "\" target=\"" + headerInfo.Attributes["blank"].Value + "\" ><i class=\"fa " + headerInfo.Attributes["icon"].Value + "\"></i> <span>" + headerInfo.Attributes["name"].Value + "</span>\r\n";
                        str += "<span class=\"pull-right-container\">\r\n";
                        str += "<i class=\"fa fa-angle-left pull-right\"></i>\r\n";
                        str += "</span>\r\n";
                        str += "</a>\r\n";

                        str += "<ul class=\"treeview-menu\">\r\n";
                        foreach (System.Xml.XmlElement menu in menus)
                        {
                            str += "<li><a href=\"" + menu.Attributes["url"].Value + "\" target=\"" + menu.Attributes["blank"].Value + "\"><i class=\"fa " + menu.Attributes["icon"].Value + "\"></i>" + menu.Attributes["name"].Value + "</a></li>\r\n";
                        }
                        str += "</ul>\r\n";
                        str += "</li>\r\n";

                    }
                    else
                    {
                        str += "<li><a href=\"" + headerInfo.Attributes["url"].Value + "\" target=\"" + headerInfo.Attributes["blank"].Value + "\"><i class=\"fa " + headerInfo.Attributes["icon"].Value + "\"></i> <span>" + headerInfo.Attributes["name"].Value + "</span></a></li>\r\n";
                    }


                }

            }


            str += "</ul>\r\n";





            try
            {
            }
            catch (Exception ex)
            {
                StringBuilder Err = new StringBuilder();
                Err.Append(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName.ToString());
                Err.Append(":" + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                Err.Append("\r\nMessage:" + ex.Message.ToString());

                logHelper.Write(Err.ToString());
                str = "菜单异常";

            }






            return str;

        }
        /*-----------读取菜单-----------*/
        #endregion
        #endregion

    }
}