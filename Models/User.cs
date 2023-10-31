using System.ComponentModel.DataAnnotations;

namespace WebApplication10.Models
{
    public class User
    {
        public Int32 Id { get; set; }
        [Display(Name="Имя")]
        [Required(ErrorMessage ="Поле не должно быть пустым")]
        public String Name { get; set; }
        [Display(Name="Возраст")]
        [Required(ErrorMessage ="Поле не должно быть пустым")]
        public Int32 Age { get; set; }
    }
}
