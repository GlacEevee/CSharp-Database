using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

class Database
{
    private string dbFolder;

    public Database()
    {
        string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        dbFolder = Path.Combine(home, ".database");

        Directory.CreateDirectory(dbFolder);
    }

    private string TablePath(string tableName)
    {
        return Path.Combine(dbFolder, tableName + ".tbl");
    }

    public void CreateTable(string tableName, params string[] columns)
    {
        string path = TablePath(tableName);
        if (File.Exists(path))
        {
            Console.WriteLine($"Table '{tableName}' already exists.");
            return;
        }

        File.WriteAllText(path, string.Join(",", columns) + "\n");
        Console.WriteLine($"Table '{tableName}' created.");
    }

    public void Insert(string tableName, params string[] values)
    {
        string path = TablePath(tableName);
        if (!File.Exists(path))
        {
            Console.WriteLine($"Table '{tableName}' does not exist.");
            return;
        }

        File.AppendAllText(path, string.Join(",", values) + "\n");
    }

    public void SelectAll(string tableName)
    {
        string path = TablePath(tableName);
        if (!File.Exists(path))
        {
            Console.WriteLine($"Table '{tableName}' does not exist.");
            return;
        }

        var lines = File.ReadAllLines(path);
        var columns = lines[0].Split(',');

        Console.WriteLine(string.Join(" | ", columns));
        Console.WriteLine(new string('-', 30));

        for (int i = 1; i < lines.Length; i++)
        {
            var row = lines[i].Split(',');
            Console.WriteLine(string.Join(" | ", row));
        }
    }
}
