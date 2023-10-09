using System.Text.Json.Serialization;
using System.IO;

public class Cliente
{
    private string nombre;
    private string direccion;
    private string telefono;
    private string referenciaDireccion;

    [JsonPropertyName("nombre")]
    public string Nombre { get => nombre; }
    [JsonPropertyName("direccion")]
    public string Direccion { get => direccion; }
    [JsonPropertyName("telefono")]
    public string Telefono { get => telefono; }
    [JsonPropertyName("referenciaDireccion")]
    public string ReferenciaDireccion { get => referenciaDireccion; }

    public Cliente(string nombre, string direccion, string telefono, string referenciaDireccion)
    {
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        this.referenciaDireccion = referenciaDireccion;
    }
}