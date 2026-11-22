using MultiShop.Cargo.Domain.Entites;
using MultiShop.Cargo.Repositories.EntityFramework.Abstract;

namespace MultiShop.Cargo.Repositories.EntityFramework;

public class EFCargoDetailRepository(CargoContext context) : GenericRepository<CargoDetail, int>(context), IEfCargoDetailRepository;