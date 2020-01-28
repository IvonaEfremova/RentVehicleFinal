using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentVehicleFinal.Models
{
    public class DealsByMonth
    {
        [Key]
        public int Id { get; set; }

        public string Month { get; set; }

        public List<Deal> Deals { get; set; }

        public DealsByMonth()
        {
            Deals = new List<Deal>();
        }
    }
}