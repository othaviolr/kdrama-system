#!/bin/bash

set -e

echo "🚀 Iniciando deploy da API KDrama System..."

RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' 

log() {
    echo -e "${GREEN}[$(date +'%Y-%m-%d %H:%M:%S')] $1${NC}"
}

warning() {
    echo -e "${YELLOW}[$(date +'%Y-%m-%d %H:%M:%S')] $1${NC}"
}

error() {
    echo -e "${RED}[$(date +'%Y-%m-%d %H:%M:%S')] $1${NC}"
}

if [ ! -f .env ]; then
    error "Arquivo .env não encontrado!"
    warning "Crie o arquivo .env baseado no .env.example"
    exit 1
fi

log "Carregando variáveis de ambiente..."
source .env

if [ -z "$JWT_SECRET" ] || [ -z "$DB_PASSWORD" ]; then
    error "Variáveis JWT_SECRET e DB_PASSWORD são obrigatórias no arquivo .env"
    exit 1
fi

log "Parando containers existentes..."
docker-compose down --remove-orphans || true

warning "Removendo imagens antigas..."
docker image prune -f || true

log "Construindo nova imagem..."
docker-compose build --no-cache

log "Testando conexão com o banco de dados..."
docker run --rm --env-file .env $(docker-compose config --services | head -n1) \
    dotnet --version > /dev/null 2>&1 || {
    error "Erro ao testar a imagem"
    exit 1
}

log "Iniciando serviços..."
docker-compose up -d

log "Aguardando API ficar disponível..."
for i in {1..30}; do
    if curl -s http://localhost:8080/health > /dev/null; then
        log "✅ API está respondendo!"
        break
    fi
    echo -n "."
    sleep 2
done

if ! curl -s http://localhost:8080/health > /dev/null; then
    error "❌ API não conseguiu inicializar corretamente"
    echo "Logs do container:"
    docker-compose logs kdrama-api
    exit 1
fi

log "📊 Status dos serviços:"
docker-compose ps

log "🎉 Deploy concluído com sucesso!"
log "🔗 API disponível em: http://localhost:8080"
log "🔗 Health check: http://localhost:8080/health"
log "🔗 Swagger (se habilitado): http://localhost:8080/swagger"

read -p "Deseja ver os logs em tempo real? (y/n): " -n 1 -r
echo
if [[ $REPLY =~ ^[Yy]$ ]]; then
    docker-compose logs -f
fi