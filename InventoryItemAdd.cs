using UnityEngine;
using System.Collections;

using System;
using System.Data;
using Mono.Data.Sqlite;
using System.Data.SqlClient;
//The above are required to allow the correct functioning of the connection

public class InventoryItemAdd : MonoBehaviour
{
	private string connectionString;//Private Method for getting the path to the database
	private SqliteConnection dbConnection; //Connection to the sqliteDatabase
	private SqliteCommand dbCmd;
	private SqliteDataReader reader;

	void Start()
	{
		connectionString = "URI=file:" + Application.dataPath + "/Database.sqlite";//Give the path to the database
		Debug.Log("Opening DB");//Shows that the database has connected to Unity
	}

	void Update() 
	{
		if (Input.GetKeyDown ("t")) //on key press
		{
			ItemAdd ();
		}
	}//The code above allows the user to test the adding on an entry to the database

	/// <summary>
	/// Class for adding an item to the inventory of the player
	/// </summary>
	private void ItemAdd()
	{
		using (IDbConnection dbConnection = new SqliteConnection(connectionString))
		{
			dbConnection.Open();//Opens the database connection
			
			using (IDbCommand dbCmd = dbConnection.CreateCommand())
			{
				string sqlQuery = "INSERT INTO Inventory (ItemId, ItemName) VALUES ('4', 'Special Sword')"; //Insert constructor data in to table
				
				dbCmd.CommandText = sqlQuery;
				
				using (IDataReader reader = dbCmd.ExecuteReader())
				{
					dbConnection.Close();//Closes the database connection
				}
			}
		}
	}

	void OnCollision(Collision c) //allows the player to collide with an object
	{
		ItemAdd ();	//calls the class to add the item to the players inventory
		Destroy (gameObject);//Removes the cube from the scene
	}	

//InScopeStudios, 2015. 3. Unity tutorial: High score with SQLite - The database. [Video online] 
//Available at:<https://www.youtube.com/watch?v=KbS58LosfBA> [Accessed 17 Novemeber 2015]

//InScopeStudios, 2015. 4. Unity tutorial: High score with SQLite - Reading data. [Video online] 
//Available at:<https://www.youtube.com/watch?v=wV-dKxJU-0Y> [Accessed 17 Novemeber 2015]
