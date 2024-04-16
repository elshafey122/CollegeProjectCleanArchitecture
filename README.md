## API school project is structured around Clean Architecture principles, emphasizing modularity, separation of concerns, and maintainability. 
## key components:
- CQRS Design Pattern: The Command Query Responsibility Segregation pattern separates read and write operations, enhancing scalability and flexibility.
- Generic Repository Design Pattern: Implements a generic repository pattern for data access, promoting code reuse and abstraction over specific data storage implementations.
- Pagination Schema: Incorporates a standardized pagination schema for handling large datasets efficiently.
- Localization of Data and Responses: Supports localization for both data and API responses, catering to diverse language preferences.
- Fluent Validations: Utilizes Fluent Validation library for robust input validation, ensuring data integrity and security.
- Configurations Using Data Annotations and Fluent API: Configures data models and relationships using data annotations and Fluent API, providing flexibility in defining database schema.
- Endpoints of Operations: Defines clear and concise endpoints for various CRUD (Create, Read, Update, Delete) operations, promoting API discoverability and usability.
- CORS Configuration: Allows Cross-Origin Resource Sharing (CORS) to enable web clients from different origins to access the API securely.
- Identity and Authentication: Integrates ASP.NET Core Identity for user authentication and management, ensuring secure access to API resources.
- JWT Token and SwaggerGen Integration: Implements JSON Web Token (JWT) authentication for secure API access and integrates SwaggerGen for API documentation and testing.
- Authorization with Roles and Claims: Utilizes role-based and claims-based authorization to control access to API endpoints based on user roles and permissions.
- Services for Sending Emails and Uploading Images: Implements service layers for sending emails and handling image uploads, encapsulating business logic and promoting code organization.
- Filters: Utilizes action filters for cross-cutting concerns such as logging, exception handling, and input validation.
- Database Operations Endpoint: Exposes endpoints for executing database operations such as querying views, calling stored procedures, and invoking database functions.
- Logging: Implements logging mechanisms to capture runtime information and errors, aiding in debugging and monitoring application health.
