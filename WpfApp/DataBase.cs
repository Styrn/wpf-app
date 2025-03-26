using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class DbCreator
    {
        SQLiteConnection dbConnection;
        SQLiteCommand command;
        string sqlCommand;
        static string dbPath = System.Environment.CurrentDirectory;
        string dbFilePath = dbPath + "\\babyshopdb.db";
        public void createDbFile()
        {
            if (!string.IsNullOrEmpty(dbPath) && !Directory.Exists(dbPath))
                Directory.CreateDirectory(dbPath);
            dbFilePath = dbPath + "\\babyshopdb.db";
            if (!System.IO.File.Exists(dbFilePath))
            {
                SQLiteConnection.CreateFile(dbFilePath);
            }
        }

        public string createDbConnection()
        {
            string strCon = string.Format("Data Source={0};", dbFilePath);
            dbConnection = new SQLiteConnection(strCon);
            dbConnection.Open();
            command = dbConnection.CreateCommand();
            return strCon;
        }

        public void createTables()
        {
            if (!checkIfExist("Users"))
            {
                sqlCommand = "CREATE TABLE Users (User_ID INTEGER PRIMARY KEY AUTOINCREMENT," +
                                                 "User_Phone TEXT," +
                                                 "User_Name TEXT," +
                                                 "User_Surname TEXT);";
                executeQuery(sqlCommand);
            }
            if (!checkIfExist("Products"))
            {
                sqlCommand = "CREATE TABLE Products (Product_ID INTEGER PRIMARY KEY AUTOINCREMENT," +
                                                    "Product_Brand TEXT," +
                                                    "Product_Type TEXT," +
                                                    "Product_Size TEXT," +
                                                    "Product_Age INTEGER," +
                                                    "Product_Price INTEGER," +
                                                    "Product_Gender TEXT);";
                executeQuery(sqlCommand);
            }
            if (!checkIfExist("Orders"))
            {
                sqlCommand = "CREATE TABLE Orders (Order_ID INTEGER PRIMARY KEY AUTOINCREMENT," +
                                                  "Order_User INTEGER REFERENCES Users (User_ID) ON DELETE CASCADE ON UPDATE CASCADE," +
                                                  "Order_Product INTEGER REFERENCES Products (Product_ID) ON DELETE CASCADE ON UPDATE CASCADE," +
                                                  "Order_Date DATE);";
                executeQuery(sqlCommand);
            }
            if (!checkIfExist("Favorites"))
            {
                sqlCommand = "CREATE TABLE Favorites (Favorite_ID INTEGER PRIMARY KEY AUTOINCREMENT," +
                                                     "Favorite_User INTEGER REFERENCES Users (User_ID) ON DELETE CASCADE ON UPDATE CASCADE," +
                                                     "Favorite_Product INTEGER REFERENCES Products (Product_ID) ON DELETE CASCADE ON UPDATE CASCADE);";
                executeQuery(sqlCommand);
            }
            if (!checkIfExist("Cart"))
            {
                sqlCommand = "CREATE TABLE Cart (Cart_ID INTEGER PRIMARY KEY AUTOINCREMENT," +
                                                "Cart_User INTEGER REFERENCES Users (User_ID) ON UPDATE CASCADE," +
                                                "Cart_Product INTEGER REFERENCES Products (Product_ID) ON DELETE CASCADE ON UPDATE CASCADE);";
                executeQuery(sqlCommand);
            }
        }

        public bool checkIfExist(string tableName)
        {
            command.CommandText = "SELECT name FROM sqlite_master WHERE name='" + tableName + "'";
            var result = command.ExecuteScalar();

            return result != null && result.ToString() == tableName ? true : false;
        }

        public void executeQuery(string sqlCommand)
        {
            SQLiteCommand triggerCommand = dbConnection.CreateCommand();
            triggerCommand.CommandText = sqlCommand;
            triggerCommand.ExecuteNonQuery();
        }

        public bool checkIfTableContainsData(string tableName)
        {
            command.CommandText = "SELECT count(*) FROM " + tableName;
            var result = command.ExecuteScalar();

            return Convert.ToInt32(result) > 0 ? true : false;
        }


        public void fillTable()
        {
            if (!checkIfTableContainsData("MY_TABLE"))
            {
                sqlCommand = "insert into MY_TABLE (code_test_type) values (999)";
                executeQuery(sqlCommand);
            }
        }
    }
}