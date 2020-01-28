using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentVehicleFinal.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Полето е задолжително")]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Полето е задолжително")]
        [Display(Name = "Презиме")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Полето е задолжително")]
        [Display(Name = "Емаил")]
        [RegularExpression(@"\S+@\S+\.\S+", ErrorMessage = "Невалиден формат на емаил")]
        public string Email { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public List<Deal> Deals { get; set; }

        
        public List<String> emails { get; set; }


        public User()
        {
            this.emails = new List<string>();
            this.Deals = new List<Deal>();
        }
    }
} 