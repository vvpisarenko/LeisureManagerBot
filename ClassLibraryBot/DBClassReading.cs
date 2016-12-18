using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryBot
{
    public delegate string[][] DelBook(string[] mas1, string[] mas2, string[] mas3, out int z, out int x, out int y);

    public delegate string[][] DelFilm(string[] mas1, string[] mas2, string[] mas3);

    public class DBClassReading
    {
        public string[][] Book(string[] mas1, string[] mas2, string[] mas3, out int z, out int x, out int y)
        {
            string[][] t = new string[3][];

            using (OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Книги.mdb;"))
            {
                OleDbCommand c = new OleDbCommand("SELECT * FROM Зарубежная", connection);
                connection.Open();
                OleDbDataReader r = c.ExecuteReader();
                mas1[0] = r.GetName(0) + "\t" + r.GetName(1) + "\t" + r.GetName(2);
                int i = 1;
                while (r.Read())
                {
                    mas1[i] = r[0].ToString();
                    mas2[i] = r[1].ToString();
                    mas3[i] = r[2].ToString();
                    i = i + 1;
                }

                z = i;

                OleDbCommand c1 = new OleDbCommand("SELECT * FROM Русская", connection);
                OleDbDataReader r1 = c1.ExecuteReader();
                while (r1.Read())
                {
                    mas1[i] = r1[0].ToString();
                    mas2[i] = r1[1].ToString();
                    mas3[i] = r1[2].ToString();
                    i = i + 1;
                }

                x = i;

                OleDbCommand c2 = new OleDbCommand("SELECT * FROM Фантастика", connection);
                OleDbDataReader r2 = c2.ExecuteReader();
                while (r2.Read())
                {

                    mas1[i] = r2[0].ToString();
                    mas2[i] = r2[1].ToString();
                    mas3[i] = r2[2].ToString();
                    i = i + 1;
                }

                y = i;

                connection.Close();

                t[0] = mas1;
                t[1] = mas2;
                t[2] = mas3;

                return t;
            }
        }

        public string[][] Film(string[] mas1, string[] mas2, string[] mas3)
        {
            string[][] t = new string[3][];
            using (OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Фильмы.mdb;"))
            {

                OleDbCommand c = new OleDbCommand("SELECT * FROM Фильмы", connection);

                connection.Open();
                OleDbDataReader r = c.ExecuteReader();

                mas1[0] = r.GetName(0) + "\t" + r.GetName(1) + "\t" + r.GetName(2);
                int i = 1;

                while (r.Read())
                {
                    mas1[i] = r[0].ToString();
                    mas2[i] = r[1].ToString();
                    mas3[i] = r[2].ToString();
                    i = i + 1;
                }
                connection.Close();
            }

            t[0] = mas1;
            t[1] = mas2;
            t[2] = mas3;

            return t;
        }
    }
}