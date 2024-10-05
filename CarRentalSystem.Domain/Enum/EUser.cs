namespace CarRentalSystem.Domain.Enum
{
    public enum ERole
    {
        Customer = 400,
        Appraiser = 450,
        Admin = 500,
    }
    public enum ESignInMethod
    {
        Default = 1,
        Facebook = 2,
        Google = 3,
    }
    public enum EUserStatus
    {
        Inactive = 0,
        Active = 1,
    }
    public enum EVerifyStatus
    {
        Unverified = 1001,
        Verified = 1002,
        Pending = 1003,
    }
}