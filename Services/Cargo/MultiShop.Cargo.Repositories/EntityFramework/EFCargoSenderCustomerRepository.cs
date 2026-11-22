using MultiShop.Cargo.Domain.Entites;
using MultiShop.Cargo.Repositories.EntityFramework.Abstract;

namespace MultiShop.Cargo.Repositories.EntityFramework;

public class EFCargoSenderCustomerRepository(CargoContext context) : GenericRepository<CargoSenderCustomer, int>(context), IEfCargoSenderCustomerRepository;