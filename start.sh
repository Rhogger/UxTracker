#!/bin/bash

# Parar todos os containers
echo "Parando todos os containers..."
docker stop $(docker ps -aq)

# Remover todos os containers
echo "Removendo todos os containers..."
docker rm $(docker ps -aq)

# Remover todas as imagens
echo "Removendo todas as imagens..."
docker rmi $(docker images -q)

# Executar docker-compose build --no-cache
echo "Iniciando docker-compose up --build..."
docker-compose build --no-cache

# Executar docker-compose up --build
echo "Iniciando docker-compose up..."
docker-compose up
