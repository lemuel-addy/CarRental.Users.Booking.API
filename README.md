

# ASP.NET Core 7 API - Car Rental Booking System
This ASP.NET Core application API project implements a Car Rental Booking system which allows Admins to create bookings for renting cars and allow Users to book cars which are available to rent out. 
The API provides various endpoints for Users to sign up, authenticate, view all their booking forms, create and delete booking forms, as well as update their previous booking forms. 
Endpoints are also provided for Admins to sign up, authenticate, view all available bookings, create and delete these bookings, as well as update available car bookings.
The project is built using modern best practices and technologies, including JWT authentication for secure user login and caching with Redis for enhanced speed and reduced downtime.

## Getting Started
To run the project locally, follow these steps:

1. Clone the repository: git clone https://github.com/lemuel-addy/CarRental.Users.Booking.API
2. Navigate to the project directory: cd CarRental.Users.Booking.API
3. Restore the project dependencies: dotnet restore
4. Update the database connection string in the appsettings.json file.
5. Apply database migrations: dotnet ef database update
6. Start the application: dotnet run
7. The API will be accessible at http://localhost:7082.
  
## API Endpoints
The API provides the following endpoints:

<img width="1436" alt="Screenshot 2023-09-24 at 10 15 54 AM" src="https://github.com/lemuel-addy/CarRental.Users.Booking.API/assets/98181554/e569c10a-83c7-4473-8510-c0665add900f">

These allow Admins to get all bookings, get booking by id, as well as create, update, and delete bookings.
These endpoints require Admin role authorization using JWT authentication claims.

<img width="1436" alt="Screenshot 2023-09-24 at 10 16 04 AM" src="https://github.com/lemuel-addy/CarRental.Users.Booking.API/assets/98181554/0c10232b-2d0c-416d-8e52-df70853fbafa">
These endpoints allow both Users and Admins to sign up and login with authentication using JWT bearer token.

<img width="1436" alt="Screenshot 2023-09-24 at 10 20 52 AM" src="https://github.com/lemuel-addy/CarRental.Users.Booking.API/assets/98181554/12926c79-a75a-4313-a2ab-b6f7f13a9a0c">
These allow Users to get all book forms, get book form by id, as well as create, update, and delete book forms.

## Authentication and Authorization
This API uses JWT (JSON Web Token) for authentication. Users are required to include their JWT token in the Authorization header of each protected request using the Bearer scheme.

## Database
The project uses a database to store user accounts, booking forms, and booking details. The default database provider is Postgres SQL, but you can configure it to use a different provider by modifying the appsettings.json file.

## Dependencies
The project relies on the following major dependencies:

ASP.NET Core 7
Entity Framework Core
JWT Bearer Authentication
Swagger UI for API documentation
Redis for caching

## Contributing
Contributions to this project are welcome! If you find any issues or have suggestions for improvements, please create a new issue or submit a pull request
