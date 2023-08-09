namespace App.Persistence.Attributes;

/// <summary>
/// Attribute class used to mark commands that should be handled within the user context unit of work.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class UserContextUnitOfWorkAttribute : Attribute { }
