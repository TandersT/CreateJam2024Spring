using Godot;
using System;

public partial class ButtonSound : Button
{
	AudioStreamPlayer hover => GetNode<AudioStreamPlayer>("%hover");
	AudioStreamPlayer click => GetNode<AudioStreamPlayer>("%click");
	public override void _Ready()
	{
		MouseEntered += () => hover.Play();
		Pressed += () => click.Play();
	}
}
