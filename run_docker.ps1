param(
    [int] $Port = 4000
)

$env:DOCS_PORT = $Port
docker compose --file (Join-Path $PSScriptRoot "docker-compose.yml") up
exit $LASTEXITCODE
