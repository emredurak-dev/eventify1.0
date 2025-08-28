# Eventify 1.0

Minimal calendar and event management (ASP.NET MVC 5 + EF6)

## Features
- Create, edit, delete events
- Drag & drop to update date/time
- FullCalendar-based modern calendar UI
- AdminLTE 3 and Bootstrap 5 styling

## Stack
- ASP.NET MVC 5 (System.Web)
- Entity Framework 6 (Code First, Migrations)
- SQL Server (LocalDB by default)
- jQuery 3, FullCalendar, AdminLTE 3, Bootstrap 5

## Structure
- `Controllers/EventController.cs`: Event CRUD + JSON endpoints
- `Entities/Event.cs`: Event entity model
- `Context/EventContext.cs`: EF `DbContext` with `Events` set
- `Migrations/*`: Code First migrations for `Events` table
- `Views/Event/*`: Calendar page, modals, styles and scripts partials
- `Content/AdminLTE-3.1.0`: Theme and plugins
- `Scripts/*` and `Content/*`: Libraries and styles

## Quick Start
1) Restore NuGet packages (Visual Studio handles automatically)
2) Prepare database (optional, Code First will create it):
   - Package Manager Console:
     - `Update-Database`
3) Run the app (IIS Express)
4) Calendar page: `/Event/Index`

## Endpoints
- `GET /Event/GetEvents` — Calendar data (JSON)
- `POST /Event/CreateEvent` — Create new event
- `POST /Event/EditEvent` — Edit event
- `POST /Event/DeleteEvent` — Delete event
- `POST /Event/UpdateEventTime` — Update date/time (drag & drop)

## Notes
- Default route lives in `RouteConfig`; calendar uses `EventController.Index`
- Connection name: `EventContext`
- `Event.EventId` is identity (per migration)

## Flow
1) `Index.cshtml` loads calendar and external event components
2) `FullCalendar` fetches events via `GetEvents`
3) Create/edit/delete via AJAX to JSON endpoints

---
Simple, fast, and extensible event calendar. Enjoy!
