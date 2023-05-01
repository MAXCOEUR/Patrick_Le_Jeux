using Godot;
using System;
using System.Data.SQLite;


abstract public partial class Collection : Node2D
{
	protected Area2D area;
	protected Database db = Database.Instance;
	protected idCollection id_collection = new idCollection();
	public override void _Ready()
	{
		area = GetNode<Area2D>("Area2D");
		area.Connect("area_entered", new Callable(this, "OnCollision"));
	}

	protected void OnCollision(Area2D otherArea)
	{
		var otherParent = otherArea.GetParent();
		if (otherParent.IsInGroup("player"))
		{
			this.QueueFree();
			setCollectionInDb();
		}
	}

	public void setCollectionInDb()
	{
		string insertQuery = "INSERT INTO Collection(numeroMap, numeroCollection) VALUES(@numeroMap, @numeroCollection)";
		SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, db.getConnection);

		// Spécification des valeurs des paramètres de la commande.
		insertCommand.Parameters.AddWithValue("@numeroCollection", id_collection.id);
		insertCommand.Parameters.AddWithValue("@numeroMap", id_collection.numeroMap);

		// Exécution de la commande.
		insertCommand.ExecuteNonQuery();
	}
}
