using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace RMP2.Model.Services
{
    public static class DatabaseService
    {
        private static SQLiteConnection _conn;

        private static SQLiteConnection Conn
        {
            get
            {
                if (_conn is null)
                {
                    _conn = new SQLiteConnection(Path);
                    _conn.CreateTable<Note>();
                }
                return _conn;
            }
        }

        private static string Path
        {
            get => "database.db3";
        }

        /// <summary>
        /// Возвращает список заметок из базы данных.
        /// </summary>
        /// <returns>Список заметок <see cref="Note"/></returns>
        public static List<Note> GetNotes()
            => Conn.Table<Note>().ToList();

        /// <summary>
        /// Добавляет новую заметку в таблицу класса <see cref="Note"/>.
        /// </summary>
        /// <param name="note">Добавляемая заметка.</param>
        public static void Insert(Note note)
            => Conn.Insert(note);

        /// <summary>
        /// Обновляет передаваемую заметку в таблице класса <see cref="Note"/>.
        /// </summary>
        /// <param name="note">Обновляемая заметка.</param>
        public static void Update(Note note)
            => Conn.Update(note);

        /// <summary>
        /// Удаляет передаваемую заметку из таблицы класса <see cref="Note"/>.
        /// </summary>
        /// <param name="note">Удаляемая заметка.</param>
        public static void Delete(Note note)
            => Conn.Delete(note);
    }
}
