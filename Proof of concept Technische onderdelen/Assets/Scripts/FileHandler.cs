using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class FileHandler : MonoBehaviour {


	private float blockCount;

	// Use this for initialization
	void Start () {
		blockCount = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float GetBlockCount(){
		return blockCount;
	}

	public void SetBlockCount(float count){
		this.blockCount = count;
	}


	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/testInfo.dat");
		PlayerData data = new PlayerData ();

		data.blockCount = blockCount;

		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load(){
		if(File.Exists(Application.persistentDataPath + "/testInfo.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/testInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close ();

			blockCount = data.blockCount;
		}
	}
}

[Serializable]
class PlayerData {
	public float blockCount;
}
