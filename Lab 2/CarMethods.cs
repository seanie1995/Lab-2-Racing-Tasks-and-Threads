using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;


namespace Lab_2
{
    internal class CarMethods
    {
                       
        private static readonly object lockObject = new object();
        private static readonly object colorLock = new object();
        static Stopwatch stopwatch = new Stopwatch();
        
        public static async Task DriveAsync(Car car, List<Car> carPlace)
        {                   
            stopwatch.Start();
                                  
            for (int i = 0; i <= 100; i++)
            {
                
                if (stopwatch.Elapsed.TotalSeconds >= 10)
                {
                    await CarProblems(car);
                    stopwatch.Restart(); 
                }
                car.distanceTravelled = i;                             
                await Task.Delay(car.speed * 5);
                              
            }         
            lock (lockObject)
            
            {
                carPlace.Add(car);
            }

            int place = carPlace.IndexOf(car);

            Console.ResetColor();
            if (place == 0)
            {               
                await Console.Out.WriteLineAsync($"{car.name} has finished the race at 1st place!");              
            }
            else
            {
                await Console.Out.WriteLineAsync($"{car.name} finished the race at place: {place + 1}");
            }                     

            if (place == 4)
            {
                Console.ForegroundColor= ConsoleColor.Green;
                await Task.Delay(1000);
                await Console.Out.WriteLineAsync($"Race completed. Results will show shortly:");
                Console.ResetColor();            
            }
        }    
        public static async Task CarProblems(Car car)
        {
            Random rnd = new Random();
            Console.ForegroundColor = ConsoleColor.Red;
            if (rnd.Next(1, 10) == 1)
            {
                await Console.Out.WriteLineAsync($"{car.name} has run out of gas. 30 second delay!");
                await Task.Delay(30000);
            }
            else if (rnd.Next(2, 10) == 2)
            {
                await Console.Out.WriteLineAsync($"{car.name} got a flat tire! 20 second delay!");
                await Task.Delay(20000);
            }
            else if (rnd.Next(5, 10) == 5)
            {
                await Console.Out.WriteLineAsync($"{car.name} has committed vehicular homicide on a bird! 10 second delay!");
                await Task.Delay(10000);
            }
            else if (rnd.Next(8, 10) == 8)
            {
                await Console.Out.WriteLineAsync($"{car.name} got engine problems! Car is travelling 10 km/h slower from now on!");
                car.speed -= 10;               
            }
        }      
        public static async Task RaceProgress(List<Car> cars, List<Car> carPlace)
        {
            if (carPlace.Count == 5)
            {
                await PrintRaceResults(carPlace);               
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                await Console.Out.WriteLineAsync($"--------------------------------------------");
                foreach (Car car in cars)
                {
                    if (car.distanceTravelled != 100)
                    {
                        await Console.Out.WriteLineAsync($"{car.name}: {car.distanceTravelled}KM || Speed: {car.speed} KM/H.");
                    }
                    else if (car.distanceTravelled == 100)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        await Console.Out.WriteLineAsync($"{car.name} finished!");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    await Console.Out.WriteLineAsync($"--------------------------------------------");
                }
            }
           
            Console.ResetColor();
        }

        public static async Task PrintRaceResults(List<Car> carPlace)
        {
            Console.Clear();
            await Console.Out.WriteLineAsync("RACE RESULTS!");
            lock (lockObject)
            {
                Console.ForegroundColor= ConsoleColor.Yellow;
                Console.Out.WriteLineAsync($"--------------------------------------------");
                for (int i = 0; i < carPlace.Count; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    
                    Console.Out.WriteLineAsync($"{i + 1}: {carPlace[i].name}");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Out.WriteLineAsync($"--------------------------------------------");
                }
                Console.ResetColor();
            }

            await Console.Out.WriteLineAsync("Press enter to exit program:");
            Console.ReadKey();
            Environment.Exit(1);
        }

        
    }
}
