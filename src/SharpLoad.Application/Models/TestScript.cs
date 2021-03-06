﻿using System;
using System.Collections.Generic;

namespace SharpLoad.Application.Models
{
    public class TestScript
    {
        public Uri TargetHost { get; set; }
        public uint MaxUsers { get; set; }
        public uint SpawnRate { get; set; }
        public uint TestDuration { get; set; }
        public ICollection<UserRequestSequence> UserRequests { get; set; }
    }
}
