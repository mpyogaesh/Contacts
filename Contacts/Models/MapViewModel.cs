using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contacts.Models
{
    public class MapViewModel:Contact
    {
        public string PinCode { get; set; }
        public string URL { get; set; }

        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}