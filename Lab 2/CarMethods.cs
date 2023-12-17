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
                Console.Clear();                
                await Console.Out.WriteLineAsync("RACE RESULTS!");
            
                Console.ForegroundColor= ConsoleColor.Yellow;
                await Console.Out.WriteLineAsync($"--------------------------------------------");
            
                for (int i = 0; i < carPlace.Count; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    
                    await Console.Out.WriteLineAsync($"{i + 1}: {carPlace[i].name}");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    await Console.Out.WriteLineAsync($"--------------------------------------------");
                }
                Console.ResetColor();          
            
                await Console.Out.WriteLineAsync("The program will exit in 10 seconds");
                await Task.Delay(10000);
                Environment.Exit(0);
            }
        }    
        public static async Task CarProblems(Car car)
        {
            Random rnd = new Random();
            int random = rnd.Next(1, 51);
            Console.ForegroundColor = ConsoleColor.Red;
            if (random == 1)
            {
                await Console.Out.WriteLineAsync($"{car.name} was hit by the spikey blue shell. 30 second delay!");
                await Task.Delay(30000);
            }
            else if (random == 2 || random == 3)
            {
                await Console.Out.WriteLineAsync($"{car.name} got a flat tire! 20 second delay!");
                await Task.Delay(20000);
            }
            else if (random >= 4 && random <= 8)
            {
                await Console.Out.WriteLineAsync($"{car.name} has committed vehicular homicide on a bird! 10 second delay!");
                await Task.Delay(10000);
            }
            else if (random >= 9 && random <= 18)
            {
                await Console.Out.WriteLineAsync($"{car.name} had to avoid a moose!! 5 second delay!");
                await Task.Delay(5000);
            }
            else if (random >= 19 && random <= 50)
            {
                await Console.Out.WriteLineAsync($"{car.name} got engine problems! Car is 10km/h slower!");
                car.speed -= 10;               
            }
        }      
        public static async Task RaceProgress(List<Car> cars, List<Car> carPlace)
        {
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            await Console.Out.WriteLineAsync($"--------------------------------------------");
            foreach (Car car in cars)
            {
                if (car.distanceTravelled != 100)
                {
                    await Console.Out.WriteLineAsync($"{car.name}: {car.distanceTravelled}km || Speed: {car.speed} km/h.");
                }
                else if (car.distanceTravelled == 100)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    await Console.Out.WriteLineAsync($"{car.name} finished!");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                await Console.Out.WriteLineAsync($"--------------------------------------------");
            }
            Console.ResetColor();
        }

        

        
    }
}
