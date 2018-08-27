using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("Account")]
    public class Account : IEntity
    {
        [Key]
        [Column("AccountId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Date created is required")]
        public DateTime DateCreated { get; set; }

        [Required(ErrorMessage = "Account type is required")]
        public string AccountType { get; set; }

        [Required(ErrorMessage = "Owner Id is required")]
        public Guid OwnerId { get; set; }
    }

    [Table("Category")]
    public class Category : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }

    public class Product : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public Guid CategoryId { get; set; }
    }

    public class Company : IEntity
    {
        [Key]
        public Guid Id { get; set; }


    }
}

