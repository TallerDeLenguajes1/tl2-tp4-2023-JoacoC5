using System.Text.Json;
using System.IO;
using Empresa;

namespace EspacioAccesoADatos;

public abstract class AccesoADatos
{
    public virtual List<Cadete> LeerArchivoCadetes()
    {
        return null;
    }

    public virtual List<Cadeteria> LeerArchivoCadeteria()
    {
        return null;
    }
}

public class AccesoCSV : AccesoADatos
{
    public override List<Cadete> LeerArchivoCadetes()
    {
        List<Cadete> listadoCadetes = new List<Cadete>();

        string archivoCadetes = @"C:\Repos\TALLER2\tl2-tp1-2023-JoacoC5\Cadetes.csv";
        StreamReader strCadetes = new StreamReader(archivoCadetes);
        string linea;
        int i = 0;

        while ((linea = strCadetes.ReadLine()) != null)
        {
            string[] fila = linea.Split(",").ToArray();

            if (i > 0)
            {
                Cadete cadeteAgregar = new(Convert.ToInt32(fila[0]), fila[1], fila[2], fila[3]);
                listadoCadetes.Add(cadeteAgregar);
            }

            i++;
        }

        return listadoCadetes;
    }

    public override List<Cadeteria> LeerArchivoCadeteria()
    {
        List<Cadeteria> listadoCadeterias = new List<Cadeteria>();

        string archivoCadeterias = @"C:\Repos\TALLER2\tl2-tp1-2023-JoacoC5\Cadeterias.csv";
        StreamReader strCadeterias = new StreamReader(archivoCadeterias);
        string linea;
        int i = 0;

        while ((linea = strCadeterias.ReadLine()) != null)
        {
            string[] fila = linea.Split(",").ToArray();

            if (i >= 0)
            {
                Cadeteria cadeteriaAgregar = new(fila[0], fila[1]);
                listadoCadeterias.Add(cadeteriaAgregar);
            }

            i++;
        }

        return listadoCadeterias;
    }
}

public class AccesoJSON : AccesoADatos
{
    public override List<Cadete> LeerArchivoCadetes()
    {
        string archivoCadetes = "Cadetes.json";
        if (File.Exists(archivoCadetes))
        {
            string? jsonstring = File.ReadAllText(archivoCadetes);
            List<Cadete>? listadoCadetes = JsonSerializer.Deserialize<List<Cadete>>(jsonstring);
            return listadoCadetes;
        }
        else
        {
            return null;
        }
    }

    public override List<Cadeteria> LeerArchivoCadeteria()
    {
        string archivoCadeteria = "Cadeteria.json";
        if (File.Exists(archivoCadeteria))
        {
            string? jsonstring = File.ReadAllText(archivoCadeteria);
            List<Cadeteria>? listadoCadeterias = JsonSerializer.Deserialize<List<Cadeteria>>(jsonstring);
            return listadoCadeterias;
        }
        else
        {
            return null;
        }
    }
}