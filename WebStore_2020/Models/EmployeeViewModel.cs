using System.ComponentModel.DataAnnotations;

namespace WebStore.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Имя является обязательным")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Фамилия является обязательной")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "В имени должно быть не менее 2х и не более 200 символов")]
        [Display(Name = "Фамиль")]
        public string SurName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Отчество является обязательным")]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Возраст является обязательным")]
        [Display(Name = "Возраст")]
        public int Age { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Должность является обязательной")]
        [Display(Name = "Должность")]
        public string Position { get; set; }

    }
}
