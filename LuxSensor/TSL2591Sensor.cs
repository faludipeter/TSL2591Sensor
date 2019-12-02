using System;
using System.Collections.Generic;
using System.Text;
using System.Device.I2c;
using System.Threading.Tasks;
using LuxSensor.GainMode;
using LuxSensor.IntegrationTimeMode;
using System.Threading;

namespace LuxSensor
{
    /// <summary>
    /// Represents TSL2591 lux Sensor.
    /// </summary>
    public class TSL2591Sensor : IDisposable
    {
        /// <summary>
        /// TLS2591 Sensor Default I2C address.
        /// </summary>
        public const byte DefaultI2cAddress = 0x29;

        /// <summary>
        /// TLS2591 Sensor Secondary I2C bus address.
        /// </summary>
        public const byte SecondaryI2cAddress = 0x28;

        /// <summary>
        /// The comman register specifies the address of the target register for future read and write operations, as well as issues special function commands.
        /// </summary>
        public const byte TSL2591_COMMAND_BIT = 0xA0;

        /// <summary>
        /// 
        /// </summary>
        private const float TSL2591_LUX_DF = 408.0F;

        private I2cDevice sensor;

        /// <summary>
        /// Integration time data.
        /// </summary>
        private TSL2591IntegrationTimeMode _TSL2591IntegrationTimeMode;
        public TSL2591IntegrationTimeMode TSL2591IntegrationTimeMode
        {
            get
            {
                return _TSL2591IntegrationTimeMode;
            }
            set
            {
                byte oldreg = ReadRegister(TSL2591Register.TSL25910_REG_CONFIG);
                byte newreg = (byte)((oldreg & 0b11111000) | (byte)value);
                WriteRegister(TSL2591Register.TSL25910_REG_CONFIG, (byte)newreg);
                _TSL2591IntegrationTimeMode = value;
            }
        }

        /// <summary>
        /// Gain value data.
        /// </summary>
        private TSL2591GainMode _TSL2591GainMode;
        public TSL2591GainMode TSL2591GainMode
        {
            get
            {
                return _TSL2591GainMode;
            }
            set
            {
                byte oldreg = ReadRegister(TSL2591Register.TSL25910_REG_CONFIG);
                byte newreg = (byte)((oldreg & 0b00000011) | (byte)value);
                WriteRegister(TSL2591Register.TSL25910_REG_CONFIG, (byte)newreg);

                _TSL2591GainMode = value;
            }
        }

        /// <summary>
        /// Device power mode.
        /// </summary>
        private bool _Enable;
        public bool Enable
        {
            get { return _Enable; }
            set
            {
                if (value == true)
                {
                    WriteRegister(TSL2591Register.TSL25910_REG_ENABLE, 0b00000011);
                }
                else
                {
                    WriteRegister(TSL2591Register.TSL25910_REG_ENABLE, 0b00000000);
                }
                _Enable = value;
            }
        }

        /// <summary>
        /// Initialize a new instance of the TSL2591Sensor class.
        /// </summary>
        /// <param name="device">I2cDevice</param>
        /// <param name="gainMode">Selected gain mode</param>
        /// <param name="integrationTimeMode">Selected integration time mode</param>
        /// <exception>Thrown when the sensor was not found</exception>
        public TSL2591Sensor(I2cDevice device, TSL2591GainMode gainMode = TSL2591GainMode.TSL25910_GAIN_LOW, TSL2591IntegrationTimeMode integrationTimeMode = TSL2591IntegrationTimeMode.TSL25910_ITIME_100MS)
        {
            sensor = device;
            //Check device ID
            if (ReadRegister(TSL2591Register.TSL25910_REG_ID) != 0x50)
            {
                throw new Exception("TSL2591 sensor was not found");
            }
            //Set integration time
            TSL2591IntegrationTimeMode = integrationTimeMode;
            //Set gain
            TSL2591GainMode = gainMode;
            //Power on device
            Enable = true;
        }

