#!/bin/bash

bash /app/wait-for-it.sh -t 600 $PG_HOST -- echo "postgres is up"
dotnet ef database update
