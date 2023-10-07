using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;
using EspacioPedido;

public class AccesoADatosPedidos
{
    public List<Pedido> Obtener()
    {
        List<Pedido> pedidos = new List<Pedido>();
        string jsonstring = File.ReadAllText("Pedidos.json");
        pedidos = JsonSerializer.Deserialize<List<Pedido>>(jsonstring);

        return pedidos;
    }

    public void Guardar(List<Pedido> pedidos)
    {
        string pedidosJson = JsonSerializer.Serialize(pedidos);
        File.WriteAllText("pedidos.json", pedidosJson);

    }
}