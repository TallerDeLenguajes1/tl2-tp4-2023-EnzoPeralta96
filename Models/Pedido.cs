namespace WebAPI;

public enum EstadoPedido
{
    Ingresado,
    Entregado, 
    EnCamino,
    Cancelado
}
public class Pedido
{
    private int nroPedido;
    private string observacionPedido;

    private EstadoPedido estado;
   // private Cliente cliente;
    private Cadete cadete;

    public int NroPedido { get => nroPedido; set => nroPedido = value;}
    public string ObservacionPedido { get => observacionPedido; set => observacionPedido = value;}
    
    public EstadoPedido Estado { get => estado; set => estado = value; }
    public Cadete Cadete { get => cadete; set => cadete = value; }
    
    public Pedido()
    {

    }

    public Pedido(int nroPedido, string observacionPedido, int estado)
    {
        NroPedido = nroPedido;
        ObservacionPedido = observacionPedido;
        Estado =(EstadoPedido)estado;
    }

    


}