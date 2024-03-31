using System.Collections.Generic;
using System.Formats.Asn1;

public abstract class SharedRole : IProficienct
{
	public List<string> General { get; set; } = new List<string>
{
	"I’ve been wondering a lot lately: How do bats piss upside down?",
	"A friend of mine told me he was feeling evil forces emanating from this place. Would you happen to know what he’s talking about, perchance?",
	"Why is the weather cloudy all the time?",
	"Are you talking to me?",
	"Making games is so easy that I can do it with just one hand. And it’s not because I’m missing an arm.",
	"You look like a very competitive person.",
	"I heard there’s free snacks here.",
	"Wake up! Just kidding… Or am I?",
	"I should’ve slept more…",
	"Can you feel it? A sudden urge for ice cream. No? Is it just me?",
	"I wonder what it is like working and living in a submarine for a few weeks straight…",
	"I need more beer…",
	"I’ve become lactose intolerant ever since my father left to get some milk.",
	"Looks like someone’s selling hotdogs outside.",
	"Do you smell that? A faint smell of ashes… Something evil’s afoot! I’m sure of it!",
	"I heard there’s a circus getting set up nearby. Do you like clowns?",
	"Imma steal your steel while you stand still! I dare you to say that 5 times in a row and FAST.",
	"Did you know that african buffaloes make decisions by voting? The more you know…",
	"Have you ever thought about what the world would look like if all conflicts were solved via dance contests? We should let disputes be settled on the dance floor!",
	"I’m running out of ideas. Do you have any to share?",
	"Could you remind me how long we have until the jam is over?",
	"WHERE’S THE CREATE JAM JAM??!",
	"I’m telling you that pyramids were definitely built by aliens."

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
		"no talk no more",
		"get reckt",
		"u dumb",
	};

	public abstract List<string> Novice { get; set; }
	public abstract List<string> Intermediate { get; set; }
	public abstract List<string> Expert { get; set; }
	public abstract List<string> Role {  get; set; }
}
