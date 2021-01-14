using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Homework4.Task3
{
    class NoteService
    {
        private readonly string _notesFileName;
        private List<Note> _notes = new List<Note>();

        public NoteService(string notesFileName)
        {
            _notesFileName = notesFileName;
        }

        public Note[] GetAllNotes()
        {
            if (_notes.Count!=0) return _notes.ToArray();

            var json = File.ReadAllText(_notesFileName);
            if (json.Length == 0)
                throw new Exception();
            _notes = JsonConvert.DeserializeObject<List<Note>>(json);
            return _notes.ToArray();
        }

        public void SearchNotes()
        {
            Console.Write("Enter the filter string to search: ");
            string filter = Console.ReadLine();

            var searchedNotes = _notes.Where(note => Convert.ToString(note.CreatedOn).Contains(filter) ||
                                                     Convert.ToString(note.Id).Contains(filter) ||
                                                     note.Title.Contains(filter) ||
                                                     note.Text.Contains(filter)).Select(note => note).ToList();
            if (searchedNotes.Count == 0)
            {
                Console.WriteLine("\nThe notes that contains the string were not found\n");
                return;
            }
            searchedNotes = SortById(searchedNotes);
            searchedNotes.ForEach(note => Console.WriteLine($"№{note.Id}           {note.CreatedOn}\n{note.Title}"));
        }
        public void ViewNote()
        {
            int id = GetIdFromUser();
            if (NoteIsExist(id))
            {
                DisplayNote(id);
            }
            else
                Console.WriteLine("\nThe note with the current id was not found\n");
        }
        public void CreateNote()
        {
            Console.WriteLine("Enter the content of the note:\n");
            string noteContent = Console.ReadLine();

            noteContent = noteContent.Trim();
            if (String.IsNullOrEmpty(noteContent))
            {
                Console.WriteLine("Empty note would not be saved");
                return;
            }
            _notes.Add(new Note(noteContent));
            UpdateNotesInFile();
        }
        public void DeleteNote()
        {
            int id = GetIdFromUser();
            if (NoteIsExist(id))
            {
                DisplayNote(id);
                if (DeletionIsConfirmed())
                {
                    _notes = _notes.Where(note => note.Id != id).ToList();
                    UpdateNotesInFile();
                }
                else
                    return;
            }
            else
                Console.WriteLine("\nThe note with the current id was not found\n");
        }

        private void UpdateNotesInFile()
        {
            var options = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
            };
            var json = JsonConvert.SerializeObject(_notes, options);
            File.WriteAllText("notes.json", json);
        }
        private int GetIdFromUser()
        {
            string id;
            do
            {
                Console.Write("Enter the id of the note: ");
                id = Console.ReadLine();
            } while (!IdIsValid(id));
            return Convert.ToInt32(id);
        }
        private List<Note> SortById(List<Note> notes)
            => notes.OrderBy(note => note.Id).ToList();
        private void DisplayNote(int id)
        {
            var note =_notes.Where(note => note.Id == id).ToList();
            Console.WriteLine(note[0].ToString());
        }
        private bool IdIsValid(string id)
        {
            try
            {
                Convert.ToInt32(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool NoteIsExist(int id)
            => _notes.Any(note => note.Id == id);
        private bool DeletionIsConfirmed() 
        {
            do
            {
                Console.Write("Are you sure that you want to delete this note?\nYour answer:");
                string answer = Console.ReadLine().ToLower();
                if (answer == "yes")
                    return true;
                else
                if (answer == "no")
                    return false;
                else
                    Console.WriteLine("Wrong answer. Please, try again.\n");

            } while (true);
        }
    }
}
