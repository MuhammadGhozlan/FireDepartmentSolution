export interface AddressRequest {
  streetAddressTxt: string;
  cityNme: string;
  stateNme: string;
  zipCode: string;
  contactTypeId: number;
  roic?: string | null;
  districtId?: number | null;
  stationId?: number | null;
  emergencyContactId?: number | null;
}

export interface AddressResponse {
  addressId: number;
  streetAddressTxt: string;
  cityNme: string;
  stateNme: string;
  zipCode: string;
  contactTypeId: number;
  roic?: string | null;
  districtId?: number | null;
  stationId?: number | null;
  emergencyContactId?: number | null;
}

export interface PhoneNumberRequest {
  phoneNbr: string;
  contactTypeId?: number | null;
  roic?: string | null;
  districtId?: number | null;
  stationId?: number | null;
  emergencyContactId?: number | null;
}

export interface PhoneNumberResponse {
  phoneNumberId: number;
  phoneNbr: string;
  contactTypeId?: number | null;
  roic?: string | null;
  districtId?: number | null;
  stationId?: number | null;
  emergencyContactId?: number | null;
}

export interface EmailAddressRequest {
  emailAddressTxt: string;
  contactTypeId: number;
  roic?: string | null;
}

export interface EmailAddressResponse {
  emailAddressId: number;
  emailAddressTxt: string;
  contactTypeId: number;
  roic?: string | null;
}

export interface EmergencyContactRequest {
  emergencyContactFname: string;
  emergencyContactLname: string;
  contactTypeId: number;
  addressId?: number | null;
  phoneNumberId?: number | null;
  roic: string;
}

export interface EmergencyContactResponse {
  emergencyContactId: number;
  emergencyContactFname: string;
  emergencyContactLname: string;
  contactTypeId: number;
  addressId?: number | null;
  phoneNumberId?: number | null;
  roic: string;
}