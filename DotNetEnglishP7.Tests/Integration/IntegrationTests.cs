using Dot.Net.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetEnglishP7.Tests.Integration
{
    public class IntegrationTests
    {
        protected DbContextOptions<LocalDbContext> GetOptions()
        {
            return new DbContextOptionsBuilder<LocalDbContext>()
            .UseInMemoryDatabase("ProductServiceRead" + Guid.NewGuid().ToString())
            .Options;
        }
    }
}