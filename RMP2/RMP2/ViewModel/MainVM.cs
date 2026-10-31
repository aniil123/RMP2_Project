using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using RMP2.View;
using RMP2.Model;

namespace RMP2.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static MainVM _instance;
        private NoteOperation _noteOperation;
        private string _searchString = "";
        private ObservableCollection<NoteVM> _notes = new ObservableCollection<NoteVM>();
        public ObservableCollection<NoteVM> DisplayedNotes { get; } = new ObservableCollection<NoteVM>();
        public Command OpenPageForCreatingNoteCommand { get; private set; }
        public Command OpenPageForEditingNoteCommand { get; private set; }

        public NoteOperation NoteOperation
        {
            get => _noteOperation;
            set
            {
                _noteOperation = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(NoteOperation)));
            }
        }

        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                UpdateDisplayedNotes();
            }
        }

        public static MainVM Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MainVM();
                return _instance;
            }
        }

        private MainVM() 
        {
            _notes.CollectionChanged += (sender, e) => { UpdateDisplayedNotes(); };
            OpenPageForCreatingNoteCommand = new Command(OpenPageForCreatingNote);
            OpenPageForEditingNoteCommand = new Command(OpenPageForEditingNote);
            string s = "";
            for (int i = 0; i < 5; i++)
            {
                s += i.ToString();
                _notes.Add(new NoteVM(s, s));
            }
        }

        private void UpdateDisplayedNotes()
        {
            DisplayedNotes.Clear();
            if (SearchString == "")
            {
                foreach (NoteVM note in _notes)
                    DisplayedNotes.Add(note);
            }
            else
            {
                foreach (NoteVM note in _notes)
                {
                    if (note.Name.Length < SearchString.Length) continue;
                    bool coincidence = true;
                    for (int i = 0; i < SearchString.Length; i++)
                    {
                        if (note.Name[i] != SearchString[i])
                        {
                            coincidence = false;
                            break;
                        }
                    }
                    if (coincidence)
                        DisplayedNotes.Add(note);
                }
            }
            for (int i = 0; i < DisplayedNotes.Count - 1; i++)
            {
                DateTime maxTime = DisplayedNotes[i].CreationTime;
                int index = i;
                for (int j = i + 1; j < DisplayedNotes.Count; j++)
                {
                    if (DisplayedNotes[j].CreationTime > maxTime)
                    {
                        maxTime = DisplayedNotes[j].CreationTime;
                        index = j;
                    }
                }
                NoteVM temp = DisplayedNotes[i];
                DisplayedNotes[i] = DisplayedNotes[index];
                DisplayedNotes[index] = temp;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayedNotes)));
        }

        private async void OpenPageForCreatingNote(object parameter)
        {
            NoteOperation = new CreatableNote(_notes);
            await Shell.Current.GoToAsync("///" + nameof(NotePage));
        }

        private async void OpenPageForEditingNote(object parameter)
        {
            if (parameter == null) return;
            NoteOperation = new EditableNote((NoteVM)parameter);
            await Shell.Current.GoToAsync("///" + nameof(NotePage));
        }
    }
}
