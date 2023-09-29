using System.Text.Json;
using System.IO;
using EspacioPedido;

namespace Empresa;

public class Cadeteria
{
    /*private string nombre;
    private string telefono;
    private List<Cadete> listaCadetes;
    private List<Pedido> listaPedidos;


    public string Nombre { get => nombre; set => nombre = value; }
    //Creo q tanto el atributo nombre como el telefono podrian ser unicamente con get
    public string Telefono { get => telefono; set => telefono = value; }
    public List<Pedido> ListaPedidos { get => listaPedidos; set => listaPedidos = value; }
    public List<Cadete> ListaCadetes { get => listaCadetes; set => listaCadetes = value; }*/

    private static Cadeteria cadeteria;
    /*private Cadeteria(string nombre, string telefono)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        listaCadetes = new List<Cadete>();
        //this.listaCadetes.AddRange(lista);
    }*/

    public static Cadeteria Instance()
    {
        if (cadeteria == null)
        {
            cadeteria = new Cadeteria();
        }
        return cadeteria;
    }

    private string nombre;
    private List<Cadete> listaCadetes;
    private List<Pedido> listaPedidos;
    public string Nombre { get => nombre; set => nombre = value; }

    public void AgregarCadete(Cadete cadete)
    {
        cadete.Id = listaCadetes.Count() + 1;
        listaCadetes.Add(cadete);
    }

    public void AsignarPedido(int nroP, int idC)
    {
        if (listaCadetes.Exists(x => x.Id == idC))
        {
            if (listaPedidos.Exists(x => x.Nro == nroP))
            {
                listaPedidos[nroP].Cadete = listaCadetes.Find(x => x.Id == idC);
            }
            //ESTO SUPONIENDO QUE EL NUMERO DEL PEDIDO COINCIDE CON SI INDICE DE LISTA
        }
    }

    public List<Pedido> DevolverPedidos()
    {
        return listaPedidos;
    }

    public List<Cadete> DevolverCadete()
    {
        return listaCadetes;
    }

    public void ReasignarPedido(int nroPedido, int idNuevoCadete)
    {
        if (listaPedidos.Exists(x => x.Nro == nroPedido))
        {
            if (listaCadetes.Exists(x => x.Id == idNuevoCadete))
            {
                listaPedidos[nroPedido].Cadete = listaCadetes[idNuevoCadete];
            }
        }

    }

    public void AltaPedido(string obser, Estado estado, string nomCLi, string telCLi, string dirCli, string datosDir)
    {
        Pedido auxPedido = new(obser, estado, CargarCliente(nomCLi, telCLi, dirCli, datosDir));
        auxPedido.Nro = listaPedidos.Count() + 1;
        listaPedidos.Add(auxPedido);
    }

    public void AgregarPedido(Pedido pedido)
    {
        pedido.Nro = listaPedidos.Count() + 1;
        listaPedidos.Add(pedido);
    }

    public Cliente CargarCliente(string nombre, string telefono, string direc, string datosDirec)
    {
        Cliente auxCliente = new(nombre, telefono, direc, datosDirec);
        return auxCliente;
    }

    public void CambiarEstadoPedido(int nroPedido, int cambio) // cambio = 0 cancelado, = 1 encamino, = 2 entregado
    {
        foreach (var pedido in listaPedidos)
        {
            if (pedido.Nro == nroPedido)
            {
                if (cambio == 0)
                {
                    pedido.Est = Estado.Cancelado;
                    listaPedidos.RemoveAt(pedido.Nro);
                }
                else
                {
                    if (cambio == 1)
                    {
                        pedido.Est = Estado.EnCamino;
                    }
                    else
                    {
                        if (cambio == 2)
                        {
                            pedido.Est = Estado.Entregado;
                        }
                    }
                }
                break;
            }
        }
    }

    public float JornalACobrar(int id) // CAMBIAR
    {
        float cont = 0;

        for (int i = 0; i < listaPedidos.Count(); i++)
        {
            if (listaPedidos[i].Cadete.Id == id)
            {
                if (listaPedidos[i].Est == Estado.Entregado)
                {
                    cont++;
                }
            }
        }

        return cont * 500;
    }

    public float Recaudacion()
    {
        float recaudacion = 0;

        for (int i = 0; i < listaCadetes.Count(); i++)
        {
            recaudacion += JornalACobrar(listaCadetes[i].Id);
        }

        return recaudacion;
    }

    /*public void MostrarInfo()
    {
        foreach (var item in listaCadetes)
        {
            float aux = item.JornalACobrar();
            Console.WriteLine(item.Nombre);
            Console.WriteLine("Ganacia del cadete: " + aux);

        }
    }

    public void EstadoPedido()
    {
        foreach (var item in listaCadetes)
        {
            Console.WriteLine("Nombre del cadete: " + item.Nombre);
            foreach (var cad in item.ListadoPedidos)
            {
                Console.WriteLine("Pedido nro: " + cad.Nro);
                Console.WriteLine("Estado: " + cad.Est);
            }
            Console.WriteLine("Jornal a cobrar por el cadete: " + item.JornalACobrar());
        }
    }*/

}


