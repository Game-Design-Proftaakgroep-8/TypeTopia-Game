using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DatabaseHandler {

	public static void Save(Database db){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/database.dat");
		
		bf.Serialize (file, db);
		file.Close ();
	}
	
	public static Database Load(){
		Database db = null;
		if(File.Exists(Application.persistentDataPath + "/database.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/database.dat", FileMode.Open);
			db = (Database)bf.Deserialize(file);
			file.Close ();
		}
		else {
			db = new Database();
			db.insertTestData();
		}
		return db;
	}
}
