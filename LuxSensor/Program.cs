using System;
using System.Device.I2c;
using System.Diagnostics;
using System.Threading;

namespace LuxSensor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lux sensor");
#if DEBUG
            Console.WriteLine("Wating for debugger to attach");
            while (!Debugger.IsAttached)
            {
                Thread.Sleep(1000);
            }
            Console.WriteLine("Debugger attached");
#endif
            I2cConnectionSettings cs = new I2cConnectionSettings(1, TLS2591Sensor.DefaultI2cAddress);

            using (TLS2591Sensor sensor = new TLS2591Sensor(cs))
            {
                sensor.Initialize();
            }
               

            Console.ReadKey();

        }
    }
}
