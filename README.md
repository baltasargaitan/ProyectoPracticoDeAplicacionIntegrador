# RedSismica_EF_SQLite

## Descripción

Esta aplicación es un sistema de gestión de órdenes de inspección para estaciones sismológicas. Permite al Responsable de Inspecciones cerrar órdenes, registrar observaciones, motivos de baja de sismógrafos y notificar a los responsables de reparaciones.  
El flujo principal está alineado con el caso de uso "Dar cierre a orden de inspección de ES".

---

## Requisitos previos

- [.NET 6 SDK o superior](https://dotnet.microsoft.com/download/dotnet/6.0) (necesario para ejecutar la aplicación)
- Git (para clonar el repositorio)
- (Opcional) Visual Studio Code o Visual Studio (para editar el código)
- (Opcional) SQLite Browser (para inspeccionar la base de datos)

---

## Instalación y ejecución

### **1. Clonar el repositorio**
Abre la terminal y ejecuta los siguientes comandos:
```sh
git clone <URL_DEL_REPO>
cd RedSismica_EF_SQLite
```

### **2. Restaurar los paquetes NuGet**
Ejecuta el siguiente comando para instalar las dependencias necesarias:
```sh
dotnet restore
```

### **3. (Opcional) Eliminar la base de datos anterior**
Si existe el archivo `RedSismica.db`, bórralo para empezar limpio:
```sh
del RedSismica.db
```
También puedes eliminarlo manualmente desde el explorador de archivos.

### **4. Aplicar las migraciones y crear la base de datos** //NO LO HAGAN, PUEDE FALLAR EL RUN DESPUES
Ejecuta el siguiente comando para inicializar la base de datos:
```sh
dotnet ef database update
```

### **5. Ejecutar la aplicación**
Ejecuta el siguiente comando para iniciar la aplicación:
```sh
dotnet run
```

### **6. Abrir el navegador**
Ve a [http://localhost:5000](http://localhost:5000) (o el puerto que indique la consola).

---

## ¿Qué hace la app?

- Muestra una pantalla inicial para seleccionar la acción principal.
- Permite ver y cerrar órdenes de inspección pendientes del empleado logueado.
- Al cerrar una orden, solicita observación y motivos de baja del sismógrafo.
- Cambia el estado del sismógrafo y registra el cierre con fecha y responsable.
- Simula notificaciones a responsables de reparaciones (por consola).

---

## Estructura principal

- **Controllers/OrdenInspeccionController.cs**: Lógica de negocio y navegación.
- **Views/OrdenInspeccion/**: Vistas para seleccionar acción, listar órdenes, cerrar orden, etc.
- **Models/**: Entidades principales (OrdenInspeccion, Empleado, EstacionSismologica, etc.)
- **Data/DbInitializer.cs**: Inicialización de datos de ejemplo.

---

## Notas importantes

- El sistema simula el usuario logueado como el empleado con ID 1.
- Puedes modificar los datos iniciales en `DbInitializer.cs` si lo deseas.
- Si tienes problemas con la base de datos, elimina `RedSismica.db` y repite los pasos de migración.
- La aplicación utiliza SQLite como base de datos, que se genera automáticamente en el archivo `RedSismica.db`.

---

## Contacto

Si tienes dudas o necesitas ayuda, contacta al responsable del repositorio.

---
