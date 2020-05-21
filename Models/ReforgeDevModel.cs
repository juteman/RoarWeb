using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SilentRoar.Models
{
    /// <summary>
    /// 开发日志模型基础模型
    /// </summary>
    public class Base
    {
        /// <summary>
        /// 主键
        /// </summary>
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Created { get; set; } = DateTime.Now.ToString("MM/dd/yyyy");
        [Required]
        [Display(Name = "作者")]
        public string Author { get; set; }
        [Required]
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "文章内容")]
        public string Content { get; set; }
    }

    /// <summary>
    /// Reforge 开发日志模型
    /// </summary>
    public class ReforgeDev : Base
    {

    }
}
