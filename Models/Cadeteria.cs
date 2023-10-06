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
    public Cadeteria()
    {
    }
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

    public List<Pedido> GetPedidos()
    {
        return accesoADatosPedidos.Obtener();
    }

    public Pedido GetPedidoXId(int NroPedido)
    {
        return accesoADatosPedidos.Obtener().FirstOrDefault(pedido => pedido.NroPedido == NroPedido);
    }

    public Pedido AgregarPedido(Pedido nuevoPedido)
    {
        var pedidos = accesoADatosPedidos.Obtener();
        pedidos.Add(nuevoPedido);
        nuevoPedido.NroPedido = pedidos.Count();
        accesoADatosPedidos.Guardar(pedidos);
        return nuevoPedido;
    }

    public void GuardarPedidos(List<Pedido> pedidos)
    {
        accesoADatosPedidos.Guardar(pedidos);
    }

    public Pedido AsignarCadetePedido(int NroPedido, int IdCadete) // Tambien sirve para reasignar
    {
        var pedidos = accesoADatosPedidos.Obtener();
        int indexPedido = pedidos.FindIndex(pedido => pedido.NroPedido == NroPedido);
        var cadete = accesoADatosCadetes.Obtener().FirstOrDefault(cadete => cadete.Id == IdCadete);

        pedidos[indexPedido].Cadete = cadete;
        accesoADatosPedidos.Guardar(pedidos);

        return pedidos[indexPedido];
    }
    public Pedido CambiarEstadoPedido(int NroPedido, EstadoPedido NuevoEstado)
    {
        var pedidos = accesoADatosPedidos.Obtener();
        int indexPedido = pedidos.FindIndex(pedido => pedido.NroPedido == NroPedido);

        pedidos[indexPedido].Estado = NuevoEstado;
        accesoADatosPedidos.Guardar(pedidos);
        
        return pedidos[indexPedido];
    }


    public List<Cadete> GetCadetes()
    {
        return accesoADatosCadetes.Obtener();
    }
    public Cadete GetCadeteXId(int IdCadete)
    {
        return accesoADatosCadetes.Obtener().FirstOrDefault(cadete => cadete.Id == IdCadete);
    }

    public Cadete AgregarCadete(Cadete cadete)
    {
        var cadetes = accesoADatosCadetes.Obtener();
        cadetes.Add(cadete);
        cadete.Id = cadetes.Count();
        accesoADatosCadetes.Guardar(cadetes);
        return cadete;
    }

    public int CantidadPedidosAsignados(int idCadete)
    {
        return accesoADatosPedidos.Obtener().Count(pedido => pedido.Cadete.Id == idCadete);
    }
    public int CantidadPedidosEntregados(int idCadete)
    {
        return accesoADatosPedidos.Obtener().Count(pedido =>
            pedido.Cadete.Id == idCadete
                        &&
            pedido.Estado == EstadoPedido.Entregado);
    }
    public double JornalACobrar(int idCadete)
    {
        return CantidadPedidosEntregados(idCadete) * PRECIO_ENVIO;
    }

}