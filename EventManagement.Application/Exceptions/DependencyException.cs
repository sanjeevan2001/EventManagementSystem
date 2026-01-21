using System;

namespace EventManagement.Application.Exceptions
{
    public class DependencyException : Exception
    {
        public DependencyException(string entityName, string dependencyName)
            : base($"Cannot delete {entityName} because it has associated {dependencyName}.")
        {
        }

        public DependencyException(string message)
            : base(message)
        {
        }

        public DependencyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
