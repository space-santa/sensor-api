#!/bin/bash

bash wait-for-it/wait-for-it.sh -t 600 sensor-api-db:5432 -- echo "postgres is up"
dotnet ef database update
