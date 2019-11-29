using System;
using System.Collections.Generic;
using System.Text;
using System.Device.I2c;
using System.Threading.Tasks;

namespace LuxSensor
{
    /// <summary>
    /// Lux Sensor TLS2591
    /// </summary>
    public class TLS2591Sensor : IDisposable
    {
        /// <summary>
        /// TLS2591 Default I2C Address
        /// </summary>
        public const byte DefaultI2cAddress = 0x29;

        /// <summary>
        ///TLS2591 Secondary I2C bus address.
        /// </summary>
        public const byte SecondaryI2cAddress = 0x28;

        private I2cConnectionSettings I2CConnection;
        private I2cDevice sensor = null;

        public TLS2591Sensor(I2cConnectionSettings con)
        {
            I2CConnection = con;
        }



        public async Task Initialize()
        {

            sensor = I2cDevice.Create(I2CConnection);

            //Get ID
            sensor.WriteByte(0xb2);
            byte read = sensor.ReadByte();
            Console.WriteLine("ID is: 0x" + read.ToString("X"));

            

        }

        /// <summary>
        /// Cleanup
        /// </summary>
        public void Dispose()
        {
            sensor?.Dispose();
            sensor = null;
        }

         
    }
}
