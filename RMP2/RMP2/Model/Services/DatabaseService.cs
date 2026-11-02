using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Maui.Storage;
using SQLite;

namespace RMP2.Model.Services
{
    public static class DatabaseService
    {
        private static SQLiteConnection _connection;

        private static SQLiteConnection Connection
        {
            get
            {
                if (_connection is null)
                {
                    _connection = new SQLiteConnection(DbPath);
                    _connection.CreateTable<Note>();
                }
                return _connection;
            }
        }

        private static string DbPath
        {
            get => Path.Combine(FileSystem.AppDataDirectory, "database.db3");
        }

        /// <summary>
        /// Возвращает список заметок из базы данных.
        /// </summary>
        /// <returns>Список заметок <see cref="Note"/></returns>
        public static List<Note> GetNotes()
            => Connection.Table<Note>().ToList();

        /// <summary>
        /// Добавляет новую заметку в таблицу класса <see cref="Note"/>.
        /// </summary>
        /// <param name="note">Добавляемая заметка.</param>
        public static void Insert(Note note)
            => Connection.Insert(note);

        /// <summary>
        /// Обновляет передаваемую заметку в таблице класса <see cref="Note"/>.
        /// </summary>
        /// <param name="note">Обновляемая заметка.</param>
        public static void Update(Note note)
            => Connection.Update(note);

        /// <summary>
        /// Удаляет передаваемую заметку из таблицы класса <see cref="Note"/>.
        /// </summary>
        /// <param name="note">Удаляемая заметка.</param>
        public static void Delete(Note note)
            => Connection.Delete(note);
    }
}
