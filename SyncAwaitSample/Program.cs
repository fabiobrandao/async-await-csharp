using System;
using System.Threading;
using System.Threading.Tasks;

namespace SyncAwaitSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pitstop starting...");
            
            Task<string> taskPitstop = PitStop();
            taskPitstop.Wait();
            string time = taskPitstop.Result;

            Console.WriteLine(string.Format("Pitstop complete! Stop Time {0}s", time));
            Console.ReadKey();
        }

        private async static Task<string> PitStop()
        {
            DateTime startTime = DateTime.Now;

            await Task.Run(() =>
            {
                Task taskTireFrontLeft = TireChange("Tire Front Left");
                Task taskTireFrontRight = TireChange("Tire Front Right");
                Task taskRefuel = Refuel(3);

                Task.WaitAll(taskTireFrontLeft, taskTireFrontRight, taskRefuel);
            });

            DateTime endTime = DateTime.Now;

            TimeSpan span = endTime.Subtract(startTime);

            return span.Seconds + "," + span.Milliseconds;
        }

        private async static Task TireChange(string tire)
        {
            await Task.Run(() =>
            {
                for (uint i = 0; i < 5; i++)
                {
                    Console.WriteLine(string.Format("\tChanging : {0}", tire));
                    Thread.Sleep(500);
                }
            });
        }

        private async static Task Refuel(ushort liters)
        {
            await Task.Run(() =>
            {
                for (uint i = 0; i < 5; i++)
                {
                    Console.WriteLine(string.Format("\tRefuel : {0} L", i + 1));
                    Thread.Sleep(1000);
                }
            });
        }
    }
}