using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Product
{
    public class ChildCategory : BaseModel
    {
        public string Name { get; set; }
        public Int64 ParentId { get; set; }
    }
}
