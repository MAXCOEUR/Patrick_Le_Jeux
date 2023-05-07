using Godot;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

public partial class Enigme
{
	public string question;
	public string reponse;
	public Enigme(string question, string reponse)
	{
		this.question = question;
		this.reponse = reponse;
	}
}
public partial class map_boss_4 : Node2D
{
	List<Enigme> enigmes = new List<Enigme>();
	protected Database db = Database.Instance;

	int numeroQestion;

	RichTextLabel message;
	LineEdit input;
	Button bt_valide;
	NinePatchRect reponseBlock;
	ProgressBar progressBar;
	Timer timerSeconde;
	Timer finish;

	level_1Script level_1;

	bool timeQuestion = false;
	const int MaxTimeQuestion = 45;
	const int MaxTimeBefor = 5;
	int currentQestionTime = MaxTimeQuestion;
	int currentBefor = MaxTimeBefor;

	Patrick patrick;
	private AudioStreamPlayer musique;
	AudioStream audioStream;
	public override void _Ready()
	{
		timerSeconde = new Timer();
		timerSeconde.WaitTime = 1;
		timerSeconde.Connect("timeout", new Callable(this, "oneSeconde"));
		AddChild(timerSeconde);
		timerSeconde.Start();

		finish = new Timer();
		finish.WaitTime = 3;
		finish.OneShot = true;
		finish.Connect("timeout", new Callable(this, "Finish"));
		AddChild(finish);


		patrick = GetNode<Patrick>("../Patrick");
		message = GetNode<RichTextLabel>("dialogue/message");
		input = GetNode<LineEdit>("reponse/input");
		bt_valide = GetNode<Button>("reponse/bt_valide");
		reponseBlock = GetNode<NinePatchRect>("reponse");
		progressBar = GetNode<ProgressBar>("ProgressBar");
		level_1 = GetNode<level_1Script>("../../level_1");
		bt_valide.Connect("button_down", new Callable(this, "_OnBt_valide"));

		enigmes.Add(new Enigme("Il peut parfois servir au classement, ou glisse sur l'eau lentement. Il est parfois fait de ciment. Il s'obtient toujours en étudiant.", "bac"));
		enigmes.Add(new Enigme("On la tourne pour avancer, mais quand on l’est, cela signifie «être branché». Qui est-elle ?", "page"));
		enigmes.Add(new Enigme("Quand on y est, on ne sait où on est. Il n’est fait que d’eau, mais peut devenir un impénétrable rideau.", "brouillard"));
		enigmes.Add(new Enigme("Il est le point de départ sans mener nulle part. Terreur de l’écolier, surtout s’il est pointé.", "zéro"));
		enigmes.Add(new Enigme("Ce tableau n’est pas vraiment apprécié, pour vous nourrir vous la cassez, en travaillant vous la gagnez, sortant du four elle est dorée.", "croûte"));
		enigmes.Add(new Enigme("S’il se montre en paillettes, ce n’est pas pour faire la fête. On l’apprécie pour nettoyer. On le passe pour enguirlander.", "savon"));
		enigmes.Add(new Enigme("Si de bon, parfois, il est qualifié. À transporter, il peut aider. S'il possède une certaine beauté, à lui, on peut vous envoyer.", "diable"));
		enigmes.Add(new Enigme("À l’origine de tout être vivant, celles du fort vous réservent bien des tournants.", "cellule"));
		enigmes.Add(new Enigme("Éternelles au pluriel, éphémères au singulier, on aime la voir à Noël, mais pas plus tard qu'en février.", "neige"));
		enigmes.Add(new Enigme("Il a une arête, et une aile de chaque côté. Comparé à une trompette, qui n'est pas forcément bouchée.", "nez"));

		enigmes.Add(new Enigme("À votre seuil elle est souvent. Son coup fatigue promptement, son code est assez répandu, nombreux y sont venus.", "barre"));
		enigmes.Add(new Enigme("Petite plante violacée, elle nous obsède sans arrêt. Donne des réponses ou des idées, sans elle, pas d'indice gagné ! Qui est-elle ?", "pensée"));
		enigmes.Add(new Enigme("C'est le refuge des pensifs, des plus ambitieux au monde, l'objectif ! Dans ses mers il n'y a point d'eau, pour la voir, on regarde en haut. Qui est-elle ?", "lune"));
		enigmes.Add(new Enigme("Qu'il soit quart ou bien demi, c'est un silence qu'il traduit. Tant qu'il n'est pas le dernier, on peut toujours le pousser. Qui est-il ?", "soupir"));
		enigmes.Add(new Enigme("Comme les rides sur l'eau, d'un choc, elles descendent à zéro. Du sol à la radio, elles suivent le micro. Qui sont-elles ?", "ondes"));
		enigmes.Add(new Enigme("Malgré son caractère, elle protège ses congénères, elle assure la passagère, et contrôle aussi les frontières. Qui est-elle ?", "police"));
		enigmes.Add(new Enigme("Sans elle point d'impression, elle nourrit les canons. Vous ne devez pas brûler, la dernière pour gagner. Qui est-elle ?", "cartouche"));
		enigmes.Add(new Enigme("Séance pour délibérer, bien le suivre est avisé. C'est un lieu d'affrontements où l'on joue pour du temps. Qui est-il ?", "conseil"));
		enigmes.Add(new Enigme("Cette donnée que l’on voit changer, Tout au long de l’année, Peut être fixée pour se rencontrer.", "date"));
		enigmes.Add(new Enigme("Ce signe de caractère, peut être noir ou jaune clair. Il habite les déserts, et il se faufile entre les pierres. Qui est-il ?", "scorpion"));

		Random myObject = new Random();
		numeroQestion = myObject.Next(0, enigmes.Count);


		message.Text = "Cependant, Avant de pouvoir toucher l'arbre, il faut que tu répondes à cette énigme :";

		musique = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		musique.Connect("finished", new Callable(this, "OnMusiqueFinish"));
		var musiqueFilePath = "res://art/musique/map_4_boss/boss4.mp3"; // chemin de la vidéo
		audioStream = (AudioStream)ResourceLoader.Load(musiqueFilePath);
		musique.Stream = audioStream;
		musique.Play();


	}
	private void OnMusiqueFinish(){
		musique.Stream = audioStream;
		musique.Play();
	}

