using System;
using System.Collections.Generic;

namespace EmovisionBlazor.Domain.Models
{
    /// <summary>
    /// Таблица эмоции.
    /// </summary>
    public partial class Emotion
    {
        public Emotion()
        {
            RecognitionResults = new HashSet<RecognitionResult>();
        }

        public short Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<RecognitionResult> RecognitionResults { get; set; }
    }
}
