using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;
using EspacioPedido;

namespace EspacioAccesoPedido;

public class AccesoADatosPedidos
{
    public List<Pedido> Obtener()
    {
        if (File.Exists("Pedidos.json"))
        {
            string jsonstring = File.ReadAllText("Pedidos.json");
            List<Pedido> pedidos = JsonSerializer.Deserialize<List<Pedido>>(jsonstring);

            return pedidos;
        }
        else
        {
            return null;
        }

    }

    public void Guardar(List<Pedido> pedidos)
    {
        string pedidosJson = JsonSerializer.Serialize(pedidos);
        File.WriteAllText("pedidos.json", pedidosJson);

    }
}