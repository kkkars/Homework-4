using System;

namespace Homework4.Task3
{
    public class Menu
    {
        private NoteService noteService = new NoteService("notes.json");
        public static void StartMenu()
        {
            var noteService = new NoteService("notes.json");

            try
            {
                noteService.GetAllNotes();
            }
            catch (Exception)
            {
            }

            do
            {
                ShowMenu();
                string option =GetOption();
                if (option == "1")
                    noteService.CreateNote();
                else
                if (option == "2")
                    noteService.SearchNotes();
                else
                if (option == "3")
                    noteService.ViewNote();
                else
                if (option == "4")
                    noteService.DeleteNote();
                else
                if (option == "5")
                    return;
                else
                    Console.WriteLine("Wrong option. Please, try again\n");
            } while (true);
        }
        private static void ShowMenu()
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1.Create new note\n2.Search the note\n3.View the note\n4.Delete the note\n5.Exit\n");
        }
        private static string GetOption()
        {
            Console.Write("\nOption: ");
            return Console.ReadLine().Trim();
        }
    }
}
