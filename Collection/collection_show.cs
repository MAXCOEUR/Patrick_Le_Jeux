using Godot;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

public partial class collection_show : Node2D
{
	protected Database db = Database.Instance;
	List<idCollection> Collections;
	public override void _Ready()
	{
		GetNode<Button>("bt_retour").Connect("button_down", new Callable(this, "_OnBt_retour"));
		Collections=getCollections();
		foreach (idCollection Collection in Collections)
		{
			
			Collection col;
			PackedScene SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/pouet.tscn");
			col = (Collection)SceneCollection.Instantiate();
			if(Collection.numeroMap==1){
				if(Collection.id==1){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/pouet.tscn");
					col = (Collection)SceneCollection.Instantiate();
				}
				if(Collection.id==2){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/kalash.tscn");
					col = (Collection)SceneCollection.Instantiate();
				}
				if(Collection.id==3){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/pot_de_fleur.tscn");
					col = (Collection)SceneCollection.Instantiate();
				}				
			}
			else if(Collection.numeroMap==2){
				if(Collection.id==1){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/renard.tscn");
					col = (Collection)SceneCollection.Instantiate();
				}
				if(Collection.id==2){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/gateau.tscn");
					col = (Collection)SceneCollection.Instantiate();
				}
				if(Collection.id==3){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/chateaux.tscn");
					col = (Collection)SceneCollection.Instantiate();
				}	
			}
			else if(Collection.numeroMap==3){
				if(Collection.id==1){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/baignoire.tscn");
					col = (Collection)SceneCollection.Instantiate();
				}
				if(Collection.id==2){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/gros_vaisseau.tscn");
					col = (Collection)SceneCollection.Instantiate();
				}
				if(Collection.id==3){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/sombrero.tscn");
					col = (Collection)SceneCollection.Instantiate();
				}	
			}
			else if(Collection.numeroMap==4){
				if(Collection.id==1){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/cle.tscn");
					col = (Collection)SceneCollection.Instantiate();
				}
				if(Collection.id==2){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/la_canne_de_brigitte.tscn");
					col = (Collection)SceneCollection.Instantiate();
				}
				if(Collection.id==3){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/saussice.tscn");
					col = (Collection)SceneCollection.Instantiate();
				}	
			}
			else if(Collection.numeroMap==5){
				if(Collection.id==1){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/mayo.tscn");
					col = (Collection)SceneCollection.Instantiate();
				}
				if(Collection.id==2){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/tnt.tscn");
					col = (Collection)SceneCollection.Instantiate();
				}
				if(Collection.id==3){
					SceneCollection = (PackedScene)ResourceLoader.Load("res://Collection/pistolet.tscn");
					col = (Collection)SceneCollection.Instantiate();
				}	
			}
			
			col.Scale = new Vector2(1,1);
			col.Position = new Vector2(480+(Collection.id-1)*145,110+(Collection.numeroMap-1)*130);
			AddChild(col);
		}
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
