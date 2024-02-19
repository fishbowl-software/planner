namespace FishbowlSoftware.Planner.Domain.Roles
{
    public static class BuiltInRoles
    {
        public const string SuperAdmin = "superadmin";
        public const string Admin = "admin";

        public static IEnumerable<string> GetRoleNames()
        {
            yield return SuperAdmin;
            yield return Admin;
        }
    }
}
