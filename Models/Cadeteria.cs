using System.Security.AccessControl;

namespace WebAPI;

public class Cadeteria
{
    private const int PRECIO_ENVIO = 500;
    private string _nombre;
    private double _telefono;
    private AccesoADatosPedidos accesoADatosPedidos;
    private AccesoADatosCadetes accesoADatosCadetes;
    private static Cadeteria _cadeteria;

    public string Nombre { get => _nombre; set => _nombre = value; }
    public double Telefono { get => _telefono; set => _telefono = value; }
    public static Cadeteria GetCadeteria()
    {
        if (_cadeteria == null)
        {
            var AccesoADatosCadeteria = new AccesoADatosCadeteria();
            _cadeteria = AccesoADatosCadeteria.Obtener();
            _cadeteria.accesoADatosCadetes = new AccesoADatosCadetes();
            _cadeteria.accesoADatosPedidos = new AccesoADatosPedidos();
        }
        return _cadeteria;
    }
   
    public Cadeteria()
    {
    }
   
    public List<Pedido> GetPedidos()
    {   
        return accesoADatosPedidos.Obtener();
    }

    public List<Cadete> GetCadetes()
    {
        return accesoADatosCadetes.Obtener();
    }

    public Pedido AgregarPedido(Pedido nuevoPedido)
    {
        var pedidos = accesoADatosPedidos.Obtener();
        pedidos.Add(nuevoPedido);
        nuevoPedido.NroPedido = pedidos.Count();
        accesoADatosPedidos.Guardar(pedidos);
        return nuevoPedido;
    }

    public Pedido AsignarCadetePedido(int NroPedido, int IdCadete)
    {
        var pedidos = accesoADatosPedidos.Obtener();
        var cadete = accesoADatosCadetes.Obtener().FirstOrDefault(cadete => cadete.Id == IdCadete);
        int indexPedido = pedidos.FindIndex(pedido => pedido.NroPedido == NroPedido);
        if (indexPedido != -1 && cadete != null)
        {
            pedidos[indexPedido].Cadete = cadete;
            accesoADatosPedidos.Guardar(pedidos);
        }
        return pedidos[indexPedido];
    }


    public Pedido CambiarEstadoPedido(int NroPedido, int NuevoEstado)
    {
        var pedidos = accesoADatosPedidos.Obtener();
        int indexPedido = pedidos.FindIndex(pedido => pedido.NroPedido == NroPedido);
        if (indexPedido != -1)
        {
            pedidos[indexPedido].Estado = (EstadoPedido)NuevoEstado;
            accesoADatosPedidos.Guardar(pedidos);
        }
        return pedidos[indexPedido];
    }

    public Pedido CambiarCadetePedido(int NroPedido, int idNuevoCadete)
    {
        var pedidos = accesoADatosPedidos.Obtener();
        var nuevoCadete = accesoADatosCadetes.Obtener().FirstOrDefault(cadete => cadete.Id == idNuevoCadete);
        var indexPedido = pedidos.FindLastIndex(pedido => pedido.NroPedido == NroPedido);
        if (indexPedido != -1 && nuevoCadete != null)
        {
            pedidos[indexPedido].Cadete = nuevoCadete;
            accesoADatosPedidos.Guardar(pedidos);
        }
        return pedidos[indexPedido];
    }

    public int CantidadPedidosAsignados(int idCadete)
    {
        return accesoADatosPedidos.Obtener().Count(pedido => pedido.Cadete.Id == idCadete);
    }


    public double JornalACobrar(int idCadete)
    {
        return CantidadPedidosEntregados(idCadete) * PRECIO_ENVIO;
    }

    public int CantidadPedidosEntregados(int idCadete)
    {
        return accesoADatosPedidos.Obtener().Count(pedido => 
            pedido.Cadete.Id == idCadete 
                        && 
            pedido.Estado == EstadoPedido.Entregado);
    }

}