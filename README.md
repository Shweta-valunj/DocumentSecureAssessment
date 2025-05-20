# Document Storage System (.NET Core + SQL Server)

Secure document storage backend with JWT authentication, versioned file uploads, and BLOB storage in SQL Server.

---

## Setup Instructions

1. Clone the repo:  
   `git clone https://github.com/Shweta-valunj/DocumentSecureAssessment.git`  
   `cd DocumentSecureAssessment`


2. Update `appsettings.json` with your SQL Server connection string and JWT settings.

3. Run database migrations:  
   `dotnet ef database update`

4. Run the app:  
   `dotnet run`

Access Swagger UI at: `https://localhost:7162/swagger`

---

## Authentication Flow

- Register: POST `/api/auth/register`  
- Login: POST `/api/auth/login` (returns JWT token)  
- Use JWT token in `Authorization: Bearer <token>` header for protected APIs.

---

## API Endpoints

- POST `/api/FileUpload/upload` — Upload file with versioning  
- GET `/api/FileUpload/{fileName}` — Download latest version  
- GET `/api/FileUpload/{fileName}?revision=1` — Download specific version

---

## Testing Instructions

Unit testing not implemented.

---
## AI Tools Used

ChatGPT (OpenAI) for coding help and documentation.
