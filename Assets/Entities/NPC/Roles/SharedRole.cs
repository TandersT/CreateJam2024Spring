using System.Collections.Generic;
using System.Formats.Asn1;

public abstract class SharedRole : IProficienct
{
    public abstract List<string> HasInteracted { get; set; }
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
    public abstract List<string> Greetings { get; set; }
    public abstract List<string> Novice { get; set; }
    public abstract List<string> Intermediate { get; set; }
    public abstract List<string> Expert { get; set; }
    public abstract List<string> Role {  get; set; }
}