using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain.Common
{
    public record BaseCommonResult
    {
        public bool IsSuccess { get; set; }
        public StatusCode StatusCode { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
