using System.Text.Json;
using WebAPI;

public class InformeCadete
{
   
    int idCadete;
    string nombreCadete;
    private int cantidadPedidosRecibidos;
    private int cantidadPedidosEntregados;
    private int promedioPedidosEntregados;
    private double montoGanado;

    public int IdCadete { get => idCadete; set => idCadete = value; }
    public string NombreCadete { get => nombreCadete; set => nombreCadete = value; }
    public int CantidadPedidosRecibidos { get => cantidadPedidosRecibidos; set => cantidadPedidosRecibidos = value; }
    public int CantidadPedidosEntregados { get => cantidadPedidosEntregados; set => cantidadPedidosEntregados = value; }
    public int PromedioPedidosEntregados { get => promedioPedidosEntregados; set => promedioPedidosEntregados = value; }
    public double MontoGanado { get => montoGanado; set => montoGanado = value; }
}

public class Informe
{
    List<InformeCadete> informes;

    public Informe() => informes = new List<InformeCadete>();

    
    private List<InformeCadete> CargarInforme(Cadeteria cadeteria)
    {
        
        foreach (var pedido in cadeteria.GetPedidos())
        {
            var InformeIndividual = new InformeCadete();
            InformeIndividual.IdCadete = pedido.Cadete.Id;
            InformeIndividual.NombreCadete = pedido.Cadete.Nombre;
            InformeIndividual.CantidadPedidosRecibidos = cadeteria.CantidadPedidosAsignados(InformeIndividual.IdCadete);
            InformeIndividual.CantidadPedidosEntregados = cadeteria.CantidadPedidosEntregados(InformeIndividual.IdCadete);
            if (InformeIndividual.CantidadPedidosRecibidos > 0)
            {
                InformeIndividual.PromedioPedidosEntregados = InformeIndividual.CantidadPedidosEntregados/InformeIndividual.CantidadPedidosRecibidos;
            }else
            {
                InformeIndividual.CantidadPedidosRecibidos = 0;
            }
            InformeIndividual.MontoGanado = cadeteria.JornalACobrar(InformeIndividual.IdCadete);

            informes.Add(InformeIndividual);
        }
        return informes;    
    }
    public string GenerarInforme(ref Cadeteria cadeteria)
    {
        var informes = CargarInforme(cadeteria);
        return JsonSerializer.Serialize(informes);
    }
}