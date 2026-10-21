using System.Globalization;
using System.Text;

namespace PetCare
{
    internal static class Program
    {
        private const decimal TarifaDiariaHospitalizacion = 38000m;

        static LoteMedicamento[] lotes = CrearLotes();
        static CitaVeterinaria[] citasAgendadas = CrearCitas();

        static CitaVeterinaria? consulta;
        static Hospitalizacion? hospitalizacion;
        static FacturaServicio? factura;

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            CultureInfo.CurrentCulture = new CultureInfo("es-CO");

            MostrarBienvenida();
            MenuPrincipal();
            MostrarDespedida();
        }

        static void MostrarBienvenida()
        {
            Console.WriteLine("=========================================");
            Console.WriteLine("      CLINICA VETERINARIA PETCARE");
            Console.WriteLine("=========================================");
        }

        static void MostrarDespedida()
        {
            Console.WriteLine("Sistema cerrado");
        }

        static string LeerOpcion()
        {
            Console.Write("Opcion: ");
            string opcion = Console.ReadLine() ?? "0";
            Console.WriteLine();
            return opcion.Trim();
        }

        static void MenuPrincipal()
        {
            string opcion = string.Empty;

            while (opcion != "0")
            {
                Console.WriteLine("\n============ MENU PRINCIPAL ============");
                Console.WriteLine("1. Farmacia");
                Console.WriteLine("2. Recepcion");
                Console.WriteLine("3. Hospitalizacion");
                Console.WriteLine("4. Facturacion");
                Console.WriteLine("5. Casos que no pueden pasar");
                Console.WriteLine("6. Resumen de las areas");
                Console.WriteLine("7. Demostracion completa (los 8 pasos seguidos)");
                Console.WriteLine("0. Salir");
                opcion = LeerOpcion();

                switch (opcion)
                {
                    case "1": MenuFarmacia(); break;
                    case "2": MenuRecepcion(); break;
                    case "3": MenuHospitalizacion(); break;
                    case "4": MenuFacturacion(); break;
                    case "5": CasosProhibidos(); break;
                    case "6": Resumen(); break;
                    case "7": DemostracionPetCare(); break;
                    case "0": break;
                    default: Console.WriteLine("Opcion no valida"); break;
                }
            }
        }


        static void MenuFarmacia()
        {
            string opcion = string.Empty;

            while (opcion != "0")
            {
                Console.WriteLine("\n---------- FARMACIA ----------");
                Console.WriteLine("1. Ver lotes");
                Console.WriteLine("2. Validar si cada lote se puede vender");
                Console.WriteLine("0. Volver");
                opcion = LeerOpcion();

                switch (opcion)
                {
                    case "1":
                        foreach (LoteMedicamento lote in lotes)
                        {
                            string estado = lote.EstaVencido() ? "VENCIDO" : "VIGENTE";
                            Console.WriteLine($"{lote.Codigo} | {lote.NombreMedicamento} | {lote.PrecioUnitario:C0} | " +
                                              $"Vence: {lote.FechaVencimiento:dd/MM/yyyy} | {estado}");
                        }
                        break;
                    case "2":
                        foreach (LoteMedicamento lote in lotes)
                        {
                            Console.WriteLine($"{lote.Codigo}: se puede vender = {lote.ValidarVenta()}");
                        }
                        break;
                    case "0": break;
                    default: Console.WriteLine("Opcion no valida"); break;
                }
            }
        }

