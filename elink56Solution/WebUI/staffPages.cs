using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebUI
{

    public class staffPages : officeBasePages
    {


        #region request

        #region action
        public void Action()
        {

            string actionstr = request.RequestAction();
            if (!string.IsNullOrEmpty(actionstr))
            {
                ProcessRequest();
                switch (actionstr)
                {
                    case "userlogin":
                        userLogin();
                        return;
                }
            }
        }
        #endregion

        #endregion

        #region userLogin
        private void userLogin()
        {
            string userName = request.RequestPOST("userName");
            string userPasswd = request.RequestPOST("userPasswd");



            System.Web.HttpContext.Current.Response.Write(JsonHelper.restfulJSON("1", userName, userPasswd));
            System.Web.HttpContext.Current.Response.End();
            return;


        }
        #endregion



    }
}
