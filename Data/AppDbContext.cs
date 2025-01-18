﻿using ApiProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Todo> ToDos{ get; set; }
    }
}
