Documento de decisiones de diseño – PetCare
1. Clase abstracta
Creamos la clase abstracta ServicioVeterinario. A ella subimos los atributos que la cita y la hospitalización tienen en común: codigo, nombreMascota y fecha. También definimos los métodos abstractos CalcularImporte() y ObtenerDescripcion().
Elegimos esto porque tanto las citas como las hospitalizaciones son servicios veterinarios y comparten información. Sin embargo, cada uno calcula su importe de manera diferente. Así evitamos repetir código y permitimos que cada clase implemente sus propios métodos.
Cambios frente a la Unidad 1: antes las clases estaban más separadas. Ahora tenemos una jerarquía de servicios y una interfaz llamada Facturable, que también pueden implementar los medicamentos.

2. Interfaz
Usamos la interfaz Facturable porque no todo lo que se factura es un servicio veterinario. Las citas y las hospitalizaciones heredan de ServicioVeterinario, mientras que los medicamentos implementan Facturable.
Elegimos una interfaz porque un medicamento no es un servicio veterinario, sino un producto que se vende. La interfaz permite que ambos puedan facturarse sin obligarlos a compartir una herencia incorrecta.

3. Composición y agregación
La composición está entre FacturaServicio y RenglonFactura. La factura contiene sus propios renglones y estos dependen de ella. Si la factura se anula, los renglones también se anulan o dejan de estar disponibles.
La agregación está entre RenglonFactura y Facturable. Cada renglón guarda una referencia al concepto que se factura, ya sea una cita, una hospitalización o un medicamento. Estos conceptos existen independientemente de la factura, por lo que pueden seguir existiendo aunque esta se anule.

4. Polimorfismo dinámico
El polimorfismo dinámico ocurre cuando se llama a los métodos CalcularImporte() u ObtenerDescripcion() mediante una referencia a un tipo común.
Por ejemplo:
concepto.CalcularImporte();
concepto.ObtenerDescripcion();
El método que se ejecuta depende del tipo real del objeto durante la ejecución del programa. Si es una CitaVeterinaria, una Hospitalizacion o un LoteMedicamento, se ejecutará la implementación correspondiente, siempre que las clases implementen o sobrescriban esos métodos según el diseño.
Esto permite trabajar con diferentes tipos de objetos sin tener que escribir una condición para cada clase.

5. Sobrecarga
Separamos las operaciones para agregar servicios y medicamentos mediante estos métodos:
- AgregarServicio(ServicioVeterinario servicio)
- AgregarMedicamento(LoteMedicamento lote, int cantidad)
Elegimos esta solución porque cada método tiene una función clara. Para agregar un medicamento, es obligatorio indicar la cantidad, mientras que para los servicios se utiliza un método específico.
De esta manera, el compilador ayuda a evitar errores y el código resulta más fácil de entender. Además, se evita que un medicamento se agregue por accidente sin especificar cuántas unidades se van a facturar.

6. Peluquería y alimentos
Si la clínica quiere ofrecer servicios de peluquería y vender alimentos, se necesitarían las siguientes clases nuevas:
- ServicioPeluqueria: heredaría de ServicioVeterinario e implementaría el cálculo de su importe y la descripción.
- ProductoAlimento o LoteAlimento: implementaría la interfaz Facturable para poder incluirse en una factura.
Clases existentes que habría que modificar: según el diseño propuesto, FacturaServicio tendría que permitir agregar estos nuevos conceptos si todavía no admite cualquier objeto que implemente Facturable.
Las demás clases podrían conservarse sin cambios, siempre que la factura ya trabaje con la interfaz Facturable y los nuevos tipos cumplan con el contrato establecido.
