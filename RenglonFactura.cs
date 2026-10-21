namespace PetCare
{
    public sealed class RenglonFactura
    {
        private readonly Facturable concepto;
        private readonly int cantidad;
        private readonly decimal subtotal;

        public Facturable Concepto => concepto;
        public int Cantidad => cantidad;
        public decimal Subtotal => subtotal;

        // Solo la factura crea renglones; el subtotal queda fijo al momento de agregarlo.
        internal RenglonFactura(Facturable concepto, int cantidad)
        {
            ArgumentNullException.ThrowIfNull(concepto);

            if (cantidad <= 0)
            {
                throw new ArgumentException("La cantidad debe ser mayor que cero");
            }

            this.concepto = concepto;
            this.cantidad = cantidad;
            subtotal = CalcularSubtotal();
        }

        public decimal CalcularSubtotal()
        {
            return concepto.CalcularImporte() * cantidad;
        }

        public string ObtenerDescripcion()
        {
            return concepto.ObtenerDescripcion();
        }
    }
}
