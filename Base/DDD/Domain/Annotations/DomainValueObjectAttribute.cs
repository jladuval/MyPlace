using System;

namespace Base.DDD.Domain.Annotations
{
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Struct)]
    public class DomainValueObjectAttribute : Attribute
    {
    }
}