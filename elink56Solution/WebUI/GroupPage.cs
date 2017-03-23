using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL;
using Models;

namespace WebUI
{
    public class GroupPage : officeBasePages
    {


        #region action
        public void Action()
        {
            string _action = request.RequestAction();

            if (!string.IsNullOrEmpty(_action))
            {
                ProcessRequest();

                switch (request.RequestAction())
                {
                    case "editorData":
                        editorData();
                        return;
                }
            }
        }
        #endregion


        #region Reqeust

        #region subjectionId
        public string RequestSubjectionId
        {
            get
            {
                return request.RequestCheckInt("subjectionId");
            }

        }

        public string RequestGroupId
        {
            get
            {
                return request.RequestCheckInt("GroupId");
            }

        }
        #endregion


        public GroupInfo requestGroupInfo
        {
            get
            {
                GroupInfo _info = new GroupInfo();
                _info.Group_name = request.RequestPOST("Group_name");
                _info.Group_recommend = request.RequestPOST("Group_recommend");

                if (string.IsNullOrEmpty(_info.Group_name))
                {
                    System.Web.HttpContext.Current.Response.Write(JsonHelper.restfulJSON("-408", "对不起，请输入部门名称！", "group_name"));
                    System.Web.HttpContext.Current.Response.End();
                    return null;
                }

                return _info;


            }

        }



        #endregion







       



        private void editorData()
        {






            System.Web.HttpContext.Current.Response.End();
            return;

        }



        #region getGrouplevel
        public string getGrouplevel(int Group_level)
        {
            Group_level = Group_level + 1;
            return String.Format("{0}级", Group_level.ToString());
        }
        #endregion


    }
}
