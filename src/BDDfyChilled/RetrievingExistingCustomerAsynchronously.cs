using BDDfyChilled.NUnit;
using BDDfyChilled.TestSubjects;
using Chill;
using FluentAssertions;
using NSubstitute;

namespace BDDfyChilled
{
    [ChillTestInitializer(typeof(DefaultChillTestInitializer<AutofacNSubstituteChillContainer>))]
    public class RetrievingExistingCustomerAsynchronously : SpecificationFor<CustomerController, View>
    {
        const int customerId = 12;

        public void Given_an_existing_customer()
        {
            Given(() =>
            {
                The<Customer>()
                    .With(x => x.Id = customerId);

                The<ICustomerStore>()
                    .GetCustomerAsync(customerId)
                    .Returns(The<Customer>().Asynchronously());
            });
        }

        public void When_retrieving_the_customer_asynchronously()
        {

            When(() => Subject.GetAsync(customerId));
        }

        public void Then_view_is_returned()
        {
            Result.Should().NotBeNull();
        }

        public void AndThen_model_is_the_existing_customer()
        {
            Result.Model.Should().Be(The<Customer>());
        }
    }
}