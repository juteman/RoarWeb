using System.ComponentModel.DataAnnotations;

namespace SilentRoar.ViewModels
{
    /// <summary>
    /// 登录视图模型
    /// </summary>
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "浏览器中维持登录状态")]
        public bool IsPresist { get; set; }
    }
}
