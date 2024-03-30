using Godot;
using System;
using System.Collections.Generic;

public class GraphicsArtistRole : SharedRole
{
	public override List<string> Novice { get; set; } = new List<string>
	{
		"When I draw circles and ellipses I can never seem to close them properly. It’s so FRUSTRATING!!",
		"I don’t understand how to draw shadows…",
		"Why do the colors in my drawings always look off? I really need a better understanding of color theory.",
		"How do I draw in perspective?",
		"I’m trying to model a head but every time it looks like a bean.",
		"Why do the limbs of my model have different sizes?",
		"I’ve just drawn the limbs! They look like noodles…",
		"So, there are shadows AND highlights? What is the latter one supposed to be?",
		"I started learning to draw a couple of months ago. The mechanical skills seem to be improving steadily, but unfortunately I can’t say the same for the cognitive side of it.",
		"How should I hold the pencil when I draw? Do I hold it close to the tip? No? What about holding it from the edge?",
		"Should I place my arm on the paper and draw from the wrist? Or should I just use my whole arm and draw from the shoulder?",
		"My straight lines don’t look straight at all… Should I draw them faster? Slower?",
		"Why does my teacher still make me draw circles and squares? I want something more advanced. I ALREADY KNOW WHAT A SQUARE LOOKS LIKE!!",
		"Drawing shapes in perspective is insanely hard. Have you tried it?",
		"Blender is so overwhelming! Please, help me…",
		"I discovered that drawing vanishing points is quite fun. You can follow the lines to draw in perspective. It’s also fun to remove the guidelines and try to eyeball it.",
		"I don’t even know where to look at when I open Blender!"
	};

	public override List<string> Intermediate { get; set; } = new List<string>
	{
		"I still have some trouble getting the colors correctly in my drawings. But it’s definitely better than it was before.",
		"Connecting dots on a daily basis has really helped me nail solid straight lines.",
		"I feel like I’ve been getting a better understanding of color theory lately.",
		"I feel like I’ve finally got hold of drawing shapes in different perspectives.",
		"I’ve started to define muscles on top of geometry.",
		"Do you want to see some low poly models I’ve been doing lately?",
		"UV unwrapping hurts my brain. Isn’t there an easier way to do this?",
		"I’ve finally moved on to painting some 3D models. It’s surprisingly quite fun.",
		"Lately I’ve been paying more attention to implementing dynamism to my posing characters.",
		"Rigging a character is boring, but animating it is awesome!",
		"My cross hatching still has a long way to go to look consistent…",
		"Sculpting models in Blender has been feeling rather cozy lately. Have you given it a try?",
		"I’ve finally moved on to studying anatomy. It might be better to get acquainted with a few muscles first…",
		"My drawings are far from perfect, but they generally nail the job.",
		"My modeling technique still has a big room for improvement, but is insanely better compared to what it used to do a few months ago.",
		"I get really playful when I experiment with animating sprites or scenes. You should try it too."
	};
	
	public override List<string> Expert { get; set; } = new List<string>
	{
		"I’m a master at foreshortening. I’ll shorten you to the size of a green pea.",
		"Every pencil stroke I make is with intent. I’m an expert at what I do, and nothing happens by coincidence.",
		"I’ve been painting for 13 years. That’s a long time…",
		"I’ve been drawing for many years.",
		"I’ve been modeling 3D characters for many years.",
		"My portfolio is a testament to my expertise. Take a look and prepare to be amazed.",
		"Pixel art? Sure. That’s child’s play.",
		"I’ve sculpted for most of my teenage years. Now I make a living out of sculpting virtual models for video games.",
		"Do you need tips for getting better at making 3D models? I have years of experience and plenty of tips to share.",
		"I used to work as an animator for a AAA game company. My expertise was already commendable, but I kept getting better.",
		"Doing high fidelity models is my everyday breakfast."
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
	"I need to change the visual style of our game. It doesn’t fit the theme anymore.",
	"I’ve done a few variations of the character’s shoes. Which ones do you like the most?",
	"Here are some models. I replaced the characters’ heads with furniture instead. Look! Say hi to Chair Head.",
	"For this illustration I’m inspiring myself on megastructures.They are so COLOSSAL that I can’t help but feel enthusiastic about it!",
	"Still need to change this character’s hair. It looks so goofy. Well… Maybe that’s actually a good thing!",
	"I’ve animated these sprites. What do you think about it?",
	"And… Done! Just added the idle and running animations.",
	"Can’t talk. Rigging this dude…",
	"I’m adding some vintage flare to the furniture props.",
	"Look at this knight I just made!",
	"What do you think of the creepy alien I made?",
	"Should I add some wings to this character?",
	"Should I add some piercings here?",
	"Should I make this character look more tribal?",
	"I’ll definitely do some helmets and hats for character customization.",
	"What do you think of the tattoos I designed for this character?",
};

}
