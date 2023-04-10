namespace DroneDeliveryService.Entities
{
    public class Location
    {
        public string Name { get; set; }
        public decimal PackageWeight { get; set; }
        public override string ToString()
        {
            return $"{Name} [{PackageWeight}]";
        }
    }
}
