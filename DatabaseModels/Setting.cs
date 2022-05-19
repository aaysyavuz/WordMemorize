using System;
using System.Collections.Generic;

#nullable disable

namespace WordMemorize.DatabaseModels
{
    public partial class Setting
    {
        public int Id { get; set; }
        public int Level1Date { get; set; }
        public int Level2Date { get; set; }
        public int Level3Date { get; set; }
        public int Level4Date { get; set; }
        public int Level5Date { get; set; }
        public int Level6Date { get; set; }
        public uint UserId { get; set; }

        public virtual User User { get; set; }
    }
}
