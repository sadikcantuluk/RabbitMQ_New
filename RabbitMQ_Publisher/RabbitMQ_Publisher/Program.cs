

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
channel.ExchangeDeclare(exchange: "topic-example", type: ExchangeType.Topic);

//Mesaj Gönderme
for (int i = 0; i < 100; i++)
{
    byte[] bytesMessage = Encoding.UTF8.GetBytes("message " + i);
    Console.Write("Mesajın gönderileceği topic formatını girin :");
    string topicKey = Console.ReadLine();
    channel.BasicPublish(exchange: "topic-example", routingKey: topicKey, body: bytesMessage);
}

Console.Read();