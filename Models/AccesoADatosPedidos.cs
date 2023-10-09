using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;
using EspacioPedido;

namespace EspacioAccesoPedido;

public class AccesoADatosPedidos
{
    public List<Pedido> Obtener()
    {
        string nomArchivo = "Pedidos.json";
        string archivo;
        List<Pedido> pedidos = new List<Pedido>();
        using (var archivoOpen = new FileStream(nomArchivo, FileMode.Open))
        {
            using (var strReader = new StreamReader(archivoOpen))
            {
                archivo = strReader.ReadToEnd();
                archivoOpen.Close();
            }
        }

        pedidos = JsonSerializer.Deserialize<List<Pedido>>(archivo);

        return pedidos;

    }

    public void Guardar(List<Pedido> pedidos)
    {
        string nomArchivo = "Pedidos.json";
        if (!File.Exists(nomArchivo))
        {
            File.Create(nomArchivo).Close();
        }

        string pedidosJson = JsonSerializer.Serialize(pedidos);
        File.WriteAllText("pedidos.json", pedidosJson);

    }
}