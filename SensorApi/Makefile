.PHONY: migrate-down
migrate-down:
	docker-compose -f docker/docker-compose-files/docker-compose.migrate.yml down

.PHONY: migrate-up
migrate-up:
	docker-compose -f docker/docker-compose-files/docker-compose.migrate.yml up

.PHONY: prod-down
prod-down:
	docker-compose -f docker/docker-compose-files/docker-compose.production.yml down

.PHONY: prod-up
prod-up:
	docker-compose -f docker/docker-compose-files/docker-compose.production.yml up -d

.PHONY: db-up
db-up:
	docker-compose -f docker/docker-compose-files/db.yml up

.PHONY: db-down
db-down:
	docker-compose -f docker/docker-compose-files/db.yml down

.PHONY: lprod-down
lprod-down:
	docker-compose -f docker/docker-compose-files/docker-compose.local-prod.yml down

.PHONY: lprod-up
lprod-up:
	docker-compose -f docker/docker-compose-files/docker-compose.local-prod.yml up

.PHONY: lprod-build
lprod-build:
	docker-compose -f docker/docker-compose-files/docker-compose.local-prod.yml build

.PHONY: build-api
build-api:
	docker build -t sensor-api -f docker/Dockerfile.production . && \
	docker image tag sensor-api neptune:5000/sensor-api

.PHONY: push-api
push-api:
	docker push neptune:5000/sensor-api

.PHONY: build-all
build-all: build-api

.PHONY: push-all
push-all: push-api

.PHONY: new-images
new-images: build-all push-all
