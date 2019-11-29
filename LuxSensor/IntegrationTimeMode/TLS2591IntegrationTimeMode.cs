using System;
using System.Collections.Generic;
using System.Text;

namespace LuxSensor.IntegrationTimeMode
{
    // <summary>
    /// ALS time sets the internal ADC integration time for both photodiode channels
    /// </summary>
    /// <remarks>
    /// Page 16 in the datasheet: https://ams.com/documents/20143/36005/TSL2591_DS000338_6-00.pdf/090eb50d-bb18-5b45-4938-9b3672f86b80
    /// </remarks>
    internal enum TLS2591IntegrationTimeMode : byte
    {

        /// <summary>
        /// 100 ms
        /// </summary>
        TSL25910_ITIME_100MS = 0x00,

        /// <summary>
        /// 200 ms
        /// </summary>
        TSL25910_ITIME_200MS = 0x01,

        /// <summary>
        /// 300 ms
        /// </summary>
        TSL25910_ITIME_300MS = 0x02,

        /// <summary>
        /// 400 ms
        /// </summary>
        TSL25910_ITIME_400MS = 0x03,

        /// <summary>
        /// 500 ms
        /// </summary>
        TSL25910_ITIME_500MS = 0x04,

        /// <summary>
        /// 600 ms
        /// </summary>
        TSL25910_ITIME_600MS = 0x05,

    }
}
