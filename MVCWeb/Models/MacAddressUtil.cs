using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace MVCWeb.Models
{
    public class MacAddressUtil
    {
        public string GetMACAddress()
        {

            List<string> Macs = new List<string>();
            List<string> Maxs = new List<string>();

            NetworkInterface[] allNet = NetworkInterface.GetAllNetworkInterfaces();
            string MAC_address = "";
            foreach (NetworkInterface item in allNet)
            {
               
                if (item.OperationalStatus == OperationalStatus.Up)
                {

                    MAC_address += item.GetPhysicalAddress().ToString();
                    Maxs.Add(item.Id);
                    Macs.Add(MAC_address);
                }
            }
            return MAC_address;
        }
    }
}
