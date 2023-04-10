namespace DroneDeliveryService.Entities
{
    public class Drone
    {
        public string Name { get; set; }
        public decimal MaximunWeight { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
