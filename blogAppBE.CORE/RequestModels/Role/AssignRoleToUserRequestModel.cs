namespace blogAppBE.CORE.RequestModels.Role
{
    public class AssignRoleToUserRequestModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}