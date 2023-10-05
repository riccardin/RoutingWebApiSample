using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DB.Routing.Api.Models
{
    public class ShardInformation
    {
     
        public Guid id;
        [Required]
        [MaxLength(500)]
        public string connectionString;
        public int customerId;
    }
}
