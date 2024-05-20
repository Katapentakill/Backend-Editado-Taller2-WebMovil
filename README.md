# Backend Taller de Introducción al Desarrollo Web/Móvil

### Por DarÍo Contreras Abaca
****
## INSTALACIÓN:
Debes instalar [Visual Studio Code](https://code.visualstudio.com/) y el [SDK .NET7](https://dotnet.microsoft.com/es-es/download/dotnet/7.0).

Para comenzar la instalación, debes abrir Visual Studio Code, ir a File -> Open Folder, y seleccionar carpeta en donde quieres clonar el proyecto.

Ir a Terminal -> New Terminal para abrir una nueva terminal.

Ejecutar los siguientes comandos en orden: 

```bash
git clone https://github.com/IDWM/project-dotnet7-api
```

```bash
dotnet restore
```
****
## ClOUDINARY:
El proyecto hace uso de los servicios de Cloudinary, por lo que es necesario registrarse en la [página de Cloudinary](https://cloudinary.com/) para obtener las credenciales del servicio.
Las credenciales necesarias para el proyecto son Cloud name, API key y API Secret.
Una vez obtenidas estas credenciales, se deben escribir en el archivo "appsettings.json" de la siguiente forma:

```json
{
  "AppSettings":{
    "Token": ""
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "CloudinarySettings":{
    "CloudName":"*INGRESAR CLOUDNAME",
    "ApiKey":"*INGRESAR APIKEY*",
    "ApiSecret":"*INGRESAR APISECRET*"
  }
}
```

****
## INCIAR SISTEMA:
En Visual Studio Code, ir a File -> Open Folder, y seleccionar la carpeta project-dotnet7-api.

Ir a Terminal -> New Terminal para abrir una nueva terminal.

Ejecutar el siguiente comando:

```bash
dotnet run
```

****
## Postman

Para probar el backend usando "postman-file", necesitas instalar [Postman](https://www.postman.com/downloads/).
Al abrir Postman y elegir un espacio de trabajo, debes hacer click en "Import" y seleccionar "TallerIDWM.postman_collection".
Asegurate que el puerto de las solicitudes coincida con el puerto de la ejecución.



