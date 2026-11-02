using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Reflection;
using RMP2.Model;

namespace RMP2.ViewModel
{
    /// <summary>
    /// Заметка расширенная для взаимодействия с View.
    /// </summary>
    public class NoteVM : INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Note _note;

        public Note Note 
        { 
            get => _note; 
            set
            {
                _note = value;
                foreach (var prop in typeof(NoteVM).GetProperties())
                    OnPropertyChanged(prop.Name);
            }
        }

        /// <summary>
        /// Название заметки.
        /// </summary>
        public string Name
        {
            get => Note.Name;
            set
            {
                Note.Name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Содержание заметки.
        /// </summary>
        public string Content
        {
            get => Note.Content;
            set
            {
                Note.Content = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Назначенное время заметки.
        /// </summary>
        public DateTime AppointedTime
        {
            get => Note.AppointedTime;
            set
            {
                Note.AppointedTime = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Время создания заметки.
        /// </summary>
        public DateTime CreationTime
        {
            get => Note.CreationTime;
            set
            {
                Note.CreationTime = value;
                OnPropertyChanged();
            }
        }

        public NoteVM(string name = "", string content = "", DateTime? appointedTime = null, DateTime? creationTime = null)
            => Note = new Note(name, content, appointedTime, creationTime);

        /// <summary>
        /// Возвращает полную копию текущего объекта.
        /// </summary>
        /// <returns>Копия текущего объекта.</returns>
        public object Clone()
        {
            NoteVM noteVM = new NoteVM(Name, Content, AppointedTime, CreationTime);
            noteVM.Note.Id = Note.Id;
            return noteVM;
        }

        /// <summary>
        /// Устанавливает <see cref="CreationTime"/> в значение текущего времени.
        /// </summary>
        public void ResetCreationTime()
            => CreationTime = DateTime.Now;

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
