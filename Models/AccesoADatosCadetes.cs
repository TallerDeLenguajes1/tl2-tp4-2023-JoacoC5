using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;
using EspacioCadete;

public class AccesoADatosCadetes
{
    public List<Cadete> Obtener()
    {
        List<Cadete> cadetes = new List<Cadete>();
        string jsonstring = File.ReadAllText("Cadetes.json");
        cadetes = JsonSerializer.Deserialize<List<Cadete>>(jsonstring);

        return cadetes;
    }
}