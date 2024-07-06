# SisteCreditoAPI

Prueba técnica

1.Clonar repositorio https://github.com/estebanquiroz94/SisteCreditoAPI.git
2.Ejecutar script Creación Esquema BD.sql para la creación de  base de datos y las tablas. 
3.En el archivo AppSettings.json se debe actualizar el valor del "Server" por el correspondiente al equipo donde se encuentra la BD.
5.Al ejecutar el proyecto, la API cargará la interfaz Swagger donde se evidencian los métodos disponibles con sus respectivos verbos y parámetros requeridos. 
6. El proyecto se encuentra estructurado bajo la arquitectura exagonal, el patron Repository implementado y autenticación mediante Bearer Token
7. Se implentó proceso de Autenticación mediante Bearer Token, el cual se obtiene desde la petición "GetToken" con los valores brindados. Actualmente se hace la autenticación contra los valores del Appsettings.
8. Para la autenticación se puede modificar el tiempo (minutos) de expiración del token en la propiedad Authentication:TokenConfiguration:Expire  
9. Se suministra archivo excel con 2 pestañas con los datos base para la homologación de datos. Adjunto en correo electrónico.

PENDIENTES
1. Implementación de pruebas unitarias
2. Aplicar reglas de negocio al momento de realizar el registro y aprobación de horas extras
3. Entregable DockerCompose.