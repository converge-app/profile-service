#!/bin/bash

docker run --rm \
-p 27017-27019:27017-27019 \
-e MONGO_INITDB_ROOT_USERNAME=admin \
-e MONGO_INITDB_ROOT_PASSWORD=password \
-e MONGO_INITDB_DATABASE=ApplicationDb \
-v "/home/hermansen/git/converge/source/services/profile-service/mongodb:/docker-entrypoint-initdb.d/" \
--name mongodb mongo

