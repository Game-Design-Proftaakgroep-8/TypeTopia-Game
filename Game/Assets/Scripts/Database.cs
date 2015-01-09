using UnityEngine;
using System.Collections;
using System.Data;
using MySql.Data.MySqlClient;

public class Database {

	//fields
	private MySqlConnection conn;

	public Database()
	{
		string connectionString =
			"host=localhost;" +
				"database=topiatrainer;" +
				"username=root;" +
				"password=IJsje!123;" +
				"port=3306;";

		conn = new MySqlConnection (connectionString);
	}

	public void ConnectionBase(MySqlCommand command)
	{
		try
		{
			if (conn.State != ConnectionState.Open)
			{
				conn.Open();
			}
			command.ExecuteNonQuery();
		}
		catch
		{
			if (conn.State == ConnectionState.Open)
			{
				conn.Close();
			}
		}
		finally
		{
			if (conn.State == ConnectionState.Open)
			{
				conn.Close();
			}
		}
	}

	public bool CheckLogin(string name, string password)
	{
		string SQL = "SELECT * FROM topiatrainer.Person WHERE pName=@nm AND pPassword=@pw";
		MySqlCommand cmd = new MySqlCommand (SQL, conn);
		cmd.Parameters.AddWithValue ("@nm", name);
		cmd.Parameters.AddWithValue ("@pw", password);

		bool exist = false;

		try
		{
			if (conn.State == ConnectionState.Closed)
			{
				conn.Open();
			}

			MySqlDataReader reader = cmd.ExecuteReader ();

			while (reader.Read())
			{
				exist = true;
			}

			reader.Close();
		}
		catch
		{
		}

		conn.Close ();

		return exist;
	}
}
