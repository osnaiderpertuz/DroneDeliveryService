namespace DroneDeliveryService.Entities
{
    public class Trip
    {
        public int Order { get; set; }
        public List<Location> Locations { get; set; }

        public override string ToString()
        {
            return $"Trip #{Order}\n{string.Join(',', Locations.Select(l => l.Name))}";
        }
    }
}
