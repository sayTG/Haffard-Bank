using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardManagementAPI.Models._ConfigurationModels
{
    public class AESConfiguration
    {
        public string SecretKey { get; set; }
        public string PublicKey { get; set; }
    }
}
