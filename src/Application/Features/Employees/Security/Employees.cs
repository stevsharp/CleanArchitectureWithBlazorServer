namespace CleanArchitecture.Blazor.Application.Common.Security;
public static partial class Permissions
{
    [DisplayName("Employee Permissions")]
    [Description("Set permissions for employee operations.")]
    public static class Employees
    {
        [Description("Allows viewing employee details.")]
        public const string View = "Permissions.Employees.View";
        [Description("Allows creating employee details.")]
        public const string Create = "Permissions.Employees.Create";
        [Description("Allows editing employee details.")]
        public const string Edit = "Permissions.Employees.Edit";
        [Description("Allows deleting employee details.")]
        public const string Delete = "Permissions.Employees.Delete";
        [Description("Allows printing employee details.")]
        public const string Print = "Permissions.Employees.Print";
        [Description("Allows searching employee details.")]
        public const string Search = "Permissions.Employees.Search";
        [Description("Allows exporting employee details.")]
        public const string Export = "Permissions.Employees.Export";
        [Description("Allows importing employee details.")]
        public const string Import = "Permissions.Employees.Import";
    }
}

public class EmployeesAccessRights
{
    public bool View { get; set; }
    public bool Create { get; set; }
    public bool Edit { get; set; }
    public bool Delete { get; set; }
    public bool Print { get; set; }
    public bool Search { get; set; }
    public bool Export { get; set; }
    public bool Import { get; set; }
}
