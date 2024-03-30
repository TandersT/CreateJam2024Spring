using Godot;
using System;
using System.Collections.Generic;

public class PeasentRole : SharedRole
{
	public override List<string> HasInteracted { get; set; } = new List<string>
	{
		"Frick off",
		"u are dumb",
		"no talk more",
		"get reckt",
		"u dumb",
	};
	public override List<string> Greetings { get; set; } = new List<string>
	{
		"I am greeting1",
		"I am greeting2",
		"I am greeting3",
		"I am greeting4",
		"I am greeting5",
	};
	public override List<string> Novice { get; set; } = new();
	public override List<string> Intermediate { get; set; } = new();
	public override List<string> Expert { get; set; } = new();
	public override List<string> Role { get; set; } = new();

}
