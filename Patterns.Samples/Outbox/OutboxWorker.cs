using Microsoft.Data.SqlClient;

namespace Patterns.Samples.Outbox;

public class OutboxWorker
{
    private const string ConnectionString = "YourDatabaseConnectionString";

    public void ProcessOutboxMessages()
    {
        while (true)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var messages = GetPendingMessages(connection);

                foreach (var message in messages)
                {
                    try
                    {
                        // Enviar el mensaje a la cola
                        SendMessageToQueue(message);

                        // Marcar el mensaje como enviado
                        MarkMessageAsSent(message.Id, connection);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error sending message: {ex.Message}");
                    }
                }
            }

            // Esperar antes de la siguiente iteración
            System.Threading.Thread.Sleep(5000);
        }
    }

    private List<OutboxMessage> GetPendingMessages(SqlConnection connection)
    {
        var messages = new List<OutboxMessage>();
        var command = new SqlCommand("SELECT Id, Content, Destination FROM Outbox WHERE Status = 'Pending'", connection);
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                messages.Add(new OutboxMessage
                {
                    Id = (Guid)reader["Id"],
                    Content = (string)reader["Content"],
                    Destination = (string)reader["Destination"]
                });
            }
        }
        return messages;
    }

    private void SendMessageToQueue(OutboxMessage message)
    {
        // Simular el envío a una cola de mensajes
        Console.WriteLine($"Sending message to {message.Destination}: {message.Content}");
    }

    private void MarkMessageAsSent(Guid messageId, SqlConnection connection)
    {
        var command = new SqlCommand("UPDATE Outbox SET Status = 'Sent' WHERE Id = @Id", connection);
        command.Parameters.AddWithValue("@Id", messageId);
        command.ExecuteNonQuery();
    }
}