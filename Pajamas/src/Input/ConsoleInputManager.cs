using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveInactiveUsersReport.src.Input
{
    public class ConsoleInputManager
    {
        public ConsoleInputManager()
        {

        }
        public static bool GetValidatedKey(ConsoleKey keyConstraint)
        {
            ConsoleKeyInfo keyInput = Console.ReadKey();
            if(keyInput.Key == keyConstraint)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static T GetValidatedInput<T>()
        {
            bool isValid = false;
            T? result = default;

            while(!isValid)
            {
                string? input = Console.ReadLine();
                if(input != "")
                {
                    try
                    {
                        result = (T)Convert.ChangeType(input, typeof(T))!;
                        isValid = true;
                    }
                    catch (Exception ex)
                    {
                        PrintError($"{ex.ToString}\n");
                        Menu.MenuManager.ReloadMenu();
                    }
                } else 
                {
                    PrintError("Input cannot be empty.");
                    Menu.MenuManager.ReloadMenu();
                }
            }
            return result!;
        }

        public static void PrintError(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{message}\nPress ANY KEY to continue...");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();
        }
    }
}
