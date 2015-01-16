using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Database {
	
	List<Person> persons;
	List<Score> scores;
	List<SumInfo> sums;

	public Database()
	{
		persons = new List<Person>();
		scores = new List<Score>();
		sums = new List<SumInfo>();
	}

	public void insertTestData()
	{
		persons.Add (new Person ("mel", "ww"));
		persons.Add (new Person ("alex", "123"));
		persons.Add (new Person ("bart", "bb"));

		scores.Add (new Score("mel", 1100));
		scores.Add (new Score("alex", 1450));
		scores.Add (new Score("bart", 760));

		sums.Add (new SumInfo ("money", 0, 1, 10, 0));
		sums.Add (new SumInfo ("money", 1, 0, 5, 1));
		sums.Add (new SumInfo ("money", 2, 0, 6, 2));
		sums [sums.Count - 1].AddCommaOption (0.25);
		sums [sums.Count - 1].AddCommaOption (0.50);
		sums [sums.Count - 1].AddCommaOption (0.75);
		sums.Add (new SumInfo ("money", 3, 0, 6, 2));
		sums.Add (new SumInfo ("time", 0, 0, 23, 0));
		sums.Add (new SumInfo ("time", 1, 0, 23, 2));
		sums [sums.Count - 1].AddCommaOption (0.15);
		sums [sums.Count - 1].AddCommaOption (0.30);
		sums [sums.Count - 1].AddCommaOption (0.45);
		sums.Add (new SumInfo ("time", 2, 0, 23, 2));
		sums.Add (new SumInfo ("time", 3, 0, 23, 2));
		sums.Add (new SumInfo ("volume", 0, 1, 100, 0));
		sums.Add (new SumInfo ("volume", 1, 1, 999, 0));
		sums.Add (new SumInfo ("volume", 2, 0, 100, 1));
		sums.Add (new SumInfo ("volume", 3, 0, 100, 1));
	}
//
//	public void ConnectionBase(MySqlCommand command)
//	{
//		try
//		{
//			if (conn.State != ConnectionState.Open)
//			{
//				conn.Open();
//			}
//			command.ExecuteNonQuery();
//		}
//		catch
//		{
//			if (conn.State == ConnectionState.Open)
//			{
//				conn.Close();
//			}
//		}
//		finally
//		{
//			if (conn.State == ConnectionState.Open)
//			{
//				conn.Close();
//			}
//		}
//	}
//
	public bool CheckLogin(string name, string password)
	{
//		string SQL = "SELECT * FROM Person WHERE pName=@nm AND pPassword=@pw";
//		MySqlCommand cmd = new MySqlCommand (SQL, conn);
//		cmd.Parameters.AddWithValue ("@nm", name);
//		cmd.Parameters.AddWithValue ("@pw", password);
//
//		bool exist = false;
//
//		try
//		{
//			if (conn.State == ConnectionState.Closed)
//			{
//				conn.Open();
//			}
//
//			MySqlDataReader reader = cmd.ExecuteReader ();
//
//			while (reader.Read())
//			{
//				exist = true;
//			}
//
//			reader.Close();
//		}
//		catch
//		{
//		}
//
//		conn.Close ();
//
//		return exist;
		return false;
	}
//
	public bool CheckUsernameExists(string name)
	{
//		string SQL = "SELECT * FROM Person WHERE pName=@nm";
//		MySqlCommand cmd = new MySqlCommand (SQL, conn);
//		cmd.Parameters.AddWithValue ("@nm", name);
//		
//		bool exist = false;
//		
//		try
//		{
//			if (conn.State == ConnectionState.Closed)
//			{
//				conn.Open();
//			}
//			
//			MySqlDataReader reader = cmd.ExecuteReader ();
//			
//			while (reader.Read())
//			{
//				exist = true;
//			}
//			
//			reader.Close();
//		}
//		catch
//		{
//		}
//		
//		conn.Close ();
//		
//		return exist;
		return false;
	}
//
	public void SignIn(string name, string password)
	{
//		string SQL = "INSERT INTO Person pName=@nm AND pPassword=@pw";
//		MySqlCommand cmd = new MySqlCommand (SQL, conn);
//		cmd.Parameters.AddWithValue ("@nm", name);
//		cmd.Parameters.AddWithValue ("@pw", password);
//		
//		try
//		{
//			if (conn.State == ConnectionState.Closed)
//			{
//				conn.Open();
//			}
//			
//			cmd.ExecuteNonQuery();
//		}
//		catch
//		{
//		}
//		
//		conn.Close ();
	}
//
	public string[] GetHighscore()
	{
//		string SQL = "SELECT * FROM Score ORDER BY score DESC";
//		MySqlCommand cmd = new MySqlCommand (SQL, conn);
//		
//		string[] scores = new string[5];
//		int count = 0;
//		
//		try
//		{
//			if (conn.State == ConnectionState.Closed)
//			{
//				conn.Open();
//			}
//			
//			MySqlDataReader reader = cmd.ExecuteReader ();
//			
//			while (reader.Read() && count < 5)
//			{
//				string name = reader.GetString("pName");
//				int score = reader.GetInt32("score");
//
//				string total = name + " - " + score;
//				scores[count] = total;
//
//				count++;
//			}
//			
//			reader.Close();
//		}
//		catch
//		{
//		}
//		
//		conn.Close ();
//		
//		return scores;
		return null;
	}
//
	public void AddScore(string name, int score)
	{
//		string SQL = "INSERT INTO Score pName=@nm AND score=@sc";
//		MySqlCommand cmd = new MySqlCommand (SQL, conn);
//		cmd.Parameters.AddWithValue ("@nm", name);
//		cmd.Parameters.AddWithValue ("@sc", score);
//		
//		try
//		{
//			if (conn.State == ConnectionState.Closed)
//			{
//				conn.Open();
//			}
//			
//			cmd.ExecuteNonQuery();
//		}
//		catch
//		{
//		}
//		
//		conn.Close ();
	}
//
	private void AddCommaOptions(int sumID, SumInfo sumInfo) {
//		string SQL = "SELECT * FROM CommaOption WHERE commaID IN (SELET commaID FROM SumComma WHERE sumID = @si)";
//		MySqlCommand cmd = new MySqlCommand (SQL, conn);
//		cmd.Parameters.AddWithValue ("@si", sumID);
//		
//		try
//		{
//			if (conn.State == ConnectionState.Closed)
//			{
//				conn.Open();
//			}
//			
//			MySqlDataReader reader = cmd.ExecuteReader ();
//			
//			while (reader.Read())
//			{
//				int commaOption = reader.GetInt32("commaValue");
//				sumInfo.AddCommaOption(commaOption);
//			}
//			
//			reader.Close();
//		}
//		catch
//		{
//		}
//		
//		conn.Close ();
	}
//
	public SumInfo GetSumInfo(string sumType, int sumLevel)
	{
//		string SQL = "SELECT * FROM Sums WHERE sumType=@st AND sumLevel=@sl";
//		MySqlCommand cmd = new MySqlCommand (SQL, conn);
//		cmd.Parameters.AddWithValue ("@st", sumType);
//		cmd.Parameters.AddWithValue ("@sl", sumLevel);
//
//		SumInfo sumInfo = null;
//		try
//		{
//			if (conn.State == ConnectionState.Closed)
//			{
//				conn.Open();
//			}
//			MySqlDataReader reader = cmd.ExecuteReader ();
//
//			bool commaOptions = false;
//			int sumID = -1;
//			while (reader.Read())
//			{
//				sumID = reader.GetInt32("sumID");
//				int minRange = reader.GetInt32("minRange");
//				int maxRange = reader.GetInt32("maxRange");
//				int sumCommas = reader.GetInt32("sumCommas");
//				commaOptions = reader.GetBoolean("commaOptions");
//				sumInfo = new SumInfo(minRange, maxRange, sumCommas);
//			}
//			
//			reader.Close();
//
//			if(commaOptions) {
//				this.AddCommaOptions(sumID, sumInfo);
//			}
//		}
//		catch
//		{
//		}
//		
//		conn.Close ();
//		
//		return sumInfo;
		return null;
	}
}

