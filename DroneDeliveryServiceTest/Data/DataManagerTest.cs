using DroneDeliveryService.Data;
using DroneDeliveryService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneDeliveryServiceTest.Data
{
    public class DataManagerTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void LoadInputDataWithBracketsTest()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "InputWithBrackets.txt");
            var success = DataManager.LoadInputData(path, out List<Drone>? drones, out List<Location>? locations);
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.True);
                Assert.That(drones, Is.Not.Null);
                Assert.That(drones?.Any(), Is.True);
                Assert.That(locations, Is.Not.Null);
                Assert.That(drones?.Any(), Is.True);
            });
        }
        [Test]
        public void LoadInputDataWithoutBracketsTest()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "InputWithoutBrackets.txt");
            var success = DataManager.LoadInputData(path, out List<Drone>? drones, out List<Location>? locations);
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.True);
                Assert.That(drones, Is.Not.Null);
                Assert.That(drones?.Any(), Is.True);
                Assert.That(locations, Is.Not.Null);
                Assert.That(drones?.Any(), Is.True);
            });
        }

        [Test]
        public void LoadInputDataFileDoesNotExistTest()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "inputDoesNotExist.txt");
            var success = DataManager.LoadInputData(path, out List<Drone>? drones, out List<Location>? locations);
            Assert.Multiple(() =>
            {
                Assert.That(success, Is.False);
                Assert.That(drones, Is.Null);
                Assert.That(locations, Is.Null);
            });
        }

        [Test]
        public void LoadInputDataIncorrectFormatTest()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "InputIncorrectFormat.txt");
            var success = DataManager.LoadInputData(path, out List<Drone>? drones, out List<Location>? locations);
            Assert.That(success, Is.False);
        }
    }
}
