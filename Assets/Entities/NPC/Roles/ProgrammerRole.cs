using Godot;
using System;
using System.Collections.Generic;

public partial class ProgrammerRole : Node, IProficienct
{
	public List<string> Greetings { get; set; } = new List<string>
	{
		"I am greeting1",
		"I am greeting2",
		"I am greeting3",
		"I am greeting4",
		"I am greeting5",
	};
	public List<string> HasInteracted { get; set; } = new List<string>
	{
		"Frick off",
		"u are dumb",
		"no talk more",
		"get reckt",
		"u dumb",
	};
	public List<string> Novice { get; set; } = new List<string>
	{
		"I am novice1",
		"I am novice2",
		"I am novice3",
		"I am novice4",
		"I am novice5",
	};

	public List<string> Intermediate { get; set; } = new List<string>
	{
		"I am intermediate1",
		"I am intermediate2",
		"I am intermediate3",
		"I am intermediate4",
		"I am intermediate5",
	};
	public List<string> Expert { get; set; } = new List<string>
	{
		"I am expert1",
		"I am expert2",
		"I am expert3",
		"I am expert4",
		"I am expert5",
	};
}
