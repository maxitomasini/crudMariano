using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using crudMariano.Database.Entities;
using System.Linq;
using System.Text;

public class DataAccess
{
    private SqlConnection conn = new SqlConnection("Server=localhost\\MSSQLSERVER01;Database=crudMariano;Trusted_Connection=True;");
        
    public SqlConnection Getconnection()
    {
        return conn;
    }

    public void Open()
    {
        conn.Open();
    }

    public void Close()
    {
        conn.Close();
    }
    
}
