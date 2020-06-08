using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace SharpGalery.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
            :base("name=AppConnection")
        {

        }

        public DbSet<Image> Images { get; set;}
    }
}