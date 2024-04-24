# AM.Cars

## Requirements
1. .Net 8.0
2. docker

## Server

### Set passwords in .env file
Example .env file:
MSSQL_SA_PASSWORD=yourStrong(!)Password
SSL_CERTIFICATE_PASSWORD=12345

### Generate ssl certificate
1. Open a terminal and navigate to the AM.Cars.Server root directory.

2. Run the following commands to generate certificate:
	- for /f "tokens=2 delims==" %a in ('findstr "SSL_CERTIFICATE_PASSWORD=" .env') do set SSL_CERTIFICATE_PASSWORD=%a
	- dotnet dev-certs https -ep ./Certificates/am_cert.pfx -p %SSL_CERTIFICATE_PASSWORD%
	- dotnet dev-certs https --trust

### Running server with Docker Compose
1. Install Docker and Docker Compose on your computer if they are not installed.

2. Open a terminal and navigate to the AM.Cars.Server root directory.

3. Run the following command to start the application using Docker Compose:
	- docker-compose up --build

## Client 

### Build client desktop Application
1. Install .Net 8 on your computer if they are not installed.

2. Open a terminal and navigate to the AM.Cars.Client root directory.

3. Run the following command to build the application:
	- dotnet publish -c Release -o app
	
### Run client Application
1. Open app folder

2. Run AM.Cars.Client.exe