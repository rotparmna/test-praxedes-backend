# test-praxedes-backend-solution

## Piezas de software

1. IdentityServer.API -> Este se encarga de generar los tokens de seguridad y de validarlos (autorización).
2. test-praxedes-backend-api -> Es la API que contiene los servicios solicitados en la prueba..
3. praxedestestdb -> Base de datos que contiene las estructuras y los datos para la prueba, se accede a los datos desde el API para consultarlos, insertarlos o actualizarlos por medio de procedimientos almacenados.

## Montaje de la aplicación

1. Crear base de datos con nombre praxedestestdb
2. Ejecutar el script de base de datos [database.sql](database.sql)
3. Validar la cadena de conexión hacia la base de datos en el proyecto test-praxedes-backend-api, [ConnectionStrings/DefaultConnection](test-praxedes-backend-api/appsettings.Development.json)
4. Lanzar el proyecto IdentityServer.API
5. Lanzar el proyecto test-praxedes-backend-api
6. Se puede usar la colección de postman [Postman](test-praxedes.postman_collection.json). Esta colección ya tiene configurado el OAuth2 para solicitar y validar tokens a IdentityServer.API

## Tokens

Cuando se lance el proyecto IdentityServer.API, se puede realizar solcitud de tokens con "client credentials" y la siguiente configuración:
* client_id: client
* client_secret: secret
* access_url_token: https://localhost:5001/connect/token
* scope: PraxedesBackendUser PraxedesBackendPost

El token generado debe enviarse en el header de las demas peticiones como Authentication