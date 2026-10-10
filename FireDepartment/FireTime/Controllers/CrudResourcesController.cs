using FireTime.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace FireTime.Controllers;

[Route("api/addresses")]
public sealed class AddressController(ICrudService<FireTime.Dtos.Contacts.AddressRequest, FireTime.Dtos.Contacts.AddressResponse, int> service)
    : CrudController<FireTime.Dtos.Contacts.AddressRequest, FireTime.Dtos.Contacts.AddressResponse, int>(service);

[Route("api/phone-numbers")]
public sealed class PhoneNumberController(ICrudService<FireTime.Dtos.Contacts.PhoneNumberRequest, FireTime.Dtos.Contacts.PhoneNumberResponse, int> service)
    : CrudController<FireTime.Dtos.Contacts.PhoneNumberRequest, FireTime.Dtos.Contacts.PhoneNumberResponse, int>(service);

[Route("api/email-addresses")]
public sealed class EmailAddressController(ICrudService<FireTime.Dtos.Contacts.EmailAddressRequest, FireTime.Dtos.Contacts.EmailAddressResponse, int> service)
    : CrudController<FireTime.Dtos.Contacts.EmailAddressRequest, FireTime.Dtos.Contacts.EmailAddressResponse, int>(service);

[Route("api/emergency-contacts")]
public sealed class EmergencyContactController(ICrudService<FireTime.Dtos.Contacts.EmergencyContactRequest, FireTime.Dtos.Contacts.EmergencyContactResponse, int> service)
    : CrudController<FireTime.Dtos.Contacts.EmergencyContactRequest, FireTime.Dtos.Contacts.EmergencyContactResponse, int>(service);

[Route("api/employee-assignments")]
public sealed class EmployeeAssignmentController(ICrudService<FireTime.Dtos.Employees.EmployeeAssignmentRequest, FireTime.Dtos.Employees.EmployeeAssignmentResponse, int> service)
    : CrudController<FireTime.Dtos.Employees.EmployeeAssignmentRequest, FireTime.Dtos.Employees.EmployeeAssignmentResponse, int>(service);

[Route("api/leave-transactions")]
public sealed class LeaveTransactionController(ICrudService<FireTime.Dtos.Leave.LeaveTransactionRequest, FireTime.Dtos.Leave.LeaveTransactionResponse, int> service)
    : CrudController<FireTime.Dtos.Leave.LeaveTransactionRequest, FireTime.Dtos.Leave.LeaveTransactionResponse, int>(service);

[Route("api/employee-status-codes")]
public sealed class EmployeeStatusCodeController(ICrudService<FireTime.Dtos.Lookups.EmployeeStatusCodeRequest, FireTime.Dtos.Lookups.EmployeeStatusCodeResponse, string> service)
    : CrudController<FireTime.Dtos.Lookups.EmployeeStatusCodeRequest, FireTime.Dtos.Lookups.EmployeeStatusCodeResponse, string>(service);

[Route("api/attendance-statuses")]
public sealed class AttendanceStatusController(ICrudService<FireTime.Dtos.Lookups.AttendanceStatusRequest, FireTime.Dtos.Lookups.AttendanceStatusResponse, int> service)
    : CrudController<FireTime.Dtos.Lookups.AttendanceStatusRequest, FireTime.Dtos.Lookups.AttendanceStatusResponse, int>(service);

[Route("api/approval-statuses")]
public sealed class ApprovalStatusController(ICrudService<FireTime.Dtos.Lookups.ApprovalStatusRequest, FireTime.Dtos.Lookups.ApprovalStatusResponse, int> service)
    : CrudController<FireTime.Dtos.Lookups.ApprovalStatusRequest, FireTime.Dtos.Lookups.ApprovalStatusResponse, int>(service);

[Route("api/contact-types")]
public sealed class ContactTypeController(ICrudService<FireTime.Dtos.Lookups.ContactTypeRequest, FireTime.Dtos.Lookups.ContactTypeResponse, int> service)
    : CrudController<FireTime.Dtos.Lookups.ContactTypeRequest, FireTime.Dtos.Lookups.ContactTypeResponse, int>(service);

[Route("api/districts")]
public sealed class DistrictController(ICrudService<FireTime.Dtos.Lookups.DistrictRequest, FireTime.Dtos.Lookups.DistrictResponse, int> service)
    : CrudController<FireTime.Dtos.Lookups.DistrictRequest, FireTime.Dtos.Lookups.DistrictResponse, int>(service);

[Route("api/stations")]
public sealed class StationController(ICrudService<FireTime.Dtos.Lookups.StationRequest, FireTime.Dtos.Lookups.StationResponse, int> service)
    : CrudController<FireTime.Dtos.Lookups.StationRequest, FireTime.Dtos.Lookups.StationResponse, int>(service);

[Route("api/companies")]
public sealed class CompanyController(ICrudService<FireTime.Dtos.Lookups.CompanyRequest, FireTime.Dtos.Lookups.CompanyResponse, int> service)
    : CrudController<FireTime.Dtos.Lookups.CompanyRequest, FireTime.Dtos.Lookups.CompanyResponse, int>(service);

