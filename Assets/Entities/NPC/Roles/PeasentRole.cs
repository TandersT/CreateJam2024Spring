using Godot;
using System;
using System.Collections.Generic;

public class PeasentRole : SharedRole
{
	public override List<string> Novice { get; set; } = new();
	public override List<string> Intermediate { get; set; } = new();
	public override List<string> Expert { get; set; } = new();
	public override List<string> Role { get; set; } = new();

}
