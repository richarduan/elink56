using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUI
{
    public class GroupPage : officeBasePages
    {


        #region Reqeust

        #region subjectionId
        public string RequestSubjectionId
        {
            get
            {
                return request.RequestCheckInt("subjectionId");
            }

        }
        #endregion

        #endregion


        #region getGrouplevel
        public string getGrouplevel(int Group_level)
        {
            Group_level = Group_level + 1;
            return String.Format("{0}级", Group_level.ToString());
        }
        #endregion


    }
}
