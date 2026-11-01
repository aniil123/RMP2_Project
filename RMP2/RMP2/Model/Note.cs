using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMP2.Model
{
    /// <summary>
    /// Заметка.
    /// </summary>
    public class Note
    {
        /// <summary>
        /// Название заметки.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Содержание заметки.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Назначенное время заметки.
        /// </summary>
        public DateTime AppointedTime { get; set; }

        /// <summary>
        /// Время создания заметки.
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Инициализирует все поля заметки.
        /// </summary>
        /// <param name="name">Название заметки.</param>
        /// <param name="content">Содержание заметки.</param>
        /// <param name="appointedTime">Назначенное время заметки.</param>
        /// <param name="creationTime">Время создания заметки.</param>
        public Note(string name = "", string content = "", DateTime? appointedTime = null, DateTime? creationTime = null)
        {
            Name = name;
            Content = content;
            AppointedTime = appointedTime == null ? DateTime.Now : (DateTime)appointedTime;
            CreationTime = creationTime == null ? DateTime.Now : (DateTime)creationTime;
        }
    }
}
