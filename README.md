Web Analytics Data Aggregator

This backend system reads website analytics and performance data, processes it through a real message broker, and exposes aggregated reporting via JWT-protected APIs.

🐳 Build / Run Services with Docker

Requirements:

Docker and Docker Compose installed

Docker daemon running

Steps:

Make sure docker-compose.yml and the project Dockerfile are in the same folder as in the repository.

Navigate to the project solution directory in your terminal.

Run the following command to build and start all services:

docker compose up --build -d


If successful, the following services should be up and running:

API service

Message Broker (RabbitMQ / Kafka)

MSSQL Server

📂 Seed / Read JSON Data

There is a bash script mock_data_generator.sh in the solution root that generates mock JSON data:

GA: ga_data.json

PSI: psi_data.json

Note:

The repository already includes pre-generated files:

ElectroQuest/AnalyticData/ga_data.json  
ElectroQuest/AnalyticData/psi_data.json


Alternative / Custom Data:

You can provide your own ga_data.json and psi_data.json.

Place them in the directory:

ElectroQuest/AnalyticData/


The application reads the file paths from appsettings.json.

To run the seeding script:

bash mock_data_generator.sh

⚡ Access Swagger + JWT Authentication

The Web API is available at:

http://localhost:5050/swagger


Endpoints:

No Authorization required:

POST /api/Auth/register → register a new account

POST /api/Auth/login → login with email & password

JWT Authorization required:

GET /api/Reports/Overview → aggregated totals across all pages & dates

GET /api/Reports/Pages → totals grouped by page

POST /api/Reports/ResetAnalytics → clear previous analytics data

POST /api/Reports/StartAnalytics → trigger (unblocking) background analytics processing

🔑 How to get a JWT Token

Register an account using /api/Auth/register.

Response message: "Login Please"

Login with /api/Auth/login using your credentials.

Response contains your JWT token.

In Swagger, click Authorize and enter:

Bearer <your-token>


All endpoints requiring authorization will now be accessible.

🛠 Analytics Controlling API

/api/Reports/ResetAnalytics → deletes previous analytics from the database.

/api/Reports/StartAnalytics → triggers the background analytics process.

Required after first API startup ( if the database is empty and need to be populated ) to control when processing begins.

Background process still runs automatically, but this endpoint allows controlled triggering.

🗄 Database Migration

EF Core migrations are automatically applied when the application starts.

🚀 Quick Start Example

Build & run services:

docker compose up --build -d

use the pre-generated ga_data.json and psi_data.json in AnalyticsData/
OR : 
if you want to generate your own , you have to place the json files like this " ga_data.json and psi_data.json in AnalyticsData/ "  
AND BUILD AGAIN 
    it should be more dynamic than that , but this is prototype .

Register & get JWT token via Swagger:

Register: /api/Auth/register

Login: /api/Auth/login → copy token

Authorize in Swagger: Bearer <token>

Start analytics processing:

POST /api/Reports/StartAnalytics


Access aggregated reports:

/api/Reports/Overview

/api/Reports/Pages
