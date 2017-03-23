using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using WebUI;
using Models;
public partial class GroupLister : WebUI.GroupPage
{

    public BLL.GroupBLL groupbll = new BLL.GroupBLL();
    public string Backhref = string.Empty,subid=string.Empty;
    public GroupInfo groupinfo = new GroupInfo();
    public bool loadShow = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        subid = this.RequestSubjectionId;

        if (Convert.ToInt32(subid) > 0)
        {
            groupinfo = groupbll.GetModel(Convert.ToInt32(subid));
            if (groupinfo == null)
            {
                Response.Redirect("GroupLister.aspx");
                Response.End();
                return;
            }

            loadShow = true;
            Backhref = string.Format("<a href=\"http://{0}/GroupLister.aspx?subjectionId={1}\" class=\" pull-right\">返回上级</a>",
                this.caterDomain,
                groupinfo.Group_subjectionId.ToString());

        }




        //    Backhref = "<a href="" >返回上级</a>"


    }

    public string GetGroupTable
    {
        get
        {
            
  


            string str = string.Empty, tr = string.Empty;
            str += "<table class=\"table table-bordered table-hover\" style=\"margin-bottom:0px;\">";
            str += "<thead>";
            str += "<tr>";
            str += "<th class=\"Textalignleft\">部门编码</th>";
            str += "<th>部门名称</th>";
            str += "<th>上级部门</th>";
            //str += "<th>所属层次</th>";
            str += "<th>部门等级</th>";
            str += "<th>部门负责人</th>";
            str += "<th class=\"textcenter\">查看</th>";
            str += "</tr>";
            str += "</thead>";
            str += "<tbody>{0}";
            str += "</tbody>";
            str += "</table>";

            IList<GroupInfo> list = this.groupbll.GetList(Convert.ToInt32(subid));
            if (list.Count < 1)
            { 
                
            }else{
                foreach (GroupInfo info in list)
                {
                    tr += "<tr>";
                    tr += "<td>{0}</td>";
                    tr += "<td>{1}</td>";
                    tr += "<td>{2}</td>";
                    //tr += "<td>{3}</td>";
                    tr += "<td>{4}</td>";
                    tr += "<td>{5}</td>";
                    tr += "<td class=\"textcenter\">";
                    //tr += "<a href=\"\">信息</a>&nbsp;&nbsp;";
                    tr += "<a href=\"http://{6}/GroupLister.aspx?subjectionId={0}\">查看</a>";
                    tr += "</td>";
                    tr += "</tr>";




                    if (info.Group_subjectionId == 0)
                    {
                        info.Group_subjectionName = "无";
                        info.Group_subjectionNames = "顶级";
                    }


                    tr = string.Format(tr,
                        info.Group_id.ToString(),
                        info.Group_name,
                        info.Group_subjectionName,
                        info.Group_subjectionNames,
                        this.getGrouplevel(info.Group_level),
                        "",
                        this.caterDomain
                        );


                }
            }



            return string.Format(str, tr);


        }

    }



}