using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using WebUI;
using Models;
public partial class GroupEditor : WebUI.GroupPage
{
    public BLL.GroupBLL groupbll = new GroupBLL();
    

    protected void Page_Load(object sender, EventArgs e)
    {

    }




    public string Groupselect(int selected)
    {

        string str = "<div class=\"col-md-3\">";
        str += "<div class=\"form-group\">";
        str += "<label>选择上级部门</label>";
        str += "<select class=\"form-control selectsubjecttion\" data-id=\"0\">";
        str += "<option value=0>请选择部门</option>";

        foreach (Models.GroupInfo info in groupbll.GetList(0))
        {
            str += "<option value=\""+info.Group_id.ToString()+"\">"+info.Group_name+"</option>";
        }

        str += "</select>";
        str += "</div>";
        str += "</div>";
        return str;

    }





}