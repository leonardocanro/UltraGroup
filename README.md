# UltraGroup

# Documentación del Proyecto

## Introducción

Este proyecto implementa una plataforma de gestión de hoteles y reservas de alojamiento utilizando microservicios desarrollados en C# con .NET 8. El sistema permite a las agencias de viajes gestionar hoteles y habitaciones, así como a los viajeros realizar reservas de manera eficiente.

## Requisitos y Especificaciones

### Historias de Usuario

#### Administración de Alojamiento Hoteles Locales

1. *Crear un nuevo hotel*: Los agentes de viajes pueden crear nuevos hoteles en la plataforma.
2. *Asignar habitaciones*: Se pueden asignar habitaciones a los hoteles creados.
3. *Modificar valores*: Es posible modificar los valores de las habitaciones y los hoteles.
4. *Habilitar/Deshabilitar*: Los hoteles y las habitaciones pueden ser habilitados o deshabilitados.
5. *Ver reservas*: Los agentes pueden ver las reservas realizadas en sus hoteles y el detalle de cada reserva.

*Observaciones:*
- Cada habitación registra el costo base, impuestos, tipo de habitación y ubicación.

#### Reserva de Hoteles

1. *Buscar hoteles*: Los viajeros pueden buscar hoteles por fecha de entrada, fecha de salida, cantidad de personas y ciudad de destino.
2. *Elegir habitación*: Los viajeros pueden seleccionar una habitación de su preferencia.
3. *Formulario de reserva*: Se despliega un formulario para ingresar los datos de los huéspedes y realizar la reserva.
4. *Notificación*: El sistema envía una notificación de la reserva por correo electrónico.

*Observaciones:*
- Los datos del pasajero incluyen nombres, fecha de nacimiento, género, tipo y número de documento, email y teléfono.
- La reserva debe incluir un contacto de emergencia con nombres completos y teléfono de contacto.

## Implementación

### Arquitectura

- *Microservicios*: Desarrollados usando .NET 8 siguiendo la arquitectura orientada al dominio.
- *Contenerización*: Usamos Docker para contenerizar todos los microservicios.
- *Base de Datos*: Se ha elegido SQL SERVER 2019 para el almacenamiento de datos.

### CI/CD

- *Configuración de CI/CD*: Implementada utilizando Azure DevOps para despliegue en Azure.

### Seguridad

- *Autenticación y Autorización*: Implementado para asegurar el acceso a las API mediante JWT

### Logging 

- *Mecanismo de Logging*: Se ha implementado un sistema de logging para monitorear las operaciones de los microservicios.

### Pruebas

- *Pruebas Unitarias*: Se han desarrollado pruebas unitarias para los endpoints principales.
- *Pruebas de Integración*: Se han implementado pruebas de integración para garantizar la comunicación adecuada entre los microservicios.

### Documentación

- *Swagger*: La API está documentada usando Swagger para facilitar la interacción y comprensión de los endpoints disponibles.

## Instalación y Ejecución

1. *Clonar el repositorio*:
    bash
    git clone https://github.com/leonardocanro/UltraGroup.git
    

2. *Construir y ejecutar los contenedores Docker*:
    bash
    docker-compose up --build
    

3. *Acceder a la API*: La documentación de Swagger estará disponible en http://localhost:5000/swagger.

## Contribuciones

Las contribuciones son bienvenidas. Para contribuir, por favor abre un issue o envía un pull request.

## Contacto

Para más información, por favor contacta a leonardo.9211@hotmail.com.

