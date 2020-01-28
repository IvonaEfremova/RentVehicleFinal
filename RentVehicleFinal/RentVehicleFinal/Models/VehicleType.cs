using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentVehicleFinal.Models
{
    public class VehicleType
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Тип")]
        public string type { get; set; }

        public List<Vehicle> Vehicle { get; set; }

        public VehicleType()
        {
            this.Vehicle = new List<Vehicle>();
        }
    }
}