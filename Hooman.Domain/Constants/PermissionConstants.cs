namespace Hooman.Domain.Constants;
public static class PermissionConstants
{
   // User Management
    public const string UsersView = "users.view";
    public const string UsersCreate = "users.create";
    public const string UsersEdit = "users.edit";
    public const string UsersDelete = "users.delete";
    public const string UsersManageRoles = "users.manage_roles";

    // Course Management
    public const string CoursesView = "courses.view";
    public const string CoursesCreate = "courses.create";
    public const string CoursesEdit = "courses.edit";
    public const string CoursesDelete = "courses.delete";
    public const string CoursesPublish = "courses.publish";
    public const string CoursesEnroll = "courses.enroll";

    // Content Management
    public const string ContentView = "content.view";
    public const string ContentCreate = "content.create";
    public const string ContentEdit = "content.edit";
    public const string ContentDelete = "content.delete";
    public const string ContentModerate = "content.moderate";

    // Assessment Management
    public const string AssessmentsView = "assessments.view";
    public const string AssessmentsCreate = "assessments.create";
    public const string AssessmentsEdit = "assessments.edit";
    public const string AssessmentsDelete = "assessments.delete";
    public const string AssessmentsGrade = "assessments.grade";

    // Analytics
    public const string AnalyticsView = "analytics.view";
    public const string AnalyticsExport = "analytics.export";

    // System
    public const string SystemSettings = "system.settings";
    public const string SystemRoles = "system.roles";
    public const string SystemAudit = "system.audit";
}
