using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace ServiceBusTopicPublisher
{
   class Program
   {
      static async Task Main(string[] args)
      {
         var conn = "Endpoint=sb://chyaservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=unOAtl6Ccm9UJEL0tVDgJ0N8vzVdVsDvhfogj60Mfeo=";
         
         Console.WriteLine("Service Bus Topic publisher...");
         var client = new TopicClient(conn, "Topic1");

         Console.WriteLine("Type messages, one msg per line, Q to end input...");

         var msgList = new List<Message>();
         while (true)
         {
            var msg = Console.ReadLine();
            if (msg == "Q")
            {
               break;
            }

            msgList.Add(new Message(Encoding.UTF8.GetBytes(msg)){
                              SessionId = Guid.NewGuid().ToString()
                           }
               );

         }

         await client.SendAsync(msgList);

         Console.WriteLine($"Messages sent, count {msgList.Count} ...");
         Console.ReadKey();

         await client.CloseAsync();

      }
   }
}
