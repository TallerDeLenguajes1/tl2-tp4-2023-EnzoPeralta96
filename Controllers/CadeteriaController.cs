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
    public ActionResult<string> GetNombreCadeteria()
    {
        return Ok(cadeteria.Nombre);
    }

    [HttpGet]
    [Route("Pedidos")]
    public ActionResult<List<Pedido>> GetPedidos()
    {
        var pedidos = cadeteria.GetPedidos();
        return Ok(pedidos);
    }

    [HttpGet]
    [Route("Cadetes")]
    public ActionResult<List<Cadete>> GetCadetes()
    {
        var cadetes = cadeteria.GetCadetes();
        return Ok(cadetes);
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

    [HttpPut("AsignarCadete")]
    public ActionResult<Pedido> AsignarPedido(int idPedido, int idCadete)
    {
        var pedido = cadeteria.AsignarCadetePedido(idPedido,idCadete);
        return Ok(pedido);
    }
    
    [HttpPut("ActualizarEstadoPedido")]
    public ActionResult<Pedido> CambiarEstadoPedido(int idPedido, int NuevoEstado)
    {
        var pedido = cadeteria.CambiarEstadoPedido(idPedido,NuevoEstado);
        return Ok(pedido);
    }

    [HttpPut("CambiarCadetePedido")]
    public ActionResult<Pedido> CambiarCadetePedido(int idPedido, int idCadete)
    {
        var pedido = cadeteria.CambiarCadetePedido(idPedido,idCadete);
        return Ok(pedido);
    }
}
