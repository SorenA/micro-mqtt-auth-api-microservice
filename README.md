# Micro MQTT - Auth API Microservice

Microservice-based authentication API using MySQL as data-layer with ACL support for micro-mqtt-broker.

## Docker & Kubernetes

The broker was built with Kubernetes in mind, under the directory `/k8s` are sample deployment configurations.

Prebuilt images can be found at on Docker Hub at [sorena/micro-mqtt-auth-api-microservice](https://hub.docker.com/r/sorena/micro-mqtt-auth-api-microservice).

## Related projects

[Micro MQTT Broker](https://github.com/SorenA/micro-mqtt-broker) - Implementation of a micro MQTT broker based on Mosca MQTT with multiple authentication providers, TLS and ACL support.

[Micro MQTT Handshake API Microservice](https://github.com/SorenA/micro-mqtt-handshake-api-microservice) - Implementation of an API microservice that can be used to register and handshake new devices on-demand. Compatible with same database as this microservice.

## Configuration

Configuration is done through environment variables, in development the appsettings.Development.json file can be used.

Defaults for environment variables:

```env
ConnectionStrings__DefaultConnection=server=$(DB_HOST);port=3306;database=$(DB_DATABASE);uid=$(DB_USERNAME);password=$(DB_PASSWORD)
AuthToken=test-token
```

If no AuthToken is present, it will not be required from the micro-mqtt-broker requests, allowing open access.

## Database

The folder `/sql` contains scripts for creating the tables with minimum needed fields, and a seed script for example data.

The passwords for the example users can be read in the seed script comments.

## Development

Copy `appsettings.Development.json.example` to  `appsettings.Development.json` and configure it to your local environment.

### Built with NuGet packages

- [BCrypt.Net-Core](https://github.com/neoKushan/BCrypt.Net-Core)