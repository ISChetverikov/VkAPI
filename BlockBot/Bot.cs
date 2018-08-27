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
using VkNet.Exception;

namespace BlockBot
{
    class Bot
    {
        const ulong APP_ID = 6329143L;
        const string LOGIN = "";
        const string PASSWORD = "";
        const ulong ID = 363056866L;

        Action<string> _Log;
        VkApi _vk;

        public Bot(Action<string> Log)
        {
            _Log = Log;
            _vk = new VkApi();
        }

        public bool TryInitialize()
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
            catch (VkApiException e)
            {
                _Log.Invoke("Authorization has failed!");
                _Log.Invoke($"Error: {e.ToString()}");
                return false;
            }

            _Log.Invoke("Authorization passed successfully...");
            _Log.Invoke("====================================");
            

            return true;
        }

        public void Run()
        {
            var friendsGetParam = new FriendsGetParams();
            friendsGetParam.Fields = ProfileFields.Nickname;
            var friends = _vk.Friends.Get(friendsGetParam);
            
            var messagesSendParams = new MessagesSendParams();
            messagesSendParams.Message = "Bot is ready to serve to you!";
            _Log.Invoke("Friends:");
            foreach (User friend in friends)
            {
                _Log.Invoke($"\t{friend.FirstName} {friend.LastName}");

                messagesSendParams.UserId = friend.Id;
                _vk.Messages.Send(messagesSendParams);
                _vk.Messages.DeleteDialog(friend.Id);
            }
            _Log.Invoke("====================================");

            MainLoop();
            
            messagesSendParams.Message = "Bot completed its work...";
            friends = _vk.Friends.Get(friendsGetParam);
            foreach (User friend in friends)
            {
                messagesSendParams.UserId = friend.Id;
                _vk.Messages.Send(messagesSendParams);
                _vk.Messages.DeleteDialog(friend.Id);
            }
            _Log.Invoke("====================================");
        }

        private void MainLoop()
        {
            while (true)
            {
                var breakFlag = false;

                var friendsGetParam = new FriendsGetParams();
                friendsGetParam.Fields = ProfileFields.Nickname;
                var friends = _vk.Friends.Get(friendsGetParam);

                var messagesGetHistoryParams = new MessagesGetHistoryParams();
                foreach (User friend in friends)
                {
                    messagesGetHistoryParams.UserId = friend.Id;
                    var messages = _vk.Messages.GetHistory(messagesGetHistoryParams).Messages;

                    foreach (Message message in messages)
                    {
                        _Log($"Request from {friend.FirstName} {friend.LastName}: {message.Body}");
                        if (ProcessMessage(message.Body))
                            breakFlag = true;
                    }

                    var ids = messages.Select(x => (long)x.Id);
                    if (ids.Any())
                    {
                        _vk.Messages.MarkAsRead(ids, null);
                    }
                    _vk.Messages.DeleteDialog(friend.Id);
                }

                if (breakFlag)
                    break;

                Thread.Sleep(1000);
            }
        }

        private bool ProcessMessage(string msg)
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
                case "Start":
                    Computer.Player.Start();
                    break;
                case "Quit":
                    Computer.Player.Quit();
                    break;
                case "Close":
                    result = true;
                    break;
                default:
                    _Log.Invoke($"\tUnknown command: \"{msg}\"");
                    break;
            }

            return result;
        }
    }
}
