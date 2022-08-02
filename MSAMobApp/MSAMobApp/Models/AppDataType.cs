using System;
using System.Collections.Generic;
using System.Text;

namespace MSAMobApp.Models
{
   public class AppDataType
    {
    }
    public enum ETCode
    {
        IR, //inventory receive
        IS,//inventory issue
        IW, //Waste
        TI,//Transfer In
        TO, //Transfer Out



    }
    public enum EDataState
    {
        New,
        Posted,
        Edited
    }
}
