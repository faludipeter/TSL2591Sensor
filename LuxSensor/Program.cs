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
            I2cConnectionSettings cs = new I2cConnectionSettings(1, TSL2591Sensor.DefaultI2cAddress);
            I2cDevice LuxSensor = I2cDevice.Create(cs);

            using (TSL2591Sensor sensor = new TSL2591Sensor(LuxSensor))
            {

                while (true)
                {
                    Console.WriteLine("Lux is: " + sensor.GetLux());
                    Thread.Sleep(1000);
                }
                
            }
        }
    }
}
