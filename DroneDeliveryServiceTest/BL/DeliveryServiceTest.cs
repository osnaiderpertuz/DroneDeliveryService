using DroneDeliveryService.BL;
using DroneDeliveryService.Entities;
using System.Reflection;

namespace DroneDeliveryServiceTest.BL
{
    public class DeliveryServiceTest
    {
        private List<Location> locations;
        private List<Drone> drones;
        private List<Drone> dronesAllCan;
        private MethodInfo ArrangeTrips;
        private MethodInfo ArrangeDeliveryByDrone;
        private MethodInfo ArrangeDeliveries;

        [SetUp]
        public void Setup()
        {
            #region Private methods to test
            Type type = typeof(DeliveryService);
            ArrangeTrips = type.GetMethod("ArrangeTrips", BindingFlags.NonPublic | BindingFlags.Static);
            ArrangeDeliveryByDrone = type.GetMethod("ArrangeDeliveryByDrone", BindingFlags.NonPublic | BindingFlags.Static);
            ArrangeDeliveries = type.GetMethod("ArrangeDeliveries", BindingFlags.NonPublic | BindingFlags.Static);
            #endregion

            #region Data
            locations = new List<Location>()
            {
                new Location()
                {
                    Name = "Loaction A",
                    PackageWeight = 10
                },
                new Location()
                {
                    Name = "Location B",
                    PackageWeight = 8
                },
                new Location()
                {
                    Name = "Location C",
                    PackageWeight = 15
                },
                new Location()
                {
                    Name = "Loaction D",
                    PackageWeight = 20
                },
                new Location()
                {
                    Name = "Location E",
                    PackageWeight = 16
                },
                new Location()
                {
                    Name = "Location F",
                    PackageWeight = 30
                }
            };
            drones = new List<Drone>()
            {
                new Drone()
                {
                    Name = "Drone One",
                    MaximunWeight = 30
                },
                new Drone()
                {
                    Name = "Drone Two",
                    MaximunWeight = 25
                },
                new Drone()
                {
                    Name = "Drone Three",
                    MaximunWeight = 20
                },
                new Drone()
                {
                    Name = "Drone Four",
                    MaximunWeight = 15
                },
                new Drone()
                {
                    Name = "Drone Five",
                    MaximunWeight = 10
                }
            };
            dronesAllCan = new List<Drone>()
            {
                new Drone()
                {
                    Name = "Drone One",
                    MaximunWeight = 30
                },
                new Drone()
                {
                    Name = "Drone Two",
                    MaximunWeight = 35
                },
                new Drone()
                {
                    Name = "Drone Three",
                    MaximunWeight = 40
                },
                new Drone()
                {
                    Name = "Drone Four",
                    MaximunWeight = 35
                },
                new Drone()
                {
                    Name = "Drone Five",
                    MaximunWeight = 32
                }
            };
            #endregion
        }

        [Test]
        public void ArrangeTripsPakckageExceedsMaximunWeightTest()
        {
            decimal maximunWeight = 10;
            List<Trip> trips = (List<Trip>)ArrangeTrips.Invoke(null, new object[] { locations, maximunWeight }); 
            Assert.That(trips.Any(), Is.False);
        }
        [Test]
        public void ArrangeTripsPakckageMeetsWeightRequirementTest()
        {
            decimal maximunWeight = 30;
            List<Trip> trips = (List<Trip>)ArrangeTrips.Invoke(null, new object[] { locations, maximunWeight });
            Assert.That(trips.Any(), Is.True);
        }
        [Test]
        public void ArrangeDeliveryByDroneCanCarryAllPakcagesTest()
        {
            Drone drone = drones.First();
            Delivery delivery = (Delivery)ArrangeDeliveryByDrone.Invoke(null, new object[] { drone, locations });
            Assert.That(delivery.DroneCanCarryAllPackages, Is.True);
        }
        [Test]
        public void ArrangeDeliveryByDroneCanNOTCarryAllPakcagesTest()
        {
            Drone drone = drones.Last();
            Delivery delivery = (Delivery)ArrangeDeliveryByDrone.Invoke(null, new object[] { drone, locations });
            Assert.That(delivery.DroneCanCarryAllPackages, Is.False);
        }
        [Test]
        public void ArrangeDeliveriesAllDroneCanCarryAllPakcagesTest()
        {
            List<Delivery> deliveries = (List<Delivery>)ArrangeDeliveries.Invoke(null, new object[] { dronesAllCan, locations });
            Assert.That(deliveries.All(d => d.DroneCanCarryAllPackages), Is.True);
        }
        [Test]
        public void ArrangeDeliveriesAnyDroneCanNOTCarryAllPakcagesTest()
        {
            List<Delivery> deliveries = (List<Delivery>)ArrangeDeliveries.Invoke(null, new object[] { drones, locations });
            Assert.That(deliveries.Any(d => !d.DroneCanCarryAllPackages), Is.True);
        }
    }
}
