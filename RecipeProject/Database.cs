using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class Database
    {
        public SQLiteConnection myConnection;
        public Database()
        {
            myConnection = new SQLiteConnection("Data Source=University.sqlite3"); 
            if (!File.Exists("./University.sqlite3")) //Ако нямаме такъв файл
            {
                SQLiteConnection.CreateFile("University.sqlite3"); //Създаваме базата
                MessageBox.Show("Database created"); //Извеждаме пояснителен текст
            }
        }

        public void OpenConnection() //Отваряне на connection към базата
        {
            if (myConnection.State != System.Data.ConnectionState.Open) //Само ако нямаме connection
                myConnection.Open(); //отваряме нова

        }

        public void CloseConnection() //Затваряне на connection към базата
        {
            if (myConnection.State != System.Data.ConnectionState.Closed) //Ако не е затворена connection
                myConnection.Close(); //я затваряме
        }
    }
}
