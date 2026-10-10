# FireTime API implementation guide

## Status

The generic DTO-backed controllers are blocked with HTTP 503 outside the ASP.NET Core Development environment until authorization, ownership, and deployment rules are added. The dedicated Attendance controller is currently unauthenticated and is not covered by that guard. Use it only with local or approved test data until authorization is implemented. Do not point the Development connection string at production data.

## Local configuration

The scaffolded SQL Server connection string was removed from `IisFireTimeContext`. Configure `ConnectionStrings:DefaultConnection` through Visual Studio user secrets or an environment variable, for example:

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=YOUR_LOCAL_SERVER;Database=IIS_FireTime;Trusted_Connection=True;TrustServerCertificate=True" --project FireTime/FireTime.csproj
```

Replace the placeholder with a local SQL Server connection. No connection string or credentials are committed.

Leave and transfer creation looks up the initial approval status by `Workflow:PendingApprovalStatusCode` (default: `Pending`). Set this to the actual code in your local `ApprovalStatus` table if different.

## Routes

- `GET /api/attendance/getAttendance` lists all non-deleted attendance records. Use `POST /api/attendance/filterAttendance` with an `AttendanceFilterRequest` to filter records.
- `GET /api/attendance/getAttendanceStatuses` lists available attendance statuses.
- `GET /api/attendance/getAttendanceAssignments?roic=...&date=YYYY-MM-DD` lists the employee's non-deleted assignments covering the date, including their shift and work period.
- `GET /api/attendance/getAttendanceRoster?date=YYYY-MM-DD&workPeriodNbr=1` lists assignments covering the date and matching the requested work period. It includes employees with no attendance yet (empty `Attendances` list). Existing multiple records for an assignment/date are all returned rather than silently hidden.
- `POST /api/attendance/takeAttendance` takes a JSON array of `AttendanceRequest` objects. Each assignment must belong to the ROIC and cover the attendance date; the status must exist. Supply `EmployeeAssignmentId` when more than one assignment matches. A non-deleted record already present for that assignment/date, or a duplicate within the batch, is rejected.
- `PUT /api/attendance/updateAttendance/{id}` updates a record and rejects a move to an assignment/date already occupied by another non-deleted record. `DELETE /api/attendance/deleteAttendance/{id}` soft-deletes a record.
- `GET /api/employees` returns summary DTOs; `GET /api/employees/{roic}` returns a detail DTO. `POST` creates and `PUT /{roic}` updates identity-safe employee fields. There is no employee delete endpoint.
- `GET /api/leave-requests`, `GET /{id}`, `POST /{requesterRoic}`, and `PUT /{id}` support pending leave requests. `POST` sets the requester, submit date, and pending status on the server. Only pending requests can be edited; approval actions are not implemented.
- `GET /api/transfer-requests`, `GET /{id}`, `POST /{requesterRoic}`, and `PUT /{id}` follow the same pending-only rule for transfer requests.
- The remaining 26 DTO-backed resources use `GET /api/{resource}?offset=0&limit=50` (limit 1-200), `GET /{id}`, `POST`, and `PUT /{id}`. `DELETE /{id}` is available only for entities with `DeletedInd`; it sets that flag rather than deleting the row. Lookup tables return 405 for DELETE. Resource routes are the kebab-case plurals shown in the controller attributes.

## Layers

Controllers translate HTTP requests and errors. Service interfaces and implementations apply DTO mapping and workflow checks. Repository interfaces and EF Core implementations query and persist the scaffolded models. `Program.cs` registers the services and SQL Server context.

The old application's date-to-group mapping comes from its `Calendar` table, which is not present in the new database. Until that mapping is migrated, the caller must supply `WorkPeriodNbr` (1-14) to the roster endpoint; it does not infer the period from the date. Attendance creation and updates use a serializable transaction and an application-level duplicate check. A database unique constraint is still advisable if other writers can modify Attendance directly.

Audit columns on new or changed auditable rows are set by the service/repository layer. The generic resources use `local-development` as their local actor; Attendance currently uses `System`. Both must be replaced by a verified identity before deployment.

## Tests and remaining work

One existing service unit test is present. Add tests for Attendance roster period filtering, unmarked employees, assignment selection, duplicate prevention, and update behavior, plus generic DTO mappings, Employee identity handling, and pending-request behavior. Database integration tests are also needed for foreign-key, unique-key, and concurrency behavior. Approval, payroll export, ownership, role authorization, and domain-specific validation must be designed before using these writes against shared data.
