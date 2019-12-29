using System;
using System.Collections.Generic;
using System.Text;

namespace SharpLoad.Application.Models
{
    public class TestScript
    {
        public Uri TargetHost { get; set; }
        public uint MaxUsers { get; set; }
        public uint SpawnRate { get; set; }
        public uint TestDuration { get; set; }
        public ICollection<UserRequestSequence> UserRequestSequences { get; set; }
    }
}
