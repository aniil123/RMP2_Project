using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMP2.Model
{
    public class Note
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime CreationTime { get; set; }

        public Note(string name, string content, DateTime? creationTime = null)
        {
            Name = name;
            Content = content;
            CreationTime = creationTime == null ? DateTime.Now : (DateTime)creationTime;
        }
    }
}
