using CoffeeShopCMS.Domain.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoffeeShopCMS.Domain.Entities
{
    public class Customer : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // [StringLength(11)]
        public long Tc { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        // [StringLength(4)]
        public int BirthYear { get; set; }

        public override void Validate()
        {
            var validationHandler = GetValidationNotificationHandler();
            ValidationHelper validationHelper = new ValidationHelper(ref notificationHandler);

            validationHelper.NullCheckStringValue("İsim", FirstName);
            validationHelper.NullCheckStringValue("Soyisim", LastName);
            validationHelper.NullAndCountCheckLongValue("Tc Kimlik No", Tc, 11);
            validationHelper.NullAndCountCheckIntValue("Doğum Yılı", BirthYear, 4);
        }
    }
}