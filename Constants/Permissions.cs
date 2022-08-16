namespace HRSystem.Constants
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsList(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.View",
                $"Permissions.{module}.Create",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            };
        }
        public static List<string> GenerateAllPermissions()
        {
            var allPermissions = new List<string>();
            var modules = Enum.GetValues(typeof(Modules));
            foreach (var module in modules)
                allPermissions.AddRange(GeneratePermissionsList(module.ToString()));
            return allPermissions;
        }
        public static class Employee
        {
            public const string View = "Permissions.employee.View";
            public const string Create = "Permissions.employee.Create";
            public const string Edit = "Permissions.employee.Edit";
            public const string Delete = "Permissions.employee.Delete";
        }
        public static class Department
        {
            public const string View = "Permissions.department.View";
            public const string Create = "Permissions.department.Create";
            public const string Edit = "Permissions.department.Edit";
            public const string Delete = "Permissions.department.Delete";
        }
        public static class Exception

        {
            public const string View = "Permissions.exception.View";
            public const string Create = "Permissions.exception.Create";
            public const string Edit = "Permissions.exception.Edit";
            public const string Delete = "Permissions.exception.Delete";
        }
        public static class Salary
        {
            public const string View = "Permissions.salaryReport.View";
            public const string Create = "Permissions.salaryReport.Create";
            public const string Edit = "Permissions.salaryReport.Edit";
            public const string Delete = "Permissions.salaryReport.Delete";
        }
        public static class Permission
        {
            public const string View = "Permissions.permission.View";
            public const string Create = "Permissions.permission.Create";
            public const string Edit = "Permissions.permission.Edit";
            public const string Delete = "Permissions.permission.Delete";
        }
        public static class chat
        {
            public const string View = "Permissions.chat.View";
            public const string Create = "Permissions.chat.Create";
            public const string Edit = "Permissions.chat.Edit";
            public const string Delete = "Permissions.chat.Delete";
        }
        public static class generalSetting
        {
            public const string View = "Permissions.generalSetting.View";
            public const string Create = "Permissions.generalSetting.Create";
            public const string Edit = "Permissions.generalSetting.Edit";
            public const string Delete = "Permissions.generalSetting.Delete";
        }
        public static class User
        {
            public const string View = "Permissions.user.View";
            public const string Create = "Permissions.user.Create";
            public const string Edit = "Permissions.user.Edit";
            public const string Delete = "Permissions.user.Delete";
        }
        public static class attendance
        {
            public const string View = "Permissions.attendance.View";
            public const string Create = "Permissions.attendance.Create";
            public const string Edit = "Permissions.attendance.Edit";
            public const string Delete = "Permissions.attendance.Delete";
        }
    }
}
