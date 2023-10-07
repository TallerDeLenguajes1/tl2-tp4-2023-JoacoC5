using System.Text.Json;
using System.IO;
using EspacioCadete;
namespace EspacioPedido;


public enum Estado
{
    Pendiente,
    Cancelado,
    EnCamino,
    Entregado,
}
public class Pedido
{
    private int nro;
    private string obs;
    private Estado est;
    private Cliente cliente;
    private Cadete cadete;


    public int Nro { get => nro; set => nro = value; }
    public string Obs { get => obs; set => obs = value; }
    public Cliente Cliente { get => cliente; set => cliente = value; }
    public Estado Est { get => est; set => est = value; }
    public Cadete Cadete { get => cadete; set => cadete = value; }

    public Pedido()
    {

    }
    public Pedido(string obs, Cliente cliente)
    {
        this.obs = obs;
        this.est = Estado.Pendiente;
        this.cliente = cliente;
    }



    public void VerDireccionCliente(Cliente cliente)
    {
        Console.WriteLine("Direccion cliente: " + cliente.Direccion);
    }

    public void VerDatosCliente(Cliente cliente)
    {
        Console.WriteLine("Referencia de direccion: " + cliente.ReferenciaDireccion);
    }


}

