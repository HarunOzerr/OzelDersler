
using OzelDersler.Entity.Concrete;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OzelDersler.Web.Areas.Admin.Models
{
    public class TeacherAddDto
    {
        [DisplayName("Ad")]
        [Required(ErrorMessage ="{0} alanı boş bırakılmamalıdır.")]
        [MinLength(3, ErrorMessage = "{0}, {1} karakterden kısa olmamalıdır.")]
        [MaxLength(100, ErrorMessage = "{0}, {1} karakterden uzun olmamalıdır.")]
        public string FirstName { get; set; }

        [DisplayName("Soyad")]
        [Required(ErrorMessage = "{0} alanı boş bırakılmamalıdır.")]
        [MinLength(3, ErrorMessage = "{0}, {1} karakterden kısa olmamalıdır.")]
        [MaxLength(100, ErrorMessage = "{0}, {1} karakterden uzun olmamalıdır.")]
        public string LastName { get; set; }


        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public string UserName { get; set; }

        [DisplayName("Eposta")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Doğum Tarihi")]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("Deneyim Yılı")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public int Experience { get; set; }

        [DisplayName("Cinsiyet")]
        public string Gender { get; set; }

        public List<Branch> Branches { get; set; }

        public int[] SelectedBranchIds { get; set; }
    }
}
