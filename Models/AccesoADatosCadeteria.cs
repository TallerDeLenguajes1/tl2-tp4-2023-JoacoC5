using System.Text.Json.Serialization;
using System.Text.Json;
using System.IO;
using Empresa;

namespace EspacioAccesoCadeteria;


public class AccesoADatosCadeteria
{
    public Cadeteria Obtener()
    {
        string nomArchivo = "Cadeteria.json";
        string archivo;
        using (var archivoOpen = new FileStream(nomArchivo, FileMode.Open))
        {
            using (var strReader = new StreamReader(archivoOpen))
            {
                archivo = strReader.ReadToEnd();
                archivoOpen.Close();
            }
        }

        List<Cadeteria> cadeterias = JsonSerializer.Deserialize<List<Cadeteria>>(archivo);
        Random ran = new Random();
        return cadeterias[ran.Next(0, cadeterias.Count())];

    }
}