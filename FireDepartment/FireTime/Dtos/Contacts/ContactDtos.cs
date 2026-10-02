namespace FireTime.Dtos.Contacts;

public sealed record AddressRequest(
    string StreetAddressTxt,
    string CityNme,
    string StateNme,
    string ZipCode,
    int ContactTypeId,
    string? Roic,
    int? DistrictId,
    int? StationId,
    int? EmergencyContactId);

public sealed record AddressResponse(
    int AddressId,
    string StreetAddressTxt,
    string CityNme,
    string StateNme,
    string ZipCode,
    int ContactTypeId,
    string? Roic,
    int? DistrictId,
    int? StationId,
    int? EmergencyContactId);

public sealed record PhoneNumberRequest(
    string PhoneNbr,
    int? ContactTypeId,
    string? Roic,
    int? DistrictId,
    int? StationId,
    int? EmergencyContactId);

public sealed record PhoneNumberResponse(
    int PhoneNumberId,
    string PhoneNbr,
    int? ContactTypeId,
    string? Roic,
    int? DistrictId,
    int? StationId,
    int? EmergencyContactId);

public sealed record EmailAddressRequest(
    string EmailAddressTxt,
    int ContactTypeId,
    string? Roic);

public sealed record EmailAddressResponse(
    int EmailAddressId,
    string EmailAddressTxt,
    int ContactTypeId,
    string? Roic);

public sealed record EmergencyContactRequest(
    string EmergencyContactFname,
    string EmergencyContactLname,
    int ContactTypeId,
    int? AddressId,
    int? PhoneNumberId,
    string Roic);

public sealed record EmergencyContactResponse(
    int EmergencyContactId,
    string EmergencyContactFname,
    string EmergencyContactLname,
    int ContactTypeId,
    int? AddressId,
    int? PhoneNumberId,
    string Roic);
