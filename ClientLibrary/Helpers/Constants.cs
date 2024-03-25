
namespace ClientLibrary.Helpers
{
    public class Constants
    {
        public const string GeneralDepartmentBaseUrl = "api/generaldepartment";
        public const string DepartmentBaseUrl = "api/department";
        public const string BranchBaseUrl = "api/branch";
        public const string ItemBaseUrl = "api/item";
        public const string ItemRequestBaseUrl = "api/manageitem";
        public const string ItemOrderBaseUrl = "api/managerequest";


        public static string New { get; } = "New";
        public static string Accepted { get; } = "Accepted";
        public static string Rejected { get; } = "Rejected";

    }
}
