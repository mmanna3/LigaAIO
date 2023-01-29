# Subir localhost a internet
- Descargar ngrok (este mismo mail)
- Ejecutar: ngrok http 8080 -host-header="localhost:8080"

# Restaurar backup
- cd LigaAIO\Liga\LigaSoft\Utilidades\Backup\Recursos
- ./SchemaZen.exe create --server '(LocalDb)\MSSQLLocalDB' --database edefi-prod-290123 --scriptDir C:\Users\matia\Downloads\BaseDeDatos-2023-01-29

# Compilar web pública para pegarle IIS Express
- cd WebPublica
- npm i
- tirar el comando del build-prod a mano porque desde consola no anda