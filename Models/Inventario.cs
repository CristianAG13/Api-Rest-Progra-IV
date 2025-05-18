namespace WebApiProyect.Models
{
    public class Inventario
    {
        public int Id { get; set; }
        public required int Nombre { get; set; }
        public required int Cantidad { get; set; }
        public required int Precio { get; set; }
    }
}
