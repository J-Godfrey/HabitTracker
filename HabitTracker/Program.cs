using System;
using Microsoft.Data.Sqlite;

internal class Program
{
    private static void Main(string[] args)
    {
        string connectionString = @"Data Source=HabitTracker.db";


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


    }
}