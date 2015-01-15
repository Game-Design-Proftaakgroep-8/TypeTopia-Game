using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

public class Database {

	//fields
	private MySqlConnection conn;

	public Database()
	{
		string connectionString =
			"server=db4free.net;" +
				"database=topiatrainer;" +
				"uid=secretagents;" +
				"pwd=groep8;" +
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

	private void AddCommaOptions(int sumID, SumInfo sumInfo) {
		string SQL = "SELECT * FROM CommaOption WHERE commaID IN (SELET commaID FROM SumComma WHERE sumID = @si)";
		MySqlCommand cmd = new MySqlCommand (SQL, conn);
		cmd.Parameters.AddWithValue ("@si", sumID);
		
		try
		{
			if (conn.State == ConnectionState.Closed)
			{
				conn.Open();
			}
			
			MySqlDataReader reader = cmd.ExecuteReader ();
			
			while (reader.Read())
			{
				int commaOption = reader.GetInt32("commaValue");
				sumInfo.AddCommaOption(commaOption);
			}
			
			reader.Close();
		}
		catch
		{
		}
		
		conn.Close ();
	}

	public SumInfo GetSumInfo(string sumType, int sumLevel)
	{
		string SQL = "SELECT * FROM Sums WHERE sumType=@st AND sumLevel=@sl";
		MySqlCommand cmd = new MySqlCommand (SQL, conn);
		cmd.Parameters.AddWithValue ("@st", sumType);
		cmd.Parameters.AddWithValue ("@sl", sumLevel);

		SumInfo sumInfo = null;
		try
		{
			if (conn.State == ConnectionState.Closed)
			{
				conn.Open();
			}
			MySqlDataReader reader = cmd.ExecuteReader ();

			bool commaOptions = false;
			int sumID = -1;
			while (reader.Read())
			{
				sumID = reader.GetInt32("sumID");
				int minRange = reader.GetInt32("minRange");
				int maxRange = reader.GetInt32("maxRange");
				int sumCommas = reader.GetInt32("sumCommas");
				commaOptions = reader.GetBoolean("commaOptions");
				sumInfo = new SumInfo(minRange, maxRange, sumCommas);
			}
			
			reader.Close();

			if(commaOptions) {
				this.AddCommaOptions(sumID, sumInfo);
			}
		}
		catch
		{
		}
		
		conn.Close ();
		
		return sumInfo;
	}
}

public class SumInfo {
	public int minRange { get; private set; }
	public int maxRange { get; private set; }
	public int sumCommas { get; private set; }
	public List<int> commaOptions { get; private set; }

	public SumInfo(int minRange, int maxRange, int sumCommas) {
		this.minRange = minRange;
		this.maxRange = maxRange;
		this.sumCommas = sumCommas;
		this.commaOptions = new List<int>();
	}

	public void AddCommaOption(int commaOption) {
		this.commaOptions.Add(commaOption);
	}
}
