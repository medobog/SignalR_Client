using Newtonsoft.Json;
using System;
using Microsoft.AspNetCore.SignalR.Client;
using Location.Infrastructure;

namespace SignalR_ClientConsole
{
    class Program
    {
        private static void Main(string[] args)
        {
            var Connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:44343/Informacije")
                .Build();

            Connection.StartAsync().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}",
                                      task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine("Connected");
                }

            }).Wait();

            Connection.On<string>("ReciveInfo", obj => {

                var deserializedObj = JsonConvert.DeserializeObject<InfoModel>(obj);
                Console.WriteLine(deserializedObj.ToString());
            });



            Console.Read();
            Connection.StopAsync().Wait();
        }
    }
}