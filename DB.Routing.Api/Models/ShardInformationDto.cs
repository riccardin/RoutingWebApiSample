using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DB.Routing.Api.Models
{
    public class ShardInformationDto
    {
        public Guid id;
        [Required (ErrorMessage ="You should provide a connection string")]
        [MaxLength (500)]
        public string connectionString;
        [Required]
        public int customerId;
    }
}
