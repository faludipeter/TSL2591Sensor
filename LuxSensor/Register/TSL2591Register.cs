using System;
using System.Collections.Generic;
using System.Text;

namespace LuxSensor
{
    // <summary>
    /// Registers for the TSL2591
    /// </summary>
    /// <remarks>
    /// Page 13 in the datasheet: https://ams.com/documents/20143/36005/TSL2591_DS000338_6-00.pdf/090eb50d-bb18-5b45-4938-9b3672f86b80
    /// </remarks>
    internal enum TSL2591Register : byte
    {
        /// <summary>
        /// R/W Enables states and interrupts
        /// </summary>
        TSL25910_REG_ENABLE = 0x00,

        /// <summary>
        /// R/W ALS gain and integration time configuration
        /// </summary>
        TSL25910_REG_CONFIG = 0x01,

        /// <summary>
        /// R/W ALS interrupt low threshold low byte
        /// </summary>
        TSL25910_REG_AILTL = 0x04,

        /// <summary>
        ///  R/W ALS interrupt low threshold high byte
        /// </summary>
        TSL25910_REG_AILTH = 0x05,

        /// <summary>
        /// R/W ALS interrupt high threshold low byte
        /// </summary>
        TSL25910_REG_AIHTL = 0x06,

        /// <summary>
        /// R/W ALS interrupt high threshold high byte
        /// </summary>
        TSL25910_REG_AIHTH = 0x07,

        /// <summary>
        /// R/W No Persist ALS interrupt low threshold low byte
        /// </summary>
        TSL25910_REG_NPAILTL = 0x08,

        /// <summary>
        /// R/W No Persist ALS interrupt low threshold high byte
        /// </summary>
        TSL25910_REG_NPAILTH = 0x09,

        /// <summary>
        /// R/W No Persist ALS interrupt high threshold low byte 0x00
        /// </summary>
        TSL25910_REG_NPAIHTL = 0x0A,

        /// <summary>
        /// R/W No Persist ALS interrupt high threshold high byte
        /// </summary>
        TSL25910_REG_NPAIHTH = 0x0B,

        /// <summary>
        /// R/W Interrupt persistence filter
        /// </summary>
        TSL25910_REG_PERSIST = 0x0C,

        /// <summary>
        /// R Package ID
        /// </summary>
        TSL25910_REG_PID = 0x11,

        /// <summary>
        /// R Device ID
        /// </summary>
        TSL25910_REG_ID = 0x12,

        /// <summary>
        /// R Device status
        /// </summary>
        TSL25910_REG_STATUS = 0x13,

        /// <summary>
        /// R CH0 ADC low data byte
        /// </summary>
        TSL25910_REG_C0DATAL = 0x14,

        /// <summary>
        /// R CH0 ADC high data byte
        /// </summary>
        TSL25910_REG_C0DATAH = 0x15,

        /// <summary>
        /// R CH1 ADC low data byte 
        /// </summary>
        TSL25910_REG_C1DATAL = 0x16,

        /// <summary>
        /// R CH1 ADC high data byte
        /// </summary>
        TSL25910_REG_C1DATAH = 0x17
    }
}
