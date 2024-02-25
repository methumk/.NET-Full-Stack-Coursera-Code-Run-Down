using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoreWebApp.Models;

namespace CoreWebApp.Data
{
    public class CoreWebAppContext : DbContext
    {   
        // 2.? The "initialized" dbcontext from Program.cs is injected into our apps context
        // The DB context is being injected to our context
        public CoreWebAppContext (DbContextOptions<CoreWebAppContext> options)
            : base(options)
        {
        }

        // NOTE: using the Dependency model as fields in the db?
        // I think this means that this attribute "Dependency" manages the "Dependency table" which is abstracted/managed internally from the Dependency Model
        public DbSet<CoreWebApp.Models.Dependency> Dependency { get; set; } = default!;
    }
}
