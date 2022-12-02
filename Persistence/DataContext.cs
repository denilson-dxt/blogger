using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence;
public class DataContext:IdentityDbContext<User>
{
    public DataContext(DbContextOptions<DataContext> options):base(options)
    {
        
    }
}