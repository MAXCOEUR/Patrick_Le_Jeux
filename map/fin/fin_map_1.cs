using Godot;
using System;

public partial class fin_map_1 : fin_map
{
	override protected void callLevel1(){
		base.callLevel1();
		level_1.Finish_map_1();
	}
}
