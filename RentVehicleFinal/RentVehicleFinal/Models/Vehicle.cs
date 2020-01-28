using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentVehicleFinal.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Слика")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Полето е задолжително")]
        [Display(Name = "Модел")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Полето е задолжително")]
        [Display(Name = "Регистрација")]
        [RegularExpression(@"[A-Z]{2}[0-9]{4}[A-Z]{2}", ErrorMessage = "Невалиден формат на регистрација")] // proveri AB1234AB
        public string RegistrationNum { get; set; }

        [Required(ErrorMessage = "Полето е задолжително")]
        [Range(1,10000, ErrorMessage ="Цената мора да е поголема од 0")]
        [Display(Name = "Цена за ден")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Полето е задолжително")]
        [Range(16, 99, ErrorMessage = "Клиентот мора да има минимум 16 години")]
        [Display(Name = "Минимална старосна граница на возачот")]
        public int ReqestedCustomerAge { get; set; }

        [Required(ErrorMessage = "Полето е задолжително")]
        [Display(Name = "Потребна категорија на возачка дозвола")]
        public string RequestedCustomerLicence { get; set; }

        [Required(ErrorMessage = "Полето е задолжително")]
        [Display(Name = "Опис")]
        public string Description { get; set; }

        [Display(Name = "Тип на возило")]
        public VehicleType VehicleType { get; set; }
        [Display(Name = "Тип на возило")]
        public int VehicleTypeId { get; set; }

        public Boolean Available { get; set; }

        public List<Deal> Deals { get; set; }

        public Vehicle()
        {
            this.Available = true;
            this.Deals = new List<Deal>();
        }
    }
}