using Mas.Domain.Common.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mas.Domain.UserCenter
{
    public class ReadDbContext : DbContext
    {
        public ReadDbContext(DbContextOptions<ReadDbContext> options)
            : base(options)
        { }


        public DbSet<Sys_User> Sys_User { get; set; }
    
    }
}
