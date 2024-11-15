using Resources.Models.DbModels;

namespace DAL.Repository;

public class AddressRepository : DirectDbRepository<Address>, IAddressRepository
{
    public AddressRepository(AppDbContext context) : base(context)
    {

    }
}