        /// <summary>
        /// Read an 8-bit value from the specified register.
        /// </summary>
        /// <param name="register">Register name</param>
        /// <returns>Register value</returns>
        private byte ReadRegister(TSL2591Register register)
        {
            int command = (byte)register | TSL2591_COMMAND_BIT;
            sensor.WriteByte((byte)command);
            return sensor.ReadByte();
        }

        /// <summary>
        /// Write a register 8-bit value from the specified register.
        /// </summary>
        /// <param name="register">Specified register</param>
        /// <param name="value">New value of register</param>
        private void WriteRegister(TSL2591Register register, byte value)
        {
            //Make a write command
            var writecommand = new byte[] { (byte)((byte)register | TSL2591_COMMAND_BIT), value };

            sensor.Write(writecommand);
        }

        /// <summary>
        /// Read channel 1 and channel 2 raw data 
        /// </summary>
        /// <returns>Byte array whit channel 1 and channel 2 data</returns>
        private byte[] RawData()
        {
            //Channel 0 ADC 16 bit data 
            byte C0DataL = ReadRegister(TSL2591Register.TSL25910_REG_C0DATAL);
            byte C0DataH = ReadRegister(TSL2591Register.TSL25910_REG_C0DATAH);

            //Channel 1 ADC 16 bit data 
            byte C1DataL = ReadRegister(TSL2591Register.TSL25910_REG_C1DATAL);
            byte C1DataH = ReadRegister(TSL2591Register.TSL25910_REG_C1DATAH);

            //Each channel independently operates the upper byte shadow register.So to minimize the potential for skew between CH0 and CH1 data, it is recommended to read all four ADC bytes in sequence.
                       return new byte[] { C0DataL, C0DataH, C1DataL, C1DataH };
        }

        /// <summary>
        /// Calculate Lux value from raw data
        /// </summary>
        /// <returns>Lux value</returns>
        public float GetLux()
        {
            byte[] rawdata = RawData();

            ushort ch0 = (ushort)(rawdata[1] << 8 | rawdata[0]);
            ushort ch1 = (ushort)(rawdata[3] << 8 | rawdata[1]);


            float atime, again;
            float cpl, lux1, lux2, lux;

            uint chan0, chan1;

            // Check for overflow conditions first
            if ((ch0 == 0xFFFF) | (ch1 == 0xFFFF))
            {
                // Signal an overflow
                return -1;
            }

            switch (TSL2591IntegrationTimeMode)
            {
                case TSL2591IntegrationTimeMode.TSL25910_ITIME_100MS:
                    atime = 100.0F;
                    break;
                case TSL2591IntegrationTimeMode.TSL25910_ITIME_200MS:
                    atime = 200.0F;
                    break;
                case TSL2591IntegrationTimeMode.TSL25910_ITIME_300MS:
                    atime = 300.0F;
                    break;
                case TSL2591IntegrationTimeMode.TSL25910_ITIME_400MS:
                    atime = 400.0F;
                    break;
                case TSL2591IntegrationTimeMode.TSL25910_ITIME_500MS:
                    atime = 500.0F;
                    break;
                case TSL2591IntegrationTimeMode.TSL25910_ITIME_600MS:
                    atime = 600.0F;
                    break;
                default: 
                    atime = 100.0F;
                    break;
            }

            switch (TSL2591GainMode)
            {
                case TSL2591GainMode.TSL25910_GAIN_LOW:
                    again = 1.0F;
                    break;
                case TSL2591GainMode.TSL25910_GAIN_MED:
                    again = 25.0F;
                    break;
                case TSL2591GainMode.TSL25910_GAIN_HIGH:
                    again = 428.0F;
                    break;
                case TSL2591GainMode.TSL25910_GAIN_MAX:
                    again = 9876.0F;
                    break;
                default:
                    again = 1.0F;
                    break;
            }

            // cpl = (ATIME * AGAIN) / DF
            cpl = (atime * again) / TSL2591_LUX_DF;

            lux = (((float)ch0 - (float)ch1)) * (1.0F - ((float)ch1 / (float)ch0)) / cpl;

            return lux;

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
