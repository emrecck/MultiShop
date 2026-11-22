using MultiShop.Cargo.Domain.Entites;
using MultiShop.Cargo.Repositories.EntityFramework.Abstract;

namespace MultiShop.Cargo.Repositories.EntityFramework;

public class EFCargoCompanyRepository(CargoContext context) : GenericRepository<CargoCompany, int>(context), IEfCargoCompanyRepository;