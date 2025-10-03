﻿namespace CleanArchitecture.Blazor.Application.Common.Security;
public static partial class Permissions
{
    [DisplayName("ProjectTask Permissions")]
    [Description("Set permissions for projecttask operations.")]
    public static class ProjectTasks
    {
        [Description("Allows viewing projecttask details.")]
        public const string View = "Permissions.ProjectTasks.View";
        [Description("Allows creating projecttask details.")]
        public const string Create = "Permissions.ProjectTasks.Create";
        [Description("Allows editing projecttask details.")]
        public const string Edit = "Permissions.ProjectTasks.Edit";
        [Description("Allows deleting projecttask details.")]
        public const string Delete = "Permissions.ProjectTasks.Delete";
        [Description("Allows printing projecttask details.")]
        public const string Print = "Permissions.ProjectTasks.Print";
        [Description("Allows searching projecttask details.")]
        public const string Search = "Permissions.ProjectTasks.Search";
        [Description("Allows exporting projecttask details.")]
        public const string Export = "Permissions.ProjectTasks.Export";
        [Description("Allows importing projecttask details.")]
        public const string Import = "Permissions.ProjectTasks.Import";
    }
}

public class ProjectTasksAccessRights
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
