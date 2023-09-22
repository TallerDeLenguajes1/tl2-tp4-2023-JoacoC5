using Empresa;
using EspacioPedido;
using Microsoft.AspNetCore.Mvc;

namespace tl2_tp4_2023_JoacoC5.Controllers;

[ApiController]
[Route("[controller]")]
public class CadeteriaController : ControllerBase
{
    private Cadeteria cadeteria;

    [HttpGet(Name = "GetPedidos")]
    public ActionResult<List<Pedido>> GetPedidos()
    {
        var listadoPedidos = cadeteria.ListaPedidos;
        if (listadoPedidos != null)
        {
            return Ok(listadoPedidos);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet(Name = "GetCadetes")]
    public ActionResult<List<Cadete>> GetCadetes()
    {
        var listadoCadetes = cadeteria.ListaCadetes;
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
    public ActionResult AsignarPedido(int idPedido, int idCadete)
    {
        cadeteria.AsignarPedido(idPedido, idCadete);

        return Ok();
    }

    [HttpPut("CambiarEstado")]
    public ActionResult<Estado> CambiarEstadoPedido(int idPedido, int nuevoEstado)
    {
        if (cadeteria.ListaPedidos.Exists(x => x.Nro == idPedido))
        {
            cadeteria.CambiarEstadoPedido(idPedido, nuevoEstado);
            return Ok(cadeteria.ListaPedidos[idPedido].Est);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPut("CambiarCadete")]
    public ActionResult<Cadete> CambiarCadetePedido(int idPedido, int idNuevoCadete)
    {
        if (cadeteria.ListaPedidos.Exists(x => x.Nro == idPedido))
        {
            if (cadeteria.ListaCadetes.Exists(x => x.Id == idNuevoCadete))
            {
                cadeteria.ReasignarPedido(idPedido, idNuevoCadete);
                return Ok(cadeteria.ListaCadetes[idNuevoCadete]);
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
