using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentVehicleFinal.Models
{
    public class AddToRoleModel
    {
        [Display(Name ="Емаил")]
        public string Email { get; set; }

        public List<string> users{ get; set; }

        [Display(Name ="Пермисија:")]
        public string Role { get; set; }

        public List<String> roles{ get; set; }

        public int selectedEmail { get; set; }

        public int selectedRole { get; set; }

        public AddToRoleModel()
        {
            roles = new List<string>();
            users = new List<string>();
        }
    }
}