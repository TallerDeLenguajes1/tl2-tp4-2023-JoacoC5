using Empresa;
using EspacioCadete;
using EspacioPedido;
using Microsoft.AspNetCore.Mvc;
using EspacioAccesoCadeteria;
using EspacioAccesoCadetes;
using EspacioAccesoPedido;

namespace tl2_tp4_2023_JoacoC5.Controllers;

[ApiController]
[Route("[controller]")]
public class CadeteriaController : ControllerBase
{
    private Cadeteria cadeteria;
    private readonly ILogger<CadeteriaController> logger;

    public CadeteriaController(ILogger<CadeteriaController> logger)
    {
        this.logger = logger;
        AccesoADatosCadeteria accesoCadeteria = new AccesoADatosCadeteria();
        AccesoADatosCadetes accesoCadetes = new AccesoADatosCadetes();
        AccesoADatosPedidos accesoPedidos = new AccesoADatosPedidos();
        cadeteria = new Cadeteria(accesoCadeteria, accesoCadetes, accesoPedidos);
    }

    [HttpGet("GetCadeteria")]
    public ActionResult<string> GetNombreCadeteria()
    {
        return Ok(cadeteria.DevolverCadeteria());
    }

    [HttpGet("GetPedidos")]
    public ActionResult<List<Pedido>> GetPedidos()
    {
        List<Pedido> listadoPedidos = cadeteria.DevolverPedidos();
        if (listadoPedidos != null)
        {
            return Ok(listadoPedidos);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("GetCadetes")]
    public ActionResult<List<Cadete>> GetCadetes()
    {
        List<Cadete> listadoCadetes = cadeteria.DevolverCadetes();
        if (listadoCadetes != null)
        {
            return Ok(listadoCadetes);
        }
        else
        {
            return NotFound();
        }
    }

    //[HttpGet(Name = "GetInforme")]

    [HttpPost("AddPedido")]
    public ActionResult<Pedido> AgregarPedido(Pedido pedido)
    {
        cadeteria.AgregarPedido(pedido);

        return Ok(pedido);
    }

    [HttpPut("AsigPed")]
    public ActionResult<string> AsignarPedido(int idPedido, int idCadete)
    {
        cadeteria.AsignarPedido(idPedido, idCadete);

        return Ok("El pedido " + idPedido + " se asigno al cadete " + idCadete);
    }

    [HttpPut("CambiarEstado")]
    public ActionResult<string> CambiarEstadoPedido(int idPedido, int nuevoEstado)
    {
        if (cadeteria.DevolverPedidos().Exists(x => x.Nro == idPedido))
        {
            cadeteria.CambiarEstadoPedido(idPedido, nuevoEstado);
            if (nuevoEstado == 0)
            {
                return Ok("El pedido fue cancelado");
            }
            else
            {
                if (nuevoEstado == 1)
                {
                    return Ok("El pedido esta en camino");
                }
                else
                {
                    if (nuevoEstado == 2)
                    {
                        return Ok("El pedido fue entregado");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPut("CambiarCadete")]
    public ActionResult<Cadete> CambiarCadetePedido(int idPedido, int idNuevoCadete)
    {
        if (cadeteria.DevolverPedidos().Exists(x => x.Nro == idPedido))
        {
            if (cadeteria.DevolverCadetes().Exists(x => x.Id == idNuevoCadete))
            {
                cadeteria.ReasignarPedido(idPedido, idNuevoCadete);
                return Ok(cadeteria.DevolverPedidos()[idNuevoCadete]);
            }
            else
            {
                return NotFound();
            }
        }
        else
        {
            return NotFound();
        }
    }

}
