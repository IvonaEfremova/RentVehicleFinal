using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentVehicleFinal.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        public string role { get; set; }

        public List<User> Users { get; set; }

        public Role()
        {
            this.Users = new List<User>();
        }
    }
}