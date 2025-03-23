using Microsoft.Data.SqlClient;

namespace Patterns.Samples.Outbox;

public class OrderService
{
    private const string ConnectionString = "YourDatabaseConnectionString";

    public void PlaceOrder(Order order)
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

                    // 2. Crear un mensaje para el servicio de inventario
                    var inventoryMessage = new OutboxMessage
                    {
                        Id = Guid.NewGuid(),
                        Content = $"UpdateInventory: {order.Product}, {order.Quantity}",
                        Destination = "inventory-queue",
                        Status = "Pending"
                    };
                    SaveOutboxMessage(inventoryMessage, connection, transaction);

                    // 3. Crear un mensaje para el servicio de envíos
                    var shippingMessage = new OutboxMessage
                    {
                        Id = Guid.NewGuid(),
                        Content = $"ScheduleDelivery: {order.Id}",
                        Destination = "shipping-queue",
                        Status = "Pending"
                    };
                    SaveOutboxMessage(shippingMessage, connection, transaction);

                    // 4. Confirmar la transacción
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // 5. En caso de error, deshacer la transacción
                    transaction.Rollback();
                    Console.WriteLine($"Error placing order: {ex.Message}");
                }
            }
        }
    }

    private void SaveOrder(Order order, SqlConnection connection, SqlTransaction transaction)
    {
        var command = new SqlCommand("INSERT INTO Orders (Id, Product, Quantity, CreatedAt) VALUES (@Id, @Product, @Quantity, @CreatedAt)", connection, transaction);
        command.Parameters.AddWithValue("@Id", order.Id);
        command.Parameters.AddWithValue("@Product", order.Product);
        command.Parameters.AddWithValue("@Quantity", order.Quantity);
        command.Parameters.AddWithValue("@CreatedAt", order.CreatedAt);
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