        static void MenuRecepcion()
        {
            string opcion = string.Empty;

            while (opcion != "0")
            {
                Console.WriteLine("\n---------- RECEPCION ----------");
                Console.WriteLine("1. Agendar consulta de 45 minutos");
                Console.WriteLine("2. Asignar veterinario");
                Console.WriteLine("3. Confirmar cita");
                Console.WriteLine("4. Ver citas");
                Console.WriteLine("0. Volver");
                opcion = LeerOpcion();

                switch (opcion)
                {
                    case "1":
                        if (consulta != null)
                        {
                            Console.WriteLine($"La consulta {consulta.Codigo} ya esta agendada");
                            break;
                        }
                        consulta = new CitaVeterinaria("CIT-009", "Rambo", DateTime.Today.AddHours(10), 45);
                        Console.WriteLine($"Consulta agendada: {consulta.ObtenerDescripcion()} | {consulta.DuracionMinutos} min | {consulta.CalcularImporte():C0}");
                        break;
                    case "2":
                        if (consulta == null)
                        {
                            Console.WriteLine("Primero agende la consulta (opcion 1)");
                            break;
                        }
                        Console.WriteLine($"Asignar al Dr. Restrepo: {consulta.AsignarVeterinario("Dr. Restrepo")} | Veterinario: {consulta.Veterinario ?? "sin asignar"}");
                        break;
                    case "3":
                        if (consulta == null)
                        {
                            Console.WriteLine("Primero agende la consulta (opcion 1)");
                            break;
                        }
                        Console.WriteLine($"Confirmar cita: {consulta.Confirmar()} | Confirmada: {consulta.Confirmada}");
                        break;
                    case "4":
                        foreach (CitaVeterinaria cita in citasAgendadas)
                        {
                            MostrarCita(cita);
                        }
                        if (consulta != null)
                        {
                            MostrarCita(consulta);
                        }
                        break;
                    case "0": break;
                    default: Console.WriteLine("Opcion no valida"); break;
                }
            }
        }

        static void MostrarCita(CitaVeterinaria cita)
        {
            Console.WriteLine($"{cita.Codigo} | {cita.NombreMascota} | {cita.Fecha:dd/MM/yyyy HH:mm} | {cita.DuracionMinutos} min | " +
                              $"Veterinario: {cita.Veterinario ?? "sin asignar"} | confirmada: {cita.Confirmada} | {cita.CalcularImporte():C0}");
        }
        static void MenuHospitalizacion()
        {
            string opcion = string.Empty;

            while (opcion != "0")
            {
                Console.WriteLine("\n---------- HOSPITALIZACION ----------");
                Console.WriteLine("1. Ingresar animal (ingreso hace 3 dias)");
                Console.WriteLine("2. Dar de alta hoy");
                Console.WriteLine("3. Ver hospitalizacion");
                Console.WriteLine("0. Volver");
                opcion = LeerOpcion();

                switch (opcion)
                {
                    case "1":
                        if (hospitalizacion != null)
                        {
                            Console.WriteLine($"{hospitalizacion.NombreMascota} ya fue ingresado ({hospitalizacion.Codigo})");
                            break;
                        }
                        hospitalizacion = new Hospitalizacion("HOS-132", "Trico", DateTime.Today.AddDays(-3), TarifaDiariaHospitalizacion);
                        Console.WriteLine($"Ingreso registrado el {hospitalizacion.FechaIngreso:dd/MM/yyyy} | Tarifa diaria: {hospitalizacion.TarifaDiaria:C0}");
                        break;
                    case "2":
                        if (hospitalizacion == null)
                        {
                            Console.WriteLine("Primero ingrese el animal (opcion 1)");
                            break;
                        }
                        Console.WriteLine($"Alta registrada: {hospitalizacion.DarAlta(DateTime.Today)}");
                        MostrarHospitalizacion(hospitalizacion);
                        break;
                    case "3":
                        if (hospitalizacion == null)
                        {
                            Console.WriteLine("No hay animales hospitalizados");
                            break;
                        }
                        MostrarHospitalizacion(hospitalizacion);
                        break;
                    case "0": break;
                    default: Console.WriteLine("Opcion no valida"); break;
                }
            }
        }

        static void MostrarHospitalizacion(Hospitalizacion hosp)
        {
            string alta = hosp.FechaAlta?.ToString("dd/MM/yyyy") ?? "pendiente";
            Console.WriteLine($"{hosp.ObtenerDescripcion()} | Ingreso: {hosp.FechaIngreso:dd/MM/yyyy} | Alta: {alta} | " +
                              $"Dias a cobrar: {hosp.CalcularDiasCobro()} | Total: {hosp.CalcularImporte():C0}");
        }

