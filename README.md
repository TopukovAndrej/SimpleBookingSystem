# Simple booking system
A simple booking system that allows users to book resources for a chosen period.

## Techical stack
- Frontend: Angular
- Backend: C# (ASP.NET Core Web API)
- Database: SQLite
- ORM: Entity Framework Core
- Testing: NUnit

## Features
- Users can see resources fetched from the database
- Users can click on the Book button, add required data and book a resource
- Booking duration overlapping and request data are validated by backend
- Mocks email sending when a resource has been successfully booked

## Dependencies
### Backend
- .NET 8+

### Frontend
- Node.js version: 20.11.1
- Angular CLI: 19.1.8

## Notes
- Applies specification pattern to check for booking overlaps
- Follows Domain Driven Design principles
- Applies Command Query Responsibility Segregation (CQRS) pattern with MediatR
