using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMP2.Model;
using RMP2.View;

namespace RMP2.ViewModel
{
    /// <summary>
    /// Операция создания заметки.
    /// </summary>
    public class CreatableNote : NoteOperation
    {
        /// <summary>
        /// Коллекция заметок, в которую добавляется заметка в конце операции.
        /// </summary>
        private ObservableCollection<NoteVM> _notes;

        /// <summary>
        /// Инициализирует поля для хранения создаваемой заметки и коллекции для хранения всех заметок.
        /// </summary>
        /// <param name="notes">Коллекция для хранения всех созданных заметок.</param>
        public CreatableNote(ObservableCollection<NoteVM> notes)
        {
            Note = new NoteVM();
            _notes = notes;
        }

        /// <summary>
        /// Добавляет созданную заметку в коллекцию _notes и возвращает экран со списком заметок.
        /// </summary>
        /// <param name="parameter"></param>
        protected override async void Save(object parameter)
        {
            Note.AppointedTime = Date + Time;
            Note.CreationTime = DateTime.Now;
            _notes.Insert(0, Note);
            await Shell.Current.GoToAsync("///" + nameof(MainPage));
        }

        /// <summary>
        /// Возвращает экран со списком заметок.
        /// </summary>
        /// <param name="parameter"></param>
        protected override async void Cancel(object parameter)
        {
            await Shell.Current.GoToAsync("///" + nameof(MainPage));
        }
    }
}
