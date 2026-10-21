namespace PetCare
{
    public abstract class ServicioVeterinario : Facturable
    {
        private readonly string codigo;
        private readonly string nombreMascota;
        private readonly DateTime fecha;

        public string Codigo => codigo;
        public string NombreMascota => nombreMascota;
        public DateTime Fecha => fecha;

        protected ServicioVeterinario(string codigo, string nombreMascota, DateTime fecha)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                throw new ArgumentException("El codigo del servicio es obligatorio");
            }
            if (string.IsNullOrWhiteSpace(nombreMascota))
            {
                throw new ArgumentException("El nombre de la mascota es obligatorio");
            }

            this.codigo = codigo.Trim();
            this.nombreMascota = nombreMascota.Trim();
            this.fecha = fecha;
        }

        public abstract decimal CalcularImporte();

        public abstract string ObtenerDescripcion();
    }
}
