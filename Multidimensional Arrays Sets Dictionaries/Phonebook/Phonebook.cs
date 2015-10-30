namespace SoftUni.Homeworks.AdvancedCSharp.Multidimetional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program that receives some info from the console about people and their phone numbers.
    /// 
    /// You are free to choose the manner in which the data is entered; each entry should have just
    /// one name and one number(both of them strings). 
    /// 
    /// After filling this simple phonebook, upon receiving the command "search", your program should
    /// be able to perform a search of a contact by name and print her details in format
    /// "{name} -> {number}". In case the contact isn't found, print "Contact {name} does not exist."
    /// 
    /// * Bonus: What happens if the user enters the same name twice in the phonebook? Modify your
    /// program to keep multiple phone numbers per contact.
    /// </summary>
    class Phonebook
    {
        static TextHelper Helper = new TextHelper();

        static char[] separators =
        {
            ':',
            '|',
            '/'
        };

        static ConsoleColor infoColor = ConsoleColor.DarkYellow;

        static void Main()
        {
            Helper.SetupConsole();

            var phonebook = new SortedDictionary<string, List<string>>();

            string userEntry = string.Empty;
            while (true)
            {
                Console.Clear();
                Helper.PrintColorText("Phonebook\n\n", "cyan");

                Helper.PrintColorText("Commands:\n", "green");
                Console.WriteLine("'search' - search the phonebook.");
                Console.WriteLine("'continue' - after you are done searching");
                Console.WriteLine("'separators' to see a list of separator characters");
                Console.WriteLine("'print' - prints the whole phonebook"); //This could be paged
                Console.WriteLine("'exit - to exit the program'");
                Console.WriteLine("'example' - shows an example entry pattern");
                // Console.WriteLine("'save' - to save the phonebook to a file");
                // Console.WriteLine("'load' - to load the saved phonebook");

                Helper.PrintColorText("Enter 'name' or 'command' followed by separating character and the 'phonenumber'\n", "white");

                userEntry = Console.ReadLine();

                switch (userEntry.ToLower())
                {
                    case "search":
                        SearchPhonebook(phonebook);
                        break;

                    case "continue":
                        Helper.PrintColorText(
                            "Sorry 'continue' is a reserved word for search"
                            , infoColor);
                        break;

                    case "separators":
                        PrintSeparators();
                        break;

                    case "print":
                        PrintPhonebook(phonebook);
                        break;

                    case "example":
                        PrintExample();
                        break;

                    case "exit":
                        Helper.PrintColorText("Bye bye!\n", "green");
                        Environment.Exit(0);
                        break;

                    case "":
                        // Skip on empty
                        break;

                    default:
                        SaveContact(userEntry, phonebook);
                        break;
                }
            }
        }

        private static void PrintExample()
        {
            Helper.PrintColorText("Wire: ", infoColor);
            Console.WriteLine("Bate Joro : 12 34 56 892\n");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

        private static void PrintPhonebook(SortedDictionary<string, List<string>> phonebook)
        {
            foreach (var entry in phonebook)
            {
                PrintEntry(entry);
                Console.WriteLine();
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }

        private static void PrintEntry(KeyValuePair<string, List<string>> entry)
        {
            if (entry.Value.Count > 1)
            {
                Console.WriteLine("{0}'s phones: ", entry.Key);
                entry.Value.ForEach(Console.WriteLine);
            }
            else
            {
                Console.WriteLine("{0} -> {1}", entry.Key, entry.Value[0]);
            }
        }

        private static void SaveContact(string userEntry, SortedDictionary<string, List<string>> phonebook)
        {
            string[] input = userEntry
                .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToArray();

            if (input.Length != 2)
            {
                Helper.PrintColorText("Invalid input!\n (Probalby a bad separator - see 'separators')\n\n", "red");
            }
            else
            {
                string name = input[0],
                phone = input[1];

                if (phonebook.ContainsKey(name))
                {
                    if (phonebook[name].IndexOf(phone) == -1)
                    {
                        phonebook[name].Add(phone);
                        Helper.PrintColorText(
                        string.Format(
                            "{0} added successfully to {1}'s list of phones\n\n"
                            , phone, name), "green");
                    }
                    else
                    {
                        Helper.PrintColorText(
                        string.Format("{0} already has this number\n\n", name), "green");
                    } 
                }
                else
                {
                    phonebook.Add(input[0], new List<string>() { phone });
                    Helper.PrintColorText(
                        string.Format("{0} added successfully to the phonebook\n\n", name), "green");
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }

        private static void PrintSeparators()
        {
            Helper.PrintColorText("Separators: \n", infoColor);
            foreach (char s in separators)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }

        private static void SearchPhonebook(SortedDictionary<string, List<string>> phonebook)
        {
            string userEntry = string.Empty;
            while (userEntry.ToLower() != "continue")
            {
                Helper.PrintColorText("You are looking for: ", infoColor);
                userEntry = Console.ReadLine();

                switch (userEntry.ToLower())
                {
                    case "search":
                        Helper.PrintColorText("You are already serching...\n", infoColor);
                        break;

                    case "separators":
                        PrintSeparators();
                        break;

                    case "print":
                        PrintPhonebook(phonebook);
                        break;

                    case "exit":
                        Helper.PrintColorText("Bye bye!\n\n", "green");
                        Environment.Exit(0);
                        break;

                    case "continue":
                        break;

                    default:
                        if (phonebook.ContainsKey(userEntry))
                        {
                            PrintEntry(new KeyValuePair<string, List<string>>(userEntry, phonebook[userEntry]));
                        }
                        else
                        {
                            Helper.PrintColorText(string.Format("Contact {0} does not exist.\n\n", userEntry), infoColor);
                        }

                        break;
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
