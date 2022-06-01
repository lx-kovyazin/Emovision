using System;
using System.Collections.Generic;

namespace EmovisionBlazor.Domain.Models
{
    /// <summary>
    /// Таблица результата распознавания.
    /// </summary>
    public partial class RecognitionResult
    {
        public long Id { get; set; }
        public long RecognitionProcessId { get; set; }
        public short EmotionId { get; set; }
        public DateTime Timestamp { get; set; }

        public virtual Emotion Emotion { get; set; } = null!;
        public virtual RecognitionProcess RecognitionProcess { get; set; } = null!;
    }
}
