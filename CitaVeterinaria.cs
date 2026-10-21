namespace PetCare
{
    public class CitaVeterinaria : ServicioVeterinario
    {
        private const int DuracionMinima = 15;
        private const int DuracionMaxima = 180;
        private const int MinutosBase = 30;
        private const decimal TarifaBase = 60000m;
        private const decimal TarifaMinutoAdicional = 1500m;

        private readonly int duracionMinutos;
        private string? veterinario;
        private bool confirmada;

        public int DuracionMinutos => duracionMinutos;
        public string? Veterinario => veterinario;
        public bool Confirmada => confirmada;

        public CitaVeterinaria(string codigo, string nombreMascota, DateTime fecha, int duracionMinutos = MinutosBase)
            : base(codigo, nombreMascota, fecha)
        {
            if (duracionMinutos < DuracionMinima || duracionMinutos > DuracionMaxima)
            {
                throw new ArgumentException($"La duracion debe estar entre {DuracionMinima} y {DuracionMaxima} minutos");
            }

            this.duracionMinutos = duracionMinutos;
        }

        // Devuelve false si el nombre esta vacio, si ya hay veterinario o si la cita ya fue confirmada,
        // para evitar errores, no se permite cambiar el veterinario una vez asignado o si la cita ya fue confirmada.
        public bool AsignarVeterinario(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return false;
            }
            if (veterinario != null || confirmada)
            {
                return false;
            }

            veterinario = nombre.Trim();
            return true;
        }

        // Devuelve false si ya estaba confirmada o si todavia no tiene veterinario asignado.
        public bool Confirmar()
        {
            if (confirmada || veterinario == null)
            {
                return false;
            }

            confirmada = true;
            return true;
        }

        public override decimal CalcularImporte()
        {
            int minutosAdicionales = Math.Max(0, duracionMinutos - MinutosBase);
            return TarifaBase + minutosAdicionales * TarifaMinutoAdicional;
        }

        public override string ObtenerDescripcion()
        {
            return $"Consulta – {NombreMascota} – {Fecha:dd/MM/yyyy HH:mm} – {Veterinario ?? "Sin veterinario"}";
        }
    }
}
