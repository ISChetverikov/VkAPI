using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;

namespace VkApiTest
{
    class Program
    {
        const ulong APP_ID = 12345L;
        const string LOGIN = "79143400153";
        const string PASSWORD = "Ironman.2209";

        static void Main(string[] args)
        {
            var vk = new VkApi();
            try
            {
                vk.Authorize(new ApiAuthParams()
                {
                    Login = LOGIN,
                    Password = PASSWORD,
                    ApplicationId = APP_ID
                });
            }
            catch (Exception e)
            {
                Console.WriteLine("Authorization has been failed!");
                Console.WriteLine("Error{0}", e.ToString());
                return;
            }

            Console.WriteLine("Authorization passed successfully...");
        }
    }
}
