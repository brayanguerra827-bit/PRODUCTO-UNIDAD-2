# Bitácora de uso de inteligencia artificial (IA.md)

**Asignatura:** Programación Orientada a Objetos  
**Unidad:** 2  
**Proyecto:** PetCare 2.0  
**Integrantes:** Luna y Brayan

## 1. Objetivo de la bitácora

Este documento reúne las consultas realizadas a herramientas de inteligencia artificial durante el desarrollo del sistema PetCare 2.0. Las herramientas se utilizaron para resolver dudas de programación, evaluar alternativas de diseño y mejorar la documentación del proyecto.

Las sugerencias recibidas se analizaron teniendo en cuenta los requisitos de la actividad. El equipo tomó las decisiones finales y verificó que las soluciones elegidas fueran coherentes con el funcionamiento de la clínica veterinaria.

## 2. Registro de consultas

| Fecha | Integrante | Herramienta | Consulta realizada | Aporte aprovechado | Decisión del equipo |
|---|---|---|---|---|---|
| 15/10/2026 | Luna | Claude | Analizar cómo incluir los medicamentos en el proceso de facturación sin confundirlos con los servicios médicos. | Se tomó como referencia el uso de una interfaz común llamada `IFacturable`. | Se evitó que los medicamentos heredaran de `Servicio`, porque sus características y reglas de cobro son diferentes. |
| 15/10/2026 | Brayan | ChatGPT y Claude | Comparar alternativas para validar las cantidades que se agregan a una factura. | Se consideró que cada tipo de concepto debía controlar sus propias reglas mediante `ValidarFacturacion(cantidad)`. | Se prefirió esta solución frente a utilizar una propiedad booleana que obligara a la factura a decidir qué validación aplicar. |
| 15/10/2026 | Luna | Claude | Revisar cómo relacionar los servicios facturados con los renglones de una factura. | Se utilizó la idea de conservar una referencia al elemento facturable dentro de `RenglonFactura`. | Se evitó almacenar solamente una descripción en texto, porque eso no permite mantener una relación directa con el objeto original. |
| 16/10/2026 | Brayan | Claude | Revisar la estructura de las clases de facturación y detectar posibles inconsistencias con los requisitos. | Se consideró separar la información general de la factura de los datos particulares de cada renglón. | Las recomendaciones se contrastaron con el código para no eliminar clases que todavía fueran necesarias para representar elementos de la Unidad 1. |
| 16/10/2026 | Luna | Claude | Evaluar cómo debía relacionarse una jaula con una hospitalización. | Se adoptó como alternativa de diseño que la hospitalización utilizara una jaula existente. | Se descartó representar la jaula como un servicio facturable por herencia, ya que su función principal es alojar al animal y no constituye por sí misma el servicio médico. |
| 16/10/2026 | Brayan | Claude | Organizar una demostración del programa que permitiera presentar las funcionalidades principales. | Se tomó como referencia una secuencia de pruebas para mostrar los objetos, sus importes y el comportamiento de la facturación. | Se revisó que los ejemplos utilizaran los nombres, tipos de datos y constructores presentes en el proyecto. |
| 16/10/2026 | Luna | Claude | Revisar las relaciones entre las clases para preparar el diagrama UML. | Se utilizó la explicación de las diferencias entre herencia, implementación de interfaces, composición y agregación. | Se comprobó que las relaciones representadas tuvieran sentido en el sistema y no se eligieran únicamente por la apariencia de las flechas. |
| 16/10/2026 | Brayan | Claude | Mejorar la explicación de las decisiones técnicas y preparar material para la presentación. | Se aprovechó la organización de los conceptos y la explicación del propósito de cada clase. | Se ajustó la documentación para distinguir entre funcionalidades implementadas y propuestas futuras, evitando presentar como terminado algo que todavía no se había desarrollado. |

**Observación:** Los registros deben corresponder a consultas reales. Si una herramienta, integrante o fecha no coincide con el historial de trabajo, se debe modificar antes de entregar.

## 3. Principales decisiones tomadas

Durante el trabajo se identificaron varias decisiones importantes:

- **Separar servicios y productos:** las citas y las hospitalizaciones comparten características de los servicios veterinarios, mientras que los medicamentos se incorporan mediante una interfaz de facturación.
- **Mantener los objetos independientes:** los renglones pertenecen a una factura, pero los conceptos facturados pueden existir sin ella.
- **Controlar las reglas de cobro:** cada clase debe aplicar las validaciones que correspondan a su tipo de concepto.
- **Conservar las clases útiles de la Unidad 1:** las clases existentes se evaluaron según su utilidad en el nuevo sistema, en lugar de eliminarlas automáticamente.
- **Verificar el diagrama con el código:** las relaciones UML deben representar la estructura que realmente se implementó.

## 4. Aprendizajes del equipo

**1. Elegir bien las relaciones entre clases.** Aprendimos que compartir una operación no significa que dos clases deban tener una relación de herencia. Las interfaces permiten compartir un contrato sin afirmar que los objetos son del mismo tipo.

**2. Diseñar pensando en los cambios futuros.** Una factura que trabaja con una interfaz puede admitir nuevos tipos de conceptos facturables sin tener que crear una estructura completamente diferente para cada producto o servicio.

**3. Diferenciar el ciclo de vida de los objetos.** Una factura administra sus renglones, pero no debería eliminar automáticamente una mascota, una jaula o un medicamento cuando se anula.

**4. Utilizar la IA con criterio.** Las explicaciones ayudaron a comparar soluciones, pero fue necesario revisar los requisitos y el código para decidir qué propuestas tenían sentido para PetCare.

