using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace RMP2.Model
{
    /// <summary>
    /// Заметка.
    /// </summary>
    public class Note
    {
        /// <summary>
        /// Идентификатор заметки.
        /// </summary>
        [PrimaryKey, Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Название заметки.
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Содержание заметки.
        /// </summary>
        [Column("content")]
        public string Content { get; set; }

        /// <summary>
        /// Назначенное время заметки.
        /// </summary>
        [Column("appointed_time")]
        public DateTime AppointedTime { get; set; }

        /// <summary>
        /// Время создания заметки.
        /// </summary>
        [Column("creation_time")]
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Инициализирует все поля заметки переданными значениями.
        /// </summary>
        /// <param name="name">Название заметки.</param>
        /// <param name="content">Содержание заметки.</param>
        /// <param name="appointedTime">Назначенное время заметки.</param>
        /// <param name="creationTime">Время создания заметки.</param>
        public Note(string name = "", string content = "", DateTime? appointedTime = null, DateTime? creationTime = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Content = content;
            AppointedTime = appointedTime == null ? DateTime.Now : (DateTime)appointedTime;
            CreationTime = creationTime == null ? DateTime.Now : (DateTime)creationTime;
        }

        /// <summary>
        /// Инициализирует все поля заметки.
        /// </summary>
        public Note() 
        {
            Id = Guid.NewGuid();
            Name = "";
            Content = "";
            AppointedTime = DateTime.Now;
            CreationTime = DateTime.Now;
        }
    }
}
