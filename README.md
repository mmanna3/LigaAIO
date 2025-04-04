# MAC

- Requisitos: Rider instalando Mono (o instalado por separado)
- Para levantar el proyecto, entrar a: Liga/LigaSoft y ejecutar el servidor xsp. (Probablemente esté en: /Library/Frameworks/Mono.framework/Versions/Current/bin/xsp). Después entrar a 0.0.0.0:9000/torneo y listo.

## BD

### Crear BD local

1- Instalar Docker

2- Descargar la imagen del contenedor
`docker pull mcr.microsoft.com/azure-sql-edge`

3- Creamos el contenedor
`docker run --cap-add SYS_PTRACE -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=Pas$word!39' -p 1433:1433 --name edefi-localhost -d mcr.microsoft.com/azure-sql-edge`

### Conectarse desde DataGrip

Conectarse desde DataGrip copiando esto en el campo url
`jdbc:sqlserver://localhost:1433;databaseName=edefi_dev;user=sa;password=Pas$word!39;encrypt=false;trustServerCertificate=true`

# WINDOWS

## Subir localhost a internet
- Descargar ngrok (este mismo mail)
- Ejecutar: ngrok http 8080 -host-header="localhost:8080"

## Restaurar backup
- cd LigaAIO\Liga\LigaSoft\Utilidades\Backup\Recursos
- ./SchemaZen.exe create --server '(LocalDb)\MSSQLLocalDB' --database edefi-prod-290123 --scriptDir C:\Users\matia\Downloads\BaseDeDatos-2023-01-29

## Compilar web pública para pegarle IIS Express
- cd WebPublica
- npm i
- tirar el comando del build-prod a mano porque desde consola no anda


