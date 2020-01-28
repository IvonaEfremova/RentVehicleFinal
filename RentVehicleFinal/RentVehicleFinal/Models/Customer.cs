using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentVehicleFinal.Models
{
    public class Customer
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
        public String Email { get; set; }

        [Required(ErrorMessage = "Полето е задолжително")]
        [Display(Name = "Возраст")]
        [Range(16, 99, ErrorMessage = "Годините не се во дозволената граница")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Полето е задолжително")]
        [Display(Name = "Телефонски број")]
        [RegularExpression(@"071[0-9]{6}|070[0-9]{6}|075[0-9]{6}|078[0-9]{6}", ErrorMessage ="Невалиден телефонски број")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Полето е задолжително")]
        [Display(Name = "Адреса")]
        public string Address { get; set; }


        public int LicenceTypeId { get; set; }
        public LicenceType LicenceType { get; set; }

        public List<Deal> Deals { get; set; }

        public Customer()
        {
            this.Deals = new List<Deal>();
        }
    }
}