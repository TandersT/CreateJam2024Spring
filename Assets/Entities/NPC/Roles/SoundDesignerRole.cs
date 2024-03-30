using Godot;
using System;
using System.Collections.Generic;

public partial class SoundDesignerRole : Node, IProficienct
{
	public List<string> Novice { get; set; } = new List<string>
	{
		"I am novice",
	};

	public List<string> Intermediate { get; set; } = new List<string>
	{
		"I am intermediate",
	};
	public List<string> Expert { get; set; } = new List<string>
	{
		"I am expert",
	};
}
