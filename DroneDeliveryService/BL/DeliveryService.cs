using DroneDeliveryService.Entities;
using DroneDeliveryService.Data;
using System.Text;

namespace DroneDeliveryService.BL
{
    public class DeliveryService
    {
        /// <summary>
        /// This method starts the process to calculate and arrange the delivery options with the given data.
        /// </summary>
        /// <param name="path">The input data file path<</param>
        public static void Start(string path)
        {
            if (DataManager.LoadInputData(path, out List<Drone>? drones, out List<Location>? locations))
            {
                if (drones?.Count <= 100)
                {
                    var deliveryOptions = ArrangeDeliveries(drones!, locations!);
                    if (deliveryOptions.Any())
                    {
                        string ouput = string.Join("\n\n", deliveryOptions);
                        string directory = new DirectoryInfo(path).Parent.FullName;
                        DataManager.SaveOuputData(Path.Combine(directory, "Output.txt"), ouput);
                        Console.WriteLine(ouput);
                    }
                }
                else
                {
                    Console.WriteLine($"The maximum number of drones in a squad must be 100. There are {drones?.Count} drones.");
                }
            }
            else
            {
                Console.WriteLine($"An error occurred while loading input data from '{path}'");
            }
        }
        /// <summary>
        /// This function arranges the possible delivery plans with the given drones and locations.
        /// </summary>
        /// <param name="drones">The list of drones of the squad.</param>
        /// <param name="locations">The locations where the packages need to be sended.</param>
        /// <returns>The arranged delivery plan for the all drones.</returns>
        private static List<Delivery> ArrangeDeliveries(List<Drone> drones, List<Location> locations)
        {
            drones = drones.Where(d => d.MaximunWeight > 0).ToList();
            locations = locations.Where(l => l.PackageWeight > 0).ToList();
            locations = locations.OrderBy(l => l.PackageWeight).ToList();
            List<Delivery> deliveryOptions = new();
            foreach (Drone drone in drones)
            {
                var delivery = ArrangeDeliveryByDrone(drone, locations);
                deliveryOptions.Add(delivery);
            }
            return deliveryOptions;
        }

        /// <summary>
        /// This function arranges the delivery plan with the specific drone and the given locations.
        /// </summary>
        /// <param name="drone"></param>
        /// <param name="locations"></param>
        /// <returns>The arranged delivery plan for the specific drone.</returns>
        private static Delivery ArrangeDeliveryByDrone(Drone drone, List<Location> locations)
        {
            List<Location> validLocations = locations.Where(l => l.PackageWeight <= drone.MaximunWeight).ToList();
            List<Location> invalidLocations = locations.Where(l => l.PackageWeight > drone.MaximunWeight).ToList();
            Delivery delivery = new()
            {
                Drone = drone,
                DroneCanCarryAllPackages = !invalidLocations.Any(),
                Trips = new()
            };
            
            if (validLocations.Any())
            {
                var trips = ArrangeTrips(validLocations, drone.MaximunWeight);
                if(trips.Any()) delivery.Trips.AddRange(trips);
            }
            if (!delivery.DroneCanCarryAllPackages)
            {
                delivery.Observations = $"{drone} can't carry the package(s) to {string.Join(',', invalidLocations)}, because it exceeds the maximum weight it can carry ({drone.MaximunWeight}). ";
            }
            return delivery;
        }

        /// <summary>
        /// This function arranges the trips that the drone needs to take to deliver all the packages to the given locations.
        /// </summary>
        /// <param name="locations">The list of drones of the squad.</param>
        /// <param name="maximunWeight">The maximun weight the dron can carry.</param>
        /// <returns>The list of trips that the drone must make to deliver all the packages.</returns>
        private static List<Trip> ArrangeTrips(List<Location> locations, decimal maximunWeight)
        {
            List<Trip> trips = new();
            if (locations.Any(l => l.PackageWeight > maximunWeight)) return trips; 
            var locationsLeft = new List<Location>(locations);
            while(locationsLeft.Any())
            {
                Trip trip = new()
                {
                    Order = trips.Count + 1,
                    Locations = new()
                };
                decimal loadedWeight = 0;
                var nextLocation = locationsLeft.FirstOrDefault();
                while (nextLocation != null && loadedWeight + nextLocation.PackageWeight <= maximunWeight)
                {
                    trip.Locations.Add(nextLocation);
                    loadedWeight += nextLocation.PackageWeight;                    
                    locationsLeft.Remove(nextLocation);
                    nextLocation = locationsLeft.FirstOrDefault();
                }
                if(trip.Locations.Any())trips.Add(trip);
            }
            return trips;
        }
    }
}
