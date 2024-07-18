namespace blogAppBE.CORE.Enums
{
    public enum StatusCode
    {
        OK = 200,
        Created = 201,
        Accepted = 202,
        BadRequest = 400,
        UnAuthorized = 401,
        PaymentRequired = 402,
        Forbidden = 403,
        NotFound = 404,
        Conflict = 409,
        InternalServerError = 500
    }
}