	void oneSeconde()
	{
		if (timeQuestion)
		{
			currentQestionTime--;
			if (currentQestionTime == 0)
			{
				ded();
			}
			progressBar.Value = currentQestionTime;
		}
		else
		{
			currentBefor--;
			if (currentBefor == 0)
			{
				timeQuestion = true;
				askQuestion();
			}
			progressBar.Value = currentBefor;
		}
	}

	void askQuestion()
	{
		progressBar.MaxValue = MaxTimeQuestion;
		progressBar.Value = MaxTimeQuestion;
		message.Text = enigmes[numeroQestion].question;
		reponseBlock.Visible = true;
	}

	private void _OnBt_valide()
	{
		reponseBlock.Visible = false;
		if (input.Text.ToUpper().Contains(enigmes[numeroQestion].reponse.ToUpper()))
		{
			victory();
		}
		else
		{
			ded();
		}
	}

	void ded()
	{
		reponseBlock.Visible = false;
		message.Text = "Vous avez mal répondu, vous êtes mort !!!";
		patrick.setEtat(0);
	}
	void victory()
	{
		timerSeconde.Stop();
		string insertQuery = "INSERT INTO Collection(numeroMap, numeroCollection) VALUES(@numeroMap, @numeroCollection)";
		SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, db.getConnection);

		insertCommand.Parameters.AddWithValue("@numeroCollection", 1);
		insertCommand.Parameters.AddWithValue("@numeroMap", 4);//ajout de pouet a la collection

		insertCommand.ExecuteNonQuery();
		message.Text = "Bien joué, vous avez bien répondu ! En guise de récompense, vous avez le droit de toucher l'arbre.";
		finish.Start();
	}

	void Finish()
	{
		level_1.Finish_map_Boss_4();
	}
	public override void _Process(double delta)
	{
	}
}
