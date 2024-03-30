using System.Collections.Generic;
using System.Formats.Asn1;

public abstract class SharedRole : IProficienct
{
	public List<string> General { get; set; } = new List<string>
{
	"I’ve been wondering a lot lately: How do bats piss upside down?",
	"A friend of mine who’s a priest told me he wasn’t coming because he felt evil forces emanating from this place. You wouldn’t know what he’s talking about, would you?",
	"Why is the weather cloudy all the time?",
	"Why are you talking to me?",
	"Making games is so easy that I can do it with just one hand. And it’s not because I’m missing an arm.",
	"You look like a very competitive person.",
	"My programmer cousin got hired for developing a security system for the local bank. Pretty cool, huh?",
	"I heard there’s free snacks here.",
	"Wake up! Just kidding… Or am I?",
	"I should’ve slept more…",
	"Can you feel it? A sudden urge for ice cream. No? Is it just me?",
	"I wonder what it is like working in a submarine…"
};
	public List<string> Greetings { get; set; } = new List<string>
{
	"Hey there!",
	"Greetings, friend!",
	"Good day to you!",
	"Well met!",
	"Howdy, partner!",
	"Salutations!",
	"Yo!",
	"Hello and welcome!",
	"Nice to see you!",
	"Hola amigo!",
	"Bonjour!",
	"Ciao!",
	"Namaste!",
	"Shalom!",
	"G'day mate!",
	"Hiya!",
	"Hey!",
	"Heya!",
	"Greetings and salutations!",
	"Good to have you here!"
};

	public List<string> HasInteracted { get; set; } = new List<string>
	{
		"Frick off",
		"u are dumb",
		"no talk more",
		"get reckt",
		"u dumb",
	};

	public abstract List<string> Novice { get; set; }
	public abstract List<string> Intermediate { get; set; }
	public abstract List<string> Expert { get; set; }
	public abstract List<string> Role {  get; set; }
}
