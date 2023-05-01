using System.Data.SQLite;

public class Database
{
	private static Database instance = null;
	private SQLiteConnection connection;

	private Database()
	{
		// Le chemin de la base de données SQLite.
		string dbPath = "database.db";

		// Si le fichier de base de données n'existe pas, on le crée.
		if (!System.IO.File.Exists(dbPath))
		{
			SQLiteConnection.CreateFile(dbPath);
		}

		// Connexion à la base de données.
		connection = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;");
		connection.Open();

		// Création de la table Collection si elle n'existe pas.
		string createTableQuery = "CREATE TABLE IF NOT EXISTS Collection (id INTEGER PRIMARY KEY AUTOINCREMENT,numeroMap INTEGER NOT NULL,numeroCollection INTEGER NOT NULL,UNIQUE(numeroMap, numeroCollection))";
		SQLiteCommand createTableCommand = new SQLiteCommand(createTableQuery, connection);
		createTableCommand.ExecuteNonQuery();
	}

	public static Database Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new Database();
			}
			return instance;
		}
	}

	public SQLiteConnection getConnection
	{
		get { return connection; }
	}
}
