﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamaisASec.Models
{
    public class Fournisseur : BaseModel
    {
        public int id { get; set; }
        public string nom { get; set; } = string.Empty;
        public string adresse { get; set; } = string.Empty; 
        public string mail { get; set; } = string.Empty;
        public string telephone { get; set; } = string.Empty;
        public string siret { get; set; } = string.Empty;
    }
}
