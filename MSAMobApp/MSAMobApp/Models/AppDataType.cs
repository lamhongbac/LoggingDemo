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
        SC, //Stock counting
        SR, //stock receive
        SI,//stock issue
        DN, //Debit notes
        CN, //Credit notes
        STI,//Stock Transfer In
        STO, //Stock Transfer Out
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
