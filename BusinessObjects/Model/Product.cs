﻿using System;
using System.Collections.Generic;

namespace BusinessObjects.Model
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
