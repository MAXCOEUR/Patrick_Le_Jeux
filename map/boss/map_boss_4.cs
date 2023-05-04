using Godot;
using System;
using System.Collections.Generic;

public partial class Enigme
{
	public string question;
	public string reponse;
	public Enigme(string question,string reponse){
		this.question=question;
		this.reponse=reponse;
	}
}
public partial class map_boss_4 : Node2D
{
	List<Enigme> enigmes = new List<Enigme>();

	int numeroQestion;

	RichTextLabel message;
	LineEdit input;
	Button bt_valide;

	Patrick patrick;
	public override void _Ready()
	{
		patrick = GetNode<Patrick>("../Patrick");
		message = GetNode<RichTextLabel>("dialogue/message");
		input = GetNode<LineEdit>("reponse/input");
		bt_valide = GetNode<Button>("reponse/bt_valide");
		bt_valide.Connect("button_down", new Callable(this, "_OnBt_valide"));

		enigmes.Add(new Enigme("Il peut parfois servir au classement, ou glisse sur l'eau lentement. Il est parfois fait de ciment. Il s'obtient toujours en étudiant.","bac"));
		enigmes.Add(new Enigme("On la tourne pour avancer, mais quand on l’est, cela signifie «être branché». Qui est-elle ?","page"));
		enigmes.Add(new Enigme("Quand on y est, on ne sait où on est. Il n’est fait que d’eau, mais peut devenir un impénétrable rideau.","brouillard"));
		enigmes.Add(new Enigme("Il est le point de départ sans mener nulle part. Terreur de l’écolier, surtout s’il est pointé.","zéro"));
		enigmes.Add(new Enigme("Ce tableau n’est pas vraiment apprécié, pour vous nourrir vous la cassez, en travaillant vous la gagnez, sortant du four elle est dorée.","croûte"));
		enigmes.Add(new Enigme("S’il se montre en paillettes, ce n’est pas pour faire la fête. On l’apprécie pour nettoyer. On le passe pour enguirlander.","savon"));
		enigmes.Add(new Enigme("Si de bon, parfois, il est qualifié. À transporter, il peut aider. S'il possède une certaine beauté, à lui, on peut vous envoyer.","diable"));
		enigmes.Add(new Enigme("À l’origine de tout être vivant, celles du fort vous réservent bien des tournants.","cellules"));
		enigmes.Add(new Enigme("Éternelles au pluriel, éphémères au singulier, on aime la voir à Noël, mais pas plus tard qu'en février.","neige"));
		enigmes.Add(new Enigme("Il a une arête, et une aile de chaque côté. Comparé à une trompette, qui n'est pas forcément bouchée.","nez"));

		Random myObject = new Random();
	  	numeroQestion= myObject.Next(0, enigmes.Count);


		message.Text=enigmes[numeroQestion].question;


	}

	private void _OnBt_valide(){
		if(input.Text.Contains(enigmes[numeroQestion].reponse)){
			//dire a level qui win et finish
		}else{
			patrick.setEtat(0); //ded ici
		}
	}
	public override void _Process(double delta)
	{
	}
}
