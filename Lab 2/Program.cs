using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Timers;

// Try using countdown event.
namespace Lab_2
{
    internal class Program
    {       
        static async Task Main(string[] args)
        {            
            Car car1 = new Car("Carson");
            Car car2 = new Car("Trucksdottir");
            Car car3 = new Car("Bilberg");
            Car car4 = new Car("Convertablelund");
            Car car5 = new Car("Lastbilsberg");

            ProgramStart:
            await Console.Out.WriteLineAsync("Input [1] to manually get race updates with [Enter]: \nInput [2] to automatically get race updates every 5 seconds:");

            var input = Console.ReadLine();

            if (input != "1" && input != "2" || input == null)
            {
                goto ProgramStart;
            }

            List<Car> carsList = new List<Car> { car1, car2, car3, car4, car5 };
            List<Car> carPlace = new List<Car> { };
            List<Task> raceTasks = carsList.Select(car => Task.Run(async () => await CarMethods.DriveAsync(car, carPlace))).ToList();
            await Console.Out.WriteLineAsync("Race started!");

            while (input == "1")
            {
                await Console.Out.WriteLineAsync("Press enter to see race status:");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    await CarMethods.RaceProgress(carsList, carPlace);
                }              
            }

            Stopwatch timer = new Stopwatch();
            timer.Start();

            while (input == "2")
            {
                
                Console.Clear();
                Console.ResetColor();
                if (carPlace.Count != 5)
                {
                    await Console.Out.WriteLineAsync($"{timer.Elapsed.TotalSeconds:F0} seconds passed");
                }
                await CarMethods.RaceProgress(carsList, carPlace);
                Thread.Sleep(5000);
             
            }


        }               
        
    }
}

