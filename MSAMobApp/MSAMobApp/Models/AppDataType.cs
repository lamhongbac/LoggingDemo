using System;
using System.Collections.Generic;
using System.Text;

namespace MSAMobApp.Models
{
   public class AppDataType
    {
    }
    /// <summary>
    /// Ware house management Tcode
    /// </summary>
    public enum EWHMTCode
    {
        IC, //Inventory counting
        IR, //inventory receive
        IS,//inventory issue
        DN, //Debit notes
        CN, //Credit notes
        TI,//Transfer In
        TO, //Transfer Out
        RW, //Raw waste
        PW, //Product waste
        SA, //Stock Adjust
        SO, //Stock Opening


    }
    public enum EDataState
    {
        New,
        Posted,
        Edited
    }
}
