using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;
using EspacioCadete;

namespace EspacioAccesoCadetes;

public class AccesoADatosCadetes
{
    public List<Cadete> Obtener()
    {
        string nomArchivo = "Cadetes.json";
        string archivo;
        List<Cadete> cadetes = new List<Cadete>();
        using (var archivoOpen = new FileStream(nomArchivo, FileMode.Open))
        {
            using (var strReader = new StreamReader(archivoOpen))
            {
                archivo = strReader.ReadToEnd();
                archivoOpen.Close();
            }
        }

        cadetes = JsonSerializer.Deserialize<List<Cadete>>(archivo);

        return cadetes;

    }

    public void Guardar(List<Cadete> cadetes)
    {
        string nomArchivo = "Cadetes.json";
        if (!File.Exists(nomArchivo))
        {
            File.Create(nomArchivo).Close();
        }

        string cadetesJson = JsonSerializer.Serialize(cadetes);
        File.WriteAllText(nomArchivo, cadetesJson);
    }
}