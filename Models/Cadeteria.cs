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
    private string nombre;
    private string telefono;
    private List<Cadete> listaCadetes;
    private List<Pedido> listaPedidos;
    private AccesoADatosCadetes accesoCadetes;
    private AccesoADatosPedidos accesoPedidos;


    [JsonPropertyName("nombre")]
    public string Nombre { get => nombre; set => nombre = value; }
    [JsonPropertyName("telefono")]
    public string Telefono { get => telefono; set => telefono = value; }

    public Cadeteria(AccesoADatosCadeteria accesoCadeteria, AccesoADatosCadetes accesoCadetes, AccesoADatosPedidos accesoPedidos)
    {
        Cadeteria aux = accesoCadeteria.Obtener();
        this.nombre = aux.nombre;
        this.telefono = aux.telefono;

        this.accesoCadetes = accesoCadetes;
        this.accesoPedidos = accesoPedidos;

        listaCadetes = accesoCadetes.Obtener();
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
        return listaPedidos;
    }

    public List<Cadete> DevolverCadetes()
    {
        return listaCadetes;
    }

    public void AgregarCadete(Cadete nuevo)
    {
        nuevo.Id = listaCadetes.Count() + 1;
        listaCadetes.Add(nuevo);
        List<Cadete> cadetes = listaCadetes;

        accesoCadetes.Guardar(cadetes);
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

    public void AgregarPedido(Pedido nuevo)
    {
        nuevo.Nro = listaPedidos.Count() + 1;
        listaPedidos.Add(nuevo);
        List<Pedido> pedidos = listaPedidos;

        accesoPedidos.Guardar(pedidos);
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

    public Cadete DevolverUnCadete(int idBuscado)
    {
        Cadete buscado = new Cadete();
        if (listaCadetes.Exists(x => x.Id == idBuscado))
        {
            buscado = listaCadetes.FirstOrDefault(x => x.Id == idBuscado);
        }

        return buscado;
    }

    public Pedido DevolverUnPedido(int idBuscado)
    {
        Pedido buscado = new Pedido();
        if (listaPedidos.Exists(x => x.Nro == idBuscado))
        {
            buscado = listaPedidos.FirstOrDefault(x => x.Nro == idBuscado);
        }

        return buscado;
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


