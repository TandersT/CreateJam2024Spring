using Godot;
using System;
using System.Collections.Generic;

public class GraphicsArtistRole : SharedRole
{
	public override List<string> Greetings { get; set; } = new List<string>
	{
		"I am greeting1",
		"I am greeting2",
		"I am greeting3",
		"I am greeting4",
		"I am greeting5",
	};
	public override List<string> HasInteracted { get; set; } = new List<string>
	{
		"Frick off",
		"u are dumb",
		"no talk more",
		"get reckt",
		"u dumb",
	};
	public override List<string> Novice { get; set; } = new List<string>
	{
		"I am novice1",
		"I am novice2",
		"I am novice3",
		"I am novice4",
		"I am novice5",
	};

	public override List<string> Intermediate { get; set; } = new List<string>
	{
		"I am intermediate1",
		"I am intermediate2",
		"I am intermediate3",
		"I am intermediate4",
		"I am intermediate5",
	};
	public override List<string> Expert { get; set; } = new List<string>
	{
		"I am expert1",
		"I am expert2",
		"I am expert3",
		"I am expert4",
		"I am expert5",
	};

	public override List<string> Role { get; set; } = new List<string>
{
    "The team elected me as the visual artist.",
    "I’m responsible for the visual art in the game.",
    "I got elected as my team’s visual artist.",
    "I’m doing the visual art for my team’s game.",
    "Yep, I’m the visual artist.",
    "This sprite is looking pretty good! I can’t wait to show this to my team!",
    "This guy will have a sword. No. Let me erase that… He will have a GIANT HAMMER. Oh yeah, that looks badass.",
    "Here’s some quick sketches I’ve just drawn. What do you think about them?",
    "I’ve just put up a moodboard. Any thoughts?",
    "I feel really cozy when I hold a pen in my hand and start thinking about what I’m going to draw.",
    "I need to change the visual style of our game. It doesn’t fit the theme anymore.",
    "I’ve done a few variations of the character’s shoes. Which ones do you like the most?",
    "Here are some models. I replaced the characters’ heads with furniture instead. Look! I call this one Chair Head.",
    "For this illustration I’m inspiring myself on megastructures.They are so COLOSSAL that I can’t help but feel enthusiastic about it!",
    "Still need to change this characters’ hair. It looks so goofy. Well… Maybe that’s actually a good thing!",
    "I’ve animated these sprites. What do you think about it?",
    "And done! Just added the idle and running animations.",
    "Can’t talk. Rigging this dude…",
    "I’m adding some vintage flare to the furniture props.",
    "Look at this knight I just made!",
    "What do you think of my creepy alien?",
    "Now I just need to add the highlights and the shadows to the sprites…",
    "Should I add some wings to this character?",
    "Should I add some piercings here?",
    "Should I make this character look more tribal?",
    "I’ll definitely do some helmets and hats for the character customization.",
    "What do you think of the tattoos I designed for this character?"
};

}
