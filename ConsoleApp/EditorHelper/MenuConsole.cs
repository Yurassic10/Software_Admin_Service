using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.EditorHelper
{
    public class MenuConsole
    {
        public static void Introdusing()
        {
            Console.WriteLine("\t\t\t\tWELCOME to the menu! \n");
        }
        public static void LoginOrRegister() 
        {
            Console.WriteLine("\n1. Register"
                +"\n2. Login");
        }
        public static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("\nMain menu."
            + "\n1. Do action with Admin."
            + "\n2. Do actions with user."
            + "\n3. See Admin-User activity."
            + "\n0. Exit."
            + "\nYour choice:");
        }
        public static void ShowAdminMenu()
        {
            Console.Clear();
            Console.WriteLine("\nAdmin menu."
                +"\n1. See all administrator."
                +"\n2. Delete user by id."
                +"\n3. Activate user."
                +"\n4. Blocked user."
                +"\n5.Exit."
                +"\nYour choice:");
        }
        public static void ShowUserMenu()
        {
            Console.Clear();
            Console.WriteLine("\nUser menu."
                +"\n1. See all users."
                +"\n2. Search user by id."
                +"\n3. Sort users."
                +"\n4. Exit."
                +"\nYour choice:");
        }
        public static void ShowAdminUserActivity()
        {
            Console.Clear();
            Console.WriteLine("\nAdmin-User activity menu"
                +"\n1. See all activity logged(what action was done)"
                +"\n2. Exit."
                +"\nYour choice:");
        }
    }
}
