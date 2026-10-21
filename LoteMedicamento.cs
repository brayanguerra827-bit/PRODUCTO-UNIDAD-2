namespace PetCare
{
    public class LoteMedicamento : Facturable
    {
        private readonly string codigo;
        private readonly string nombreMedicamento;
        private readonly DateTime fechaVencimiento;
        private readonly decimal precioUnitario;

        public string Codigo => codigo;
        public string NombreMedicamento => nombreMedicamento;
        public DateTime FechaVencimiento => fechaVencimiento;
        public decimal PrecioUnitario => precioUnitario;

        public LoteMedicamento(string codigo, string nombreMedicamento, DateTime fechaVencimiento, decimal precioUnitario)
        {
            if (string.IsNullOrWhiteSpace(codigo))
            {
                throw new ArgumentException("El codigo del lote es obligatorio");
            }
            if (string.IsNullOrWhiteSpace(nombreMedicamento))
            {
                throw new ArgumentException("El nombre del medicamento es obligatorio");
            }
            if (precioUnitario <= 0)
            {
                throw new ArgumentException("El precio por unidad debe ser mayor que cero");
            }

            this.codigo = codigo.Trim();
            this.nombreMedicamento = nombreMedicamento.Trim();
            this.fechaVencimiento = fechaVencimiento;
            this.precioUnitario = precioUnitario;
        }

        // Vencido si la fecha de vencimiento ya paso (el dia de vencimiento todavia es valido).
        public bool EstaVencido()
        {
            return fechaVencimiento.Date < DateTime.Today;
        }

        public bool ValidarVenta()
        {
            return !EstaVencido();
        }

        // Importe de UNA unidad; el renglon de la factura lo multiplica por la cantidad.
        public decimal CalcularImporte()
        {
            return precioUnitario;
        }

        public string ObtenerDescripcion()
        {
            return $"{nombreMedicamento} – Lote {codigo}";
        }
    }
}
