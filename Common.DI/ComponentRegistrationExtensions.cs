namespace Common.DI
{
    using Castle.MicroKernel.Registration;

    using Common.DI.PerSessionLifestyle;

    public static class ComponentRegistrationExtensions
    {
        public static BasedOnDescriptor LifestylePerSession(this BasedOnDescriptor reg)
        {
            return reg.LifestyleScoped<WebSessionScopeAccessor>();
        }
    }
}