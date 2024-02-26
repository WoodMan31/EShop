#!/bin/bash

# Define variables
CONTAINER_NAME="khai-eshop"
DB_NAME="eshop"
DB_PASSWORD="aStrongP@ssword"
DB_PORT="5499"
PGDATA_PATH="/var/lib/postgresql/static-data"

# Check if the container already exists and is running
if [ "$(docker ps -q -f name=^${CONTAINER_NAME}$)" ]; then
    echo "Container ${CONTAINER_NAME} is already running."
else
    # Check if the container exists but is stopped
    if [ "$(docker ps -aq -f name=^${CONTAINER_NAME}$)" ]; then
        # Remove the stopped container
        docker rm ${CONTAINER_NAME}
    fi

    # Run PostgreSQL container
    docker run -d \
        --name ${CONTAINER_NAME} \
        -e POSTGRES_DB=${DB_NAME} \
        -e POSTGRES_PASSWORD=${DB_PASSWORD} \
        -e PGDATA=${PGDATA_PATH} \
        -p ${DB_PORT}:5432 \
        -v "${PGDATA_PATH}:${PGDATA_PATH}" \
        postgres:16-alpine

    echo "PostgreSQL database ${DB_NAME} is running on port ${DB_PORT}"
fi
