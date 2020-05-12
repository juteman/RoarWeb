using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilentRoar.Models
{
    /// <summary>
    /// 开发日志模型基础模型
    /// </summary>
    public class Base
    {
        public int ID { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string Author { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }

    /// <summary>
    /// Reforge 开发日志模型
    /// </summary>
    public class ReforgeDev : Base
    {

    }
}
