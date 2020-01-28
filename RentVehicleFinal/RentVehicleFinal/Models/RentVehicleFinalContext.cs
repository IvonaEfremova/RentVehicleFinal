using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RentVehicleFinal.Models
{
    public class RentVehicleFinalContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public RentVehicleFinalContext() : base("name=DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<RentVehicleFinal.Models.Vehicle> Vehicles { get; set; }

        public System.Data.Entity.DbSet<RentVehicleFinal.Models.VehicleType> VehicleTypes { get; set; }

        public System.Data.Entity.DbSet<RentVehicleFinal.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<RentVehicleFinal.Models.Role> Roles { get; set; }

        public System.Data.Entity.DbSet<RentVehicleFinal.Models.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<RentVehicleFinal.Models.LicenceType> LicenceTypes { get; set; }

        public System.Data.Entity.DbSet<RentVehicleFinal.Models.Deal> Deals { get; set; }
    }
}
