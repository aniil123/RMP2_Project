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
    /// <summary>
    /// Главный класс ViewModel.
    /// </summary>
    public class MainVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Поля для хранения единственного возможного экземпляра класса <see cref="MainVM"/>.
        /// </summary>
        private static MainVM _instance;

        /// <summary>
        /// Текущая выполняемая операция над заметкой.
        /// </summary>
        private NoteOperation _noteOperation;

        /// <summary>
        /// Строка для поиска заметок.
        /// </summary>
        private string _searchString = "";

        /// <summary>
        /// Коллекция, содержащая все созданные заметки.
        /// </summary>
        private ObservableCollection<NoteVM> _notes = new ObservableCollection<NoteVM>();

        /// <summary>
        /// Коллекция для отображения заметок с учетом значения в строке поиска.
        /// </summary>
        public ObservableCollection<NoteVM> DisplayedNotes { get; } = new ObservableCollection<NoteVM>();

        /// <summary>
        /// Команда для открытия страницы для создания заметки.
        /// </summary>
        public Command OpenPageForCreatingNoteCommand { get; private set; }

        /// <summary>
        /// Команда для открытия страницы для редактирования выбранной заметки.
        /// </summary>
        public Command OpenPageForEditingNoteCommand { get; private set; }

        /// <summary>
        /// Возвращает единственный возможный экземпляр класса <see cref="MainVM"/>.
        /// </summary>
        public static MainVM Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MainVM();
                return _instance;
            }
        }

        /// <summary>
        /// Возвращает и задает операцию над заметкой.
        /// </summary>
        public NoteOperation NoteOperation
        {
            get => _noteOperation;
            set
            {
                _noteOperation = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(NoteOperation)));
            }
        }

        /// <summary>
        /// Возвращает и задает строку поиска заметок.
        /// </summary>
        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                UpdateDisplayedNotes();
            }
        }

        /// <summary>
        /// Инициализирует поля для хранения команд.
        /// </summary>
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

        /// <summary>
        /// Обновляет коллекцию <see cref="DisplayedNotes"/>.
        /// </summary>
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

        /// <summary>
        /// Открывает страницу для создания заметки.
        /// </summary>
        /// <param name="parameter"></param>
        private async void OpenPageForCreatingNote(object parameter)
        {
            NoteOperation = new CreatableNote(_notes);
            await Shell.Current.GoToAsync("///" + nameof(NotePage));
        }

        /// <summary>
        /// Открывает страницу для редактирования заметки.
        /// </summary>
        /// <param name="parameter">Заметка, выбранная пользователем для редактирования.</param>
        private async void OpenPageForEditingNote(object parameter)
        {
            if (parameter == null) return;
            NoteOperation = new EditableNote((NoteVM)parameter);
            await Shell.Current.GoToAsync("///" + nameof(NotePage));
        }
    }
}
