using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SilentRoar.Models;

namespace SilentRoar.Data
{
    /// <summary>
    /// 包含所有的用户数据库表格
    /// </summary>
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        DbSet<ReforgeDev> ReforgeDevs { get; set; } 
    }
}
