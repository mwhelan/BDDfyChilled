using System.Threading.Tasks;

namespace BDDfyChilled.TestSubjects
{
    public interface ICustomerStore
    {
        Customer GetCustomer(int id);

        Task<Customer> GetCustomerAsync(int id);

        void DeleteCustomer(int id);
    }
}