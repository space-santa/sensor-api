.PHONY: prod-down
prod-down:
	docker-compose -f docker/docker-compose-files/docker-compose.production.yml down

.PHONY: prod-up
prod-up:
	docker-compose -f docker/docker-compose-files/docker-compose.production.yml up -d

.PHONY: prod-build
prod-build:
	docker-compose -f docker/docker-compose-files/docker-compose.production.yml build

.PHONY: db-up
db-up:
	docker-compose -f docker/docker-compose-files/db.yml up

.PHONY: db-down
db-down:
	docker-compose -f docker/docker-compose-files/db.yml down
