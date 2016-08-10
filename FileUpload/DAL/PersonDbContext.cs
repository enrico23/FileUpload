using FileUpload.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FileUpload.DAL
{
    public class PersonDbContext : DbContext
    {
        public DbSet<Person> Person { get; set; }
    }
}