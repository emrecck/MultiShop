using MultiShop.Cargo.Domain.Entites;
using MultiShop.Cargo.Repositories.EntityFramework.Abstract;

namespace MultiShop.Cargo.Repositories.EntityFramework;

public class EFCargoReceiverCustomerRepository(CargoContext context) : GenericRepository<CargoReceiverCustomer, int>(context), IEfCargoReceiverCustomerRepository;