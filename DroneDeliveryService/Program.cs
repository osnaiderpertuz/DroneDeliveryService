using DroneDeliveryService.BL;

string path = Path.Combine(Environment.CurrentDirectory, "Input.txt") ;
string answer = "y";
if(File.Exists(path))
{
    DeliveryService.Start(path);
    Console.WriteLine("\nDone! Do you want to process other data?(y/n)");
    answer = Console.ReadLine();
}
while (answer.Equals("y", StringComparison.OrdinalIgnoreCase) || answer.Equals("yes", StringComparison.OrdinalIgnoreCase))
{
    Console.WriteLine("Please, enter input data file path:");
    path = Console.ReadLine();
    DeliveryService.Start(path);
    Console.WriteLine("\nDone! Do you want to process other data?(y/n)");
    answer = Console.ReadLine();
}
Console.WriteLine("Bye!");