        static void MenuFacturacion()
        {
            string opcion = string.Empty;

            while (opcion != "0")
            {
                Console.WriteLine("\n---------- FACTURACION ----------");
                Console.WriteLine("1. Crear factura: consulta + hospitalizacion + 2 unidades del lote vigente");
                Console.WriteLine("2. Mostrar factura");
                Console.WriteLine("3. Intentar agregar el lote vencido");
                Console.WriteLine("4. Anular factura e intentar agregar otro renglon");
                Console.WriteLine("5. Factura nueva con la misma consulta");
                Console.WriteLine("0. Volver");
                opcion = LeerOpcion();

                switch (opcion)
                {
                    case "1": CrearFactura(); break;
                    case "2":
                        if (factura == null)
                        {
                            Console.WriteLine("Primero cree la factura (opcion 1)");
                            break;
                        }
                        factura.MostrarFactura();
                        break;
                    case "3":
                        if (factura == null)
                        {
                            Console.WriteLine("Primero cree la factura (opcion 1)");
                            break;
                        }
                        Console.WriteLine($"Agregar lote vencido {lotes[2].Codigo}: {factura.AgregarMedicamento(lotes[2], 1)}");
                        Console.WriteLine($"La factura sigue igual, total: {factura.CalcularTotal():C0}");
                        break;
                    case "4":
                        if (factura == null)
                        {
                            Console.WriteLine("Primero cree la factura (opcion 1)");
                            break;
                        }
                        Console.WriteLine($"Factura anulada: {factura.Anular()}");
                        Console.WriteLine($"Agregar otro renglon a la factura anulada: {factura.AgregarMedicamento(lotes[0], 1)}");
                        break;
                    case "5":
                        if (consulta == null)
                        {
                            Console.WriteLine("Primero agende la consulta en Recepcion (opcion 2 del menu principal)");
                            break;
                        }
                        FacturaServicio nuevaFactura = new FacturaServicio("FAC-1002", DateTime.Today, "Ivan");
                        if (nuevaFactura.AgregarServicio(consulta))
                        {
                            Console.WriteLine($"La consulta {consulta.Codigo} sigue existiendo y se factura de nuevo");
                            nuevaFactura.MostrarFactura();
                        }
                        else
                        {
                            Console.WriteLine("No se pudo facturar la consulta: debe estar confirmada (asigne veterinario y confirme en Recepcion)");
                        }
                        break;
                    case "0": break;
                    default: Console.WriteLine("Opcion no valida"); break;
                }
            }
        }

        static void CrearFactura()
        {
            if (factura != null)
            {
                Console.WriteLine($"La factura {factura.Numero} ya fue creada");
                return;
            }
            if (consulta == null)
            {
                Console.WriteLine("Primero agende la consulta en Recepcion (opcion 2 del menu principal)");
                return;
            }
            if (hospitalizacion == null)
            {
                Console.WriteLine("Primero ingrese el animal en Hospitalizacion (opcion 3 del menu principal)");
                return;
            }

            FacturaServicio nueva = new FacturaServicio("FAC-1001", DateTime.Today, "Ivan");
            bool consultaAgregada = nueva.AgregarServicio(consulta);
            bool hospitalizacionAgregada = nueva.AgregarServicio(hospitalizacion);
            bool medicamentoAgregado = nueva.AgregarMedicamento(lotes[0], 2);

            if (!consultaAgregada)
            {
                Console.WriteLine("No se pudo facturar la consulta: debe estar confirmada (asigne veterinario y confirme en Recepcion)");
            }
            if (!hospitalizacionAgregada)
            {
                Console.WriteLine("No se pudo facturar la hospitalizacion");
            }
            if (!medicamentoAgregado)
            {
                Console.WriteLine($"No se pudo facturar el lote {lotes[0].Codigo}");
            }
            if (!consultaAgregada || !hospitalizacionAgregada || !medicamentoAgregado)
            {
                Console.WriteLine("La factura no se creo");
                return;
            }

            factura = nueva;
            Console.WriteLine($"Factura {factura.Numero} creada con {factura.Renglones.Count} renglones");
        }

