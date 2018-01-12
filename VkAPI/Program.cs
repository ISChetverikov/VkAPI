using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Enums;
using VkNet.Model.RequestParams;
using VkNet.Model;

namespace VkApiTest
{
    class Program
    {
        const ulong APP_ID = 6329143L;
        const string LOGIN = "79143400153";
        const string PASSWORD = "Ironman.2209";
        const ulong ID = 363056866L;

        static void Main(string[] args)
        {
            var vk = new VkApi();
            try
            {
                vk.Authorize(new ApiAuthParams()
                {
                    Login = LOGIN,
                    Password = PASSWORD,
                    ApplicationId = APP_ID,
                    Settings = Settings.Messages
                });
            }
            catch (Exception e)
            {
                Console.WriteLine("Authorization has been failed!");
                Console.WriteLine("Error{0}", e.ToString());
                return;
            }

            Console.WriteLine("Authorization passed successfully...");

            var friendsGetParam = new FriendsGetParams();
            var friends = vk.Friends.Get(friendsGetParam);

            var msgGetParams = new MessagesGetHistoryParams();
            msgGetParams.UserId = friends.First().Id;
            var msgHistory = vk.Messages.GetHistory(msgGetParams);

            foreach (Message msg in msgHistory.Messages)
            {
                var shift = (msg.Type == MessageType.Received) ? "" : "\t\t";
                Console.WriteLine($"{shift}{msg.Body}");
            }
            
        }
    }
}
