

using RabbitMQ.Client;
using System.Text;
using System.Threading.Channels;

//Bağlantı Oluşturma
ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://ozxsyaja:p-mAAUXc-eGa5m7NcvEn4r9dqktDIIl-@kebnekaise.lmq.cloudamqp.com/ozxsyaja");

//Bağlantıyı Aktifleştirme ve Kanal Açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Exchange Oluşturma
channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

//Mesaj Gönderme
while (true)
{
    Console.Write("Mesaj Girin :");
    var message = Console.ReadLine();
    byte[] bytesMessage = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(exchange: "direct-exchange-example", routingKey: "direct-key", body: bytesMessage);
}

Console.Read();