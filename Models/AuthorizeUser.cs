using System.ComponentModel.DataAnnotations;

namespace WebApplication10.Models
{
	public class AuthorizeUser
	{
		public Int32 Id { get; set; }

		[Display(Name = "Логин")]
		[Required(ErrorMessage = "Поле не должно быть пустым")]
		public String Login { get; set; }

		[Display(Name = "Пароль")]
		[Required(ErrorMessage = "Поле не должно быть пустым")]
		public String Password { get; set; }
	}
}
