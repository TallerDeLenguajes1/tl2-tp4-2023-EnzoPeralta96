using Microsoft.AspNetCore.Mvc;
using WebAPI;
namespace tl2_tp4_2023_EnzoPeralta96.Controllers;

[ApiController]
[Route("[controller]")]
public class CadeteriaController : ControllerBase
{

    private readonly Cadeteria cadeteria;
    private readonly ILogger<CadeteriaController> _logger;


    public CadeteriaController(ILogger<CadeteriaController> logger)
    {
        _logger = logger;
        cadeteria = Cadeteria.GetCadeteria();
    }

    [HttpGet]
    [Route("Pedidos")]
    public ActionResult<List<Pedido>> GetPedidos()
    {
        var pedidos = cadeteria.GetPedidos();
        return Ok(pedidos);
    }

    [HttpGet("BuscarPedido")]
    public ActionResult<Pedido> GetPedidoXId(int NroPedido)
    {
        var pedido = cadeteria.GetPedidoXId(NroPedido);
        if (pedido == null)
        {
            return NotFound("Pedido no encontrado");
        }
        return Ok(pedido);
    }

    [HttpGet]
    [Route("Cadetes")]
    public ActionResult<List<Cadete>> GetCadetes()
    {
        var cadetes = cadeteria.GetCadetes();
        return Ok(cadetes);
    }

    [HttpGet("BuscarCadete")]
    public ActionResult<Cadete> GetCadeteXId(int idCadete)
    {
        var cadete = cadeteria.GetCadeteXId(idCadete);
        if (cadete == null)
        {
            return NotFound("Cadete no encontrado");
        }
        return Ok(cadete);
    }


    [HttpGet("InformeJornada")]
    public ActionResult<string> GetInforme()
    {
        var informe = new Informe();
        string informeJson = informe.GenerarInformeJson(cadeteria);
        return Ok(informeJson);
    }

    [HttpPost("AgregarPedido")]
    public ActionResult<Pedido> AgregarPedido(Pedido pedido)
    {
        var nuevoPedido = cadeteria.AgregarPedido(pedido);
        return Ok(nuevoPedido);
    }

    [HttpPost("AgregarCadete")]
    public ActionResult<Cadete> AgregarCadete(Cadete cadete)
    {
        var nuevoCadete = cadeteria.AgregarCadete(cadete);
        return Ok(nuevoCadete);
    }

    [HttpPut("AsignarCadete")]
    public ActionResult<Pedido> AsignarPedido(int idPedido, int idCadete)
    {
        var pedidos = cadeteria.GetPedidos();
        int indexPedido = pedidos.FindIndex(pedido => pedido.NroPedido == idPedido);
        if (indexPedido == -1)
        {
            return NotFound("Pedido no encontrado");
        }
        var cadete = cadeteria.GetCadeteXId(idCadete);
        if (cadete == null)
        {
            return NotFound("Cadete no encontrado");
        }
        pedidos[indexPedido].Cadete = cadete;
        cadeteria.GuardarPedidos(pedidos);
        return NoContent();
    }

    [HttpPut("ActualizarEstadoPedido")]
    public ActionResult<Pedido> CambiarEstadoPedido(int idPedido, int NuevoEstado)
    {
        if (NuevoEstado < 0 && NuevoEstado > 3)
        {
            return NotFound("Estado invalido");
        }
        var pedido = cadeteria.CambiarEstadoPedido(idPedido, NuevoEstado);
        if (pedido == null)
        {
            return NotFound("Pedido no encontrado");
        }
        return NoContent(); // indica operacion exitosa, no muestra el objeto.
    }

    /*[HttpPut("CambiarCadetePedido")]
    public ActionResult<Pedido> CambiarCadetePedido(int idPedido, int idCadete)
    {
        var pedido = cadeteria.CambiarCadetePedido(idPedido, idCadete);
        if (pedido == null)
        {
            return NotFound("Pedido no encontrado");
        }
        return NoContent(); // indica operacion exitosa, no muestra el objeto.
    } 
    ESTE METODO HACE LO MISMO QUE REASIGNAR - DUPLICACION DE CODIGO*/
}
