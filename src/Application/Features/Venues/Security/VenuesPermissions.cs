using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Blazor.Application.Common.Security;

public static partial class Permissions
{
    [DisplayName("Venue Permissions")]
    [Description("Set permissions for venue operations.")]
    public static class Venues
    {
        [Description("Allows viewing venue details.")]
        public const string View = "Permissions.Venues.View";
        [Description("Allows creating venue details.")]
        public const string Create = "Permissions.Venues.Create";
        [Description("Allows editing venue details.")]
        public const string Edit = "Permissions.Venues.Edit";
        [Description("Allows deleting venue details.")]
        public const string Delete = "Permissions.Venues.Delete";
        [Description("Allows printing venue details.")]
        public const string Print = "Permissions.Venues.Print";
        [Description("Allows searching venue details.")]
        public const string Search = "Permissions.Venues.Search";
        [Description("Allows exporting venue details.")]
        public const string Export = "Permissions.Venues.Export";
        [Description("Allows importing venue details.")]
        public const string Import = "Permissions.Venues.Import";
    }
}

public class VenuesAccessRights
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

