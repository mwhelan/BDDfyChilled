using Chill;
using Ploeh.AutoFixture;

namespace BDDfyChilled.TestSubjects
{
    public class CustomerMother : ObjectMother<Customer>
    {
        static Fixture fixture = new Fixture();
        protected override Customer Create()
        {
            return fixture.Create<Customer>();
        }
    }
}