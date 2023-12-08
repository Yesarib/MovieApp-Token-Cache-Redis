# MovieApp Token Cache Redis

This is a sample API project developed with .NET Core, featuring JWT token authentication, caching, and Redis integration. The project serves as a mini MovieApp to showcase these functionalities.

### Features
1. JWT Token Authentication: Secure your API with JSON Web Token (JWT) authentication.
2. Caching: Implement caching to enhance performance by storing frequently accessed data in-memory.
3. Redis Integration: Utilize Redis as a distributed caching solution to enhance scalability and reduce load on the primary database.

### Setup
1. Clone The Repository
2. Configure Database:  
Ensure that SqlServer is running and accessible. Update the SQL connection details in the appsettings.json file:  
```` "SqlServer" :"your sql server connection string" ````
4. Configure Redis:  
Ensure that Redis is running and accessible. Update the Redis connection details in the appsettings.json file:  
   ``` "CacheOptions": {`"Url": "yoururl:port"  }```

### Usage
1. Obtain JWT Token:  
Authenticate and obtain a JWT token by making a POST request to /api/auth/createtoken with valid credentials.
2. Access Movie Data:
  Use the obtained JWT token to access movie data by including it in the Authorization header of your requests:
      ```
     GET /api/movies
    Authorization: Bearer your-jwt-token  
    ```
3. Access Movie Data With Redis:
   Use the obtained JWT token to access movie data by including it in the Authorization header of your requests:
     ```
     GET /api/getMoviesFromRedis
    Authorization: Bearer your-jwt-token  
    ```
