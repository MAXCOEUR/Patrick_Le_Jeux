using Godot;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

public partial class collection_show : Control
{
	protected Database db = Database.Instance;
	List<idCollection> Collections;
	private AudioStreamPlayer musique;
	AudioStream audioStream;

	public override void _Ready()
	{
		musique = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		musique.Connect("finished", new Callable(this, "OnMusiqueFinish"));
		var musiqueFilePath = "res://art/musique/collection/1.mp3"; // chemin de la vidéo
		audioStream = (AudioStream)ResourceLoader.Load(musiqueFilePath);
		musique.Stream = audioStream;
		musique.Play();


		GetNode<Button>("bt_retour").Connect("button_down", new Callable(this, "_OnBt_retour"));
		Collections=getCollections();
		foreach (idCollection Collection in Collections)
		{
			
			Collection col;
			PackedScene SceneCollection = null;
			if(Collection.numeroMap==1){
				if(Collection.id==1){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/pouet.tscn");
				}
				if(Collection.id==2){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/kalash.tscn");
				}
				if(Collection.id==3){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/pot_de_fleur.tscn");
				}				
			}
			else if(Collection.numeroMap==2){
				if(Collection.id==1){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/renard.tscn");
				}
				if(Collection.id==2){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/gateau.tscn");
				}
				if(Collection.id==3){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/chateaux.tscn");
				}	
			}
			else if(Collection.numeroMap==3){
				if(Collection.id==1){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/baignoire.tscn");
				}
				if(Collection.id==2){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/gros_vaisseau.tscn");
				}
				if(Collection.id==3){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/sombrero.tscn");
				}	
			}
			else if(Collection.numeroMap==4){
				if(Collection.id==1){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/cle.tscn");
				}
				if(Collection.id==2){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/la_canne_de_brigitte.tscn");
				}
				if(Collection.id==3){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/saussice.tscn");
				}	
			}
			else if(Collection.numeroMap==5){
				if(Collection.id==1){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/mayo.tscn");
				}
				if(Collection.id==2){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/tnt.tscn");
				}
				if(Collection.id==3){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/pistolet.tscn");
				}	
			}
			if(SceneCollection!=null){
				col = (Collection)SceneCollection.Instantiate();
				col.Scale = new Vector2(1,1);
				col.Position = new Vector2(480+(Collection.id-1)*145,110+(Collection.numeroMap-1)*130);
				AddChild(col);
			}
			
		}
	}

	private void OnMusiqueFinish(){
		musique.Stream = audioStream;
		musique.Play();
	}

	private void _OnBt_retour(){
		GetTree().ChangeSceneToFile("res://interface/StartMenu.tscn");
	}

	private List<idCollection> getCollections(){
		List<idCollection> Collections = new List<idCollection>();
		SQLiteCommand command = new SQLiteCommand("SELECT numeroMap, numeroCollection FROM Collection", db.getConnection);
		SQLiteDataReader reader = command.ExecuteReader();
		while (reader.Read())
		{
			idCollection id_collection = new idCollection();
			id_collection.numeroMap = reader.GetInt32(0);
			id_collection.id=reader.GetInt32(1);
			Collections.Add(id_collection);
		}
		return Collections;
	}
	
	public override void _Process(double delta)
	{
	}
}
