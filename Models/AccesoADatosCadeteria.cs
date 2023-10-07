using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;
using Empresa;


public class AccesoADatosCadeteria
{
    public Cadeteria Obtener()
    {
        List<Cadeteria> cadeterias = new List<Cadeteria>();
        string jsonstring = File.ReadAllText("Cadeteria.json");
        cadeterias = JsonSerializer.Deserialize<List<Cadeteria>>(jsonstring);

        Random ran = new Random();
        return cadeterias[ran.Next(0, cadeterias.Count())]; // si presenta fallas establecer en 5 que es nro actual de cadeterias en la lista
    }
}