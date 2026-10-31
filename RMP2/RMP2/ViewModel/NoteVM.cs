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
    public class NoteVM : Note, INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public new string Name
        {
            get => base.Name;
            set
            {
                base.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public new string Content
        {
            get => base.Content;
            set
            {
                base.Content = value;
                OnPropertyChanged(nameof(Content));
            }
        }

        public new DateTime CreationTime
        {
            get => base.CreationTime;
            set
            {
                base.CreationTime = value;
                OnPropertyChanged(nameof(CreationTime));
            }
        }

        public NoteVM(string name, string content, DateTime? creationTime = null) :
            base(name, content, creationTime) { }

        public void Copy(NoteVM note)
        {
            Name = note.Name;
            Content = note.Content;
            CreationTime = note.CreationTime;
        }

        public object Clone()
        {
            return new NoteVM(Name, Content);
        }

        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