        static void Resumen()
        {
            Console.WriteLine("---------- RESUMEN DE LAS AREAS ----------");

            Console.WriteLine("FARMACIA");
            foreach (LoteMedicamento lote in lotes)
            {
                Console.WriteLine($"  {lote.Codigo}: {lote.NombreMedicamento} | {lote.PrecioUnitario:C0} | vencido: {lote.EstaVencido()} | venta valida: {lote.ValidarVenta()}");
            }

            Console.WriteLine("RECEPCION");
            foreach (CitaVeterinaria cita in citasAgendadas)
            {
                Console.WriteLine($"  {cita.Codigo}: confirmada: {cita.Confirmada} | {cita.CalcularImporte():C0}");
            }
            if (consulta != null)
            {
                Console.WriteLine($"  {consulta.Codigo}: confirmada: {consulta.Confirmada} | {consulta.CalcularImporte():C0}");
            }

            Console.WriteLine("HOSPITALIZACION");
            if (hospitalizacion == null)
            {
                Console.WriteLine("  Sin animales hospitalizados");
            }
            else
            {
                Console.WriteLine("  " + hospitalizacion.ObtenerDescripcion() + $" | total: {hospitalizacion.CalcularImporte():C0}");
            }

            Console.WriteLine("FACTURACION");
            Console.WriteLine(factura == null
                ? "  Sin factura creada"
                : $"  {factura.Numero} | {factura.Renglones.Count} renglones | total {factura.CalcularTotal():C0} | anulada: {factura.Anulada}");
        }

        // Datos de prueba

        static LoteMedicamento[] CrearLotes()
        {
            DateTime hoy = DateTime.Today;
            return new[]
            {
                new LoteMedicamento("MED-0142", "Amoxivet 250 mg", hoy.AddMonths(8), 1800m),
                new LoteMedicamento("MED-0207", "Rabivac 10 mL", hoy.AddMonths(12), 32000m),
                new LoteMedicamento("MED-0315", "Drontal Plus 700 mg", hoy.AddDays(-20), 9500m)
            };
        }

        static CitaVeterinaria[] CrearCitas()
        {
            return new[]
            {
                new CitaVeterinaria("CIT-001", "Manchas", DateTime.Today.AddDays(5).AddHours(9.5)),
                new CitaVeterinaria("CIT-002", "Rocky", DateTime.Today.AddDays(1).AddHours(14), 45),
                new CitaVeterinaria("CIT-003", "Luna", DateTime.Today.AddDays(-3).AddHours(8), 90)
            };
        }

        static void CasosProhibidos()
        {
            Console.WriteLine("---------- CASOS QUE NO PUEDEN PASAR ----------");
            LoteMedicamento[] lotesPrueba = CrearLotes();
            CitaVeterinaria[] citasPrueba = CrearCitas();
            FacturaServicio facturaPrueba = new FacturaServicio("FAC-3001", DateTime.Today, "Diana Perez");

            Console.WriteLine($"\n[1] Facturar el lote vencido {lotesPrueba[2].Codigo} (vencio el {lotesPrueba[2].FechaVencimiento:dd/MM/yyyy})");
            Console.WriteLine($"    Resultado: {facturaPrueba.AgregarMedicamento(lotesPrueba[2], 1)} | Total: {facturaPrueba.CalcularTotal():C0}");

            Console.WriteLine("\n[2] Facturar 0 unidades de un lote vigente");
            Console.WriteLine($"    Resultado: {facturaPrueba.AgregarMedicamento(lotesPrueba[0], 0)}");

            citasPrueba[0].AsignarVeterinario("Dra. Lina Restrepo");
            Console.WriteLine($"\n[3] Asignar otro veterinario en CIT-001, que ya tiene a {citasPrueba[0].Veterinario}");
            Console.WriteLine($"    Resultado: {citasPrueba[0].AsignarVeterinario("Dr. Andres Zapata")} | Sigue {citasPrueba[0].Veterinario}");

            Console.WriteLine("\n[4] Confirmar CIT-002, que no tiene veterinario");
            Console.WriteLine($"    Resultado: {citasPrueba[1].Confirmar()} | Confirmada: {citasPrueba[1].Confirmada}");

            citasPrueba[0].Confirmar();
            Console.WriteLine("\n[5] Confirmar CIT-001 otra vez, que ya esta confirmada");
            Console.WriteLine($"    Resultado: {citasPrueba[0].Confirmar()}");

            Console.WriteLine("\n[6] Crear una cita de 10 minutos");
            try
            {
                new CitaVeterinaria("CIT-004", "Nube", DateTime.Today.AddDays(2), 10);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"    Error: {ex.Message}");
            }

