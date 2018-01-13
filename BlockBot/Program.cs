using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using VkNet;
using VkNet.Enums.Filters;
using VkNet.Enums;
using VkNet.Model.RequestParams;
using VkNet.Model;

namespace BlockBot
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
                    Settings = Settings.All
                });
            }
            catch (Exception e)
            {
                Console.WriteLine("Authorization has been failed!");
                Console.WriteLine("Error{0}", e.ToString());
                return;
            }

            Console.WriteLine("Authorization passed successfully...");
            Console.WriteLine("====================================");
            Console.WriteLine("Friends:");

            var friendsGetParam = new FriendsGetParams();
            friendsGetParam.Fields = ProfileFields.Nickname;
            var friends = vk.Friends.Get(friendsGetParam);
            foreach (User friend in friends)
            {
                Console.WriteLine("\t{0} {1}", friend.FirstName, friend.LastName);
            }
            Console.WriteLine("====================================");
            
            int count = 500;
            while (count-- > 0)
            {
                var messagesGetHistoryParams = new MessagesGetHistoryParams();
                foreach (var friend in friends)
                {
                    messagesGetHistoryParams.UserId = friend.Id;
                    var messages = vk.Messages.GetHistory(messagesGetHistoryParams).Messages;

                    foreach (var message in messages)
                    {
                        if(message.Body == "Lock")
                        {
                            ComputerOperations.Lock();
                        }
                    }
                    var ids = messages.Select(x => (ulong)x.Id);
                    if (ids.Any())
                        vk.Messages.DeleteDialog(friend.Id);
                }

                // TODO Индикатор работы программы
                Thread.Sleep(1000);
            }
        }
    }
}
