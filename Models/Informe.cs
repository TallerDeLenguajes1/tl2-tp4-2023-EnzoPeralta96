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
    public int PromedioPedidosEntregados { get => promedioPedidosEntregados;
     set => promedioPedidosEntregados = value; }
    public double MontoGanado { get => montoGanado; set => montoGanado = value; }
}

public class Informe
{
    List<InformeCadete> informes;

    public Informe()
    {
        informes = new List<InformeCadete>();
    } 


    private List<InformeCadete> CargarInforme(Cadeteria cadeteria)
    {
        
        foreach (var cadete in cadeteria.GetCadetes())
        {
            var informe = new InformeCadete
            {
                IdCadete = cadete.Id,
                NombreCadete = cadete.Nombre,
                CantidadPedidosRecibidos = cadeteria.CantidadPedidosAsignados(cadete.Id),
                CantidadPedidosEntregados = cadeteria.CantidadPedidosEntregados(cadete.Id),
                MontoGanado = cadeteria.JornalACobrar(cadete.Id)
            };

            if (informe.CantidadPedidosRecibidos > 0)
            {
                informe.PromedioPedidosEntregados = informe.CantidadPedidosEntregados / informe.CantidadPedidosRecibidos; // encapsular
            }
            
            informes.Add(informe);
        }

        return informes;
    }
    public string GenerarInformeJson(Cadeteria cadeteria)//consultar si pasar por ref o no
    {
        var informes = CargarInforme(cadeteria);
        return JsonSerializer.Serialize(informes);
    }
}