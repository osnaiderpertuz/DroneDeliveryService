namespace DroneDeliveryService.Entities
{
    public class Delivery
    {
        public Drone Drone { get; set; }
        public List<Trip> Trips { get; set; }
        public bool DroneCanCarryAllPackages { get; set; }
        public string Observations { get; set; }
        public override string ToString()
        {
            return $"{Drone}\n{string.Join('\n', Trips)}{(Observations != null ? $"\n{Observations}" : "")}";
        }
    }
}
