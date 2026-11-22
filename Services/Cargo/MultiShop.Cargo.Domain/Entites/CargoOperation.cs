using System;

namespace MultiShop.Cargo.Domain.Entites;

public class CargoOperation : BaseEntity<int>, IEntity<int>
{
    public string Location { get; set; }
    public string TargetLocation { get; set; }

}
