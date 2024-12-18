

using RabbitMQ.Client;
using System.Text;
using System.Threading.Channels;

//Bağlantı Oluşturma
ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://ozxsyaja:p-mAAUXc-eGa5m7NcvEn4r9dqktDIIl-@kebnekaise.lmq.cloudamqp.com/ozxsyaja");

//Bağlantıyı Aktifleştirme ve Kanal Açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Queue Oluşturma
channel.QueueDeclare(queue: "example 1", exclusive: false);

//Queue'ya Mesaj Gönderme
byte[] message = Encoding.UTF8.GetBytes("Message 4");
channel.BasicPublish(exchange: "", routingKey: "example 1", body: message);

Console.ReadKey();