using System;

namespace Base.CQRS.Query.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ReaderAttribute : Attribute
    {
    }
}
