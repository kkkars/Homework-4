using System;

namespace Homework4.Task3
{
    class Note : INote
    {
        private int id;
        private string title;
        private string text;
        private DateTime createdOn;

        private static int counter = 1;
        public Note(string text)
        {
            id = counter;
            counter++;

            createdOn = DateTime.UtcNow;

            this.title = text.Length <= 32 ? text : text.Substring(0, 32);

            this.text = text;
        }

        public override string ToString()
        {
            return $"№{id}                 {createdOn}\n    {title}\n\n{text}\n";
        }

        public int Id => id;
        public string Title => title;
        public string Text => text;
        public DateTime CreatedOn => createdOn;
    }
}
