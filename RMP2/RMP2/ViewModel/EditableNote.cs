using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMP2.Model;
using RMP2.View;

namespace RMP2.ViewModel
{
    public class EditableNote : NoteOperation
    {
        private NoteVM _editableNote;

        public EditableNote(NoteVM note)
        {
            _editableNote = note;
            Note = (NoteVM)note.Clone();
        }

        protected override async void Save(object parameter)
        {
            _editableNote.Copy(Note);
            await Shell.Current.GoToAsync("///" + nameof(MainPage));
        }

        protected override async void Cancel(object parameter)
        {
            await Shell.Current.GoToAsync("///" + nameof(MainPage));
        }
    }
}
