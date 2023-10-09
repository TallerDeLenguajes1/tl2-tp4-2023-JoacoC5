using System.Text.Json;
using System.IO;
using EspacioPedido;
using System.Text.Json.Serialization;

namespace EspacioCadete;

public class Cadete
{
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;

    [JsonPropertyName("id")]
    public int Id { get => id; set => id = value; }
    [JsonPropertyName("nombre")]
    public string Nombre { get => nombre; set => nombre = value; }
    [JsonPropertyName("direccion")]
    public string Direccion { get => direccion; set => direccion = value; }
    [JsonPropertyName("telefono")]
    public string Telefono { get => telefono; set => telefono = value; }

    public Cadete(int id, string nombre, string direccion, string telefono)
    {
        this.Id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
    }

    public Cadete()
    {

    }





}