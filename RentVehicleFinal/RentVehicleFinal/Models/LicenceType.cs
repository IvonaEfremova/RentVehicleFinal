using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentVehicleFinal.Models
{
    public class LicenceType
    {
        [Key]
        public int Id { get; set; }

        public string licenceType { get; set; }

        public List<Customer> Customers { get; set; }

        public LicenceType()
        {
            this.Customers = new List<Customer>();
        }
    }
}