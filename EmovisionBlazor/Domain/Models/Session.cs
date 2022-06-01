using System;
using System.Collections.Generic;

namespace EmovisionBlazor.Domain.Models
{
    /// <summary>
    /// Таблица сеанса работы пользователя.
    /// </summary>
    public partial class Session
    {
        public Session()
        {
            RecognitionProcesses = new HashSet<RecognitionProcess>();
        }

        /// <summary>
        /// Уникальный номер сеанса.
        /// </summary>
        public long Id { get; set; }
        public string UserName { get; set; } = null!;

        public virtual User UserNameNavigation { get; set; } = null!;
        public virtual ICollection<RecognitionProcess> RecognitionProcesses { get; set; }
    }
}