//using System;
//using System.Collections.Generic;
//using System.Data.OleDb;
//using AniMaze.Classes;
//using MySql.Data.MySqlClient;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Audio;
//
//namespace AniMaze
//{
//	public class DatabaseConnector
//	{
//		private OleDbConnection AccessConnection;
//		private MySqlConnection mySqlConnection;
//		
//		public DatabaseConnector()
//		{
//			String Pad;
//			String Provider;
//			String ApplicatiePad;
//			String AccessConnectionString;
//			
//			// ACCES datadase
//			
//			Provider = "Provider=Microsoft.ACE.OLEDB.12.0";
//			
//			ApplicatiePad = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\"));
//			Pad = "Data Source=" + ApplicatiePad + "/AniMazeDBS.accdb";
//			
//			AccessConnectionString = Provider + ";" + Pad;
//			
//			AccessConnection = new OleDbConnection(AccessConnectionString);
//			
//			Open();
//			
//			// MySql database
//			#region MySql Database
//			
//			String Server;
//			String Database;
//			String MySqlConnectionString;
//			String TimeOut;
//			
//			Server = "server=athena01.fhict.local";
//			Database = "database=dbi280401";
//			TimeOut = "connect timeout=30";
//			
//			#region 
//			String Inlognaam = "uid=dbi280401";
//			String Password = "password=Ry9hQ4RGaw";
//			#endregion
//			
//			MySqlConnectionString = Server + ";" + Database + ";" + Inlognaam + ";" + Password + ";" + TimeOut + ";";
//			
//			mySqlConnection = new MySqlConnection(MySqlConnectionString);
//			#endregion
//		}
//		
//		public List<User> GetAllUsers()
//		{
//			string Sql = "SELECT * FROM Player";
//			OleDbCommand Command = new OleDbCommand(Sql, AccessConnection);
//			
//			List<User> AllUsers = new List<User>();
//			
//			try
//			{
//				OleDbDataReader Reader = Command.ExecuteReader();
//				
//				while (Reader.Read())
//				{
//					int playerId = Convert.ToInt32(Reader["PlayerId"]);
//					string userName = Convert.ToString(Reader["UserName"]);
//					string password = Convert.ToString(Reader["PasswordField"]);
//					string gender = Convert.ToString(Reader["Gender"]);
//					
//					Texture2D PlayerImage = null;
//					
//					if (gender == "M") { PlayerImage = Game1.PlayerImages[0]; }
//					else { PlayerImage = Game1.PlayerImages[1]; }
//					
//					AllUsers.Add(new User(playerId, userName, password, gender, PlayerImage));
//				}
//			}
//			catch (Exception exception)
//			{
//				Console.WriteLine("Could not execute reader: " + exception.Message);
//			}
//			return AllUsers;
//		}
//		
//		/// <summary>
//		/// Get the corresponding image of the animal
//		/// </summary>
//		/// <param name="imageString"></param>
//		/// <returns></returns>
//		private Texture2D GetAnimalImage(string imageString)
//		{
//			int Index = Game1.AnimalImageStrings.IndexOf(imageString);
//			return Game1.AnimalImages[Index];
//		}
//		
//		/// <summary>
//		/// Get the corresponding sound of the animal
//		/// </summary>
//		/// <param name="soundString"></param>
//		/// <returns></returns>
//		private SoundEffect GetAnimalSound(string soundString)
//		{
//			int Index = Game1.AnimalSoundStrings.IndexOf(soundString);
//			return Game1.AnimalSounds[Index];
//		}
//		
//		/// <summary>
//		/// Execute the reader for Animals
//		/// </summary>
//		/// <param name="Command"></param>
//		/// <returns></returns>
//		private List<Animal> ExecuteAnimalReader(OleDbCommand Command)
//		{
//			List<Animal> Animals = new List<Animal>();
//			
//			try
//			{
//				OleDbDataReader Reader = Command.ExecuteReader();
//				
//				while (Reader.Read())
//				{
//					int animalNr = Convert.ToInt32(Reader["AnimalNr"]);
//					string animalName = Convert.ToString(Reader["AnimalName"]);
//					string imageString = Convert.ToString(Reader["Image"]);
//					string soundString = Convert.ToString(Reader["Sound"]);
//					
//					Texture2D Image = GetAnimalImage(imageString);
//					SoundEffect Sound = GetAnimalSound(soundString);
//					
//					Animals.Add(new Animal(animalNr, animalName, Image, Sound));
//				}
//			}
//			catch (Exception exception)
//			{
//				Console.WriteLine("Could not execute animalreader: " + exception.Message);
//			}
//			return Animals;
//		}
//		
//		/// <summary>
//		/// Get the animals from the given category
//		/// </summary>
//		/// <param name="catId"></param>
//		/// <returns></returns>
//		public List<Animal> GetAnimals(string catId)
//		{
//			string Sql = "SELECT * FROM Animal WHERE AnimalNr IN (SELECT AnimalNr FROM CategoryRow WHERE CatId = '" + catId + "')";
//			OleDbCommand Command = new OleDbCommand(Sql, AccessConnection);
//			
//			return ExecuteAnimalReader(Command);
//		}
//		
//		/// <summary>
//		/// Get the collected animals from the current player for the inventory
//		/// </summary>
//		/// <param name="currentPlayerId"></param>
//		/// <returns></returns>
//		public List<Animal> GetCollectedAnimals(int currentPlayerId)
//		{
//			string Sql = "SELECT * FROM Animal WHERE AnimalNr IN (SELECT AnimalNr FROM Inventory WHERE PlayerId = " + currentPlayerId + ")";
//			OleDbCommand Command = new OleDbCommand(Sql, AccessConnection);
//			
//			return ExecuteAnimalReader(Command);
//		}
//		
//		/// <summary>
//		/// Get the local topscores
//		/// </summary>
//		/// <param name="NumberOfScores">The number of highscores you want to receive</param>
//		/// <returns>A list of scorestrings (Score - UserName (Gender))</returns>
//		public List<string> GetLocalHighscores(int NumberOfScores)
//		{
//			string Sql = "SELECT TOP " + NumberOfScores + " Score, UserName FROM Game G, Player P WHERE G.PlayerId = P.PlayerId ORDER BY Score DESC";
//			OleDbCommand Command = new OleDbCommand(Sql, AccessConnection);
//			
//			List<string> Highscores = new List<string>();
//			
//			try
//			{
//				OleDbDataReader Reader = Command.ExecuteReader();
//				
//				while (Reader.Read())
//				{
//					int score = Convert.ToInt32(Reader["Score"]);
//					string userName = Convert.ToString(Reader["UserName"]);
//					
//					Highscores.Add(String.Format("{0} - {1}", score, userName));
//				}
//			}
//			catch (Exception exception)
//			{
//				Console.WriteLine("Could not execute reader: " + exception.Message);
//			}
//			return Highscores;
//		}
//		
//		/// <summary>
//		/// Get the global highscores
//		/// </summary>
//		/// <param name="NumberOfScores"></param>
//		/// <returns></returns>
//		public List<string> GetGlobalHighscores(int NumberOfScores)
//		{
//			string Sql = "SELECT Score, UserName FROM game ORDER BY Score DESC LIMIT " + NumberOfScores;
//			MySqlCommand Command = new MySqlCommand(Sql, mySqlConnection);
//			
//			List<string> Highscores = new List<string>();
//			
//			try
//			{
//				mySqlConnection.Open();
//				MySqlDataReader Reader = Command.ExecuteReader();
//				
//				while (Reader.Read())
//				{
//					int score = Convert.ToInt32(Reader["Score"]);
//					string userName = Convert.ToString(Reader["UserName"]);
//					
//					Highscores.Add(String.Format("{0} - {1}", score, userName));
//				}
//			}
//			catch (Exception exception)
//			{
//				Console.WriteLine("Could not execute mysql reader: " + exception.Message);
//			}
//			finally
//			{
//				mySqlConnection.Close();
//			}
//			return Highscores;
//		}
//		
//		/// <summary>
//		/// Get de mazepath from the given category.
//		/// </summary>
//		/// <param name="catId"></param>
//		/// <returns></returns>
//		public Texture2D GetMazePath(string catId)
//		{
//			string Sql = "SELECT CatImage FROM Category WHERE CatId = '" + catId + "'";
//			OleDbCommand Command = new OleDbCommand(Sql, AccessConnection);
//			
//			string MazePathString = "";
//			Texture2D MazePath = null;
//			
//			try
//			{
//				OleDbDataReader Reader = Command.ExecuteReader();
//				
//				while (Reader.Read())
//				{
//					MazePathString = Convert.ToString(Reader["CatImage"]);
//					int Index = Game1.LevelImageStrings.IndexOf(MazePathString);
//					MazePath = Game1.LevelImages[Index];
//				}
//			}
//			catch (Exception exception)
//			{
//				Console.WriteLine("Could not execute reader: " + exception.Message);
//			}
//			
//			return MazePath;
//		}
//		
//		/// <summary>
//		/// Get de categoryName from the given category.
//		/// </summary>
//		/// <param name="catId"></param>
//		/// <returns></returns>
//		private string GetCatName(string catId)
//		{
//			string Sql = "SELECT CatName FROM Category WHERE CatId = '" + catId + "'";
//			OleDbCommand Command = new OleDbCommand(Sql, AccessConnection);
//			
//			string CatName = "";
//			
//			try
//			{
//				OleDbDataReader Reader = Command.ExecuteReader();
//				
//				while (Reader.Read()) { CatName = Convert.ToString(Reader["CatName"]); }
//			}
//			catch (Exception exception)
//			{
//				Console.WriteLine("Could not execute reader: " + exception.Message);
//			}
//			
//			return CatName;
//		}
//		
//		public void AddUser(string userName, string password, string gender)
//		{
//			string Sql = "INSERT INTO Player (UserName, PasswordField, Gender) VALUES ('" + userName + "', '" + password + "', '" + gender + "')";
//			OleDbCommand Command = new OleDbCommand(Sql, AccessConnection);
//			
//			ExecuteNonQuery(Command);
//		}
//		
//		public void AddGameInfo(int levelNr, User user, int score, double time, int repeatSound, string catId)
//		{
//			//2014-01-11
//			#region Make current date string
//			string DateYear = DateTime.Now.Year.ToString();
//			string DateMonth = DateTime.Now.Month.ToString();
//			string DateDay = DateTime.Now.Day.ToString();
//			string Date = String.Format("{0}-{1}-{2}", DateYear, DateMonth, DateDay);
//			#endregion
//			
//			string Sql = "INSERT INTO Game (LevelNr, PlayerId, Score, PlayTime, RepeatSound, PlayDate, CatId) VALUES (" + levelNr + ", " + user.PlayerId + ", " + score + ", " + (int)time + ", " + repeatSound + ", #" + Date + "#, '" + catId + "')";
//			OleDbCommand Command = new OleDbCommand(Sql, AccessConnection);
//			
//			ExecuteNonQuery(Command);
//			
//			#region MySql
//			
//			string catName = GetCatName(catId);
//			if (catName == "") { return; }
//			
//			string mySql = "INSERT INTO game (LevelNr, UserName, Score, PlayTime, RepeatSound, PlayDate, Category) VALUES (" + levelNr + ", '" + user.UserName + "', " + score + ", " + (int)time + ", " + repeatSound + ", '" + Date + "', '" + catName + "')";
//			MySqlCommand MyCommand = new MySqlCommand(mySql, mySqlConnection);
//			
//			try
//			{
//				mySqlConnection.Open();
//				MyCommand.ExecuteNonQuery();
//			}
//			catch (Exception exception)
//			{
//				Console.WriteLine("Could not write to mysql database: " + exception.Message);
//			}
//			finally
//			{
//				mySqlConnection.Close();
//			}
//			
//			#endregion
//		}
//		
//		/// <summary>
//		/// Add the given collected animal to the inventory from the current player
//		/// </summary>
//		/// <param name="CollectedAnimal"></param>
//		public void UpdateInventory(Animal collectedAnimal, int currentPlayerId)
//		{
//			string Sql = "INSERT INTO Inventory (AnimalNr, PlayerId) VALUES (" + collectedAnimal.Nr + ", " + currentPlayerId + ")";
//			OleDbCommand Command = new OleDbCommand(Sql, AccessConnection);
//			
//			ExecuteNonQuery(Command);
//		}
//		
//		private void ExecuteNonQuery(OleDbCommand Command)
//		{
//			try
//			{
//				Command.ExecuteNonQuery();
//			}
//			catch(Exception exception)
//			{
//				Console.WriteLine("Could not execute non query: " + exception.Message);
//			}
//		}
//		
//		private void Open()
//		{
//			try
//			{
//				AccessConnection.Open();
//			}
//			catch (Exception exception)
//			{
//				Console.WriteLine("Could not connect to database: " + exception.Message);
//			}
//		}
//		
//		public void Close()
//		{
//			try
//			{
//				AccessConnection.Close();
//			}
//			catch(Exception exception)
//			{
//				Console.WriteLine("Could not close database: " + exception.Message);
//			}
//		}
//	}
//}



