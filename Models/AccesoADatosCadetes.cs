using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;
using EspacioCadete;

namespace EspacioAccesoCadetes;

public class AccesoADatosCadetes
{
    public List<Cadete> Obtener()
    {
        if (File.Exists("Cadetes.json"))
        {
            string jsonstring = File.ReadAllText("Cadetes.json");
            List<Cadete> cadetes = JsonSerializer.Deserialize<List<Cadete>>(jsonstring);
            return cadetes;
        }
        else
        {
            return null;
        }



    }
}