using System;
using System.Collections.Generic;

#nullable disable

namespace WordMemorize.DatabaseModels
{
    public partial class Question
    {
        public uint Id { get; set; }
        public uint UserId { get; set; }
        public string EnglishWord { get; set; }
        public string TurkishWord { get; set; }
        public int Level { get; set; }
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
    }
}
