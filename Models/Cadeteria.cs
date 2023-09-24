namespace WebAPI;

public class Cadeteria
{
    private const int PRECIO_ENVIO = 500;
    private static Cadeteria cadeteria;

    public static Cadeteria GetCadeteria()
    {
        if (cadeteria == null)
        {
            cadeteria = new Cadeteria();
        }
        return cadeteria;
    }

    private string _nombre;
    private double _telefono;
    private List<Cadete> _cadetes;

    private List<Pedido> _pedidos;

    public string Nombre { get => _nombre; }
    public double Telefono { get => _telefono; }
    public List<Pedido> Pedidos { get => _pedidos;}

    /*public List<Cadete> Cadetes { get => _cadetes;}
public List<Pedido> Pedidos { get => _pedidos;}*/

    public Cadeteria()
    {
        _nombre = "FlashCadeteria";
        _telefono = 3813110011;
        _cadetes = new List<Cadete>();
        _pedidos = new List<Pedido>();

        _cadetes.Add(new Cadete(1, "Sam Winchester", "dakota del norte", 123456));
        _cadetes.Add(new Cadete(2, "Dean Winchester", "dakota del norte", 321654));
        _cadetes.Add(new Cadete(3, "Jhon Winchester", "Dakota del norte", 789456));

        _pedidos.Add(new Pedido(1, "este es pedido 1", 1));
        _pedidos.Add(new Pedido(2, "este es pedido 2", 1));
        _pedidos.Add(new Pedido(3, "este es pedido 3", 1));
        _pedidos.Add(new Pedido(4, "este es pedido 4", 1));
        _pedidos.Add(new Pedido(5, "este es pedido 5", 1));
        _pedidos.Add(new Pedido(6, "este es pedido 6", 1));
        _pedidos.Add(new Pedido(7, "este es pedido 7", 1));
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
        return nuevoPedido;
    }

    public Pedido AsignarCadetePedido(int NroPedido, int IdCadete)
    {
        var pedido = _pedidos.FirstOrDefault(pedido => pedido.NroPedido == NroPedido);
        var cadete = _cadetes.FirstOrDefault(cadete => cadete.Id == IdCadete);
        if (pedido != null && cadete != null)
        {
            pedido.Cadete = cadete;
        }
        return pedido;
    }

    public Pedido CambiarEstadoPedido(int idPedido, int NuevoEstado)
    {

        var pedido = _pedidos.FirstOrDefault(pedido => pedido.NroPedido == idPedido);
        if (pedido != null)
        {
            pedido.Estado = (EstadoPedido)NuevoEstado;
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
        }
        return pedido;
    }

    public int CantidadPedidosAsignados(int idCadete)
    {
        int cantidadPedidosAsignados = 0;
        foreach (var pedido in _pedidos)
        {
            if (pedido.Cadete.Id == idCadete)
            {
                cantidadPedidosAsignados++;
            }
        }
        return cantidadPedidosAsignados;
    }
    public int CantidadPedidosEntregados()
    {
        int cantEntregados = 0;
        foreach (var pedido in _pedidos)
        {
            if (pedido.Estado == EstadoPedido.Entregado)
            {
                cantEntregados++;
            }
        }
        return cantEntregados;
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
            if (pedido.Estado==EstadoPedido.Entregado && pedido.Cadete.Id == idCadete)
            {
                cantPedidos++;
            }
        }
        return cantPedidos;
    }




}