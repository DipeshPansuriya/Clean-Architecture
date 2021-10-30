using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Domain.Genric
{
    public class ResponseRequest_cls
    {
        public int Id { get; set; }
        public string filename { get; set; }
        public string userid { get; set; }
        public string request { get; set; }
        public string response { get; set; }
    }
}
