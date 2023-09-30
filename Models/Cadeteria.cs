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
        var cadetes = accesoADatosCadetes.Obtener();
        var pedido = pedidos.FirstOrDefault(pedido => pedido.NroPedido == NroPedido);
        var cadete = cadetes.FirstOrDefault(cadete => cadete.Id == IdCadete);
        if (pedido != null && cadete != null)
        {
            pedido.Cadete = cadete;
            pedidos.Add(pedido);
            accesoADatosPedidos.Guardar(pedidos);
        }
        return pedido;
    }

    public Pedido CambiarEstadoPedido(int idPedido, int NuevoEstado)
    {
        var pedidos = accesoADatosPedidos.Obtener();
        var pedido = pedidos.FirstOrDefault(pedido => pedido.NroPedido == idPedido);
        if (pedido != null)
        {
            pedido.Estado = (EstadoPedido)NuevoEstado;
            pedidos.Add(pedido);
            accesoADatosPedidos.Guardar(pedidos);
        }
        return pedido;
    }

    public Pedido CambiarCadetePedido(int idPedido, int idNuevoCadete)
    {
        var pedidos = accesoADatosPedidos.Obtener();
        var cadetes = accesoADatosCadetes.Obtener();
        var pedido = pedidos.FirstOrDefault(pedido => pedido.NroPedido == idPedido);
        var nuevoCadete = cadetes.FirstOrDefault(cadete => cadete.Id == idNuevoCadete);
        if (pedido != null && nuevoCadete != null)
        {
            pedido.Cadete = nuevoCadete;
            accesoADatosPedidos.Guardar(pedidos);
        }
        return pedido;
    }

    public int CantidadPedidosAsignados(int idCadete)
    {
        int cantidadPedidosAsignados = 0;
        foreach (var pedido in accesoADatosPedidos.Obtener())
        {
            if (pedido.Cadete != null && pedido.Cadete.Id == idCadete)
            {
                cantidadPedidosAsignados++;
            }
        }
        return cantidadPedidosAsignados;
    }


    public double JornalACobrar(int idCadete)
    {
        return CantidadPedidosEntregados(idCadete) * PRECIO_ENVIO;
    }

    public int CantidadPedidosEntregados(int idCadete)
    {
        int cantPedidos = 0;

        foreach (var pedido in accesoADatosPedidos.Obtener())
        {
            if (pedido.Cadete != null && pedido.Cadete.Id == idCadete && pedido.Estado == EstadoPedido.Entregado)
            {
                cantPedidos++;
            }
        }
        return cantPedidos;
    }
}