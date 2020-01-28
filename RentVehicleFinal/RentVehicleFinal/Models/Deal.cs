using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentVehicleFinal.Models
{
    public class Deal
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Возило")]
        public int VehicleId { get; set; }
        //public Vehicle Vehicle { get; set; }

        [Display(Name = "Клиент")]
        public int CustomerId { get; set; }
      //  public Customer Customer { get; set; }

        [Display(Name = "Вработен")]
        public int UserId { get; set; }
       // public User User { get; set; }

        [Display(Name = "Датум од")]
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        [Display(Name = "Датум до")]
        public DateTime DateTo { get; set; }

        [Display(Name = "Цена во денари за ден")]
        public int TotalPrice { get; set; }

        public Boolean plateno { get; set; }

        public Deal()
        {
            this.plateno = false;
        }

    }
}