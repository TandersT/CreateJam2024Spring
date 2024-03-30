using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class SpawnablePositions : Node2D
{

	public override void _EnterTree()
	{
		Global.SpawnablePositions = GetChildren().Cast<Marker2D>().Select(x => x.Position).ToList();
	}
}
