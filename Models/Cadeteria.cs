using System.Security.AccessControl;

namespace WebAPI;

public class Cadeteria
{
    private const int PRECIO_ENVIO = 500;
    private string _nombre;
    private double _telefono;
    private List<Cadete> _cadetes;
    private List<Pedido> _pedidos ;
    private AccesoADatosPedidos accesoADatosPedidos;
    
    public string Nombre { get => _nombre; set => _nombre = value; }
    public double Telefono { get => _telefono; set => _telefono = value; }
    public List<Cadete> Cadetes { get => _cadetes; set => _cadetes = value; }
    public List<Pedido> Pedidos { get => _pedidos; set => _pedidos = value; }
    public AccesoADatosPedidos AccesoADatosPedidos { get => accesoADatosPedidos; set => accesoADatosPedidos = value; }
  
    public Cadeteria()
    {
        _cadetes = new List<Cadete>();
        _pedidos = new List<Pedido>();
    }
    public Cadeteria(string nombre, double tel)
    {
        Nombre = nombre;
        Telefono = tel;
        Pedidos = new List<Pedido>();
        Cadetes = new List<Cadete>();
    }

    public List<Pedido> GetPedidos()
    {
        return _pedidos;
    }

    public List<Cadete> GetCadetes()
    {
        return _cadetes;
    }

    public Pedido AgregarPedido(Pedido nuevoPedido)
    {
        _pedidos.Add(nuevoPedido);
        nuevoPedido.NroPedido = _pedidos.Count();
        AccesoADatosPedidos.Guardar(_pedidos);
        return nuevoPedido;
    }

    public Pedido AsignarCadetePedido(int NroPedido, int IdCadete)
    {
        var pedido = _pedidos.FirstOrDefault(pedido => pedido.NroPedido == NroPedido);
        var cadete = _cadetes.FirstOrDefault(cadete => cadete.Id == IdCadete);
        if (pedido != null && cadete != null)
        {
            pedido.Cadete = cadete;
            AccesoADatosPedidos.Guardar(_pedidos);
        }

        return pedido;
    }

    public Pedido CambiarEstadoPedido(int idPedido, int NuevoEstado)
    {

        var pedido = _pedidos.FirstOrDefault(pedido => pedido.NroPedido == idPedido);
        if (pedido != null)
        {
            pedido.Estado = (EstadoPedido)NuevoEstado;
            AccesoADatosPedidos.Guardar(_pedidos);
        }
        return pedido;
    }

    public Pedido CambiarCadetePedido(int idPedido, int idNuevoCadete)
    {
        var pedido = _pedidos.FirstOrDefault(pedido => pedido.NroPedido == idPedido);
        var nuevoCadete = _cadetes.FirstOrDefault(cadete => cadete.Id == idNuevoCadete);
        if (pedido != null && nuevoCadete != null)
        {
            pedido.Cadete = nuevoCadete;
            AccesoADatosPedidos.Guardar(_pedidos);
        }
        return pedido;
    }

    public int CantidadPedidosAsignados(int idCadete)
    {
        int cantidadPedidosAsignados = 0;
        foreach (var pedido in _pedidos)
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
        return CantidadPedidosEntregados(idCadete)*PRECIO_ENVIO;
    }

    public int CantidadPedidosEntregados(int idCadete)
    {
        int cantPedidos = 0;

        foreach (var pedido in _pedidos)
        {
            if (pedido.Cadete != null && pedido.Cadete.Id == idCadete && pedido.Estado==EstadoPedido.Entregado )
            {
                cantPedidos++;
            }
        }
        return cantPedidos;
    }
}