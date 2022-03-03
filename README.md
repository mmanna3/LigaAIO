# Subir localhost a internet
- Descargar ngrok (este mismo mail)
- Ejecutar: ngrok http 8080 -host-header="localhost:8080"

# Restaurar backup
- SchemaZen.exe create --server localhost --database db --scriptDir c:\somedir

# Compilar web pública para pegarle IIS Express
- cd WebPublica
- npm i
- tirar el comando del build-prod a mano porque desde consola no anda