using FireTime.Interfaces.RepoInterfaces;
using FireTime.Interfaces.ServiceInterfaces;
using FireTime.Repositories;
using FireTime.Services;

namespace FireTime;

public static class CrudRegistration
{
    public static IServiceCollection AddCrudResources(this IServiceCollection services)
    {
        services.AddScoped<ICrudRepository<FireTime.Models.Address, int>, EfCrudRepository<FireTime.Models.Address, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.Contacts.AddressRequest, FireTime.Dtos.Contacts.AddressResponse, int>>(provider =>
            new CrudService<FireTime.Models.Address, FireTime.Dtos.Contacts.AddressRequest, FireTime.Dtos.Contacts.AddressResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.Address, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.Address
                {
                StreetAddressTxt = request.StreetAddressTxt,
                CityNme = request.CityNme,
                StateNme = request.StateNme,
                ZipCode = request.ZipCode,
                ContactTypeId = request.ContactTypeId,
                Roic = request.Roic,
                DistrictId = request.DistrictId,
                StationId = request.StationId,
                EmergencyContactId = request.EmergencyContactId
                },
                (entity, request) =>
                {
                entity.StreetAddressTxt = request.StreetAddressTxt;
                entity.CityNme = request.CityNme;
                entity.StateNme = request.StateNme;
                entity.ZipCode = request.ZipCode;
                entity.ContactTypeId = request.ContactTypeId;
                entity.Roic = request.Roic;
                entity.DistrictId = request.DistrictId;
                entity.StationId = request.StationId;
                entity.EmergencyContactId = request.EmergencyContactId;
                },
                entity => new FireTime.Dtos.Contacts.AddressResponse(entity.AddressId, entity.StreetAddressTxt, entity.CityNme, entity.StateNme, entity.ZipCode, entity.ContactTypeId, entity.Roic, entity.DistrictId, entity.StationId, entity.EmergencyContactId)));

