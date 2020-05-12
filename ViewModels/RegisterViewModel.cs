using System.ComponentModel.DataAnnotations;


namespace SilentRoar.ViewModels
{
    /// <summary>
    /// 注册视图模型
    /// </summary>
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }
    }
}
