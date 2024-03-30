using Godot;
using System;
using System.Collections.Generic;

public partial class PeasentRole : Node, IProficienct
{
	public List<string> HasInteracted { get; set; } = new List<string>
	{
		"Frick off",
		"u are dumb",
		"no talk more",
		"get reckt",
		"u dumb",
	};
	public List<string> Greetings { get; set; } = new List<string>
	{
		"I am greeting1",
		"I am greeting2",
		"I am greeting3",
		"I am greeting4",
		"I am greeting5",
	};
	public List<string> Novice { get; set; } = new List<string>
	{
		"I am peasent1",
		"I am peasent2",
		"I am peasent3",
		"I am peasent4",
		"I am peasent5",
		"I am peasent6",
	};
	public List<string> Intermediate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public List<string> Expert { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

}
