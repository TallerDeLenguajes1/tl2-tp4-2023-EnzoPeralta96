using System.Text.Json;
using WebAPI;

public static class AccesoADatos
{
    public static bool ExisteArchivo(string rutaArchivo)
    {
        if (File.Exists(rutaArchivo))
        {
            var info = new FileInfo(rutaArchivo);

            if (info.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}

public class AccesoADatosCadeteria
{
    public Cadeteria Obtener()
    {
        Cadeteria cadeteria = null;
        if (AccesoADatos.ExisteArchivo("DatosJson/cadeteria.json"))
        {
            string TextoJson = File.ReadAllText("DatosJson/cadeteria.json");
            cadeteria = JsonSerializer.Deserialize<Cadeteria>(TextoJson);
        }
        return cadeteria;
    }
}



public class AccesoADatosCadetes
{
    private string datosCadetes = "DatosJson/cadetes.json";
    public List<Cadete> Obtener()
    {
        var cadetes = new List<Cadete>();

        if (AccesoADatos.ExisteArchivo(datosCadetes))
        {
            string TextoJson = File.ReadAllText(datosCadetes);
            cadetes = JsonSerializer.Deserialize<List<Cadete>>(TextoJson);
        }
        return cadetes;
    }

}
public class AccesoADatosPedidos
{
    private string datosPedidos = "DatosJson/pedidos.json";
    public List<Pedido> Obtener()
    {
        var pedidos = new List<Pedido>();
        if (AccesoADatos.ExisteArchivo(datosPedidos))
        {
            string TextoJson = File.ReadAllText(datosPedidos);
            pedidos = JsonSerializer.Deserialize<List<Pedido>>(TextoJson);
        }
        return pedidos;
    }

    public void Guardar(List<Pedido> Pedidos)
    {
        string formatoJson = JsonSerializer.Serialize(Pedidos);
        File.WriteAllText(datosPedidos, formatoJson);
    }

}
