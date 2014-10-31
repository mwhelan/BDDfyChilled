using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Chill;
using NSubstitute;

namespace BDDfyChilled
{
    internal class AutofacNSubstituteChillContainer : AutofacChillContainer
    {

        public AutofacNSubstituteChillContainer()
            : base(CreateBuilder())
        {
        }

        static ContainerBuilder CreateBuilder()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterSource(new NSubstituteRegistrationHandler());
            return containerBuilder;
        }

        /// <summary> Resolves unknown interfaces and Mocks using the <see cref="Substitute"/>. </summary>
        internal class NSubstituteRegistrationHandler : IRegistrationSource
        {
            /// <summary>
            /// Retrieve a registration for an unregistered service, to be used
            /// by the container.
            /// </summary>
            /// <param name="service">The service that was requested.</param>
            /// <param name="registrationAccessor"></param>
            /// <returns>
            /// Registrations for the service.
            /// </returns>
            public IEnumerable<IComponentRegistration> RegistrationsFor
                (Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
            {
                if (service == null)
                    throw new ArgumentNullException("service");

                var typedService = service as IServiceWithType;
                if (typedService == null ||
                    !typedService.ServiceType.IsInterface ||
                    typedService.ServiceType.IsGenericType &&
                    typedService.ServiceType.GetGenericTypeDefinition() == typeof(IEnumerable<>) ||
                    typedService.ServiceType.IsArray ||
                    typeof(IStartable).IsAssignableFrom(typedService.ServiceType))
                    return Enumerable.Empty<IComponentRegistration>();

                var rb = RegistrationBuilder.ForDelegate((c, p) => Substitute.For(new[] { typedService.ServiceType }, null))
                    .As(service)
                    .InstancePerLifetimeScope();

                return new[] { rb.CreateRegistration() };
            }

            public bool IsAdapterForIndividualComponents
            {
                get { return false; }
            }
        }
    }
    internal class AutofacChillContainer : IChillContainer
    {
        private IContainer _container;
        private ContainerBuilder _containerBuilder;

        public AutofacChillContainer()
            : this(new ContainerBuilder())
        {
        }

        public AutofacChillContainer(IContainer container)
        {
            _container = container;
        }

        public AutofacChillContainer(ContainerBuilder containerBuilder)
        {
            _containerBuilder = containerBuilder;
        }

        protected IContainer Container
        {
            get
            {
                if (_container == null)
                    _container = _containerBuilder.Build();
                return _container;
            }
        }

        public void Dispose()
        {
            Container.Dispose();
        }


        public void RegisterType<T>()
        {
            Container.ComponentRegistry.Register(RegistrationBuilder.ForType<T>().InstancePerLifetimeScope().CreateRegistration());
        }

        public T Get<T>(string key = null) where T : class
        {
            if (key == null)
            {
                return Container.Resolve<T>();
            }
            else
            {
                return Container.ResolveKeyed<T>(key);
            }
        }

        public T Set<T>(T valueToSet, string key = null) where T : class
        {
            if (key == null)
            {
                Container.ComponentRegistry
                    .Register(RegistrationBuilder.ForDelegate((c, p) => valueToSet)
                        .InstancePerLifetimeScope().CreateRegistration());

            }
            else
            {
                Container.ComponentRegistry
                    .Register(RegistrationBuilder.ForDelegate((c, p) => valueToSet)
                        .As(new KeyedService(key, typeof(T)))
                        .InstancePerLifetimeScope().CreateRegistration());
            }
            return Get<T>();
        }


        public bool IsRegistered<T>()
        {
            return IsRegistered(typeof(T));
        }

        public bool IsRegistered(System.Type type)
        {
            return Container.IsRegistered(type);
        }
    }

}