[Route("api/company-positions")]
public sealed class CompanyPositionController(ICrudService<FireTime.Dtos.Lookups.CompanyPositionRequest, FireTime.Dtos.Lookups.CompanyPositionResponse, int> service)
    : CrudController<FireTime.Dtos.Lookups.CompanyPositionRequest, FireTime.Dtos.Lookups.CompanyPositionResponse, int>(service);

[Route("api/shifts")]
public sealed class ShiftController(ICrudService<FireTime.Dtos.Lookups.ShiftRequest, FireTime.Dtos.Lookups.ShiftResponse, int> service)
    : CrudController<FireTime.Dtos.Lookups.ShiftRequest, FireTime.Dtos.Lookups.ShiftResponse, int>(service);

[Route("api/job-titles")]
public sealed class JobTitleController(ICrudService<FireTime.Dtos.Lookups.JobTitleRequest, FireTime.Dtos.Lookups.JobTitleResponse, int> service)
    : CrudController<FireTime.Dtos.Lookups.JobTitleRequest, FireTime.Dtos.Lookups.JobTitleResponse, int>(service);

[Route("api/work-periods")]
public sealed class WorkPeriodController(ICrudService<FireTime.Dtos.Lookups.WorkPeriodRequest, FireTime.Dtos.Lookups.WorkPeriodResponse, int> service)
    : CrudController<FireTime.Dtos.Lookups.WorkPeriodRequest, FireTime.Dtos.Lookups.WorkPeriodResponse, int>(service);

[Route("api/station-distances")]
public sealed class StationDistanceController(ICrudService<FireTime.Dtos.Lookups.StationDistanceRequest, FireTime.Dtos.Lookups.StationDistanceResponse, int> service)
    : CrudController<FireTime.Dtos.Lookups.StationDistanceRequest, FireTime.Dtos.Lookups.StationDistanceResponse, int>(service);

[Route("api/leave-categories")]
public sealed class LeaveCategoryController(ICrudService<FireTime.Dtos.Lookups.LeaveCategoryRequest, FireTime.Dtos.Lookups.LeaveCategoryResponse, int> service)
    : CrudController<FireTime.Dtos.Lookups.LeaveCategoryRequest, FireTime.Dtos.Lookups.LeaveCategoryResponse, int>(service);

[Route("api/leave-detail-codes")]
public sealed class LeaveDetailCodeController(ICrudService<FireTime.Dtos.Lookups.LeaveDetailCodeRequest, FireTime.Dtos.Lookups.LeaveDetailCodeResponse, int> service)
    : CrudController<FireTime.Dtos.Lookups.LeaveDetailCodeRequest, FireTime.Dtos.Lookups.LeaveDetailCodeResponse, int>(service);

[Route("api/leave-transaction-types")]
public sealed class LeaveTransactionTypeController(ICrudService<FireTime.Dtos.Lookups.LeaveTransactionTypeRequest, FireTime.Dtos.Lookups.LeaveTransactionTypeResponse, int> service)
    : CrudController<FireTime.Dtos.Lookups.LeaveTransactionTypeRequest, FireTime.Dtos.Lookups.LeaveTransactionTypeResponse, int>(service);

[Route("api/leave-default-hours")]
public sealed class LeaveDefaultHourController(ICrudService<FireTime.Dtos.Lookups.LeaveDefaultHourRequest, FireTime.Dtos.Lookups.LeaveDefaultHourResponse, int> service)
    : CrudController<FireTime.Dtos.Lookups.LeaveDefaultHourRequest, FireTime.Dtos.Lookups.LeaveDefaultHourResponse, int>(service);

[Route("api/sick-sellback-codes")]
public sealed class SickSellbackCodeController(ICrudService<FireTime.Dtos.Lookups.SickSellbackCodeRequest, FireTime.Dtos.Lookups.SickSellbackCodeResponse, int> service)
    : CrudController<FireTime.Dtos.Lookups.SickSellbackCodeRequest, FireTime.Dtos.Lookups.SickSellbackCodeResponse, int>(service);

[Route("api/sick-sellback-selections")]
public sealed class SickSellbackSelectionController(ICrudService<FireTime.Dtos.SickSellback.SickSellbackSelectionRequest, FireTime.Dtos.SickSellback.SickSellbackSelectionResponse, int> service)
    : CrudController<FireTime.Dtos.SickSellback.SickSellbackSelectionRequest, FireTime.Dtos.SickSellback.SickSellbackSelectionResponse, int>(service);

[Route("api/sick-sellback-payouts")]
public sealed class SickSellbackPayoutController(ICrudService<FireTime.Dtos.SickSellback.SickSellbackPayoutRequest, FireTime.Dtos.SickSellback.SickSellbackPayoutResponse, int> service)
    : CrudController<FireTime.Dtos.SickSellback.SickSellbackPayoutRequest, FireTime.Dtos.SickSellback.SickSellbackPayoutResponse, int>(service);

[Route("api/transfer-mileages")]
public sealed class TransferMileageController(ICrudService<FireTime.Dtos.Transfers.TransferMileageRequest, FireTime.Dtos.Transfers.TransferMileageResponse, int> service)
    : CrudController<FireTime.Dtos.Transfers.TransferMileageRequest, FireTime.Dtos.Transfers.TransferMileageResponse, int>(service);
