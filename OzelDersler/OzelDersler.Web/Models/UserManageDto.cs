using OzelDersler.Entity.Concrete.Identity;
using OzelDersler.Entity.Concrete;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OzelDersler.Web.Models
{
    public class UserManageDto
    {
        public string Id { get; set; }


        [DisplayName("Ad")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public string FirstName { get; set; }

        [DisplayName("Soyad")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public string LastName { get; set; }

        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public string UserName { get; set; }

        [DisplayName("Eposta")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Yaş")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public int Age { get; set; }


        [DisplayName("Hakkımda")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public string About { get; set; }


        [DisplayName("Doğum Tarihi")]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("Deneyim Yılı")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public int Experience { get; set; }

        [DisplayName("Cinsiyet")]
        public string Gender { get; set; }

        [DisplayName("Telefon Numarası")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
