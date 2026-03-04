# WebLearn (ASP.NET Core Web API + EF Core + PostgreSQL)

A small training project for a course platform built with **ASP.NET Core Web API** and **Entity Framework Core** using **PostgreSQL**.  
The main goal is to practice building REST APIs while focusing on:
- EF Core (relationships, includes, migrations)
- DTOs
- AutoMapper
- Validation (FluentValidation)
- Designing real-world endpoints for a course platform

---

## Features
- Manage **Students / Teachers / Courses / Enrollments**
- Database relationships:
  - Teacher (1) → (Many) Courses
  - Student (Many) ↔ (Many) Course through Enrollments (with `Grade` + `EnrolledAt`)
- Swagger UI for API testing
- AutoMapper for mapping between Entities and DTOs
- FluentValidation for request validation

---

## Tech Stack
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- AutoMapper
- FluentValidation
- Swagger (OpenAPI)

---

## Database Schema (Tables)
- `teachers`: teachers data
- `students`: students data
- `courses`: courses (linked to teachers via `TeacherId`)
- `enrollments`: student enrollments in courses (`StudentId`, `CourseId`, `Grade`, `EnrolledAt`)




