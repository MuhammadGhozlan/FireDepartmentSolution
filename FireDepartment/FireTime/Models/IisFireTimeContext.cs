using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FireTime.Models;

public partial class IisFireTimeContext : DbContext
{
    public IisFireTimeContext()
    {
    }

    public IisFireTimeContext(DbContextOptions<IisFireTimeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<ApprovalStatus> ApprovalStatuses { get; set; }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<AttendanceStatus> AttendanceStatuses { get; set; }

    public virtual DbSet<AuditHistory> AuditHistories { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyPosition> CompanyPositions { get; set; }

    public virtual DbSet<ContactType> ContactTypes { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<EmailAddress> EmailAddresses { get; set; }

    public virtual DbSet<EmergencyContact> EmergencyContacts { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeAssignment> EmployeeAssignments { get; set; }

    public virtual DbSet<EmployeeStatusCode> EmployeeStatusCodes { get; set; }

    public virtual DbSet<JobTitle> JobTitles { get; set; }

    public virtual DbSet<LeaveCategory> LeaveCategories { get; set; }

    public virtual DbSet<LeaveDefaultHour> LeaveDefaultHours { get; set; }

    public virtual DbSet<LeaveDetailCode> LeaveDetailCodes { get; set; }

    public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }

    public virtual DbSet<LeaveTransaction> LeaveTransactions { get; set; }

    public virtual DbSet<LeaveTransactionType> LeaveTransactionTypes { get; set; }

    public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<SickSellbackCode> SickSellbackCodes { get; set; }

    public virtual DbSet<SickSellbackPayout> SickSellbackPayouts { get; set; }

    public virtual DbSet<SickSellbackSelection> SickSellbackSelections { get; set; }

    public virtual DbSet<Station> Stations { get; set; }

    public virtual DbSet<StationDistance> StationDistances { get; set; }

    public virtual DbSet<TransferMileage> TransferMileages { get; set; }

    public virtual DbSet<TransferRequest> TransferRequests { get; set; }

    public virtual DbSet<WorkPeriod> WorkPeriods { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId)
                .HasName("PK2")
                .IsClustered(false);

            entity.ToTable("Address");

            entity.Property(e => e.AddressId).HasColumnName("Address_ID");
            entity.Property(e => e.CityNme)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("City_Nme");
            entity.Property(e => e.ContactTypeId).HasColumnName("Contact_Type_ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.DeletedInd).HasColumnName("Deleted_Ind");
            entity.Property(e => e.DistrictId).HasColumnName("District_ID");
            entity.Property(e => e.EmergencyContactId).HasColumnName("Emergency_Contact_ID");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Last_Update_By");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Last_Update_Date");
            entity.Property(e => e.Roic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ROIC");
            entity.Property(e => e.StateNme)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("State_Nme");
            entity.Property(e => e.StationId).HasColumnName("Station_ID");
            entity.Property(e => e.StreetAddressTxt)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Street_Address_Txt");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Zip_Code");

            entity.HasOne(d => d.ContactType).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.ContactTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContactType_Address");

            entity.HasOne(d => d.District).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK_District_Address");

            entity.HasOne(d => d.EmergencyContact).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.EmergencyContactId)
                .HasConstraintName("FK_EmergencyContact_Address");

            entity.HasOne(d => d.RoicNavigation).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.Roic)
                .HasConstraintName("FK_Employee_Address");

            entity.HasOne(d => d.Station).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("FK_Station_Address");
        });

        modelBuilder.Entity<ApprovalStatus>(entity =>
        {
            entity.HasKey(e => e.ApprovalStatusId)
                .HasName("PK25")
                .IsClustered(false);

            entity.ToTable("Approval_Status");

            entity.Property(e => e.ApprovalStatusId).HasColumnName("Approval_Status_ID");
            entity.Property(e => e.ApprovalStatusCde)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Approval_Status_Cde");
        });

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId)
                .HasName("PK7")
                .IsClustered(false);

            entity.ToTable("Attendance");

            entity.Property(e => e.AttendanceId).HasColumnName("Attendance_ID");
            entity.Property(e => e.AttendanceComments)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Attendance_Comments");
            entity.Property(e => e.AttendanceDate).HasColumnName("Attendance_Date");
            entity.Property(e => e.AttendanceStatusId).HasColumnName("Attendance_Status_ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.DeletedInd).HasColumnName("Deleted_Ind");
            entity.Property(e => e.EmployeeAssignmentId).HasColumnName("Employee_Assignment_ID");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Last_Update_By");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Last_Update_Date");
            entity.Property(e => e.Roic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ROIC");

            entity.HasOne(d => d.AttendanceStatus).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.AttendanceStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AttendanceStatus_Attendance");

            entity.HasOne(d => d.EmployeeAssignment).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.EmployeeAssignmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeAssignnment_Attendance");

            entity.HasOne(d => d.RoicNavigation).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.Roic)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_Attendance");
        });

        modelBuilder.Entity<AttendanceStatus>(entity =>
        {
            entity.HasKey(e => e.AttendanceStatusId)
                .HasName("PK23")
                .IsClustered(false);

            entity.ToTable("Attendance_Status");

            entity.Property(e => e.AttendanceStatusId).HasColumnName("Attendance_Status_ID");
            entity.Property(e => e.AttendanceStatusCde)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Attendance_Status_Cde");
            entity.Property(e => e.AttendanceStatusDesc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Attendance_Status_Desc");
        });

        modelBuilder.Entity<AuditHistory>(entity =>
        {
            entity.HasKey(e => e.AuditHistoryId)
                .HasName("PK31")
                .IsClustered(false);

            entity.ToTable("Audit_History");

            entity.Property(e => e.AuditHistoryId).HasColumnName("Audit_History_ID");
            entity.Property(e => e.AuditHistoryDate)
                .HasColumnType("datetime")
                .HasColumnName("Audit_History_Date");
            entity.Property(e => e.AuditHistoryUpdateBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Audit_History_Update_By");
            entity.Property(e => e.FieldNme)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Field_Nme");
            entity.Property(e => e.NewValue)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("New_Value");
            entity.Property(e => e.OldValue)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Old_Value");
            entity.Property(e => e.TableNme)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Table_Nme");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId)
                .HasName("PK19")
                .IsClustered(false);

            entity.ToTable("Company");

            entity.Property(e => e.CompanyId).HasColumnName("Company_ID");
            entity.Property(e => e.CompanyComment)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Company_Comment");
            entity.Property(e => e.CompanyMemberCount)
                .HasDefaultValue(3)
                .HasColumnName("Company_Member_Count");
            entity.Property(e => e.CompanyNme)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Company_Nme");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("Is_Active");
            entity.Property(e => e.MedicalClassification)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("Medical_Classification");
            entity.Property(e => e.StationId).HasColumnName("Station_ID");

            entity.HasOne(d => d.Station).WithMany(p => p.Companies)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("FK_Station_Company");
        });

        modelBuilder.Entity<CompanyPosition>(entity =>
        {
            entity.HasKey(e => e.CompanyPositionId)
                .HasName("PK22")
                .IsClustered(false);

            entity.ToTable("Company_Position");

            entity.Property(e => e.CompanyPositionId).HasColumnName("Company_Position_ID");
            entity.Property(e => e.CompanyId).HasColumnName("Company_ID");
            entity.Property(e => e.JobTitleId).HasColumnName("Job_Title_ID");
            entity.Property(e => e.ShiftId).HasColumnName("Shift_ID");
            entity.Property(e => e.TruckPosition).HasColumnName("Truck_Position");

            entity.HasOne(d => d.Company).WithMany(p => p.CompanyPositions)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_CompanyPosition");

            entity.HasOne(d => d.JobTitle).WithMany(p => p.CompanyPositions)
                .HasForeignKey(d => d.JobTitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobTitle_CompanyPosition");

            entity.HasOne(d => d.Shift).WithMany(p => p.CompanyPositions)
                .HasForeignKey(d => d.ShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shift_CompanyPosition");
        });

        modelBuilder.Entity<ContactType>(entity =>
        {
            entity.HasKey(e => e.ContactTypeId)
                .HasName("PK14")
                .IsClustered(false);

            entity.ToTable("Contact_Type");

            entity.Property(e => e.ContactTypeId).HasColumnName("Contact_Type_ID");
            entity.Property(e => e.ContactTypeDesc)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("Contact_Type_Desc");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.DistrictId)
                .HasName("PK17")
                .IsClustered(false);

            entity.ToTable("District");

            entity.Property(e => e.DistrictId).HasColumnName("District_ID");
            entity.Property(e => e.DistrictComment)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("District_Comment");
            entity.Property(e => e.DistrictNme)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("District_Nme");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("Is_Active");
            entity.Property(e => e.StationCount).HasColumnName("Station_Count");
        });

        modelBuilder.Entity<EmailAddress>(entity =>
        {
            entity.HasKey(e => e.EmailAddressId)
                .HasName("PK3")
                .IsClustered(false);

            entity.ToTable("Email_Address");

            entity.Property(e => e.EmailAddressId).HasColumnName("Email_Address_ID");
            entity.Property(e => e.ContactTypeId).HasColumnName("Contact_Type_ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.DeletedInd).HasColumnName("Deleted_Ind");
            entity.Property(e => e.EmailAddressTxt)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Email_Address_Txt");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Last_Update_By");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Last_Update_Date");
            entity.Property(e => e.Roic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ROIC");

            entity.HasOne(d => d.ContactType).WithMany(p => p.EmailAddresses)
                .HasForeignKey(d => d.ContactTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContactType_EmailAddress");

            entity.HasOne(d => d.RoicNavigation).WithMany(p => p.EmailAddresses)
                .HasForeignKey(d => d.Roic)
                .HasConstraintName("FK_Employee_EmailAddress");
        });

        modelBuilder.Entity<EmergencyContact>(entity =>
        {
            entity.HasKey(e => e.EmergencyContactId)
                .HasName("PK5")
                .IsClustered(false);

            entity.ToTable("Emergency_Contact");

            entity.Property(e => e.EmergencyContactId).HasColumnName("Emergency_Contact_ID");
            entity.Property(e => e.AddressId).HasColumnName("Address_ID");
            entity.Property(e => e.ContactTypeId).HasColumnName("Contact_Type_ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.DeletedInd).HasColumnName("Deleted_Ind");
            entity.Property(e => e.EmergencyContactFname)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Emergency_Contact_Fname");
            entity.Property(e => e.EmergencyContactLname)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Emergency_Contact_Lname");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Last_Update_By");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Last_Update_Date");
            entity.Property(e => e.PhoneNumberId).HasColumnName("Phone_Number_ID");
            entity.Property(e => e.Roic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ROIC");

            entity.HasOne(d => d.Address).WithMany(p => p.EmergencyContacts)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_Address_EmergencyContact");

            entity.HasOne(d => d.ContactType).WithMany(p => p.EmergencyContacts)
                .HasForeignKey(d => d.ContactTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContactType_EmergencyContact");

            entity.HasOne(d => d.PhoneNumber).WithMany(p => p.EmergencyContacts)
                .HasForeignKey(d => d.PhoneNumberId)
                .HasConstraintName("FK_PhoneNumber_EmergencyContact");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Roic)
                .HasName("PK1")
                .IsClustered(false);

            entity.ToTable("Employee");

            entity.Property(e => e.Roic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ROIC");
            entity.Property(e => e.BirthDate).HasColumnName("Birth_Date");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.EmployeeNbr).HasColumnName("Employee_Nbr");
            entity.Property(e => e.EmployeeStatusCde)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Employee_Status_Cde");
            entity.Property(e => e.FirstNme)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("First_Nme");
            entity.Property(e => e.LastNme)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Last_Nme");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Last_Update_By");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Last_Update_Date");
            entity.Property(e => e.MaritalStatusCde)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Marital_Status_Cde");
            entity.Property(e => e.MiddleInitialNme)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("Middle_Initial_Nme");
            entity.Property(e => e.SocialSecurityNbr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Social_Security_Nbr");
            entity.Property(e => e.Suffix)
                .HasMaxLength(5)
                .IsUnicode(false);

            entity.HasOne(d => d.EmployeeStatusCdeNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.EmployeeStatusCde)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeStatusCode_Employee");
        });

        modelBuilder.Entity<EmployeeAssignment>(entity =>
        {
            entity.HasKey(e => e.EmployeeAssignmentId)
                .HasName("PK6")
                .IsClustered(false);

            entity.ToTable("Employee_Assignment");

            entity.Property(e => e.EmployeeAssignmentId).HasColumnName("Employee_Assignment_ID");
            entity.Property(e => e.AssignmentEndDate).HasColumnName("Assignment_End_Date");
            entity.Property(e => e.AssignmentStartDate).HasColumnName("Assignment_Start_Date");
            entity.Property(e => e.CompanyPositionId).HasColumnName("Company_Position_ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.DeletedInd).HasColumnName("Deleted_Ind");
            entity.Property(e => e.IsTemp).HasColumnName("Is_Temp");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Last_Update_By");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Last_Update_Date");
            entity.Property(e => e.Roic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ROIC");
            entity.Property(e => e.WorkPeriodId).HasColumnName("Work_Period_ID");

            entity.HasOne(d => d.CompanyPosition).WithMany(p => p.EmployeeAssignments)
                .HasForeignKey(d => d.CompanyPositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyPosition_EmployeeAssignment");

            entity.HasOne(d => d.RoicNavigation).WithMany(p => p.EmployeeAssignments)
                .HasForeignKey(d => d.Roic)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_EmployeeAssignment");

            entity.HasOne(d => d.WorkPeriod).WithMany(p => p.EmployeeAssignments)
                .HasForeignKey(d => d.WorkPeriodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WorkPeriod_EmployeeAssignment");
        });

        modelBuilder.Entity<EmployeeStatusCode>(entity =>
        {
            entity.HasKey(e => e.EmployeeStatusCde)
                .HasName("PK15")
                .IsClustered(false);

            entity.ToTable("Employee_Status_Code");

            entity.Property(e => e.EmployeeStatusCde)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Employee_Status_Cde");
            entity.Property(e => e.EmployeeStatusDesc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Employee_Status_Desc");
        });

        modelBuilder.Entity<JobTitle>(entity =>
        {
            entity.HasKey(e => e.JobTitleId)
                .HasName("PK16")
                .IsClustered(false);

            entity.ToTable("Job_Title");

            entity.Property(e => e.JobTitleId).HasColumnName("Job_Title_ID");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("Is_Active");
            entity.Property(e => e.JobTitleDesc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Job_Title_Desc");
            entity.Property(e => e.JobTitleShortDesc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Job_Title_Short_Desc");
            entity.Property(e => e.SalaryRangeGradeCde)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("Salary_Range_Grade_Cde");
        });

        modelBuilder.Entity<LeaveCategory>(entity =>
        {
            entity.HasKey(e => e.LeaveCategoryId)
                .HasName("PK26")
                .IsClustered(false);

            entity.ToTable("Leave_Category");

            entity.Property(e => e.LeaveCategoryId).HasColumnName("Leave_Category_ID");
            entity.Property(e => e.LeaveCategoryCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Leave_Category_Code");
            entity.Property(e => e.LeaveCategoryDesc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Leave_Category_Desc");
        });

        modelBuilder.Entity<LeaveDefaultHour>(entity =>
        {
            entity.HasKey(e => e.LeaveDefaultHoursId)
                .HasName("PK29")
                .IsClustered(false);

            entity.ToTable("Leave_Default_Hours");

            entity.Property(e => e.LeaveDefaultHoursId).HasColumnName("Leave_Default_Hours_ID");
            entity.Property(e => e.DefaultHours)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("Default_Hours");
            entity.Property(e => e.LeaveCategoryId).HasColumnName("Leave_Category_ID");
            entity.Property(e => e.LeaveDetailId).HasColumnName("Leave_Detail_ID");
            entity.Property(e => e.LeaveTransactionTypeId).HasColumnName("Leave_Transaction_Type_ID");
            entity.Property(e => e.ShiftId).HasColumnName("Shift_ID");

            entity.HasOne(d => d.LeaveCategory).WithMany(p => p.LeaveDefaultHours)
                .HasForeignKey(d => d.LeaveCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeaveCategory_LeaveDefaultHours");

            entity.HasOne(d => d.LeaveDetail).WithMany(p => p.LeaveDefaultHours)
                .HasForeignKey(d => d.LeaveDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Leave_Default_Hours_Leave_Detail_Code");

            entity.HasOne(d => d.LeaveTransactionType).WithMany(p => p.LeaveDefaultHours)
                .HasForeignKey(d => d.LeaveTransactionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeaveTransactionType_LeaveDefaultHours");

            entity.HasOne(d => d.Shift).WithMany(p => p.LeaveDefaultHours)
                .HasForeignKey(d => d.ShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shift_LeaveDefaultHours");
        });

        modelBuilder.Entity<LeaveDetailCode>(entity =>
        {
            entity.HasKey(e => e.LeaveDetailId)
                .HasName("PK27")
                .IsClustered(false);

            entity.ToTable("Leave_Detail_Code");

            entity.Property(e => e.LeaveDetailId).HasColumnName("Leave_Detail_ID");
            entity.Property(e => e.LeaveCategoryId).HasColumnName("Leave_Category_ID");
            entity.Property(e => e.LeaveDetailCode1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Leave_Detail_Code");
            entity.Property(e => e.LeaveDetailDesc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Leave_Detail_Desc");

            entity.HasOne(d => d.LeaveCategory).WithMany(p => p.LeaveDetailCodes)
                .HasForeignKey(d => d.LeaveCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeaveCategory_LeaveDetailCode");
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(e => e.LeaveRequestId)
                .HasName("PK11")
                .IsClustered(false);

            entity.ToTable("Leave_Request");

            entity.Property(e => e.LeaveRequestId).HasColumnName("Leave_Request_ID");
            entity.Property(e => e.ApprovalStatusId).HasColumnName("Approval_Status_ID");
            entity.Property(e => e.ApproverComment)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Approver_Comment");
            entity.Property(e => e.ApproverRoic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Approver_ROIC");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.DeletedInd).HasColumnName("Deleted_Ind");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Last_Update_By");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Last_Update_Date");
            entity.Property(e => e.LeaveDetailId).HasColumnName("Leave_Detail_ID");
            entity.Property(e => e.LeaveEndDate)
                .HasColumnType("datetime")
                .HasColumnName("Leave_End_Date");
            entity.Property(e => e.LeaveRequestHours)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("Leave_Request_Hours");
            entity.Property(e => e.LeaveRequestSubmitDate)
                .HasColumnType("datetime")
                .HasColumnName("Leave_Request_Submit_Date");
            entity.Property(e => e.LeaveStartDate)
                .HasColumnType("datetime")
                .HasColumnName("Leave_Start_Date");
            entity.Property(e => e.RequesterComment)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Requester_Comment");
            entity.Property(e => e.RequesterRoic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Requester_ROIC");

            entity.HasOne(d => d.ApprovalStatus).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.ApprovalStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApprovalStatus_LeaveRequest");

            entity.HasOne(d => d.ApproverRoicNavigation).WithMany(p => p.LeaveRequestApproverRoicNavigations)
                .HasForeignKey(d => d.ApproverRoic)
                .HasConstraintName("FK_Employee_LeaveRequest_Approver");

            entity.HasOne(d => d.LeaveDetail).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.LeaveDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeaveDetailCode_LeaveRequest");

            entity.HasOne(d => d.RequesterRoicNavigation).WithMany(p => p.LeaveRequestRequesterRoicNavigations)
                .HasForeignKey(d => d.RequesterRoic)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_LeaveRequest_Requester");
        });

        modelBuilder.Entity<LeaveTransaction>(entity =>
        {
            entity.HasKey(e => e.LeaveTransactionId)
                .HasName("PK10")
                .IsClustered(false);

            entity.ToTable("Leave_Transaction");

            entity.Property(e => e.LeaveTransactionId).HasColumnName("Leave_Transaction_ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.DeletedInd).HasColumnName("Deleted_Ind");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Last_Update_By");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Last_Update_Date");
            entity.Property(e => e.LeaveDetailId).HasColumnName("Leave_Detail_ID");
            entity.Property(e => e.LeaveHours)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Leave_Hours");
            entity.Property(e => e.LeaveTransactionComments)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Leave_Transaction_Comments");
            entity.Property(e => e.LeaveTransactionDate).HasColumnName("Leave_Transaction_Date");
            entity.Property(e => e.LeaveTransactionTypeId).HasColumnName("Leave_Transaction_Type_ID");
            entity.Property(e => e.Roic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ROIC");

            entity.HasOne(d => d.LeaveDetail).WithMany(p => p.LeaveTransactions)
                .HasForeignKey(d => d.LeaveDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LeaveDetailCode_LeaveTransaction");

            entity.HasOne(d => d.LeaveTransactionType).WithMany(p => p.LeaveTransactions)
                .HasForeignKey(d => d.LeaveTransactionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RefLeave_Transaction_Type60");

            entity.HasOne(d => d.RoicNavigation).WithMany(p => p.LeaveTransactions)
                .HasForeignKey(d => d.Roic)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_LeaveTransaction");
        });

        modelBuilder.Entity<LeaveTransactionType>(entity =>
        {
            entity.HasKey(e => e.LeaveTransactionTypeId)
                .HasName("PK28")
                .IsClustered(false);

            entity.ToTable("Leave_Transaction_Type");

            entity.Property(e => e.LeaveTransactionTypeId).HasColumnName("Leave_Transaction_Type_ID");
            entity.Property(e => e.LeaveTransactionDesc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Leave_Transaction_Desc");
        });

        modelBuilder.Entity<PhoneNumber>(entity =>
        {
            entity.HasKey(e => e.PhoneNumberId)
                .HasName("PK4")
                .IsClustered(false);

            entity.ToTable("Phone_Number");

            entity.Property(e => e.PhoneNumberId).HasColumnName("Phone_Number_ID");
            entity.Property(e => e.ContactTypeId).HasColumnName("Contact_Type_ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.DeletedInd).HasColumnName("Deleted_Ind");
            entity.Property(e => e.DistrictId).HasColumnName("District_ID");
            entity.Property(e => e.EmergencyContactId).HasColumnName("Emergency_Contact_ID");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Last_Update_By");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Last_Update_Date");
            entity.Property(e => e.PhoneNbr)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("Phone_Nbr");
            entity.Property(e => e.Roic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ROIC");
            entity.Property(e => e.StationId).HasColumnName("Station_ID");

            entity.HasOne(d => d.ContactType).WithMany(p => p.PhoneNumbers)
                .HasForeignKey(d => d.ContactTypeId)
                .HasConstraintName("FK_ContactType_PhoneNumber");

            entity.HasOne(d => d.District).WithMany(p => p.PhoneNumbers)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK_District_PhoneNumber");

            entity.HasOne(d => d.EmergencyContact).WithMany(p => p.PhoneNumbers)
                .HasForeignKey(d => d.EmergencyContactId)
                .HasConstraintName("FK_EmergencyContact_PhoneNumber");

            entity.HasOne(d => d.RoicNavigation).WithMany(p => p.PhoneNumbers)
                .HasForeignKey(d => d.Roic)
                .HasConstraintName("FK_Employee_PhoneNumber");

            entity.HasOne(d => d.Station).WithMany(p => p.PhoneNumbers)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("FK_Station_PhoneNumber");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.ShiftId)
                .HasName("PK21")
                .IsClustered(false);

            entity.ToTable("Shift");

            entity.Property(e => e.ShiftId).HasColumnName("Shift_ID");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("Is_Active");
            entity.Property(e => e.ShiftCode)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Shift_Code");
            entity.Property(e => e.ShiftNme)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Shift_Nme");
            entity.Property(e => e.SortOrder).HasColumnName("Sort_Order");
        });

        modelBuilder.Entity<SickSellbackCode>(entity =>
        {
            entity.HasKey(e => e.SickSellbackCodeId)
                .HasName("PK30")
                .IsClustered(false);

            entity.ToTable("Sick_Sellback_Code");

            entity.Property(e => e.SickSellbackCodeId).HasColumnName("Sick_Sellback_Code_ID");
            entity.Property(e => e.ShiftId).HasColumnName("Shift_ID");
            entity.Property(e => e.SickSellbackCode1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Sick_Sellback_Code");
            entity.Property(e => e.SickSellbackCodeComments)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Sick_Sellback_Code_Comments");
            entity.Property(e => e.SickSellbackDesc)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Sick_Sellback_Desc");

            entity.HasOne(d => d.Shift).WithMany(p => p.SickSellbackCodes)
                .HasForeignKey(d => d.ShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shift_SickSellbackCode");
        });

        modelBuilder.Entity<SickSellbackPayout>(entity =>
        {
            entity.HasKey(e => e.SickSellbackPayoutId)
                .HasName("PK13")
                .IsClustered(false);

            entity.ToTable("Sick_Sellback_Payout");

            entity.Property(e => e.SickSellbackPayoutId).HasColumnName("Sick_Sellback_Payout_ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.DeletedInd).HasColumnName("Deleted_Ind");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Last_Update_By");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Last_Update_Date");
            entity.Property(e => e.PayTypeCde).HasColumnName("Pay_Type_Cde");
            entity.Property(e => e.PayrollExportDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Payroll_Export_DateTime");
            entity.Property(e => e.Roic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ROIC");
            entity.Property(e => e.SellbackPayoutHours)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("Sellback_Payout_Hours");
            entity.Property(e => e.SellbackPayoutYear).HasColumnName("Sellback_Payout_Year");
            entity.Property(e => e.SickSellbackSelectionId).HasColumnName("Sick_Sellback_Selection_ID");

            entity.HasOne(d => d.RoicNavigation).WithMany(p => p.SickSellbackPayouts)
                .HasForeignKey(d => d.Roic)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_SickSellbackPayout");

            entity.HasOne(d => d.SickSellbackSelection).WithMany(p => p.SickSellbackPayouts)
                .HasForeignKey(d => d.SickSellbackSelectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SickSellbackSelection_SickSellbackPayout");
        });

        modelBuilder.Entity<SickSellbackSelection>(entity =>
        {
            entity.HasKey(e => e.SickSellbackSelectionId)
                .HasName("PK12")
                .IsClustered(false);

            entity.ToTable("Sick_Sellback_Selection");

            entity.Property(e => e.SickSellbackSelectionId).HasColumnName("Sick_Sellback_Selection_ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.DeletedInd).HasColumnName("Deleted_Ind");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Last_Update_By");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Last_Update_Date");
            entity.Property(e => e.Roic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ROIC");
            entity.Property(e => e.SellbackSelectionDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Sellback_Selection_DateTime");
            entity.Property(e => e.SellbackSelectionYear).HasColumnName("Sellback_Selection_Year");
            entity.Property(e => e.ShiftId).HasColumnName("Shift_ID");
            entity.Property(e => e.SickSellbackCodeId).HasColumnName("Sick_Sellback_Code_ID");

            entity.HasOne(d => d.RoicNavigation).WithMany(p => p.SickSellbackSelections)
                .HasForeignKey(d => d.Roic)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_SickSellbackSelection");

            entity.HasOne(d => d.Shift).WithMany(p => p.SickSellbackSelections)
                .HasForeignKey(d => d.ShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shift_SickSellbackSelection");

            entity.HasOne(d => d.SickSellbackCode).WithMany(p => p.SickSellbackSelections)
                .HasForeignKey(d => d.SickSellbackCodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SickSellbackCode_SickSellbackSelection");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.StationId)
                .HasName("PK18")
                .IsClustered(false);

            entity.ToTable("Station");

            entity.Property(e => e.StationId).HasColumnName("Station_ID");
            entity.Property(e => e.DistrictId).HasColumnName("District_ID");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("Is_Active");
            entity.Property(e => e.StationComment)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("Station_Comment");
            entity.Property(e => e.StationNbr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Station_Nbr");

            entity.HasOne(d => d.District).WithMany(p => p.Stations)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_District_Station");
        });

        modelBuilder.Entity<StationDistance>(entity =>
        {
            entity.HasKey(e => e.StationDistanceId)
                .HasName("PK24")
                .IsClustered(false);

            entity.ToTable("Station_Distance");

            entity.Property(e => e.StationDistanceId).HasColumnName("Station_Distance_ID");
            entity.Property(e => e.DistanceMiles)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("Distance_Miles");
            entity.Property(e => e.FromStationId).HasColumnName("From_Station_ID");
            entity.Property(e => e.ToStationId).HasColumnName("To_Station_ID");

            entity.HasOne(d => d.FromStation).WithMany(p => p.StationDistanceFromStations)
                .HasForeignKey(d => d.FromStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Station_StationDistance_From");

            entity.HasOne(d => d.ToStation).WithMany(p => p.StationDistanceToStations)
                .HasForeignKey(d => d.ToStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Station_StationDistance_To");
        });

        modelBuilder.Entity<TransferMileage>(entity =>
        {
            entity.HasKey(e => e.TransferMileageId)
                .HasName("PK8")
                .IsClustered(false);

            entity.ToTable("Transfer_Mileage");

            entity.Property(e => e.TransferMileageId).HasColumnName("Transfer_Mileage_ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.DeletedInd).HasColumnName("Deleted_Ind");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Last_Update_By");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Last_Update_Date");
            entity.Property(e => e.Roic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ROIC");
            entity.Property(e => e.ScheduledReportDate)
                .HasColumnType("datetime")
                .HasColumnName("Scheduled_Report_Date");
            entity.Property(e => e.StationDistanceId).HasColumnName("Station_Distance_ID");
            entity.Property(e => e.TransferDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Transfer_DateTime");

            entity.HasOne(d => d.RoicNavigation).WithMany(p => p.TransferMileages)
                .HasForeignKey(d => d.Roic)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_TransferMileage");

            entity.HasOne(d => d.StationDistance).WithMany(p => p.TransferMileages)
                .HasForeignKey(d => d.StationDistanceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StationDistance_TransferMileage");
        });

        modelBuilder.Entity<TransferRequest>(entity =>
        {
            entity.HasKey(e => e.TransferRequestId)
                .HasName("PK9")
                .IsClustered(false);

            entity.ToTable("Transfer_Request");

            entity.Property(e => e.TransferRequestId).HasColumnName("Transfer_Request_ID");
            entity.Property(e => e.ApprovalStatusId).HasColumnName("Approval_Status_ID");
            entity.Property(e => e.ApproverComment)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Approver_Comment");
            entity.Property(e => e.ApproverRoic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Approver_ROIC");
            entity.Property(e => e.CompanyFromId).HasColumnName("Company_From_ID");
            entity.Property(e => e.CompanyPositionFromId).HasColumnName("Company_Position_From_ID");
            entity.Property(e => e.CompanyPositionToId).HasColumnName("Company_Position_To_ID");
            entity.Property(e => e.CompanyToId).HasColumnName("Company_To_ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.DeletedInd).HasColumnName("Deleted_Ind");
            entity.Property(e => e.HazMatPosition).HasColumnName("HazMat_Position");
            entity.Property(e => e.HazMatRequested).HasColumnName("HazMat_Requested");
            entity.Property(e => e.IsTemp).HasColumnName("Is_Temp");
            entity.Property(e => e.LastUpdateBy)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Last_Update_By");
            entity.Property(e => e.LastUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Last_Update_Date");
            entity.Property(e => e.ParamedicPosition).HasColumnName("Paramedic_Position");
            entity.Property(e => e.ParamedicRequested).HasColumnName("Paramedic_Requested");
            entity.Property(e => e.RequesterJobTitleId).HasColumnName("Requester_Job_Title_Id");
            entity.Property(e => e.RequesterRoic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Requester_ROIC");
            entity.Property(e => e.ShiftFromId).HasColumnName("Shift_From_ID");
            entity.Property(e => e.ShiftToId).HasColumnName("Shift_To_ID");
            entity.Property(e => e.StationFromId).HasColumnName("Station_From_ID");
            entity.Property(e => e.StationToId).HasColumnName("Station_To_ID");
            entity.Property(e => e.TransferRequestComment)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Transfer_Request_Comment");
            entity.Property(e => e.TransferRequestDate)
                .HasColumnType("datetime")
                .HasColumnName("Transfer_Request_Date");

            entity.HasOne(d => d.ApprovalStatus).WithMany(p => p.TransferRequests)
                .HasForeignKey(d => d.ApprovalStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApprovalStatus_TransferRequest");

            entity.HasOne(d => d.ApproverRoicNavigation).WithMany(p => p.TransferRequestApproverRoicNavigations)
                .HasForeignKey(d => d.ApproverRoic)
                .HasConstraintName("FK_Employee_TransferRequest_Approver");

            entity.HasOne(d => d.CompanyFrom).WithMany(p => p.TransferRequestCompanyFroms)
                .HasForeignKey(d => d.CompanyFromId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_TransferRequest_From");

            entity.HasOne(d => d.CompanyPositionFrom).WithMany(p => p.TransferRequestCompanyPositionFroms)
                .HasForeignKey(d => d.CompanyPositionFromId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyPosition_TransferRequest_From");

            entity.HasOne(d => d.CompanyPositionTo).WithMany(p => p.TransferRequestCompanyPositionTos)
                .HasForeignKey(d => d.CompanyPositionToId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyPosition_TransferRequest_To");

            entity.HasOne(d => d.CompanyTo).WithMany(p => p.TransferRequestCompanyTos)
                .HasForeignKey(d => d.CompanyToId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Company_TransferRequest_To");

            entity.HasOne(d => d.RequesterJobTitle).WithMany(p => p.TransferRequests)
                .HasForeignKey(d => d.RequesterJobTitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobTitle_TransferRequest");

            entity.HasOne(d => d.RequesterRoicNavigation).WithMany(p => p.TransferRequestRequesterRoicNavigations)
                .HasForeignKey(d => d.RequesterRoic)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employee_TransferRequest_Requester");

            entity.HasOne(d => d.ShiftFrom).WithMany(p => p.TransferRequestShiftFroms)
                .HasForeignKey(d => d.ShiftFromId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shift_TransferRequest_From");

            entity.HasOne(d => d.ShiftTo).WithMany(p => p.TransferRequestShiftTos)
                .HasForeignKey(d => d.ShiftToId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shift_TransferRequest_To");

            entity.HasOne(d => d.StationFrom).WithMany(p => p.TransferRequestStationFroms)
                .HasForeignKey(d => d.StationFromId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Station_TransferRequest_From");

            entity.HasOne(d => d.StationTo).WithMany(p => p.TransferRequestStationTos)
                .HasForeignKey(d => d.StationToId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Station_TransferRequest_To");
        });

        modelBuilder.Entity<WorkPeriod>(entity =>
        {
            entity.HasKey(e => e.WorkPeriodId)
                .HasName("PK20")
                .IsClustered(false);

            entity.ToTable("Work_Period");

            entity.HasIndex(e => e.WorkPeriodNbr, "UQ_Work_Period_Work_Period_Nbr").IsUnique();

            entity.Property(e => e.WorkPeriodId).HasColumnName("Work_Period_ID");
            entity.Property(e => e.WeekDay)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.WorkPeriodNbr).HasColumnName("Work_Period_Nbr");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
