using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMP2.Model;

namespace RMP2.ViewModel
{
    public abstract class NoteOperation
    {
        public NoteVM Note { get; protected set; }
        public Command SaveCommand { get; private set; }
        public Command CancelCommand { get; private set; }

        public NoteOperation()
        {
            SaveCommand = new Command(Save);
            CancelCommand = new Command(Cancel);
        }

        protected abstract void Save(object parameter);

        protected abstract void Cancel(object parameter);
    }
}
