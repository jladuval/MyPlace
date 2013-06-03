using System;

namespace Base.CQRS.Commands.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandAttribute : Attribute
    {
    }
}