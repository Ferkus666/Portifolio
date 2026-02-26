--------||KITCHEN ROUTING SYSTEM (THE OPERATOR V.1)||--------

-DESCRIPTION

This project implements a restaurant order routing system.
Orders sent from multiple POS (Point of Sale) terminals are received via HTTP and routed to specific kitchen queues, according to the area responsible for preparing each item.
The system was developed in ASP.NET Core Web API (.NET 6), using in-memory storage, focusing on code clarity, safe concurrency, and separation of responsibilities.

→ARCHITECTURE AND ORGANIZATION←

Main project structure:

KitchenRoutingSystem

Controllers -> HTTP Endpoints
Contracts -> DTOs (API input and output)
Domain -> Domain models (immutable)
Services -> Application logic (routing)
Infrastructure -> In-memory storage (queues)
Program.cs -> Application configuration

||Main architectural decisions||

DTOs are used to decouple the API contract from the domain.
The domain is immutable, ensuring predictability and security.
State in memory is handled with thread-safe structures.
The controller only coordinates the HTTP flow, without containing business rules.

→KITCHEN AREAS←

Kitchen areas are represented by an enum: (0 - 4)

Fries
Grill
Salad
Drink
Dessert

→CONCURRENCY AND STORAGE←

Kitchen queues are kept in memory.

ConcurrentDictionary was used to map kitchen areas.
Each area has a ConcurrentQueue to ensure safety in concurrent scenarios.
Storage is registered as Singleton, allowing all requests to share the same state.

→→SWAGGER TESTS←←

AVAILABLE ENDPOINTS

_______________________
Create Order
Endpoint: POST /orders

Area:

0 = Fries
1 = Grill
2 = Salad
3 = Drink
4 = Dessert

name: Name of the order item ex: Burger, Fries, Soda, etc. Example Request:

{
"items": [
{ "name": "Burger", "area": 1 },
{ "name": "Fries", "area": 0 }
]
}

Example Response:

{
"orderId": "guid",
"itemsRouted": 2
}

The API returns status 202 (Accepted), indicating that the order was received and routed to the correct queues.

_______________________

_______________________
Query existing kitchen queues Endpoint: GET /orders/queues

Example Response:

{
"Grill": 1,
"Fries": 1
}


This endpoint is used for debugging, routing validation, and integration testing.

______________________

→→VALIDATION←←

The API uses Data Annotations for automatic data validation.
Invalid requests return a 400 (Bad Request) error with details of the error (message).
Data binding is done directly from the request body.

--TESTS--

The project has a separate test project, containing:
Unit tests to validate the routing logic without HTTP dependency.
Integration tests to validate the API endpoints using WebApplicationFactory.

The tests ensure that:
Items are correctly routed to the kitchen queues.
The order creation endpoint responds correctly.
The API validations work as expected.

⇨⇨⇨⇨⇨⇨⇨⇨⇨⇨⇨ HOW TO RUN THE PROJECT ⇦⇦⇦⇦⇦⇦⇦⇦⇦⇦⇦⇦⇦

Run the application:

dotnet run

The API will be available in the browser through Swagger.

→→Run the tests:

dotnet test

----||FINAL CONSIDERATIONS||----

This project was developed focusing on:

Code readability
Separation of responsibilities
Secure concurrency
Ease of maintenance and evolution
The current structure allows for future improvements, such as:
Database persistence
New routing rules
Monitoring and metrics
Expansion of the test suite