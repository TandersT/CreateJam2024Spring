using Godot;
using System;

public partial class NpcContainer : Node2D
{
	public override void _EnterTree()
	{
		Global.NpcContatainer = this;
	}
}
