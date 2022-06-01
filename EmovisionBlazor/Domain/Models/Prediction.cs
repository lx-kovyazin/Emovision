using System;
using System.Collections.Generic;

namespace EmovisionBlazor.Domain.Models
{
    /// <summary>
    /// Таблица прогноза
    /// </summary>
    public partial class Prediction
    {
        public Prediction()
        {
            PredictionResults = new HashSet<PredictionResult>();
        }

        public short Id { get; set; }
        public string Value { get; set; } = null!;

        public virtual ICollection<PredictionResult> PredictionResults { get; set; }
    }
}
