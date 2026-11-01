using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using RMP2.Model;

namespace RMP2.ViewModel
{
    /// <summary>
    /// Операция над заметкой.
    /// </summary>
    public abstract class NoteOperation : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private TimeSpan _time;
        private DateTime _date;

        /// <summary>
        /// Заметка, над которой производится операция.
        /// </summary>
        public NoteVM Note { get; protected set; }

        /// <summary>
        /// Время, выбранное пользователем во время операции.
        /// </summary>
        public TimeSpan Time 
        { 
            get => _time; 
            set
            {
                _time = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Time)));
            }
        }

        /// <summary>
        /// Дата, выбранная пользователем во время операции.
        /// </summary>
        public DateTime Date 
        {
            get => _date;
            set
            {
                _date = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Date)));
            } 
        }

        /// <summary>
        /// Команда для сохранения результатов операции.
        /// </summary>
        public Command SaveCommand { get; private set; }

        /// <summary>
        /// Команда для отмены операции.
        /// </summary>
        public Command CancelCommand { get; private set; }

        /// <summary>
        /// Инициализирует поля для хранения команд.
        /// </summary>
        public NoteOperation(NoteVM note = null)
        {
            Note = note is null ? new NoteVM() : note;
            Time = Note.AppointedTime.TimeOfDay;
            Date = Note.AppointedTime.Date;
            SaveCommand = new Command(Save);
            CancelCommand = new Command(Cancel);
        }

        /// <summary>
        /// Сохраняет результаты операции.
        /// </summary>
        /// <param name="parameter"></param>
        protected abstract void Save(object parameter);

        /// <summary>
        /// Отменяет операцию.
        /// </summary>
        /// <param name="parameter"></param>
        protected abstract void Cancel(object parameter);
    }
}
