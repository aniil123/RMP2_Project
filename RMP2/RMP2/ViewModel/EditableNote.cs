using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using RMP2.Model;
using RMP2.View;

namespace RMP2.ViewModel
{
    /// <summary>
    /// Операция редактирования заметки.
    /// </summary>
    public class EditableNote : NoteOperation
    {
        /// <summary>
        /// Ссылка на заметку, выбранную пользователем для редактирования.
        /// </summary>
        private NoteVM _editableNote;

        /// <summary>
        /// Инициализирует поля для хранения ссылки на выбранную заметку и редактирования ее данных.
        /// </summary>
        /// <param name="note">Ссылка на выбранную заметку.</param>
        public EditableNote(NoteVM note) : base(note)
        {
            _editableNote = note;
            Note = (NoteVM)note.Clone();
        }

        /// <summary>
        /// Сохраняет совершенные пользователем изменения и возвращает экран со списком заметок.
        /// </summary>
        /// <param name="parameter"></param>
        protected override async void Save(object parameter)
        {
            _editableNote.Copy(Note);
            _editableNote.AppointedTime = Date + Time;
            await Shell.Current.GoToAsync("///" + nameof(MainPage));
        }

        /// <summary>
        /// Отменяет изменения и возвращает экран со списком заметок.
        /// </summary>
        /// <param name="parameter"></param>
        protected override async void Cancel(object parameter)
        {
            await Shell.Current.GoToAsync("///" + nameof(MainPage));
        }
    }
}
