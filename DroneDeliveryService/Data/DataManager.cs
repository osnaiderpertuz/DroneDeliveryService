using DroneDeliveryService.Entities;
using System.Globalization;

namespace DroneDeliveryService.Data
{
    public class DataManager
    {
        /// <summary>
        /// This function load the data from the provided input data file.
        /// </summary>
        /// <param name="path">The input data file path</param>
        /// <param name="drones">Drone data output</param>
        /// <param name="locations">Location data ouput</param>
        /// <returns>True if the file exists, it is in the expected format and the data is successfully loaded ; otherwise, false</returns>
        public static bool LoadInputData(string path, out List<Drone>? drones, out List<Location>? locations)
        {
            bool success = false;
            drones = null;
            locations = null;
            try
            {
                if (string.IsNullOrWhiteSpace(path))
                {
                    throw new Exception("The path can't be null or empty.");
                }
                if (!File.Exists(path))
                {
                    throw new Exception("The file doesn't exist.");
                }
                string[] inputFileLines = File.ReadAllLines(path);
                string dronesLine = inputFileLines[0];
                string[] locationLines = inputFileLines.Skip(1).ToArray();
                var dronesAndMaxWeight = dronesLine.Split(',');
                drones = new();
                for (int i = 0; i < dronesAndMaxWeight.Length; i += 2)
                {
                    var drone = new Drone()
                    {
                        Name = dronesAndMaxWeight[i].Trim(),
                        MaximunWeight = ConverToDecimal(dronesAndMaxWeight[i + 1])
                    };
                    drones.Add(drone);
                }
                locations = locationLines.Select(l => new Location()
                                                {
                                                    Name = l.Split(',')[0].Trim(),
                                                    PackageWeight = ConverToDecimal(l.Split(',')[1])
                })
                                                .ToList();
                success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return success;
        }
        /// <summary>
        /// This method save de output data in the given path.
        /// </summary>
        /// <param name="path">The output data file path.</param>
        /// <param name="content">The content to be saved.</param>
        public static void SaveOuputData(string path, string content)
        {
            try
            {
                File.WriteAllText(path, content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// This function convert from string (with or without brackets) to decimal.
        /// </summary>
        /// <param name="stringDecimal"></param>
        /// <returns>The decimal number.</returns>
        private static decimal ConverToDecimal(string stringDecimal)
        {
            stringDecimal = stringDecimal.Replace("[","").Replace("]","").Trim();
            if (decimal.TryParse(stringDecimal, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal number))
            {
                return number;
            }
            else
            {
                throw new Exception("Number is not in the correct format.");//return 0;
            }
        }
    }
}
