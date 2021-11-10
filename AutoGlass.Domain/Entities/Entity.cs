﻿using System;

namespace AutoGlass.Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}