﻿using Microsoft.EntityFrameworkCore;

namespace Backend.Database;

public class ApplicationDbContext:DbContext
{
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    
}