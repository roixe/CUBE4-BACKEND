using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamaisASec.Models
{
    class CommandeDTO
    {
        public int ID { get; set; }
        public string Reference { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

        public int? Clients_ID { get; set; }
        public int? Fournisseurs_id { get; set; }
    }
}
