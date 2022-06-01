using System;
using System.Collections.Generic;

namespace EmovisionBlazor.Domain.Models
{
    /// <summary>
    /// Таблица процесса распознавания
    /// </summary>
    public partial class RecognitionProcess
    {
        public RecognitionProcess()
        {
            RecognitionResults = new HashSet<RecognitionResult>();
        }

        public long Id { get; set; }
        public long SessionId { get; set; }

        public virtual Session Session { get; set; } = null!;
        public virtual PredictionResult PredictionResult { get; set; } = null!;
        public virtual ICollection<RecognitionResult> RecognitionResults { get; set; }
    }
}