            Hospitalizacion hospPrueba = new Hospitalizacion("HOS-006", "Simba", DateTime.Today, TarifaDiariaHospitalizacion);
            Console.WriteLine("\n[7] Dar de alta antes del ingreso");
            Console.WriteLine($"    Resultado: {hospPrueba.DarAlta(DateTime.Today.AddDays(-1))} | Alta: {hospPrueba.FechaAlta?.ToString("dd/MM/yyyy") ?? "pendiente"}");

            hospPrueba.DarAlta(DateTime.Today);
            Console.WriteLine("\n[8] Dar de alta una hospitalizacion que ya tiene alta");
            Console.WriteLine($"    Resultado: {hospPrueba.DarAlta(DateTime.Today)}");

            Console.WriteLine("\n[9] Facturar CIT-002, que no esta confirmada");
            Console.WriteLine($"    Resultado: {facturaPrueba.AgregarServicio(citasPrueba[1])}");

            facturaPrueba.Anular();
            Console.WriteLine("\n[10] Agregar un renglon a una factura anulada");
            Console.WriteLine($"    Resultado: {facturaPrueba.AgregarMedicamento(lotesPrueba[1], 1)}");
            Console.WriteLine($"    Anular otra vez: {facturaPrueba.Anular()}");
        }

        // Demostracion

        static void DemostracionPetCare()
        {
            Console.WriteLine("\n[1] Consulta de 45 minutos");
            CitaVeterinaria cita = new CitaVeterinaria("CIT-009", "Rambo", DateTime.Today.AddHours(10), 45);
            Console.WriteLine($"  Confirmada: {cita.Confirmada}");
            Console.WriteLine($"  Confirmar sin veterinario: {cita.Confirmar()}");
            Console.WriteLine($"  Asignar veterinario: {cita.AsignarVeterinario("Dr. Mengueche")}");
            Console.WriteLine($"  Asignar otro veterinario: {cita.AsignarVeterinario("Dra. Lina Restrepo")}");
            Console.WriteLine($"  Confirmar: {cita.Confirmar()} | Confirmada: {cita.Confirmada}");
            Console.WriteLine($"  Confirmar otra vez: {cita.Confirmar()}");
            Console.WriteLine($"  {cita.ObtenerDescripcion()}");
            Console.WriteLine($"  Costo de la consulta: {cita.CalcularImporte():C0}");

            Console.WriteLine("\n[2] Hospitalizacion que ingreso hace 3 dias, alta hoy");
            Hospitalizacion hosp = new Hospitalizacion("HOS-132", "Trico", DateTime.Today.AddDays(-3), TarifaDiariaHospitalizacion);
            Console.WriteLine($"  Al ingresar: {hosp.ObtenerDescripcion()} | tarifa diaria: {hosp.TarifaDiaria:C0}");
            Console.WriteLine($"  Alta antes del ingreso: {hosp.DarAlta(DateTime.Today.AddDays(-4))}");
            Console.WriteLine($"  Alta registrada: {hosp.DarAlta(DateTime.Today)}");
            Console.WriteLine($"  Alta otra vez: {hosp.DarAlta(DateTime.Today)}");
            Console.WriteLine($"  {hosp.ObtenerDescripcion()}");
            Console.WriteLine($"  Costo de la hospitalizacion: {hosp.CalcularImporte():C0}");

            Console.WriteLine("\n[3] Lotes de medicamento: uno vigente y uno vencido");
            DateTime hoy = DateTime.Today;
            LoteMedicamento loteActivo = new LoteMedicamento("L-203", "Amoxicilina 500 mg", hoy.AddMonths(6), 2500m);
            LoteMedicamento loteVencido = new LoteMedicamento("L-118", "Meloxicam 1 mg", hoy.AddDays(-1), 1800m);
            Console.WriteLine($"  {loteActivo.ObtenerDescripcion()} | vencido: {loteActivo.EstaVencido()} | venta valida: {loteActivo.ValidarVenta()}");
            Console.WriteLine($"  {loteVencido.ObtenerDescripcion()} | vencido: {loteVencido.EstaVencido()} | venta valida: {loteVencido.ValidarVenta()}");

            Console.WriteLine("\n    Recorrido polimorfico: la misma linea responde distinto segun el objeto");
            List<Facturable> facturables = new List<Facturable> { cita, hosp, loteActivo };
            foreach (Facturable item in facturables)
            {
                Console.WriteLine($"  - {item.ObtenerDescripcion()} | importe: {item.CalcularImporte():C0}");
            }

            Console.WriteLine("\n[4] Factura con la consulta, la hospitalizacion y 2 unidades del lote vigente");
            FacturaServicio facturaDemo = new FacturaServicio("FAC-1001", DateTime.Today, "Ivan");
            Console.WriteLine($"  Consulta agregada: {facturaDemo.AgregarServicio(cita)}");
            Console.WriteLine($"  Hospitalizacion agregada: {facturaDemo.AgregarServicio(hosp)}");
            Console.WriteLine($"  Medicamento agregado: {facturaDemo.AgregarMedicamento(loteActivo, 2)}");
            Console.WriteLine($"  Renglones agregados: {facturaDemo.Renglones.Count}");

            Console.WriteLine("\n[5] Factura impresa");
            facturaDemo.MostrarFactura();

            Console.WriteLine("\n[6] Intento de facturar el lote vencido");
            bool vencidoAgregado = facturaDemo.AgregarMedicamento(loteVencido, 1);
            Console.WriteLine($"  Agregar lote vencido: {vencidoAgregado}");
            if (!vencidoAgregado)
            {
                Console.WriteLine($"  El sistema lo impidio: el lote {loteVencido.Codigo} de {loteVencido.NombreMedicamento} " +
                                  $"vencio el {loteVencido.FechaVencimiento:dd/MM/yyyy}, no se puede vender ni una unidad");
            }
            Console.WriteLine($"  La factura sigue igual, total: {facturaDemo.CalcularTotal():C0}");

            Console.WriteLine("\n    Intento de facturar una cita sin confirmar");
            CitaVeterinaria citaSinConfirmar = new CitaVeterinaria("CIT-010", "Luna", DateTime.Today.AddDays(2));
            bool citaAgregada = facturaDemo.AgregarServicio(citaSinConfirmar);
            Console.WriteLine($"  Agregar cita sin confirmar: {citaAgregada}");
            if (!citaAgregada)
            {
                Console.WriteLine($"  El sistema lo impidio: la cita {citaSinConfirmar.Codigo} no esta confirmada, no se puede facturar");
            }

            Console.WriteLine("\n[7] Anular la factura e intentar agregar otro renglon");
            Console.WriteLine($"  Factura anulada: {facturaDemo.Anular()}");
            bool renglonAnulada = facturaDemo.AgregarMedicamento(loteActivo, 1);
            Console.WriteLine($"  Agregar renglon a factura anulada: {renglonAnulada}");
            if (!renglonAnulada)
            {
                Console.WriteLine($"  El sistema lo impidio: la factura {facturaDemo.Numero} esta anulada y no admite mas renglones");
            }
            Console.WriteLine("  Estado final de la factura:");
            facturaDemo.MostrarFactura();

            Console.WriteLine("\n[8] Factura nueva con la misma consulta");
            FacturaServicio nuevaFactura = new FacturaServicio("FAC-1002", DateTime.Today, "Ivan");
            bool consultaNueva = nuevaFactura.AgregarServicio(cita);
            Console.WriteLine($"  Consulta agregada a la nueva factura: {consultaNueva}");
            Console.WriteLine($"  La consulta {cita.Codigo} sigue existiendo (confirmada: {cita.Confirmada}) y se factura de nuevo");
            nuevaFactura.MostrarFactura();
        }
    }
}