        services.AddScoped<ICrudRepository<FireTime.Models.PhoneNumber, int>, EfCrudRepository<FireTime.Models.PhoneNumber, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.Contacts.PhoneNumberRequest, FireTime.Dtos.Contacts.PhoneNumberResponse, int>>(provider =>
            new CrudService<FireTime.Models.PhoneNumber, FireTime.Dtos.Contacts.PhoneNumberRequest, FireTime.Dtos.Contacts.PhoneNumberResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.PhoneNumber, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.PhoneNumber
                {
                PhoneNbr = request.PhoneNbr,
                ContactTypeId = request.ContactTypeId,
                Roic = request.Roic,
                DistrictId = request.DistrictId,
                StationId = request.StationId,
                EmergencyContactId = request.EmergencyContactId
                },
                (entity, request) =>
                {
                entity.PhoneNbr = request.PhoneNbr;
                entity.ContactTypeId = request.ContactTypeId;
                entity.Roic = request.Roic;
                entity.DistrictId = request.DistrictId;
                entity.StationId = request.StationId;
                entity.EmergencyContactId = request.EmergencyContactId;
                },
                entity => new FireTime.Dtos.Contacts.PhoneNumberResponse(entity.PhoneNumberId, entity.PhoneNbr, entity.ContactTypeId, entity.Roic, entity.DistrictId, entity.StationId, entity.EmergencyContactId)));

        services.AddScoped<ICrudRepository<FireTime.Models.EmailAddress, int>, EfCrudRepository<FireTime.Models.EmailAddress, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.Contacts.EmailAddressRequest, FireTime.Dtos.Contacts.EmailAddressResponse, int>>(provider =>
            new CrudService<FireTime.Models.EmailAddress, FireTime.Dtos.Contacts.EmailAddressRequest, FireTime.Dtos.Contacts.EmailAddressResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.EmailAddress, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.EmailAddress
                {
                EmailAddressTxt = request.EmailAddressTxt,
                ContactTypeId = request.ContactTypeId,
                Roic = request.Roic
                },
                (entity, request) =>
                {
                entity.EmailAddressTxt = request.EmailAddressTxt;
                entity.ContactTypeId = request.ContactTypeId;
                entity.Roic = request.Roic;
                },
                entity => new FireTime.Dtos.Contacts.EmailAddressResponse(entity.EmailAddressId, entity.EmailAddressTxt, entity.ContactTypeId, entity.Roic)));

        services.AddScoped<ICrudRepository<FireTime.Models.EmergencyContact, int>, EfCrudRepository<FireTime.Models.EmergencyContact, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.Contacts.EmergencyContactRequest, FireTime.Dtos.Contacts.EmergencyContactResponse, int>>(provider =>
            new CrudService<FireTime.Models.EmergencyContact, FireTime.Dtos.Contacts.EmergencyContactRequest, FireTime.Dtos.Contacts.EmergencyContactResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.EmergencyContact, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.EmergencyContact
                {
                EmergencyContactFname = request.EmergencyContactFname,
                EmergencyContactLname = request.EmergencyContactLname,
                ContactTypeId = request.ContactTypeId,
                AddressId = request.AddressId,
                PhoneNumberId = request.PhoneNumberId,
                Roic = request.Roic
                },
                (entity, request) =>
                {
                entity.EmergencyContactFname = request.EmergencyContactFname;
                entity.EmergencyContactLname = request.EmergencyContactLname;
                entity.ContactTypeId = request.ContactTypeId;
                entity.AddressId = request.AddressId;
                entity.PhoneNumberId = request.PhoneNumberId;
                entity.Roic = request.Roic;
                },
                entity => new FireTime.Dtos.Contacts.EmergencyContactResponse(entity.EmergencyContactId, entity.EmergencyContactFname, entity.EmergencyContactLname, entity.ContactTypeId, entity.AddressId, entity.PhoneNumberId, entity.Roic)));

        services.AddScoped<ICrudRepository<FireTime.Models.EmployeeAssignment, int>, EfCrudRepository<FireTime.Models.EmployeeAssignment, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.Employees.EmployeeAssignmentRequest, FireTime.Dtos.Employees.EmployeeAssignmentResponse, int>>(provider =>
            new CrudService<FireTime.Models.EmployeeAssignment, FireTime.Dtos.Employees.EmployeeAssignmentRequest, FireTime.Dtos.Employees.EmployeeAssignmentResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.EmployeeAssignment, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.EmployeeAssignment
                {
                AssignmentStartDate = request.AssignmentStartDate,
                AssignmentEndDate = request.AssignmentEndDate,
                IsTemp = request.IsTemp,
                Roic = request.Roic,
                CompanyPositionId = request.CompanyPositionId,
                WorkPeriodId = request.WorkPeriodId
                },
                (entity, request) =>
                {
                entity.AssignmentStartDate = request.AssignmentStartDate;
                entity.AssignmentEndDate = request.AssignmentEndDate;
                entity.IsTemp = request.IsTemp;
                entity.Roic = request.Roic;
                entity.CompanyPositionId = request.CompanyPositionId;
                entity.WorkPeriodId = request.WorkPeriodId;
                },
                entity => new FireTime.Dtos.Employees.EmployeeAssignmentResponse(entity.EmployeeAssignmentId, entity.AssignmentStartDate, entity.AssignmentEndDate, entity.IsTemp, entity.Roic, entity.CompanyPositionId, entity.WorkPeriodId)));

        services.AddScoped<ICrudRepository<FireTime.Models.LeaveTransaction, int>, EfCrudRepository<FireTime.Models.LeaveTransaction, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.Leave.LeaveTransactionRequest, FireTime.Dtos.Leave.LeaveTransactionResponse, int>>(provider =>
            new CrudService<FireTime.Models.LeaveTransaction, FireTime.Dtos.Leave.LeaveTransactionRequest, FireTime.Dtos.Leave.LeaveTransactionResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.LeaveTransaction, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.LeaveTransaction
                {
                Roic = request.Roic,
                LeaveHours = request.LeaveHours,
                LeaveTransactionDate = request.LeaveTransactionDate,
                LeaveDetailId = request.LeaveDetailId,
                LeaveTransactionTypeId = request.LeaveTransactionTypeId,
                LeaveTransactionComments = request.LeaveTransactionComments
                },
                (entity, request) =>
                {
                entity.Roic = request.Roic;
                entity.LeaveHours = request.LeaveHours;
                entity.LeaveTransactionDate = request.LeaveTransactionDate;
                entity.LeaveDetailId = request.LeaveDetailId;
                entity.LeaveTransactionTypeId = request.LeaveTransactionTypeId;
                entity.LeaveTransactionComments = request.LeaveTransactionComments;
                },
                entity => new FireTime.Dtos.Leave.LeaveTransactionResponse(entity.LeaveTransactionId, entity.Roic, entity.LeaveHours, entity.LeaveTransactionDate, entity.LeaveDetailId, entity.LeaveTransactionTypeId, entity.LeaveTransactionComments)));

        services.AddScoped<ICrudRepository<FireTime.Models.EmployeeStatusCode, string>, EfCrudRepository<FireTime.Models.EmployeeStatusCode, string>>();
        services.AddScoped<ICrudService<FireTime.Dtos.Lookups.EmployeeStatusCodeRequest, FireTime.Dtos.Lookups.EmployeeStatusCodeResponse, string>>(provider =>
            new CrudService<FireTime.Models.EmployeeStatusCode, FireTime.Dtos.Lookups.EmployeeStatusCodeRequest, FireTime.Dtos.Lookups.EmployeeStatusCodeResponse, string>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.EmployeeStatusCode, string>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.EmployeeStatusCode
                {
                EmployeeStatusCde = request.EmployeeStatusCde,
                EmployeeStatusDesc = request.EmployeeStatusDesc
                },
                (entity, request) =>
                {
                if (request.EmployeeStatusCde != entity.EmployeeStatusCde)
                    throw new ArgumentException("The employee status code cannot be changed.");
                entity.EmployeeStatusDesc = request.EmployeeStatusDesc;
                },
                entity => new FireTime.Dtos.Lookups.EmployeeStatusCodeResponse(entity.EmployeeStatusCde, entity.EmployeeStatusDesc)));

        services.AddScoped<ICrudRepository<FireTime.Models.AttendanceStatus, int>, EfCrudRepository<FireTime.Models.AttendanceStatus, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.Lookups.AttendanceStatusRequest, FireTime.Dtos.Lookups.AttendanceStatusResponse, int>>(provider =>
            new CrudService<FireTime.Models.AttendanceStatus, FireTime.Dtos.Lookups.AttendanceStatusRequest, FireTime.Dtos.Lookups.AttendanceStatusResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.AttendanceStatus, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.AttendanceStatus
                {
                AttendanceStatusCde = request.AttendanceStatusCde,
                AttendanceStatusDesc = request.AttendanceStatusDesc
                },
                (entity, request) =>
                {
                entity.AttendanceStatusCde = request.AttendanceStatusCde;
                entity.AttendanceStatusDesc = request.AttendanceStatusDesc;
                },
                entity => new FireTime.Dtos.Lookups.AttendanceStatusResponse(entity.AttendanceStatusId, entity.AttendanceStatusCde, entity.AttendanceStatusDesc)));

        services.AddScoped<ICrudRepository<FireTime.Models.ApprovalStatus, int>, EfCrudRepository<FireTime.Models.ApprovalStatus, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.Lookups.ApprovalStatusRequest, FireTime.Dtos.Lookups.ApprovalStatusResponse, int>>(provider =>
            new CrudService<FireTime.Models.ApprovalStatus, FireTime.Dtos.Lookups.ApprovalStatusRequest, FireTime.Dtos.Lookups.ApprovalStatusResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.ApprovalStatus, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.ApprovalStatus
                {
                ApprovalStatusCde = request.ApprovalStatusCde
                },
                (entity, request) =>
                {
                entity.ApprovalStatusCde = request.ApprovalStatusCde;
                },
                entity => new FireTime.Dtos.Lookups.ApprovalStatusResponse(entity.ApprovalStatusId, entity.ApprovalStatusCde)));

        services.AddScoped<ICrudRepository<FireTime.Models.ContactType, int>, EfCrudRepository<FireTime.Models.ContactType, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.Lookups.ContactTypeRequest, FireTime.Dtos.Lookups.ContactTypeResponse, int>>(provider =>
            new CrudService<FireTime.Models.ContactType, FireTime.Dtos.Lookups.ContactTypeRequest, FireTime.Dtos.Lookups.ContactTypeResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.ContactType, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.ContactType
                {
                ContactTypeDesc = request.ContactTypeDesc
                },
                (entity, request) =>
                {
                entity.ContactTypeDesc = request.ContactTypeDesc;
                },
                entity => new FireTime.Dtos.Lookups.ContactTypeResponse(entity.ContactTypeId, entity.ContactTypeDesc)));

        services.AddScoped<ICrudRepository<FireTime.Models.District, int>, EfCrudRepository<FireTime.Models.District, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.Lookups.DistrictRequest, FireTime.Dtos.Lookups.DistrictResponse, int>>(provider =>
            new CrudService<FireTime.Models.District, FireTime.Dtos.Lookups.DistrictRequest, FireTime.Dtos.Lookups.DistrictResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.District, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.District
                {
                DistrictNme = request.DistrictNme,
                StationCount = request.StationCount,
                IsActive = request.IsActive,
                DistrictComment = request.DistrictComment
                },
                (entity, request) =>
                {
                entity.DistrictNme = request.DistrictNme;
                entity.StationCount = request.StationCount;
                entity.IsActive = request.IsActive;
                entity.DistrictComment = request.DistrictComment;
                },
                entity => new FireTime.Dtos.Lookups.DistrictResponse(entity.DistrictId, entity.DistrictNme, entity.StationCount, entity.IsActive, entity.DistrictComment)));

        services.AddScoped<ICrudRepository<FireTime.Models.Station, int>, EfCrudRepository<FireTime.Models.Station, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.Lookups.StationRequest, FireTime.Dtos.Lookups.StationResponse, int>>(provider =>
            new CrudService<FireTime.Models.Station, FireTime.Dtos.Lookups.StationRequest, FireTime.Dtos.Lookups.StationResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.Station, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.Station
                {
                StationNbr = request.StationNbr,
                IsActive = request.IsActive,
                StationComment = request.StationComment,
                DistrictId = request.DistrictId
                },
                (entity, request) =>
                {
                entity.StationNbr = request.StationNbr;
                entity.IsActive = request.IsActive;
                entity.StationComment = request.StationComment;
                entity.DistrictId = request.DistrictId;
                },
                entity => new FireTime.Dtos.Lookups.StationResponse(entity.StationId, entity.StationNbr, entity.IsActive, entity.StationComment, entity.DistrictId)));

        services.AddScoped<ICrudRepository<FireTime.Models.Company, int>, EfCrudRepository<FireTime.Models.Company, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.Lookups.CompanyRequest, FireTime.Dtos.Lookups.CompanyResponse, int>>(provider =>
            new CrudService<FireTime.Models.Company, FireTime.Dtos.Lookups.CompanyRequest, FireTime.Dtos.Lookups.CompanyResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.Company, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.Company
                {
                CompanyNme = request.CompanyNme,
                IsActive = request.IsActive,
                CompanyMemberCount = request.CompanyMemberCount,
                MedicalClassification = request.MedicalClassification,
                CompanyComment = request.CompanyComment,
                StationId = request.StationId
                },
                (entity, request) =>
                {
                entity.CompanyNme = request.CompanyNme;
                entity.IsActive = request.IsActive;
                entity.CompanyMemberCount = request.CompanyMemberCount;
                entity.MedicalClassification = request.MedicalClassification;
                entity.CompanyComment = request.CompanyComment;
                entity.StationId = request.StationId;
                },
                entity => new FireTime.Dtos.Lookups.CompanyResponse(entity.CompanyId, entity.CompanyNme, entity.IsActive, entity.CompanyMemberCount, entity.MedicalClassification, entity.CompanyComment, entity.StationId)));

        services.AddScoped<ICrudRepository<FireTime.Models.CompanyPosition, int>, EfCrudRepository<FireTime.Models.CompanyPosition, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.Lookups.CompanyPositionRequest, FireTime.Dtos.Lookups.CompanyPositionResponse, int>>(provider =>
            new CrudService<FireTime.Models.CompanyPosition, FireTime.Dtos.Lookups.CompanyPositionRequest, FireTime.Dtos.Lookups.CompanyPositionResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.CompanyPosition, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.CompanyPosition
                {
                TruckPosition = request.TruckPosition,
                CompanyId = request.CompanyId,
                JobTitleId = request.JobTitleId,
                ShiftId = request.ShiftId
                },
                (entity, request) =>
                {
                entity.TruckPosition = request.TruckPosition;
                entity.CompanyId = request.CompanyId;
                entity.JobTitleId = request.JobTitleId;
                entity.ShiftId = request.ShiftId;
                },
                entity => new FireTime.Dtos.Lookups.CompanyPositionResponse(entity.CompanyPositionId, entity.TruckPosition, entity.CompanyId, entity.JobTitleId, entity.ShiftId)));

        services.AddScoped<ICrudRepository<FireTime.Models.Shift, int>, EfCrudRepository<FireTime.Models.Shift, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.Lookups.ShiftRequest, FireTime.Dtos.Lookups.ShiftResponse, int>>(provider =>
            new CrudService<FireTime.Models.Shift, FireTime.Dtos.Lookups.ShiftRequest, FireTime.Dtos.Lookups.ShiftResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.Shift, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.Shift
                {
                ShiftCode = request.ShiftCode,
                ShiftNme = request.ShiftNme,
                SortOrder = request.SortOrder,
                IsActive = request.IsActive
                },
                (entity, request) =>
                {
                entity.ShiftCode = request.ShiftCode;
                entity.ShiftNme = request.ShiftNme;
                entity.SortOrder = request.SortOrder;
                entity.IsActive = request.IsActive;
                },
                entity => new FireTime.Dtos.Lookups.ShiftResponse(entity.ShiftId, entity.ShiftCode, entity.ShiftNme, entity.SortOrder, entity.IsActive)));

        services.AddScoped<ICrudRepository<FireTime.Models.JobTitle, int>, EfCrudRepository<FireTime.Models.JobTitle, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.Lookups.JobTitleRequest, FireTime.Dtos.Lookups.JobTitleResponse, int>>(provider =>
            new CrudService<FireTime.Models.JobTitle, FireTime.Dtos.Lookups.JobTitleRequest, FireTime.Dtos.Lookups.JobTitleResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.JobTitle, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.JobTitle
                {
                JobTitleDesc = request.JobTitleDesc,
                JobTitleShortDesc = request.JobTitleShortDesc,
                IsActive = request.IsActive,
                SalaryRangeGradeCde = request.SalaryRangeGradeCde
                },
                (entity, request) =>
                {
                entity.JobTitleDesc = request.JobTitleDesc;
                entity.JobTitleShortDesc = request.JobTitleShortDesc;
                entity.IsActive = request.IsActive;
                entity.SalaryRangeGradeCde = request.SalaryRangeGradeCde;
                },
                entity => new FireTime.Dtos.Lookups.JobTitleResponse(entity.JobTitleId, entity.JobTitleDesc, entity.JobTitleShortDesc, entity.IsActive, entity.SalaryRangeGradeCde)));

        services.AddScoped<ICrudRepository<FireTime.Models.WorkPeriod, int>, EfCrudRepository<FireTime.Models.WorkPeriod, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.Lookups.WorkPeriodRequest, FireTime.Dtos.Lookups.WorkPeriodResponse, int>>(provider =>
            new CrudService<FireTime.Models.WorkPeriod, FireTime.Dtos.Lookups.WorkPeriodRequest, FireTime.Dtos.Lookups.WorkPeriodResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.WorkPeriod, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.WorkPeriod
                {
                WeekDay = request.WeekDay,
                WorkPeriodNbr = request.WorkPeriodNbr
                },
                (entity, request) =>
                {
                entity.WeekDay = request.WeekDay;
                entity.WorkPeriodNbr = request.WorkPeriodNbr;
                },
                entity => new FireTime.Dtos.Lookups.WorkPeriodResponse(entity.WorkPeriodId, entity.WeekDay, entity.WorkPeriodNbr)));

        services.AddScoped<ICrudRepository<FireTime.Models.StationDistance, int>, EfCrudRepository<FireTime.Models.StationDistance, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.Lookups.StationDistanceRequest, FireTime.Dtos.Lookups.StationDistanceResponse, int>>(provider =>
            new CrudService<FireTime.Models.StationDistance, FireTime.Dtos.Lookups.StationDistanceRequest, FireTime.Dtos.Lookups.StationDistanceResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.StationDistance, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.StationDistance
                {
                DistanceMiles = request.DistanceMiles,
                FromStationId = request.FromStationId,
                ToStationId = request.ToStationId
                },
                (entity, request) =>
                {
                entity.DistanceMiles = request.DistanceMiles;
                entity.FromStationId = request.FromStationId;
                entity.ToStationId = request.ToStationId;
                },
                entity => new FireTime.Dtos.Lookups.StationDistanceResponse(entity.StationDistanceId, entity.DistanceMiles, entity.FromStationId, entity.ToStationId)));

        services.AddScoped<ICrudRepository<FireTime.Models.LeaveCategory, int>, EfCrudRepository<FireTime.Models.LeaveCategory, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.Lookups.LeaveCategoryRequest, FireTime.Dtos.Lookups.LeaveCategoryResponse, int>>(provider =>
            new CrudService<FireTime.Models.LeaveCategory, FireTime.Dtos.Lookups.LeaveCategoryRequest, FireTime.Dtos.Lookups.LeaveCategoryResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.LeaveCategory, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.LeaveCategory
                {
                LeaveCategoryCode = request.LeaveCategoryCode,
                LeaveCategoryDesc = request.LeaveCategoryDesc
                },
                (entity, request) =>
                {
                entity.LeaveCategoryCode = request.LeaveCategoryCode;
                entity.LeaveCategoryDesc = request.LeaveCategoryDesc;
                },
                entity => new FireTime.Dtos.Lookups.LeaveCategoryResponse(entity.LeaveCategoryId, entity.LeaveCategoryCode, entity.LeaveCategoryDesc)));

        services.AddScoped<ICrudRepository<FireTime.Models.LeaveDetailCode, int>, EfCrudRepository<FireTime.Models.LeaveDetailCode, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.Lookups.LeaveDetailCodeRequest, FireTime.Dtos.Lookups.LeaveDetailCodeResponse, int>>(provider =>
            new CrudService<FireTime.Models.LeaveDetailCode, FireTime.Dtos.Lookups.LeaveDetailCodeRequest, FireTime.Dtos.Lookups.LeaveDetailCodeResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.LeaveDetailCode, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.LeaveDetailCode
                {
                LeaveDetailCode1 = request.LeaveDetailCode1,
                LeaveDetailDesc = request.LeaveDetailDesc,
                LeaveCategoryId = request.LeaveCategoryId
                },
                (entity, request) =>
                {
                entity.LeaveDetailCode1 = request.LeaveDetailCode1;
                entity.LeaveDetailDesc = request.LeaveDetailDesc;
                entity.LeaveCategoryId = request.LeaveCategoryId;
                },
                entity => new FireTime.Dtos.Lookups.LeaveDetailCodeResponse(entity.LeaveDetailId, entity.LeaveDetailCode1, entity.LeaveDetailDesc, entity.LeaveCategoryId)));

        services.AddScoped<ICrudRepository<FireTime.Models.LeaveTransactionType, int>, EfCrudRepository<FireTime.Models.LeaveTransactionType, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.Lookups.LeaveTransactionTypeRequest, FireTime.Dtos.Lookups.LeaveTransactionTypeResponse, int>>(provider =>
            new CrudService<FireTime.Models.LeaveTransactionType, FireTime.Dtos.Lookups.LeaveTransactionTypeRequest, FireTime.Dtos.Lookups.LeaveTransactionTypeResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.LeaveTransactionType, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.LeaveTransactionType
                {
                LeaveTransactionDesc = request.LeaveTransactionDesc
                },
                (entity, request) =>
                {
                entity.LeaveTransactionDesc = request.LeaveTransactionDesc;
                },
                entity => new FireTime.Dtos.Lookups.LeaveTransactionTypeResponse(entity.LeaveTransactionTypeId, entity.LeaveTransactionDesc)));

        services.AddScoped<ICrudRepository<FireTime.Models.LeaveDefaultHour, int>, EfCrudRepository<FireTime.Models.LeaveDefaultHour, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.Lookups.LeaveDefaultHourRequest, FireTime.Dtos.Lookups.LeaveDefaultHourResponse, int>>(provider =>
            new CrudService<FireTime.Models.LeaveDefaultHour, FireTime.Dtos.Lookups.LeaveDefaultHourRequest, FireTime.Dtos.Lookups.LeaveDefaultHourResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.LeaveDefaultHour, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.LeaveDefaultHour
                {
                DefaultHours = request.DefaultHours,
                LeaveCategoryId = request.LeaveCategoryId,
                LeaveTransactionTypeId = request.LeaveTransactionTypeId,
                ShiftId = request.ShiftId,
                LeaveDetailId = request.LeaveDetailId
                },
                (entity, request) =>
                {
                entity.DefaultHours = request.DefaultHours;
                entity.LeaveCategoryId = request.LeaveCategoryId;
                entity.LeaveTransactionTypeId = request.LeaveTransactionTypeId;
                entity.ShiftId = request.ShiftId;
                entity.LeaveDetailId = request.LeaveDetailId;
                },
                entity => new FireTime.Dtos.Lookups.LeaveDefaultHourResponse(entity.LeaveDefaultHoursId, entity.DefaultHours, entity.LeaveCategoryId, entity.LeaveTransactionTypeId, entity.ShiftId, entity.LeaveDetailId)));

        services.AddScoped<ICrudRepository<FireTime.Models.SickSellbackCode, int>, EfCrudRepository<FireTime.Models.SickSellbackCode, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.Lookups.SickSellbackCodeRequest, FireTime.Dtos.Lookups.SickSellbackCodeResponse, int>>(provider =>
            new CrudService<FireTime.Models.SickSellbackCode, FireTime.Dtos.Lookups.SickSellbackCodeRequest, FireTime.Dtos.Lookups.SickSellbackCodeResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.SickSellbackCode, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.SickSellbackCode
                {
                SickSellbackCode1 = request.SickSellbackCode1,
                SickSellbackDesc = request.SickSellbackDesc,
                SickSellbackCodeComments = request.SickSellbackCodeComments,
                ShiftId = request.ShiftId
                },
                (entity, request) =>
                {
                entity.SickSellbackCode1 = request.SickSellbackCode1;
                entity.SickSellbackDesc = request.SickSellbackDesc;
                entity.SickSellbackCodeComments = request.SickSellbackCodeComments;
                entity.ShiftId = request.ShiftId;
                },
                entity => new FireTime.Dtos.Lookups.SickSellbackCodeResponse(entity.SickSellbackCodeId, entity.SickSellbackCode1, entity.SickSellbackDesc, entity.SickSellbackCodeComments, entity.ShiftId)));

        services.AddScoped<ICrudRepository<FireTime.Models.SickSellbackSelection, int>, EfCrudRepository<FireTime.Models.SickSellbackSelection, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.SickSellback.SickSellbackSelectionRequest, FireTime.Dtos.SickSellback.SickSellbackSelectionResponse, int>>(provider =>
            new CrudService<FireTime.Models.SickSellbackSelection, FireTime.Dtos.SickSellback.SickSellbackSelectionRequest, FireTime.Dtos.SickSellback.SickSellbackSelectionResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.SickSellbackSelection, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.SickSellbackSelection
                {
                Roic = request.Roic,
                SellbackSelectionYear = request.SellbackSelectionYear,
                SickSellbackCodeId = request.SickSellbackCodeId,
                ShiftId = request.ShiftId,
                SellbackSelectionDateTime = DateTime.UtcNow,
                },
                (entity, request) =>
                {
                entity.Roic = request.Roic;
                entity.SellbackSelectionYear = request.SellbackSelectionYear;
                entity.SickSellbackCodeId = request.SickSellbackCodeId;
                entity.ShiftId = request.ShiftId;
                },
                entity => new FireTime.Dtos.SickSellback.SickSellbackSelectionResponse(entity.SickSellbackSelectionId, entity.Roic, entity.SellbackSelectionYear, entity.SellbackSelectionDateTime, entity.SickSellbackCodeId, entity.ShiftId)));

        services.AddScoped<ICrudRepository<FireTime.Models.SickSellbackPayout, int>, EfCrudRepository<FireTime.Models.SickSellbackPayout, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.SickSellback.SickSellbackPayoutRequest, FireTime.Dtos.SickSellback.SickSellbackPayoutResponse, int>>(provider =>
            new CrudService<FireTime.Models.SickSellbackPayout, FireTime.Dtos.SickSellback.SickSellbackPayoutRequest, FireTime.Dtos.SickSellback.SickSellbackPayoutResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.SickSellbackPayout, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.SickSellbackPayout
                {
                Roic = request.Roic,
                SickSellbackSelectionId = request.SickSellbackSelectionId,
                SellbackPayoutYear = request.SellbackPayoutYear,
                SellbackPayoutHours = request.SellbackPayoutHours,
                PayTypeCde = request.PayTypeCde
                },
                (entity, request) =>
                {
                entity.Roic = request.Roic;
                entity.SickSellbackSelectionId = request.SickSellbackSelectionId;
                entity.SellbackPayoutYear = request.SellbackPayoutYear;
                entity.SellbackPayoutHours = request.SellbackPayoutHours;
                entity.PayTypeCde = request.PayTypeCde;
                },
                entity => new FireTime.Dtos.SickSellback.SickSellbackPayoutResponse(entity.SickSellbackPayoutId, entity.Roic, entity.SickSellbackSelectionId, entity.SellbackPayoutYear, entity.SellbackPayoutHours, entity.PayTypeCde, entity.PayrollExportDateTime)));

        services.AddScoped<ICrudRepository<FireTime.Models.TransferMileage, int>, EfCrudRepository<FireTime.Models.TransferMileage, int>>();
        services.AddScoped<ICrudService<FireTime.Dtos.Transfers.TransferMileageRequest, FireTime.Dtos.Transfers.TransferMileageResponse, int>>(provider =>
            new CrudService<FireTime.Models.TransferMileage, FireTime.Dtos.Transfers.TransferMileageRequest, FireTime.Dtos.Transfers.TransferMileageResponse, int>(
                provider.GetRequiredService<ICrudRepository<FireTime.Models.TransferMileage, int>>(),
                provider.GetRequiredService<IHttpContextAccessor>(),
                request => new FireTime.Models.TransferMileage
                {
                Roic = request.Roic,
                StationDistanceId = request.StationDistanceId,
                TransferDateTime = request.TransferDateTime,
                ScheduledReportDate = request.ScheduledReportDate
                },
                (entity, request) =>
                {
                entity.Roic = request.Roic;
                entity.StationDistanceId = request.StationDistanceId;
                entity.TransferDateTime = request.TransferDateTime;
                entity.ScheduledReportDate = request.ScheduledReportDate;
                },
                entity => new FireTime.Dtos.Transfers.TransferMileageResponse(entity.TransferMileageId, entity.Roic, entity.StationDistanceId, entity.TransferDateTime, entity.ScheduledReportDate)));

        return services;
    }
}
