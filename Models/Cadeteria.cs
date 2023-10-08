using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using EspacioCadete;
using EspacioPedido;
using EspacioAccesoADatos;
using EspacioAccesoCadeteria;
using EspacioAccesoCadetes;
using EspacioAccesoPedido;

namespace Empresa;

public class Cadeteria
{
    [JsonPropertyName("nombre")]
    private string nombre;
    [JsonPropertyName("telefono")]
    private string telefono;
    [JsonPropertyName("listaCadetes")]
    private List<Cadete> listaCadetes;
    [JsonPropertyName("listaPedidos")]
    private List<Pedido> listaPedidos;
    private AccesoADatosCadetes accesoCadetes;
    private AccesoADatosPedidos accesoPedidos;
    /*private static Cadeteria? instance;
    public static Cadeteria GetInstance()
    {
        if (instance == null)
        {
            instance = new Cadeteria();
            var accesoCadeteria = new AccesoADatosCadeteria();

            instance = accesoCadeteria.Obtener();
            /*instance.accesoCadetes = new AccesoADatosCadetes();
            instance.accesoPedidos = new AccesoADatosPedidos();

            instance.CargarPedidos();
            instance.CargarCadetes();

        }
        return instance;
    }*/


    [JsonPropertyName("nombre")]
    public string Nombre { get => nombre; set => nombre = value; }
    [JsonPropertyName("telefono")]
    public string Telefono { get => telefono; set => telefono = value; }

    public Cadeteria(AccesoADatosCadeteria accesoCadeteria, AccesoADatosCadetes accesoCadetes, AccesoADatosPedidos accesoPedidos)
    {
        Cadeteria aux = accesoCadeteria.Obtener();
        this.nombre = aux.nombre;
        this.telefono = aux.telefono;

        accesoCadetes = new AccesoADatosCadetes();
        listaCadetes = accesoCadetes.Obtener();

        accesoPedidos = new AccesoADatosPedidos();
        listaPedidos = accesoPedidos.Obtener();
    }


    public Cadeteria(string nombre, string telefono)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        listaCadetes = new List<Cadete>();
        listaPedidos = new List<Pedido>();
    }


    public Cadeteria()
    {
        listaCadetes = new List<Cadete>();
        listaPedidos = new List<Pedido>();
    }

    public string DevolverCadeteria()
    {
        return nombre;
    }

    public List<Pedido> DevolverPedidos()
    {
        return this.listaPedidos;
    }

    public List<Cadete> DevolverCadetes()
    {
        return this.listaCadetes;
    }

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

    public void AltaPedido(string obser, string nomCLi, string telCLi, string dirCli, string datosDir)
    {
        Pedido auxPedido = new(obser, CargarCliente(nomCLi, telCLi, dirCli, datosDir));
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


