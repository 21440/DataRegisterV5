using System;
using System.IO;
using System.Linq;

namespace V5
{
    class Program
    {

		public static string filepath;

        static void Main(string[] args)
        {

			filepath = args[0];

			var Exists = (File.Exists(filepath));

			if (!Exists)
			{

				Console.Clear();
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true))
				{

					file.WriteLine("ID,First Name,Last Name,Savings,Password,Data");

				}
				Console.WriteLine("\nThe file of records has been created!\n");
			
			}

			while (true)
            {

                Menu();

            }
		}

        public static void Menu()
        {

            Console.WriteLine("\n 	DataRegister App! v5.0.0\n========================================");
            Console.WriteLine("\n1. Registry a record");
            Console.WriteLine("2. View the file of records");
            Console.WriteLine("3. Record Finder");
            Console.WriteLine("4. Record Remover");
            Console.WriteLine("5. Record Editer");
            Console.WriteLine("6. Exit\n");

            string rightselec = Console.ReadLine();

            switch (rightselec)
            {

                case "1":

                    Console.Clear();
                    Procedure();

                    break;

                case "2":

                    Console.Clear();
                    ToList();

                    break;

                case "3":

                    Console.Clear();
                    Console.WriteLine("\nEnter the ID of the record you want:");
                    string id = numInput();
                    Finder(id);

                    break;

                case "4":

                    Console.Clear();
                    Console.WriteLine("\nEnter the ID of the record you want:");
                    string remove = numInput();
                    Delete(remove);

                    break;

                case "5":

                    Console.Clear();
                    Console.WriteLine("\nEnter the ID of the record you want:");
                    string change = numInput();
                    Edit(change);

                    break;

                case "6":

                    Console.Clear();
                    Environment.Exit(0);

                    break;

                default:

                    Console.Clear();
					Console.WriteLine("\nSomething went wrong, try again.\n");
                    Menu();

                    break;

            }

        }

		public static void Procedure()
		{

            while (true)
            {

                Console.WriteLine("\nEnter the ID: ");
                string id = numInput();

                if (UniqueID(id))
                {

                    Console.WriteLine("\nThis ID has already been recorded.");
                    break;

                }

                Console.WriteLine("\nEnter the First Name: ");
                string fname = stdInput();
                Console.WriteLine("\nEnter the Last Name: ");
                string lname = stdInput();
                Console.WriteLine("\nEnter the Age: ");
                int age = Convert.ToInt32(numInput());
                Console.WriteLine("\nEnter the Sex: [M|F]");
                char sex = Convert.ToChar(Console.ReadLine());
                Console.WriteLine("\nEnter the Marital Status: [S|M]");
                char maritalsts = Convert.ToChar(Console.ReadLine());
                Console.WriteLine("\nEnter the Education Level: [I|M|G|P]");
                char grade = Convert.ToChar(Console.ReadLine());
                Console.WriteLine("\nEnter the Savings: ");
                string saving = decInput();
                string fpw, spw;
                do
                {
                    
                    Console.WriteLine("\nEnter the Password: ");
                    fpw = pwInput();
                    Console.WriteLine("\nConfirm the Password: ");
                    spw = pwInput();

                } while (fpw != spw);

                int data = Encode(age, sex, maritalsts, grade);
                
                Console.WriteLine("\nSave [S]; Discard[D]; Exit[E]");
                string Selection = Console.ReadLine();

                switch (Selection.ToLower())
                {

                    case "s":

                        Record(id, fname, lname, saving, fpw, data, filepath);
                        Console.Clear();
                        Console.WriteLine("\nRecord registered correctly!\n");

                        break;

                    case "d":

                        Console.Clear();
                        Procedure();

                        break;

                    case "e":

                        Console.Clear();
                        break;

                    default:

                        Console.Clear();
                        Console.WriteLine("\nSomething went wrong, try again.");

                        break;

                }

                break;

            }


		}

		public static void Record(string ID, string FirstName, string LastName, string Savings, string Password, int Data, string filepath)
		{

			try
			{

				using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true))
				{

					file.WriteLine(ID + "," + FirstName + "," + LastName + "," + Savings + "," + Password + "," + Data);

				}

			}

			catch(Exception exc)
			{

				throw new ApplicationException("This program failed to run correctly: ", exc);

			}

		}

        public static void ToList()
        {

            Console.WriteLine("");
            var content = File.ReadAllLines(filepath);
            
            foreach (var lines in content)
            {

                var element = lines.Split(",");
                var record = element[0] + "," + element[1] + "," + element[2] + "," + element[3] + "," + element[4];
                if (element.Contains("ID"))
                {

                    Console.WriteLine(record + ",Age,Sex,Marital Status,Education Level");

                }
                
                else
                {

                    Console.WriteLine(record + Decode(Convert.ToInt32(element[5])));

                }

            }

        }

        public static void Finder(string id)
        {       

            int counter = 0;
            var content = File.ReadAllLines(filepath);

            foreach(var line in content)
            {
               
                var identity = line.Split(",");

                if (identity[0] == id)
                {

                    Console.Clear();
                    string header = "\nID,First Name,Last Name,Savings,Password,Age,Sex,Marital Status,Education Level\n";
                    var record = identity[0] + "," + identity[1] + "," + identity[2] + "," + identity[3] + "," + identity[4];
                    Console.WriteLine("Record found!\n" + header + record + Decode(Convert.ToInt32(identity[5])));
                    counter = 1;

                }

            }

            if (counter == 0)
            {
                
                Console.Clear();
                Console.WriteLine("That record doesn't exist.");

            }

        }

        public static bool UniqueID(string id)
        {

            bool verify = false;
            var content = File.ReadAllLines(filepath);

            foreach (var item in content)
            {

                var exists = item.Split(",");

                if (exists[0] == id)
                {

                    return !verify;

                }
                
            }

            return verify;

        }

        public static void Delete(string id)
        {

            bool counter = false;
            var content = File.ReadAllLines(filepath);
            
            System.IO.StreamWriter file = new System.IO.StreamWriter(filepath, false);

            foreach (var item in content)
            {

                var element = item.Split(",");

                if (element[0] == id)
                {

                    Console.Clear();
                    string header = "ID,First Name,Last Name,Savings,Password,Age,Sex,Marital Status,Education Level\n";
                    var record = element[0] + "," + element[1] + "," + element[2] + "," + element[3] + "," + element[4];
                    Console.WriteLine(header + record + Decode(Convert.ToInt32(element[5])));
                    Console.WriteLine("\nThe record has been deleted.");
                    counter = !counter;
                    break;

                }

                file.WriteLine(item);

            }

            if (counter == false)
            {

                Console.Clear();
                Console.WriteLine("That record is not in the Registry File.");

            }

            file.Close();

        }

        public static void Edit(string id)
        {

            bool counter = false;
            string select = "";
            var content = File.ReadAllLines(filepath);
            System.IO.StreamWriter file = new System.IO.StreamWriter(filepath, false);

            foreach (var item in content)
            {

                var element = item.Split(",");

                if (element[0] == id)
                {

                    Console.Clear();
                    Console.WriteLine("Proceed to make the changes!");
                    Console.WriteLine("\nEnter the new First Name: ");
                    string fname = stdInput();
                    Console.WriteLine("\nEnter the new Last Name: ");
                    string lname = stdInput();
                    Console.WriteLine("\nEnter the Age: ");
                    int age = Convert.ToInt32(numInput());
                    Console.WriteLine("\nEnter the Sex: [M|F]");
                    char sex = Convert.ToChar(Console.ReadLine().ToUpper());
                    Console.WriteLine("\nEnter the Marital Status: [S|M]");
                    char maritalsts = Convert.ToChar(Console.ReadLine().ToUpper());
                    Console.WriteLine("\nEnter the Education Level: [I|M|G|P]");
                    char grade = Convert.ToChar(Console.ReadLine().ToUpper());
                    Console.WriteLine("\nEnter the new Savings: ");
                    string saving = decInput();
                    string fpw, spw;

                    do
                    {

                        Console.WriteLine("\nEnter the new Password: ");
                        fpw = pwInput();
                        Console.WriteLine("\nConfirm the new Password: ");
                        spw = pwInput();

                    } while (fpw != spw);

                    int data = Encode(age, sex, maritalsts, grade);
                    var record = $"{id},{fname},{lname},{saving},{fpw}";

                    Console.WriteLine("\nSave [S]; Discard[D]; Exit[E]");
                    string Selection = Console.ReadLine();

                    switch (Selection.ToLower())
                    {

                        case "s":

                            file.WriteLine(record + $",{data}");
                            if (element[1] != fname)
                            {

                                Console.WriteLine("\nChanges in the First Name made successfully!");

                            }

                            if (element[2] != lname)
                            {

                                Console.WriteLine("\nChanges in the Last Name made successfully!");

                            }

                            if (element[3] != saving)
                            {

                                Console.WriteLine("\nChanges in the Savings made successfully!");

                            }

                            if (element[4] != fpw)
                            {

                                Console.WriteLine("\nChanges in the Password made successfully!");

                            }

                            if (Convert.ToInt32(element[5]) != data)
                            {

                                Console.WriteLine("\nChanges in the Data made successfully!");

                            }

                            if ((element[1] == fname) && (element[2] == lname) && (element[3] == saving) && (element[4] == fpw) && (Convert.ToInt32(element[5]) == data))
                            {

                                Console.WriteLine("\nIt appears no changes has been made.");

                            }

                            counter = !counter;
                            select = "success";
                            string header = "\nID,First Name,Last Name,Savings,Password,Age,Sex,Marital Status,Education Level\n";
                            Console.WriteLine(header + record + Decode(data));

                            break;

                        case "d":

                            file.WriteLine(item);
                            select = "discard";

                            break;

                        case "e":

                            file.WriteLine(item);
                            select = "exit";

                            break;

                        default:

                            Console.WriteLine("\nSomething went wrong, try again.");
                            file.WriteLine(item);
                            select = "failed";

                            break;

                    }

                    if (select.Length > 1)
                        continue;

                }

                file.WriteLine(item);

            }

            if (counter == false)
            {

                Console.Clear();
                Console.WriteLine("That record is not in the Registry File or its edit was aborted.");

            }

            file.Close();

            if (select == "discard")
                Edit(id);

        }

        public static string pwInput()
        {

            string input = "";

            while (true)
            {

                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {

                    Console.WriteLine();
                    break;

                }

                else if (key.Key == ConsoleKey.Backspace)
                {

                    if (Console.CursorLeft == 0)
                        continue;

                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    Console.Write(" ");

                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    input = string.Join("", input.Take(input.Length - 1));

                }

                else
                {

                    if (key.KeyChar != 44)
                    {

                        input += key.KeyChar.ToString();
                        Console.Write("*");

                    }

                }

            }

            return input;

        }

        public static string numInput()
        {

            string input = "";

            while (true)
            {

                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {

                    Console.WriteLine();
                    break;

                }

                else if (key.Key == ConsoleKey.Backspace)
                {

                    if (Console.CursorLeft == 0)
                        continue;

                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    Console.Write(" ");

                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    input = string.Join("", input.Take(input.Length - 1));

                }

                else if (char.IsDigit(key.KeyChar))
                {

                    if (input.Length <= 11)
                    {

                        input += key.KeyChar.ToString();
                        Console.Write(key.KeyChar);

                    }

                }

            }

            return input;

        }

        public static string decInput()
        {

            string input = "";

            while (true)
            {

                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {

                    Console.WriteLine();
                    break;

                }

                else if (key.Key == ConsoleKey.Backspace)
                {

                    if (Console.CursorLeft == 0)
                        continue;

                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    Console.Write(" ");

                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    input = string.Join("", input.Take(input.Length - 1));

                }

                else if (char.IsDigit(key.KeyChar))
                {

                    input += key.KeyChar.ToString();
                    Console.Write(key.KeyChar);

                }

                else if (key.KeyChar == 46)
                {

                    if (!input.Contains("."))
                    {

                        input += key.KeyChar.ToString();
                        Console.Write(key.KeyChar);

                    }

                }

            }

            return input;

        }

        public static string stdInput()
        {

            string input = "";

            while (true)
            {

                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {

                    Console.WriteLine();
                    break;

                }

                else if (key.Key == ConsoleKey.Backspace)
                {

                    if (Console.CursorLeft == 0)
                        continue;

                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    Console.Write(" ");

                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

                    input = string.Join("", input.Take(input.Length - 1));

                }

                else if (!char.IsDigit(key.KeyChar))
                {

                    input += key.KeyChar.ToString();
                    Console.Write(key.KeyChar);

                }

            }

            return input;

        }

        public static int Encode(int age, char sex, char maritalsts, char grade)
        {

            int data = age << 4;

            if (sex == 'M')
            {

                data = data | 8;

            }

            if (maritalsts == 'M')
            {

                data = data | 4;

            }

            if (grade == 'M')
            {

                data = data | 1;

            }

            else if (grade == 'G')
            {

                data = data | 2;

            }

            else if (grade == 'P')
            {

                data = data | 3;

            }

            return data;

        }

        public static string Decode(int data)
        {

            int age = data >> 4;
            
            int opS = data - (age << 4);

            int sex = opS >> 3;
            var sexP = "";

            if (sex == 1)
            {

                sexP = "Male";

            }

            else
            {

                sexP = "Female";

            }

            int opM = opS - (sex << 3);

            int maritalsts = opM >> 2;
            var maritalstsP = "";

            if (maritalsts == 1)
            {

                maritalstsP = "Married";

            }

            else
            {

                maritalstsP = "Single";

            }

            int grade = opM - (maritalsts << 2);
            var gradeP = "";

            if (grade == 1)
            {

                gradeP = "Medium";

            }

            else if (grade == 2)
            {

                gradeP = "Grade";

            }

            else if (grade == 3)
            {

                gradeP = "Post-Grade";

            }

            else
            {

                gradeP = "Initial";

            }

            return $",{age},{sexP},{maritalstsP},{gradeP}";

        }

    }
}
