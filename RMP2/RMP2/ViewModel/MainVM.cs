using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using RMP2.View;
using RMP2.Model;
using RMP2.Model.Services;

namespace RMP2.ViewModel
{
    /// <summary>
    /// Главный класс ViewModel.
    /// </summary>
    public class MainVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Поле для хранения единственного возможного экземпляра класса <see cref="MainVM"/>.
        /// </summary>
        private static MainVM _instance;

        /// <summary>
        /// Строка для поиска.
        /// </summary>
        private string _searchString = "";

        /// <summary>
        /// Заметка <see cref="NoteVM"/>, над которой совершается операция: создание или редактирование.
        /// </summary>
        private NoteVM? _noteCouch = null;

        /// <summary>
        /// Выбранная пользователем заметка.
        /// </summary>
        public NoteVM? _selectedNote = null;

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
        /// Команда для удаления выбранной заметки.
        /// </summary>
        public Command DeleteCommand { get; private set; }

        /// <summary>
        /// Команда для сохранения внесенных пользователем изменений на странице заметки.
        /// </summary>
        public Command SaveCommand { get; private set; }

        /// <summary>
        /// Команда для отмены внесенных пользователем изменений на странице заметки.
        /// </summary>
        public Command CancelCommand { get; private set; }

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
        /// Возвращает и задает строку поиска.
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
        /// Возвращает заметку <see cref="NoteVM"/>, над которым совершается операция: создание или редактирование.
        /// </summary>
        public NoteVM? NoteCouch
        {
            get => _noteCouch;
            set
            {
                _noteCouch = value;
                OnPropertyChanged();
                
            }
        }

        /// <summary>
        /// Возвращает заметку, выбранную пользователем.
        /// </summary>
        public NoteVM? SelectedNote
        {
            get => _selectedNote;
            set
            {
                _selectedNote = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Инициализирует поля для хранения команд.
        /// </summary>
        private MainVM() 
        {
            _notes.CollectionChanged += (sender, e) => UpdateDisplayedNotes();
            OpenPageForCreatingNoteCommand = new Command(OpenPageForCreatingNote);
            OpenPageForEditingNoteCommand = new Command(OpenPageForEditingNote);
            DeleteCommand = new Command(Delete);
            SaveCommand = new Command(Save);
            CancelCommand = new Command(Cancel);
            DatabaseService.GetNotes().ForEach(note =>
            {
                NoteVM noteVM = new NoteVM { Note = note };
                _notes.Add(noteVM);
            });
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
            NoteCouch = new NoteVM();
            SelectedNote = null;
            await Shell.Current.GoToAsync("///" + nameof(NotePage));
        }

        /// <summary>
        /// Открывает страницу для редактирования заметки.
        /// </summary>
        /// <param name="parameter">Заметка, выбранная пользователем для редактирования.</param>
        private async void OpenPageForEditingNote(object parameter)
        {
            if (SelectedNote is null) return;
            NoteCouch = (NoteVM)SelectedNote.Clone();
            await Shell.Current.GoToAsync("///" + nameof(NotePage));
        }

        /// <summary>
        /// Удаляет выбранную заметку.
        /// </summary>
        /// <param name="parameter">Удаляемая заметка.</param>
        private void Delete(object parameter)
        {
            if (SelectedNote is null) return;
            DatabaseService.Delete(SelectedNote.Note);
            _notes.Remove(SelectedNote);
            SelectedNote = null;
        }

        /// <summary>
        /// Сохраняет внесенные пользователем изменения.
        /// Возвращает страницу со списком заметок.
        /// </summary>
        /// <param name="parameter"></param>
        private async void Save(object parameter)
        {
            if (SelectedNote is null)
            {
                NoteCouch.ResetCreationTime();
                _notes.Insert(0, NoteCouch);
                DatabaseService.Insert(NoteCouch.Note);
            }
            else
            {
                SelectedNote.Note = NoteCouch.Note;
                SelectedNote.ResetCreationTime();
                DatabaseService.Update(SelectedNote.Note);
                UpdateDisplayedNotes();
            }
            NoteCouch = null;
            SelectedNote = null;
            await Shell.Current.GoToAsync("///" + nameof(MainPage));
        }

        /// <summary>
        /// Возвращает страницу со списком заметок.
        /// </summary>
        /// <param name="parameter"></param>
        private async void Cancel(object parameter)
        {
            NoteCouch = null;
            SelectedNote = null;
            await Shell.Current.GoToAsync("///" + nameof(MainPage));
        }

        private void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
