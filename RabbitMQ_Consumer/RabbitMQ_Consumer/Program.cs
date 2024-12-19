
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
channel.ExchangeDeclare(exchange: "topic-example", type: ExchangeType.Topic);

// 2.Adım Kuyruk Oluşturma
var queueName = channel.QueueDeclare().QueueName;

// 3.Adım Kullanıcıdan Topic Değeri Alma
Console.Write("Dinlenecek Topic formatını belirtin :");
string topicKey = Console.ReadLine();

// 4.Adım Bind İşlemini Yapma
channel.QueueBind(queue: queueName, exchange: "topic-example", routingKey: topicKey);

// Queue'dan Mesaj Okuma
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();
