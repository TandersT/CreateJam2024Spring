using Godot;
using System;
using System.Collections.Generic;

public class SoundDesignerRole : SharedRole
{
	public override List<string> Novice { get; set; } = new List<string>
	{
		"I just downloaded Audacity. I’m not sure how to make sounds or music tracks.",
		"I play a few instruments, so I thought it would be cool to try to design sounds and songs.",
		"I just bought a new microphone! Ok, how do I plug it in my computer? What did you say? XLR? How do I plug that in my pc?",
		"No one told me that I needed an external audio interface to plug microphones… Could I just buy a USB microphone? What? They sound bad? Hmmm…",
		"I’m using Audacity, but I’ve heard a lot about Reaper and FL Studio. I’d like to try them sometime soon…",
		"How does an equalizer work?",
		"What does a normalizer do?",
		"What does a noise suppressor do?",
		"How does a compressor work?",
		"I’ll just use this one sound effect for everything. I’m pretty sure no one will notice… Right?",
		"There are some tricky sounds my team asked of me. Should I just use stock samples?",
		"If the audio samples don’t sound that great, I can always throw a bunch of reverb and it’ll probably fix it… Some delay should also help… No?",
		"I can’t tell the difference between uncompressed and compressed audio, so it probably doesn’t make any difference.",
		"I’ll just record sounds with my phone. Professional equipment is just a scam. Right?",
		"Equalizers make my audio samples sound horrible. What a horrible tool!",
		"Music theory? Bah! That’s just for nerds.",
		"I’ll just use free sound effects. They sound as good as the paid ones, right?",
		"Sound compression? What is that?"
	};

	public override List<string> Intermediate { get; set; } = new List<string>
	{
		"Finally decided to download FL Studio. Looks like a compelling program for making music and sound effects.",
		"I’ve been working with Sound Design for a year and a half now…",
		"I think I finally understand how equalizers work. They’re insanely versatile and useful!",
		"I’ve attained a better understanding of which types of microphones are the best depending on specific scenarios.",
		"I’ve learned an interesting trick. If I blur an audio sample, I can transform it into an atmospheric sound.",
		"When I look back on my progress, I see a clear evolution in the quality of the musical tracks and sound effects I’ve been composing. But I still have so much to learn.",
		"I’ve grown very comfortable using a DAW to make music tracks.",
		"I’m starting to understand the importance of sound consistency and coherence throughout a project.",
		"Lately I’ve been exploring the use of different recording techniques to capture more diverse and authentic sounds. The results can be surprising sometimes.",
		"I feel like I’ve been getting the hang of using equalizers and compressors more effectively to shape and balance my audio mixes. It’s a process that feels rewarding once you strike the right balance.",
		"I’m starting to pay closer attention to sound design principles like rhythm, pacing, and timing.",
		"Steadily, it’s becoming ever more evident how sound interacts with visuals and other elements of storytelling."
	};
	public override List<string> Expert { get; set; } = new List<string>
	{
		"I’ve graduated with an educational background in Sound Engineering and Design. I’d personally consider myself to be particularly good at it.",
		"I have been working with sound design for several years now.",
		"I make a living as a professional Sound Designer.",
		"I professionally tailor my audio tracks to evoke specific emotions from the players.",
		"Every sound I include in my audio tracks has a purpose. Nothing is there by accident.",
		"I have a perfect understanding of what sound effects are specifically needed for my team’s project. I am able to pinpoint exactly what is missing.",
		"I don’t design sound independently from a game. I collaborate closely with the other designers to complement and enhance the game in a synergetic way.",
		"After years of experience I can say that the most effective way to design sounds is minimizing the reliance on post-editing and instead focusing on capturing high-quality recordings from the outset."
	};

	public override List<string> Role { get; set; } = new List<string>
{
	"I’m the sound guy.",
	"I’m doing the sound design for my team.",
	"What type of music should I compose for our game? Hmmm…",
	"I think I’ve finally come up with a banger song for our main menu!",
	"YOOO THIS RIFF GOES SO HARD!! I nailed this!",
	"I’m going to record someone shaking a big metal sheet for the thunder sound FX.",
	"I’m going to record myself biting a plastic bottle and I’ll use it for the sound FX of biting an apple.",
	"It’s always so fun to compose music and sounds for games.",
	"Composing music and sounds is definitively my biggest passion.",
	"Wait! That sound! Can you do it again, please? Let me just get the microphone ready.",
	"I brought my MIDI keyboard. I’m ready to roll!",
	"I just created a synthwave track with saxophone leads.",
	"If I was jamming from home I could’ve recorded some guitar riffs. I’ll just stick with synthesizers and MIDI drums this time…",
	"I just recorded the sound of rubbing a balloon. I’ll use it for the sound FX for when 3D models are getting stretched.",
	"I’m doing a disco sound track with jazz elements…",
	"Am I done with the audio track? Almost. I still need to mess around with the equalizer.",
	"Add a distortion effect to a mouth harp and tell me what you think about it. I personally think you’ll be blown away."
};


}
