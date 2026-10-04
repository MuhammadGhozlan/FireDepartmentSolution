# FireTime API implementation guide

## Status

This is a development-only first pass over the DTO-backed tables. The controllers are blocked with HTTP 503 outside the ASP.NET Core Development environment until authorization, ownership, and deployment rules are added. Do not point the Development connection string at production data.

## Local configuration

The scaffolded SQL Server connection string was removed from `IisFireTimeContext`. Configure `ConnectionStrings:DefaultConnection` through Visual Studio user secrets or an environment variable, for example:

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=YOUR_LOCAL_SERVER;Database=IIS_FireTime;Trusted_Connection=True;TrustServerCertificate=True" --project FireTime/FireTime.csproj
```

Replace the placeholder with a local SQL Server connection. No connection string or credentials are committed.

Leave and transfer creation looks up the initial approval status by `Workflow:PendingApprovalStatusCode` (default: `Pending`). Set this to the actual code in your local `ApprovalStatus` table if different.

## Routes

- `GET /api/attendance/getAttendance?roic=...&date=YYYY-MM-DD` lists one employee's non-deleted attendance on a date.
- `POST /api/attendance/takeAttendance` takes an `AttendanceRequest`. The assignment must belong to the ROIC and cover the attendance date; the status must exist. Standard attendance CRUD routes are also available at `/api/attendance`.
- `GET /api/employees` returns summary DTOs; `GET /api/employees/{roic}` returns a detail DTO. `POST` creates and `PUT /{roic}` updates identity-safe employee fields. There is no employee delete endpoint.
- `GET /api/leave-requests`, `GET /{id}`, `POST /{requesterRoic}`, and `PUT /{id}` support pending leave requests. `POST` sets the requester, submit date, and pending status on the server. Only pending requests can be edited; approval actions are not implemented.
- `GET /api/transfer-requests`, `GET /{id}`, `POST /{requesterRoic}`, and `PUT /{id}` follow the same pending-only rule for transfer requests.
- The remaining 26 DTO-backed resources use `GET /api/{resource}?offset=0&limit=50` (limit 1-200), `GET /{id}`, `POST`, and `PUT /{id}`. `DELETE /{id}` is available only for entities with `DeletedInd`; it sets that flag rather than deleting the row. Lookup tables return 405 for DELETE. Resource routes are the kebab-case plurals shown in the controller attributes.

## Layers

Controllers translate HTTP requests and errors. Service interfaces and implementations apply DTO mapping and workflow checks. Repository interfaces and EF Core implementations query and persist the scaffolded models. `Program.cs` registers the services and SQL Server context.

Audit columns on new or changed auditable rows are set by the service/repository layer. During local unauthenticated development the actor is `local-development`; this must be replaced by a verified identity before deployment.

## Tests and remaining work

Unit tests have not been added to the branch yet. Add tests for generic DTO mappings, Attendance validation, Employee identity handling, and pending-request behavior. Database integration tests are also needed for foreign-key, unique-key, and concurrency behavior. Approval, payroll export, ownership, role authorization, and domain-specific validation must be designed before using these writes against shared data.
