using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using RMP2.Model;

namespace RMP2.ViewModel
{
    /// <summary>
    /// Заметка расширенная для взаимодействия с View.
    /// </summary>
    public class NoteVM : Note, INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Название заметки.
        /// </summary>
        public new string Name
        {
            get => base.Name;
            set
            {
                base.Name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Содержание заметки.
        /// </summary>
        public new string Content
        {
            get => base.Content;
            set
            {
                base.Content = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Назначенное время заметки.
        /// </summary>
        public new DateTime AppointedTime
        {
            get => base.AppointedTime;
            set
            {
                base.AppointedTime = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Время создания заметки.
        /// </summary>
        public new DateTime CreationTime
        {
            get => base.CreationTime;
            set
            {
                base.CreationTime = value;
                OnPropertyChanged();
            }
        }

        public NoteVM(string name = "", string content = "", DateTime? appointedTime = null, DateTime? creationTime = null) :
            base(name, content, appointedTime, creationTime) { }

        /// <summary>
        /// Присваивает свойствам текущего объекта значения всех свойств передаваемого объекта,
        /// создавая полную копию.
        /// </summary>
        /// <param name="note">Объект, значения свойств которого копируются в свойства текущего объекта.</param>
        public void Copy(NoteVM note)
        {
            Name = note.Name;
            Content = note.Content;
            AppointedTime = note.AppointedTime;
            CreationTime = note.CreationTime;
        }

        /// <summary>
        /// Возвращает копию текущего объекта с новым временем создания <see cref="CreationTime"/>.
        /// </summary>
        /// <returns>Копия текущего объекта с новым временем создания <see cref="CreationTime"/>.</returns>
        public object Clone()
        {
            return new NoteVM(Name, Content, AppointedTime);
        }

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
