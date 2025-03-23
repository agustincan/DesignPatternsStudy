using Microsoft.Data.SqlClient;

namespace Patterns.Samples.Outbox;

public class OutboxPatternExample
{
    private const string ConnectionString = "YourDatabaseConnectionString";

    public void ProcessOrder(Order order)
    {
        using (var connection = new SqlConnection(ConnectionString))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // 1. Guardar el pedido en la base de datos
                    SaveOrder(order, connection, transaction);

                    // 2. Guardar el mensaje en la tabla Outbox
                    var outboxMessage = new OutboxMessage
                    {
                        Id = Guid.NewGuid(),
                        Content = $"OrderCreated: {order.Id}",
                        Destination = "order-queue",
                        Status = "Pending"
                    };
                    SaveOutboxMessage(outboxMessage, connection, transaction);

                    // 3. Confirmar la transacción
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // 4. En caso de error, deshacer la transacción
                    transaction.Rollback();
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }

    private void SaveOrder(Order order, SqlConnection connection, SqlTransaction transaction)
    {
        var command = new SqlCommand("INSERT INTO Orders (Id, Product, Quantity) VALUES (@Id, @Product, @Quantity)", connection, transaction);
        command.Parameters.AddWithValue("@Id", order.Id);
        command.Parameters.AddWithValue("@Product", order.Product);
        command.Parameters.AddWithValue("@Quantity", order.Quantity);
        command.ExecuteNonQuery();
    }

    private void SaveOutboxMessage(OutboxMessage message, SqlConnection connection, SqlTransaction transaction)
    {
        var command = new SqlCommand("INSERT INTO Outbox (Id, Content, Destination, Status) VALUES (@Id, @Content, @Destination, @Status)", connection, transaction);
        command.Parameters.AddWithValue("@Id", message.Id);
        command.Parameters.AddWithValue("@Content", message.Content);
        command.Parameters.AddWithValue("@Destination", message.Destination);
        command.Parameters.AddWithValue("@Status", message.Status);
        command.ExecuteNonQuery();
    }
}