namespace PetCare
{
    public class FacturaServicio
    {
        private readonly string numero;
        private readonly DateTime fechaEmision;
        private readonly string nombreCliente;
        private bool anulada;
        private readonly List<RenglonFactura> renglones = new();

        public string Numero => numero;
        public DateTime FechaEmision => fechaEmision;
        public string NombreCliente => nombreCliente;
        public bool Anulada => anulada;
        public IReadOnlyList<RenglonFactura> Renglones => renglones.AsReadOnly();

        public FacturaServicio(string numero, DateTime fechaEmision, string nombreCliente)
        {
            if (string.IsNullOrWhiteSpace(numero))
            {
                throw new ArgumentException("El numero de factura es obligatorio");
            }
            if (string.IsNullOrWhiteSpace(nombreCliente))
            {
                throw new ArgumentException("El nombre del cliente es obligatorio");
            }

            this.numero = numero.Trim();
            this.fechaEmision = fechaEmision;
            this.nombreCliente = nombreCliente.Trim();
        }

        // Los servicios (cita, hospitalizacion...) se cobran siempre como 1 unidad.
        // Devuelve false si la factura esta anulada, el servicio es nulo
        // o es una cita que todavia no esta confirmada.
        public bool AgregarServicio(ServicioVeterinario servicio)
        {
            if (servicio == null || anulada)
            {
                return false;
            }
            if (servicio is CitaVeterinaria cita && !cita.Confirmada)
            {
                return false;
            }

            renglones.Add(new RenglonFactura(servicio, 1));
            return true;
        }

        // Devuelve false si la factura esta anulada, el lote es nulo,
        // la cantidad no es positiva o el lote esta vencido.
        public bool AgregarMedicamento(LoteMedicamento lote, int cantidad)
        {
            if (lote == null || anulada || cantidad <= 0)
            {
                return false;
            }
            if (!lote.ValidarVenta())
            {
                return false;
            }

            renglones.Add(new RenglonFactura(lote, cantidad));
            return true;
        }

        public decimal CalcularTotal()
        {
            return renglones.Sum(r => r.Subtotal);
        }

        public bool Anular()
        {
            if (anulada)
            {
                return false;
            }

            anulada = true;
            return true;
        }

        public void MostrarFactura()
        {
            Console.WriteLine($"  Factura {numero} | {fechaEmision:dd/MM/yyyy} | Cliente: {nombreCliente}" +
                              (anulada ? " | ANULADA" : ""));
            foreach (RenglonFactura renglon in renglones)
            {
                Console.WriteLine($"  - {renglon.ObtenerDescripcion()} | Cantidad: {renglon.Cantidad} | Subtotal: {renglon.Subtotal:C0}");
            }
            Console.WriteLine($"  Total: {CalcularTotal():C0}");
        }
    }
}
