using MultiShop.Cargo.Domain.Entites;
using MultiShop.Cargo.Repositories.EntityFramework.Abstract;

namespace MultiShop.Cargo.Repositories.EntityFramework;

public class EFCargoOperationRepository(CargoContext context) : GenericRepository<CargoOperation, int>(context), IEfCargoOperationRepository;