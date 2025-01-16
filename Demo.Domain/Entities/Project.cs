using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Demo.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }
    }
}
