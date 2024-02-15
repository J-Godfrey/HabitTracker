using System;
using System.Data;
using System.Reflection.PortableExecutable;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic.FileIO;

internal class Program
{
    static string connectionString = @"Data Source=HabitTracker.db";
    private static void Main(string[] args)
    {
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand tablecmd = connection.CreateCommand();

            tablecmd.CommandText = 
                    @"CREATE TABLE IF NOT EXISTS drinking_water (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Date TEXT,
                        Quantity INTEGER
                        )";

            tablecmd.ExecuteNonQuery();

            connection.Close();
        }

        GetUserInput();

    }

    static void GetUserInput()
    {
        Console.Clear();
        bool closeApp = false;

        while (closeApp == false)
        {
            Console.WriteLine(
                @"Main Menu

What would you like to do?

Type 0 to close applications
Type 1 to view all records.
Type 2 to insert records.
Type 3 to Delete Records.
Type 4 to update records.
-------------------------------");

            string commandInput = Console.ReadLine();

            switch (commandInput) 
            {
                case "0":
                    Console.WriteLine("\nGoodbye!\n");
                    closeApp = true;
                    break;
                case "1":
                    GetAllRecords();
                    break;
                case "2":
                    Insert();
                    break;
                //case "3":
                //    Delete();
                //    break;
                //case "4":
                //    Update();
                //    break;
                default:
                    Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                    GetUserInput();
                    break;
            }


        }




    }

    private static void GetAllRecords()
    {
        Console.Clear();

        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand tablecmd = connection.CreateCommand();

            tablecmd.CommandText =
                    $"SELECT * FROM drinking water";

            List<DrinkingWater> tableDate = new();
            
            SqliteDataReader reader = tablecmd.ExecuteReader();



        }
    }

    private static void Insert()
    {
        string date = GetDateInput();

        int quantity = GetNumberInput("\n\nPlease insert number of glasses or other measure of your choice (no decimals allowed)\n\n");

        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand tablecmd = connection.CreateCommand();

            tablecmd.CommandText =
                    $"INSERT INTO drinking_water (Date, Quantity) VALUES('{date}', {quantity})"; 
          
            tablecmd.ExecuteNonQuery();

            connection.Close();
        }

    }

    private static int GetNumberInput(string message)
    {
        Console.WriteLine(message);

        string numberInput = Console.ReadLine();

        if (numberInput == "0")
            GetUserInput();

        int finalInput = Convert.ToInt32(numberInput);

        return finalInput;
    }

    internal static string GetDateInput()
    {
        Console.WriteLine("\n\nPlease insert the date: (Format: dd-mm-yy). Type 0 to return to the main menu");

        string dateInput = Console.ReadLine();

        if (dateInput == "0")
            GetDateInput();

        return dateInput;
    }



}

internal class DrinkingWater
{

    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int Quantity { get; set; }
}