# Resumen del Proyecto: DentalCare

Este documento proporciona un resumen general del proyecto `DentalCare` basado en la estructura de su código fuente.

## Propósito del Proyecto

El proyecto `DentalCare` es un sistema de software diseñado para la gestión de una clínica dental. La estructura del dominio sugiere que el sistema manejará entidades clave como:
- **Clínicas Dentales** (`DentalClinic`)
- **Dentistas** (`Dentist`)
- **Pacientes** (`Patient`)
- **Citas** (`Date`)

## Arquitectura y Diseño

El proyecto está organizado siguiendo principios de **Arquitectura Limpia (Clean Architecture)** y **Diseño Orientado al Dominio (Domain-Driven Design - DDD)**. Esto se evidencia en la separación de responsabilidades en diferentes capas:

1.  **`DentalCare.Domain` (Capa de Dominio):**
    - Contiene la lógica de negocio y las reglas fundamentales.
    - Define las entidades principales (`DentalClinic`, `Patient`, etc.) y los Objetos de Valor (`Email`, `Name`, `TimeInterval`).
    - Es el núcleo del sistema, independiente de la tecnología de la base de datos o la interfaz de usuario.

2.  **`DentalCare.Application` (Capa de Aplicación):**
    - Orquesta los casos de uso del sistema (por ejemplo, `CreateDentalClinic`).
    - Utiliza el patrón **CQRS (Command Query Responsibility Segregation)** para separar las operaciones de escritura (Comandos) de las de lectura (Consultas).
    - Define abstracciones para la persistencia de datos a través de los patrones **Repositorio (Repository)** y **Unidad de Trabajo (Unit of Work)**, como `IRepository` y `IUnitOfWork`.

3.  **Capa de Infraestructura (Implícita):**
    - Aunque no hay un proyecto explícito en la estructura mostrada, esta capa sería la responsable de implementar las interfaces de la capa de aplicación (por ej., los repositorios) para interactuar con una base de datos, servicios externos, etc.

## Tecnología

- **Framework:** .NET (C#), como indican los archivos de solución (`.slnx`) y proyecto (`.csproj`). La versión de destino parece ser `.net9.0`.
- **Testing:** Se utiliza **xUnit** como framework de pruebas y **NSubstitute** para la creación de mocks y stubs. Esto demuestra un enfoque en la calidad y la mantenibilidad del código a través de pruebas unitarias.

## Estado Actual

- La estructura del proyecto está bien definida.
- Se ha implementado al menos un caso de uso: la creación de una nueva clínica dental (`CreateDentalClinic`).
- Existen pruebas unitarias para validar la lógica del dominio y de la aplicación, lo que indica un desarrollo robusto.
- El proyecto parece estar en una fase de desarrollo del backend, sin una capa de presentación (UI o API) visible por el momento.

## Etiquetas

`.NET`, `C#`, `Clean Architecture`, `Domain-Driven Design`, `CQRS`
