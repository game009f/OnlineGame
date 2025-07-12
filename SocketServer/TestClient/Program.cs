using Common.Network;
using Common.Proto.Request;
using Common.Proto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string user, pwd;

            Console.WriteLine("输入用户名！");
            user = Console.ReadLine();
            Console.WriteLine("输入用户名密码！");
            pwd = Console.ReadLine();


            Client client = new Client("127.0.0.1", 8888);
            client.MessageHandler += (msg) =>
            {
                BaseResponse rm = msg as BaseResponse;
                Console.WriteLine("返回结果ResponseCode:{0} Content:{1}", rm.ResponseCode, rm.Content);
                //switch (msg.ActionCode)
                //{
                //    case Common.ActionCode.Register:
                //        ResponeMessage rm = msg as ResponeMessage;
                //        Console.WriteLine("返回结果ResponeCode:{0} Content:{1}", rm.ResponeCode, rm.Content);
                //        break;
                //}
            };
            client.Disconnect += () =>
            {
                Console.WriteLine("连接断开，尝试重新连接");
                //do
                //{
                //    client.TryConnect();
                //} while (!client.Connected);
                //Console.WriteLine("重新连接成功");
                //client.Start();
                //Thread.Sleep(2000);
                //client.Send(new UserLoginRequest() { UserName = user, Password = pwd });
            };
            client.ExceptionHandle += (e) =>
            {
                Console.WriteLine(e.ToString());
            };
            client.Start();

            client.Send(new UserRegisterRequest() { UserName = user, Password = pwd });
            Thread.Sleep(2000);
            client.Send(new UserLoginRequest() { UserName = user, Password = pwd });

            while (true)
            {
                if (!client.Connected)
                {
                    break;
                }
                else
                {
                    try
                    {
                    }
                    catch
                    { }
                }
                Thread.Sleep(2000);
            }

            Console.ReadKey();
        }
    }
}
