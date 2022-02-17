using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingAPI.Configurations
{
    public class TestOption
    {
        public TestOption()
        {
            ImageConfig = new ImageConfiguration();
        }
        public int CompanyType { get; set; }
        public string ConnectionString { get; set; }
        public ImageConfiguration ImageConfig { get; set; }
    }
}
