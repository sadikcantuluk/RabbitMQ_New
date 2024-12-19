
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

// Bağlantı Oluşturma
ConnectionFactory factory = new();
factory.Uri = new("amqps://ozxsyaja:p-mAAUXc-eGa5m7NcvEn4r9dqktDIIl-@kebnekaise.lmq.cloudamqp.com/ozxsyaja");

// Bağlantı Aktifleştirme ve Kanal Açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

// 1.Adım Exchange Oluşturma
channel.ExchangeDeclare(exchange: "fanout-example", type: ExchangeType.Fanout);

// 2.Adım Kuyruk Oluşturma
Console.Write("Kuyruk ismini giirn :");
var queueName = Console.ReadLine();
channel.QueueDeclare(queue: queueName, exclusive: false);

// 3.Adım Bind İşlemini Yapma
channel.QueueBind(queue: queueName, exchange: "fanout-example", routingKey: string.Empty);

// Queue'dan Mesaj Okuma
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer);
consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();
