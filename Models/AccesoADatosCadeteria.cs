using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;
using Empresa;

namespace EspacioAccesoCadeteria;


public class AccesoADatosCadeteria
{
    public Cadeteria Obtener()
    {
        if (File.Exists("Cadeteria.json"))
        {
            string jsonstring = File.ReadAllText("Cadeteria.json");
            List<Cadeteria> cadeterias = JsonSerializer.Deserialize<List<Cadeteria>>(jsonstring);
            Random ran = new Random();
            return cadeterias[ran.Next(0, cadeterias.Count())];
        }
        else
        {
            return null;
        }



    }
}