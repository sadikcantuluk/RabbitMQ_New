
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

// Bağlantı Oluşturma
ConnectionFactory factory = new();
factory.Uri = new("amqps://ozxsyaja:p-mAAUXc-eGa5m7NcvEn4r9dqktDIIl-@kebnekaise.lmq.cloudamqp.com/ozxsyaja");

// Bağlantı Aktifleştirme ve Kanal Açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// Queue Oluşturma
channel.QueueDeclare(queue: "example 1", exclusive: false, autoDelete: false, durable: true); // Consumer'da da kuyruk publisher'daki ile birebir aynı yapılandırmada tanımlanmalıdır.

// Queue'dan Mesaj Okuma
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example 1", autoAck: false, consumer);
consumer.Received += (sender, e) =>
{
    // Kuyruğa gelen mesajın işlendiği yerdir!
    // e.Body : Kuyruktaki mesajın verisini bütünsel olarak getirecektir.
    // e.Body.Span veya e.Body.ToArray() : Kuyruktaki mesajın byte verisini getirecektir.
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
    channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
};

Console.Read();
