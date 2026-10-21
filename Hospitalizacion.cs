namespace PetCare
{
    public class Hospitalizacion : ServicioVeterinario
    {
        private readonly DateTime fechaIngreso;
        private DateTime? fechaAlta;
        private readonly decimal tarifaDiaria;

        public DateTime FechaIngreso => fechaIngreso;
        public DateTime? FechaAlta => fechaAlta;
        public decimal TarifaDiaria => tarifaDiaria;

        public Hospitalizacion(string codigo, string nombreMascota, DateTime fechaIngreso, decimal tarifaDiaria)
            : base(codigo, nombreMascota, fechaIngreso)
        {
            if (tarifaDiaria <= 0)
            {
                throw new ArgumentException("La tarifa diaria debe ser mayor que cero");
            }

            this.fechaIngreso = fechaIngreso;
            this.tarifaDiaria = tarifaDiaria;
        }

        // Devuelve false si ya tenia alta o si la fecha es anterior al ingreso.
        public bool DarAlta(DateTime fecha)
        {
            if (fechaAlta != null || fecha < fechaIngreso)
            {
                return false;
            }

            fechaAlta = fecha;
            return true;
        }

        // Minimo se cobra 1 dia. Si no hay alta, se cuenta hasta hoy.
        public int CalcularDiasCobro()
        {
            DateTime fin = fechaAlta ?? DateTime.Today;
            return Math.Max(1, (fin.Date - fechaIngreso.Date).Days);
        }

        public override decimal CalcularImporte()
        {
            return CalcularDiasCobro() * tarifaDiaria;
        }

        public override string ObtenerDescripcion()
        {
            return $"Hospitalizacion – {NombreMascota} – {CalcularDiasCobro()} dias";
        }
    }
}
