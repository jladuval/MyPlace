namespace Common.DI
{
    using Castle.Facilities.Startable;
    using Castle.MicroKernel.Registration;

    using Base.Infrastructure.Attributes;

    public static class ReflectionHelpers
    {
        public static BasedOnDescriptor StartIfNecessary(this BasedOnDescriptor descriptor)
        {
            return descriptor.Configure(x =>
                                            {
                                                if (x.Implementation.ShouldStartComponent())
                                                {
                                                    x.Start();
                                                }
                                            });
        }
    }
}