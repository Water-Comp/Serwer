﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;

namespace ServerConsole
{
    class DB
    {
        public string path;
        public SQLiteConnection m_dbConnection;
        public DB(string source)
        {
            path = source;
            m_dbConnection = new SQLiteConnection("Data Source=" + source + ";Version=3;");
            m_dbConnection.Open();
        }

        public void ChangeSource(string source)
        {
            path = source;
            m_dbConnection.Close();
            m_dbConnection = new SQLiteConnection("Data Source=" + source + ";Version=3;");
            m_dbConnection.Open();
        }
        public string Compute(string txt)
        {
                using (MD5 md5Hash = MD5.Create())
                {
                    return GetMd5Hash(md5Hash, txt);
                }
            }

        private string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        private bool VerifyMd5Hash(string input, string hash)
        {
            string hashOfInput = Compute(input);
            Console.WriteLine("z veryfiy hash");
            Console.WriteLine(hashOfInput);
            Console.WriteLine(hash);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool VerifyPassword(string login, string password)
        {
            password = Compute(password);
            login = login.ToUpper();
            login = Compute(login);
            string d = Query("Select ID from accounts_list where login = '" + login + "' and password = '" + password + "'");
            Console.WriteLine(d);
            if (d != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void CreateNewUser(string login, string password)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                login = login.ToUpper();
                password = Compute(password);
                login = Compute(login);
            }
            string sql = "insert into accounts_list (login,password) values ('" + login + "', '" + password + "')";
            Query("insert into accounts_list (login,password) values ('" + login + "', '" + password + "')");
        }

        public int get_ID(string login)
        {
            int ID;
            login = login.ToUpper();
            login = Compute(login);
            string sql = "select ID from accounts_list where login = '" + login + "'";
            SQLiteCommand get_id = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader id = get_id.ExecuteReader();
            id.Read();
            ID = id.GetInt32(0);
            return ID;
        }

        public string Query(string sql)
        {
            try
            {
                string tmp = "";
                string wynik = "";
                int i = 0;
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    while (reader.FieldCount > i)
                    {
                        wynik += reader[i].ToString();
                        wynik += " ";
                        i++;
                    }

                    i = 0;
                }

                for (int j = 0; j < wynik.Length - 1; j++)
                {
                    tmp += wynik[j];
                }

                tmp = tmp.Replace('\r', ' ');
                tmp = tmp.Replace('\n', ' ');
                string[] splitedvalues = tmp.Split(' ');
                List<string> list = new List<string>();
                foreach (var value in splitedvalues)
                {
                    if (value != "") list.Add(value);
                }

                string result = "";
                foreach (var value in list)
                {
                    result += value + " ";
                }

                return result;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public int getsize()
        {
            int i = 0;
            SQLiteCommand command = new SQLiteCommand("get * from structure", m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                i++;
            }
            return i;
        }

        public int CountResults(string sql)
        {
                int i = 0;
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    i++;
                }
                return i;
        }

    }


}

