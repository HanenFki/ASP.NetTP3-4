using System.ComponentModel.DataAnnotations;
using WebApplication3.Models;

namespace WebApplication3.ViewModels
{
    public class creatviewmodelsSchool
    {
        public int SchoolID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        [Display(Name = "Nom de l'école :")]
        public string SchoolName { get; set; }

        [Required]
        [Display(Name = "Adresse de l'école :")]
        public string SchoolAdress { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
