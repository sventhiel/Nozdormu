using Nozdormu.Server.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Nozdormu.Server.Models
{
    public class CreateAccountModel
    {
        [Required]
        [Display(Name = "Host")]
        public string Host { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required, Compare("Password")]
        [Display(Name = "ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Prefix")]
        public string Prefix { get; set; }
    }

    public class ReadAccountModel
    {
        [Required]
        [Display(Name = "Id")]
        public long Id { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Prefix")]
        public string Prefix { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Host")]
        public string Host { get; set; }

        public static ReadAccountModel Convert(Account account)
        {
            return new ReadAccountModel()
            {
                Id = account.Id,
                Name = account.Name,
                Password = account.Password,
                Prefix = account.Prefix,
                Host = account.Host
            };
        }
    }

    public class UpdateAccountModel
    {
        [Required]
        [Display(Name = "Id")]
        public long Id { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Prefix")]
        public string Prefix { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Host")]
        public string Host { get; set; }

        public static UpdateAccountModel Convert(Account account)
        {
            return new UpdateAccountModel()
            {
                Id = account.Id,
                Name = account.Name,
                Password = account.Password,
                Prefix = account.Prefix,
                Host = account.Host
            };
        }
    }
}