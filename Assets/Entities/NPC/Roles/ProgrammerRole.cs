using Godot;
using System;
using System.Collections.Generic;

public class ProgrammerRole : SharedRole
{
	public override List<string> Novice { get; set; } = new List<string>
	{
		"I’ve been delving into programming lately.",
		"Coding is so hard. Help me!",
		"You want me to be the programmer for the project?! I’m not so sure about that…",
		"All these numbers are starting to make my brain numb.",
		"I don’t understand the coding documentation.",
		"This documentation is wrong and these functions don’t exist! Wait… Am I using this wrong?",
		"Why isn’t this line working? Oh.. Am I missing a semicolon?",
		"YES! My ‘Hello World’ script is working!",
		"What the hell is a class??",
		"How does a lerp work?",
		"What is a singleton?",
		"I forgot the syntax for this…",
		"It’s been a few weeks since I've started to learn how to program…",
		"Learning to code online is getting challenging. Should I get into an education in Computer Science?",
		"My cousin told me if I had an education in programming I would understand coding a lot better than I do now.",
		"So, I can use a dot to access the properties of an object. Did I get that one right?",
		"My friend is teaching me the basics of coding every time I go to his place.",
		"I got an error: “Variable not declared in scope.” What the hell does that mean?",
		"I’m dividing 3 by 2 and it’s giving me 1, while it’s definitely 1.5! What did you say? Floats? I don’t know what you are saying…",
		"What is a double?",
		"What? Did I mess up the indenting?"
	};

	public override List<string> Intermediate { get; set; } = new List<string>
{
	"You want me to code an Arduino? I could probably do that… I think…",
	"I’ve nailed the basics of programming, but I still have a lot to learn.",
	"I’ve noticed a really good evolution in my coding in the last months. I’m starting to feel pretty much confident!",
	"My code might not be flawless, but it gets the job pretty well done.",
	"I’ve graduated from ‘Hello World’ to tackle more complex projects.",
	"Every bug I squash brings me one step closer to becoming a coding master.",
	"I’m in an odd spot between beginner mistakes and more advanced techniques.",
	"I feel like I finally have a solid foundation in programming.",
	"What? A syntax error? How do I still fall for those stupid mistakes?",
	"My programming skills are a work in progress…",
	"I’m getting better with every bug I encounter.",
	"Debugging challenges keep me on my toes, but I’m getting better at tackling them.",
	"I think I’m ready for more advanced coding concepts.",
	"I’ve finally transitioned from following tutorials to building my own projects from scratch!",
	"I’m comfortable enough that I could mentor beginners, but I still seek guidance from experts."
};

	public override List<string> Expert { get; set; } = new List<string>
{
	"It’s been a few years since I’ve graduated from Harvard University in Computer Science…",
	"I’ve been programming for… uhhh… how many years was it?. A  lot!",
	"All it took was the right teacher. Now I code for a living in my own company.",
	"Debugging? That’s just a warm-up exercise for me.",
	"Programming is just my second language.",
	"It’ll take me only a few lines of code to bring your black screen into a masterpiece.",
	"Coding algorithms are just child’s play.",
	"Give me a problem, and I’ll program a solution faster than you can say ‘semicolon’.",
	"Syntax errors? Please, I haven’t had those in years.",
	"When it comes to coding, I’m like a wizard casting spells with my keyboard.",
	"Coding is my superpower, and bugs are my arch-enemies.",
	"Debugging is so easy that I eat bugs for breakfast. Wait… That sounded better in my head…",
	"I don’t write comments. My code speaks for itself.",
	"Programming isn’t just what I do. It’s who I am.",
	"The amount of lines of code I’ve debugged in my life is bigger than how many sand grains there are in the Sahara Desert.",
	"My coding skills are so good that it’ll impress advanced alien civilizations.",
	"My coding is ART. Full on stop.",
	"I don’t follow the best coding practices; I set them!",
	"Algorithms bow down to my logic. I am a programming prodigy."
};


	public override List<string> Role { get; set; } = new List<string>
	{
		"I got the programmer role.",
		"I’m the one coding for my team’s project.",
		"Yes, I’m the programmer. What about it?",
		"They chose me for doing the code.",
		"I’m doing the coding for my team.",
		"Writing algorithms is so fun. Well, most of the time…",
		"We are missing an animator. Now I need to animate props with lines of code…",
		"Line by line, I’m building the code…",
		"Should I create a class for this? Hmmm…",
		"I’m implementing a dictionary to save the character’s coordinates for when they transition to different parts of the map.",
		"And I’ll need a singleton for these variables… And… Done! Sorry, did you say something?",
		"Create a variable… And another one… And another one… And another one…",
		"I’m writing an algorithm for our game.",
		"I’m coding the physics.",
		"Can’t talk; Coding shaders…",
		"What programming language will I use for this jam?",
		"Looks like I got elected for the programmer role.",
		"I’m the one programming for this jam."
	};
}
