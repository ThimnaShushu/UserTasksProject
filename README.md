# UserTasksProject


A simple **ASP.NET Core Web API** for managing **Users** and **Tasks**, with CRUD operations, filtering, and optional authentication.  
This project uses **Entity Framework Core** with **SQLite** and integrates **Swagger** for interactive API documentation.

---

## Table of Contents

- [Features](#features)  
- [Technologies](#technologies)  
- [Getting Started](#getting-started)  
- [Database Setup](#database-setup)  
- [Running the API](#running-the-api)  
- [API Endpoints](#api-endpoints)  
- [Filtering Tasks](#filtering-tasks)  
- [Authentication (Optional)](#authentication-optional)  
- [Swagger UI](#swagger-ui)  
- [Contributing](#contributing)  
- [License](#license)  

---

## Features

- CRUD operations for **Users** and **Tasks**  
- Tasks filtering:
  - Expired tasks  
  - Active tasks  
  - Tasks by due date  
  - Tasks by assigned user  
- Optional authentication:
  - API Key  
  - JWT Bearer token  
- Swagger UI for interactive API documentation  

---

## Technologies

- ASP.NET Core Web API  
- C# 11  
- Entity Framework Core  
- SQLite (for local development)  
- Swagger / Swashbuckle  

---

## Getting Started

### Prerequisites

- [.NET SDK 7+](https://dotnet.microsoft.com/download)  
- SQLite installed (optional, SQLite file included)  
- IDE: Visual Studio / VS Code  

---

## Database Setup

1. Add connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=usertasks.db"
}
