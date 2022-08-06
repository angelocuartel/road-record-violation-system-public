using RoadRecordViolationSystem.Utils.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoadRecordViolationSystem.Utils.Implementation
{
    public static class ItextMoUtil
    {

        public static void SendMessage<T>(string number,MessageTemplate<T> messageTemplate)
        {
            object functionReturnValue = null;

            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                System.Collections.Specialized.NameValueCollection parameter = new System.Collections.Specialized.NameValueCollection();
                string url = "https://www.itexmo.com/php_api/api.php";
                parameter.Add("1", number);
                parameter.Add("2", messageTemplate.GetMessage());
                parameter.Add("3", "YOUR API KEY HERE");
                parameter.Add("passwd", "YOUR PASSWORD HERE");
                dynamic rpb = client.UploadValues(url, "POST", parameter);
                functionReturnValue = (new System.Text.UTF8Encoding()).GetString(rpb);
            }

        }



    }
}


