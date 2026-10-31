using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMP2.Model;
using RMP2.View;

namespace RMP2.ViewModel
{
    public class CreatableNote : NoteOperation
    {
        private ObservableCollection<NoteVM> _notes;

        public CreatableNote(ObservableCollection<NoteVM> notes)
        {
            Note = new NoteVM("", "");
            _notes = notes;
        }

        protected override async void Save(object parameter)
        {
            Note.CreationTime = DateTime.Now;
            _notes.Insert(0, Note);
            await Shell.Current.GoToAsync("///" + nameof(MainPage));
        }

        protected override async void Cancel(object parameter)
        {
            await Shell.Current.GoToAsync("///" + nameof(MainPage));
        }
    }
}
