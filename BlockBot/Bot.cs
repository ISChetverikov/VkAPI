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
    class Bot
    {
        const ulong APP_ID = 6329143L;
        const string LOGIN = "79143400153";
        const string PASSWORD = "Ironman.2209";
        const ulong ID = 363056866L;

        Action<string> _Log;
        VkApi _vk;

        public Bot(Action<string> Log)
        {
            _Log = Log;
            _vk = new VkApi();
        }

        public void Initialize()
        {
            try
            {
                _vk.Authorize(new ApiAuthParams()
                {
                    Login = LOGIN,
                    Password = PASSWORD,
                    ApplicationId = APP_ID,
                    Settings = Settings.All
                });
            }
            catch (Exception e)
            {
                _Log.Invoke("Authorization has been failed!");
                _Log.Invoke($"Error {e.ToString()}");
                return;
            }

            _Log.Invoke("Authorization passed successfully...");
            _Log.Invoke("====================================");
            _Log.Invoke("Friends:");

            return;
        }

        public void Run()
        {
            var friendsGetParam = new FriendsGetParams();
            friendsGetParam.Fields = ProfileFields.Nickname;
            var friends = _vk.Friends.Get(friendsGetParam);
            foreach (User friend in friends)
            {
                _Log.Invoke($"\t{friend.FirstName} {friend.LastName}");
            }
            _Log.Invoke("====================================");
            
            while (true)
            {
                var breakFlag = false;

                var messagesGetHistoryParams = new MessagesGetHistoryParams();
                foreach (var friend in friends)
                {
                    messagesGetHistoryParams.UserId = friend.Id;
                    var messages = _vk.Messages.GetHistory(messagesGetHistoryParams).Messages;

                    foreach (var message in messages)
                    {
                        if (ProcessMessage(message.Body))
                            breakFlag = true;
                    }

                    var ids = messages.Select(x => (long)x.Id);
                    if (ids.Any())
                    {
                        _vk.Messages.MarkAsRead(ids, null);
                        _vk.Messages.DeleteDialog(friend.Id);
                    }
                }

                if (breakFlag)
                    break;
                // TODO Индикатор работы программы
                Thread.Sleep(1000);
            }

        }

        public bool ProcessMessage(string msg)
        {
            bool result = false;
            switch (msg)
            {
                case "Lock":
                    Computer.Lock();
                    break;
                case "Pause":
                    Computer.Player.Pause();
                    break;
                case "Close":
                    result = true;
                    _Log.Invoke("Принята команда завершения программы.");
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
