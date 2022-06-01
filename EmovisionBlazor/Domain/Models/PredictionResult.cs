using System;
using System.Collections.Generic;

namespace EmovisionBlazor.Domain.Models
{
    /// <summary>
    /// Таблица результата распознавания.
    /// </summary>
    public partial class PredictionResult
    {
        public long RecognitionProcessId { get; set; }
        public short PredictionId { get; set; }

        public virtual Prediction Prediction { get; set; } = null!;
        public virtual RecognitionProcess RecognitionProcess { get; set; } = null!;
    }
}
