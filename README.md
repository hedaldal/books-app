# 📚 Books App

Full-stack book tracking app with a Vue 3 frontend and an ASP.NET Core Web API backend, implemented for Walkers technical assessment.

## Tech Stack

- Frontend: Vue 3 + TypeScript + Vuetify
- Backend: ASP.NET Core 8 Web API with in-memory repository

## Features

- Create, list, search, sort, and delete books
- View book details and update rating/comments
- Pagination with configurable page size (stored in local storage)
- Settings page to update profile display name and default My Books page size
- Analytics page showing total books, average rating, and rated books count
- Validation on both frontend and backend

## Project Structure

```text
books-app/
  backend/    # ASP.NET Core API
  frontend/   # Vue + Vite app
```

## 🚀 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/hedaldal/books-app.git
cd books-app
```

### 2. Start backend API:

```bash
cd backend
dotnet run --project src/Books.Api
```

Backend runs at `http://localhost:5000`.

### 3. In a new terminal, start frontend:

```bash
cd frontend
npm install
npm run dev
```

Frontend runs at `http://localhost:5173`.

## Testing

Backend tests:

```bash
cd backend
dotnet test
```

Frontend tests:

```bash
cd frontend
npm test
```

## API Notes

- Base route: `/api/books`
- `GET /api/books` supports `page`, `pageSize`, `search`, `sort`
- `pageSize` must be between `1` and `25`
- Maximum number of books: `25`
- Rating must be between `1` and `5`
- Comments are required when rating is provided
