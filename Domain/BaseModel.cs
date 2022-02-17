using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class BaseModel
    {
        public Int64 Id { get; set; }
        public Int64 CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Int64 UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
