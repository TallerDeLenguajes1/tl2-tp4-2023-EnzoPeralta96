namespace WebAPI;

public class Cliente
{
    private string nombre;
    private string direccion;

    private long telefono;

    private string datosReferenciaDireccion;

    public Cliente(string nombre, string direccion, long telefono, string datosReferenciaDireccion)
    {
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        this.datosReferenciaDireccion = datosReferenciaDireccion;
    }

    public string Nombre { get => nombre; }
    public string Direccion { get => direccion; }
    public long Telefono { get => telefono; }
    public string DatosReferenciaDireccion { get => datosReferenciaDireccion; }

}