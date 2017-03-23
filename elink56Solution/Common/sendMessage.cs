using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public class sendMessage
    {

        public static string postMessage(string content, string mobile)
        {
            sendMessage sendmessage = new sendMessage();
            return sendmessage.postmessage(content, mobile);
        }


        public string postmessage(string content, string mobile)
        {

            string url = sendMessageConfig[0];

            string paramData=(sendMessageConfig[1]);
            paramData = paramData.Replace("#", "&");
            paramData = string.Format(paramData, mobile, content);


            string status = HttpWebRequestHelper.PostWebRequest(url, Convert.ToString(paramData), Encoding.UTF8);
            return status;
        }




        private string[] sendMessageConfig
        {
            get
            {
                string[] list = null;
                string config = Utils.SetAppSettings("sendMessage");
                if (!string.IsNullOrEmpty(config)) list = config.Split(';');

                return list;
            }
        }



    }








