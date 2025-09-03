#!/bin/bash

# Script de deploy para VPS
set -e

echo "🚀 Iniciando deploy da API KDrama System..."

# Cores para output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Função para log colorido
log() {
    echo -e "${GREEN}[$(date +'%Y-%m-%d %H:%M:%S')] $1${NC}"
}

warning() {
    echo -e "${YELLOW}[$(date +'%Y-%m-%d %H:%M:%S')] $1${NC}"
}

error() {
    echo -e "${RED}[$(date +'%Y-%m-%d %H:%M:%S')] $1${NC}"
}

# Verificar se o arquivo .env existe
if [ ! -f .env ]; then
    error "Arquivo .env não encontrado!"
    warning "Crie o arquivo .env baseado no .env.example"
    exit 1
fi

log "Carregando variáveis de ambiente..."
source .env

# Verificar variáveis obrigatórias
if [ -z "$JWT_SECRET" ] || [ -z "$DB_PASSWORD" ]; then
    error "Variáveis JWT_SECRET e DB_PASSWORD são obrigatórias no arquivo .env"
    exit 1
fi

# Parar containers existentes
log "Parando containers existentes..."
docker-compose down --remove-orphans || true

# Remover imagens antigas (opcional)
warning "Removendo imagens antigas..."
docker image prune -f || true

# Build da nova imagem
log "Construindo nova imagem..."
docker-compose build --no-cache

# Testar se consegue conectar no banco
log "Testando conexão com o banco de dados..."
docker run --rm --env-file .env $(docker-compose config --services | head -n1) \
    dotnet --version > /dev/null 2>&1 || {
    error "Erro ao testar a imagem"
    exit 1
}

# Subir os serviços
log "Iniciando serviços..."
docker-compose up -d

# Aguardar a API ficar pronta
log "Aguardando API ficar disponível..."
for i in {1..30}; do
    if curl -s http://localhost:8080/health > /dev/null; then
        log "✅ API está respondendo!"
        break
    fi
    echo -n "."
    sleep 2
done

# Verificar se a API está rodando
if ! curl -s http://localhost:8080/health > /dev/null; then
    error "❌ API não conseguiu inicializar corretamente"
    echo "Logs do container:"
    docker-compose logs kdrama-api
    exit 1
fi

# Mostrar status
log "📊 Status dos serviços:"
docker-compose ps

log "🎉 Deploy concluído com sucesso!"
log "🔗 API disponível em: http://localhost:8080"
log "🔗 Health check: http://localhost:8080/health"
log "🔗 Swagger (se habilitado): http://localhost:8080/swagger"

# Mostrar logs em tempo real (opcional)
read -p "Deseja ver os logs em tempo real? (y/n): " -n 1 -r
echo
if [[ $REPLY =~ ^[Yy]$ ]]; then
    docker-compose logs -f
fi