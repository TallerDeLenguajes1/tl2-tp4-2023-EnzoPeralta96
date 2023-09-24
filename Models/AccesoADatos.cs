using System.Text.Json;
using WebAPI;

public abstract class AccesoADatos
{
    protected bool ExisteArchivo(string rutaArchivo)
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
    
    public abstract Cadeteria CrearCadeteria(string rutaDatosCadeteria);
    public abstract List<Cadete> CargarCadetes(string rutaArchivo);


}
public class AccesoCSV : AccesoADatos
{    

    public override Cadeteria CrearCadeteria(string rutaDatosCadeteria)
    {
       Cadeteria cadeteria = null;

        if (ExisteArchivo(rutaDatosCadeteria))
        {
            string[] linea = File.ReadAllLines(rutaDatosCadeteria);
            string primeraLinea = linea[0];
            string[] datosCadeteria = primeraLinea.Split(',');
            string nombre = datosCadeteria[0];
            long telefono = long.Parse(datosCadeteria[1]);
            
            cadeteria = new Cadeteria(nombre,telefono);
        }

        return cadeteria; 
    }
    public override List<Cadete> CargarCadetes(string rutaArchivo)
    {
        List<Cadete> cadetes = null;
        if (ExisteArchivo(rutaArchivo))
        {
            using (var infoCadete = new StreamReader(rutaArchivo))
            {
                while (!infoCadete.EndOfStream)
                {
                    string linea = infoCadete.ReadLine();
                    string[] datosCadete = linea.Split(';');

                    int id = int.Parse(datosCadete[0]);
                    string nombre = datosCadete[1];
                    string direccion = datosCadete[2];
                    long telefono = long.Parse(datosCadete[3]);
                    cadetes.Add(new Cadete(id,nombre,direccion,telefono));     
                }
            }
        }
        return cadetes;
    }

   
}

public class AccesoJSON : AccesoADatos
{
    public override Cadeteria CrearCadeteria(string rutaArchivo)
    {
        Cadeteria cadeteria = null;
        if (ExisteArchivo(rutaArchivo))
        {
            string TextoJson = File.ReadAllText(rutaArchivo);
            cadeteria = JsonSerializer.Deserialize<Cadeteria>(TextoJson);
        }
        return cadeteria;
    }
    public override List<Cadete> CargarCadetes(string rutaArchivo)
    {
        var cadetes = new List<Cadete>();

        if(ExisteArchivo(rutaArchivo))
        {
            string TextoJson = File.ReadAllText(rutaArchivo);
            cadetes = JsonSerializer.Deserialize<List<Cadete>>(TextoJson); 
        }
        return cadetes;
    }

  




}