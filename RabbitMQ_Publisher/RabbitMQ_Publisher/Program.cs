

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
channel.ExchangeDeclare(exchange: "header-example", type: ExchangeType.Headers);


//Mesaj Gönderme
for (int i = 0; i < 100; i++)
{
    byte[] bytesMessage = Encoding.UTF8.GetBytes("message " + i);

    Console.Write("Mesaj için value değerini girin :");
    string value = Console.ReadLine();

    IBasicProperties basicProperties = channel.CreateBasicProperties();
    basicProperties.Headers = new Dictionary<string, object>
    {
        ["exampleKey"] = value
    };

    channel.BasicPublish(exchange: "header-example", routingKey: string.Empty, body: bytesMessage, basicProperties: basicProperties);
}

Console.Read();