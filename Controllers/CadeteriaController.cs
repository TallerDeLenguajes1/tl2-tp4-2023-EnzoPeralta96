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
    public ActionResult<List<InformeCadete>> GetInforme()
    {
        var informe = new Informe();
        return Ok(informe.GetInforme(cadeteria));
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
        if (cadeteria.GetPedidoXId(idPedido) == null)
        {
            return NotFound("Pedido no encontrado");
        }

        if (cadeteria.GetCadeteXId(idCadete) == null)
        {
            return NotFound("Cadete no encontrado");
        }

        var pedido = cadeteria.AsignarCadetePedido(idPedido,idCadete);

        return Ok(pedido);
    }

    [HttpPut("ActualizarEstadoPedido")]
    public ActionResult<Pedido> CambiarEstadoPedido(int idPedido, EstadoPedido nuevoEstado)
    {
        if (cadeteria.GetPedidoXId(idPedido) == null)
        {
            return NotFound("Pedido no encontrado");
        }

        var pedido = cadeteria.CambiarEstadoPedido(idPedido, nuevoEstado);

        return Ok(pedido);
        
        //return NoContent(); indica operacion exitosa, no muestra el objeto.
    }

}
