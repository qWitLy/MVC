using System.ComponentModel.DataAnnotations;

namespace WebApplication10.Models
{
    public class User
    {
        public int Id { get; set; }
        [Display(Name="Имя")]
        [Required(ErrorMessage ="Поле не должно быть пустым")]
        public string Name { get; set; }
        [Display(Name="Возраст")]
        [Required(ErrorMessage ="Поле не должно быть пустым")]
        public int Age { get; set; }
    }
}
