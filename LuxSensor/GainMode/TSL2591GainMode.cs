﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LuxSensor.GainMode
{
    // <summary>
    /// Gain mode for the TSL2591
    /// </summary>
    /// <remarks>
    /// Page 16 in the datasheet: https://ams.com/documents/20143/36005/TSL2591_DS000338_6-00.pdf/090eb50d-bb18-5b45-4938-9b3672f86b80
    /// </remarks>
    public enum TSL2591GainMode : byte
    {
        /// <summary>
        ///  Low gain mode (1x)
        /// </summary>
        TSL25910_GAIN_LOW = 0b00000000,

        /// <summary>
        /// Medium gain mode (25x)
        /// </summary>
        TSL25910_GAIN_MED = 0b00010000,

        /// <summary>
        /// High gain mode (400x)
        /// </summary>
        TSL25910_GAIN_HIGH = 0b00100000,

        /// <summary>
        /// Maximum gain mode (9550x)
        /// </summary>
        TSL25910_GAIN_MAX = 0b00110000,

    }
}
