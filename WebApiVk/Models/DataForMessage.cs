﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiVk.Models
{
    public class DataForMessage
    {
        public int Id { get; set; }
        public string Login { get; set; }

        public string Password { get; set; }

        public string Message { get; set; }
    }
}
