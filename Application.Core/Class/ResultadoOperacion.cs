namespace Application.Core.Class
{
    public class ResultadoOperacion<T>
    {
        public bool Exito { get; set; }
        public T Datos { get; set; }
        public string MensajeError { get; set; }
    }
}
