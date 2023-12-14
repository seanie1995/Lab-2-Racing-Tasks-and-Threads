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
            
            List<Car> carsList = new List<Car> { car1, car2, car3, car4, car5 };
            List<Car> carPlace = new List<Car> { };
            List<Task> raceTasks = carsList.Select(car => Task.Run(async () => await CarMethods.DriveAsync(car, carPlace))).ToList();
            await Console.Out.WriteLineAsync("Race started!");

            //while (carPlace.Count != 5)
            //{
            //    await Console.Out.WriteLineAsync("Press enter to see race status:");

            //    ConsoleKeyInfo keyInfo = Console.ReadKey();
            //    if (keyInfo.Key == ConsoleKey.Enter)
            //    {
            //        Console.Clear();
            //        CarMethods.RaceProgress(carsList, carPlace);
            //    }
            //}

            Stopwatch timer = new Stopwatch();
            timer.Start();

            while (true)
            {
                Console.Clear();
                Console.ResetColor();
                await Console.Out.WriteLineAsync($"{timer.Elapsed.TotalSeconds:F0} seconds passed");
                await CarMethods.RaceProgress(carsList, carPlace);             
                Thread.Sleep(5000);                             
            }

            
        }               
        
    }
}

