using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoffeeShopCMS.Service.Dto
{
    public class CustomerDto : DataTransferObject
    {
        public int Id { get; set; }

        public long Tc { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public int BirthYear { get; set; }
    }
}