VAR knowledge_Spaghetti = 0
VAR knowledge_prostitute_name = 0
// DEBUG mode adds a few shortcuts - remember to set to false in release!
VAR DEBUG = true

->start_capo_apartment
===start_capo_apartment===

{DEBUG:
	IN DEBUG MODE! # player
	*	[Start]	-> start_capo_apartment.start_office
	*	Start of the game[]
		**	[Discussing with Lanza in the Lobby] -> start_capo_apartment.lobby_apartment
		**	[Deeper discussion with Lanza] -> start_capo_apartment.lanza_dialogue
		**  [Checking the apartment] -> start_capo_apartment.check_apartment
	*	Prostitute Motel[] #NewInvestigation
		**	[Start] ->prostitute_motel
		**	[Motel lobby] ->prostitute_motel.motel_lobby
		**	[Motel room] ->prostitute_motel.motel_room
	*	Condor Club[] #NewInvestigation #NewInvestigation
		**	[Start] ->condor_club
		**	[Entrance] ->condor_club.condor_entrance
		**	[First discussion with madam] ->condor_club.condor_madam
		**	[Second discussion with madam] ->condor_club.condor_madam2
- else:
	// First diversion: where do we begin?
 ->start_capo_apartment.start_office
 }

/*--------------------------------------------------------------------------------

	Start the story!

--------------------------------------------------------------------------------*/

=start_office 
VAR lanza_stitch_first = 0

TODO Tags (see with Killian)
TODO Intro capo's apartment (retakes)
// METTRE UN FOND NOIR?
# DisableDiscussion
#PlayerDBox #1
<i>(Fucking city.)</i> # player 
<i>("Come to San Francisco" they said. "It's like a Gold Rush for private detectives".)</i> # player
<i>(And I fell for it.)</i> # player
<i>(Ain't nobody like Bugsy Rosenthal to look like a damn fool.)</i> # player 
<i>(Next thing I know, my wife leaves me, and I'm nearly all out of legit businesses.)</i> # player
<i>(But everybody has to do something for a living.)</i> # player 
<i>(And this mob is paying quite well.)</i> # player
<i>(But they're fucking savages, killing each other and all.)</i> # player 
<i>(Can't sleep tonight. Cars, gun shots, everybody in town wants to drive me crazy.)</i> # player 
<i>(And now the fuckin' phone is ringing.)</i> # player #SFXPlay #8
<i>(Don't even wants to answer, fuck it.)</i> # player 
- (phone) {<i>(The phone rings.)</i>|<i>(The phone doesn't stop.)</i>|<i>(It continues to ring.)</i>} # player 
# jump
	*	[Answer] <i>(This damn phone ain't gonna stop itself.)</i> # player 
	+	[Try to sleep]{<i>(Ain't no time to call somebody, they'll wait tomorrow.)</i>|<i>(Could be a business call... Who am I kidding?)</i>|<i>(Twenty-four sheeps. Twenty-five sheeps. Twenty-six sheeps...)</i>|} # player
		->phone
- <i>(I decide to get up and finally answer it.)</i> # player #SFXStop #8

TODO Find the capo's address (confirm)
TODO Find Bugsy's address
TODO Find dead capo's name
# jump
	*	Are you out of your mind?[] Did you fucking see the hour? # player #PlayerDBox #0
- It's <color=yellow>James Lanza</color>. We had a problem with Giovanni. #otherCharacter #NewCharacterSprite #7 #NewCharacterLog #2
You need to come here immediately. # otherCharacter 
# jump
	*	What time is it[?] for fuck's sake? # player 
- 3AM, but we pay you to come even when you're shitting. #otherCharacter 
You understand me Rosenthal? Come here right now. # otherCharacter 
# jump
    *	What's the address[?] Lanza? # player 
- 98 Francisco Street . Hurry up, fucking stinks here. # otherCharacter 
# jump
    *   [Hang up] <> # player #NewCharacterSprite #0
    *   Don't tell me to hurry[] boy, it's the middle of the fucking night. # player
- <i>(I hung up and got out of bed.)</i> # player #PlayerDBox #1  #NewCharacterSprite #0
<i>(For fuck's sake, what did they do again?)</i> # player
# jump
    *   [Go to Francisco Street] <> # player # NewBigBackground #4
    #MusicPlay #1
-  (cab){<i>(Not even the time for a coffee, I put on my hat and go outside to find a cab.)</i>|} # player 
# jump
    +   [{Hail a cab|Hail a cab again|Try to hail a cab}] <> # player #SFXPlay #15
        {<i>(That prick doesn't even slow down.)</i>-> cab|<i>(Am I covered in shit?)</i>-> cab|} # player
- <i>(As I'm slowly starting to lose my temper and head back to the office, a 47' Cadillac slows by.)</i> # player
Mr. Rosenthal? # otherCharacter # NewCharacterSprite #8
# jump  #PlayerDBox #0
    *   Ain't no Yid here kid[], get lost. # player
        Ain't the time to joke Mr. Rosenthal. # otherCharacter
    *   Who's askin? # player
- Mr. Lanza sent me to get you, could you please get in the car? # otherCharacter
    *   Alright, I'm coming[]. Couldn't he warn me that you were going to fetch me? # player
    *   Not so much of a choice[], am I right? # player
- <i>(I step into the car.)</i># player # NewBackground #10 #NewCharacterSprite #0 #PlayerDBox #1 #SFXPlay #14 #MusicPlay #3
<i>(As soon as I seat, my nocturnal driver starts the engine and takes me to Francisco Street.)</i> # player
<i>(He rides smoothly and hardly ever speaks. Francisco is not so far, but he seems to take quite a lot of detours.)</i> # player #NewNarrativeLog #0
- (car_convo)
# jump # NewCharacterSprite #8
    *   Are you new in town, kid? # player #PlayerDBox #0
        Came here 6 months ago from Sicily. # otherCharacter 
        # jump
        **  Lanza got you in here? # player
            Let's say that he needed the skills that I'm able to provide for his protection. # otherCharacter
        **  More Italians?[] Is there any place left in North Beach? # player
            I'm not here to settle down, Mr. Lanza asked me to come to ensure his protection. # otherCharacter
        -- 
        **	What is he afraid of?[] He's not been linked to Lima, has he? # player
        Not that I know of, the boss was smart enough to make sure that the organization would not be too much harmed. # otherCharacter
        So who's going after him? # player
        I can't say anything, but I'm sure Mr. Lanza will talk to you about it. # otherCharacter #NewNarrativeLog #1
        ->car_convo
    *   Can't you go directly to the apartment? # player #PlayerDBox #0
        Must make sure that we're not tailed sir # otherCharacter # NewCharacterSprite #8
        # jump
        **  Who would follow me?[] Am I a Jewish Marilyn? # player 
        //revoir 
            Let's say that we caught a lot of heat from the trial. # otherCharacter
        **  I ain't mixed with all that[] kid, Jimmy should have told you so. I'm a simple detective that helps some friends in need. # player
            Not you personnally, but we must stay on our guard with the trial. # otherCharacter
        --  I don't get it, your boss Lima is judged, couldn't you find a straw man to take his place? # player
            Let's say that it's exactly where you're intervening. # otherCharacter
        # jump
        **  Are you throwing me under the bus?[] I'll not go down easily motherfucker. # player
            Don't be stupid, Mr. Lima cannot be saved. # otherCharacter
            So what am I doing here? # player
        **  Lima didn't request my help for the trial[], what could I possibly do? # player
            If all had gone accordingly to the plan, you would not have been involved in this. # otherCharacter
            So where do I fit in your plan now? # player
        --  Our straw man is dead. # otherCharacter
        # jump
        **  Fuck... What happened to Giovanni? # player 
        //à changer
            You will discover it soon enough. # otherCharacter #NewNarrativeLog #2
            ->car_convo
    *->
- <i>(The ride continued in a complete silence.)</i> # player #NewCharacterSprite #0 #PlayerDBox #1
<i>(Giovanni is dead. Poor kid. And more Italians flooding North Beach.)</i> # player
<i>(Something's wrong. And I'm gettin' mixed in all that.)</i> # player
<i>(We finally park in Francisco Street.)</i> # player #MusicPlay #1 # NewBackground #15 #SFXPlay #14
I let you go by yourself. Mr. Lanza is waiting for you inside. # otherCharacter # NewCharacterSprite #8 #NewCharacterLog #0
# jump
    *   Thank you kid # player #PlayerDBox #0 # NewCharacterSprite #8
    *   [Just a question...] Before I leave kid, can I ask you something? # player #PlayerDBox #0
        Go ahead. # otherCharacter
        Am I being set up? Are they trying to fuck me over to save their heads? # player
        Mr. Rosenthal, you're not important enough to worry yourself. # otherCharacter
        <i>(And the young prick leaves.)</i> # player #PlayerDBox #1 # NewCharacterSprite #0
- <i>(I cross the threshold of the building and go to the second floor. Apartment 237.)</i> # player #PlayerDBox #1 # NewCharacterSprite #0 #MusicPlay #0

TODO First dialogue with Lanza (retakes)

# NewBigBackground #12
<i>(Jimmy Lanza is waiting for me, near to the door.)</i> # player # NewCharacterSprite #5
For fuck's sake, ain't all Jew boys supposed to arrive on time? # otherCharacter 
# jump #PlayerDBox #0
*   Never when there are [Italians]greaseballs like you waiting for me at a crime scene. # player
    I'm not here to joke around, not with two bodies waiting two feet away. # otherCharacter
*   Fuck you Lanza[], I don't have to come like a dog whenever you fuck things up! # player
    You think I'm happy with this shit? I could have done without two more bodies. # otherCharacter
*   Calm down[], we have more important to do. Where is he? # player
    He? You mean them. We found a girl in the bathroom. # otherCharacter
- # jump
*   What do you mean?[] I thought Giovanni was the only one. # player
-   I guess that you were wrong. # otherCharacter
    We found a prostitute in the bathroom. # otherCharacter
*   For fuck's sake[...], you people really can't keep it in your pants. # player
    Show some respect, Rosenthal. We're not gonna always feed you. # otherCharacter
*   [The fuck was that good?] The fuck was so good that they both died? # player
    Stop jockin' around. # otherCharacter
- Someone killed them both, or they killed each other, but I don't think so. # otherCharacter
 But you're the expert here! So what you gonna do? # otherCharacter #NewNarrativeLog #3
-> lobby_apartment

/*--------------------------------------------------------------------------------

	Hub choice: discussing with Lanza or checking the apartment

--------------------------------------------------------------------------------*/

= lobby_apartment
#ActivateDiscussion
# jump
    *   I have some questions for you Lanza. # player #PlayerDBox #0
    ->lanza_dialogue
    *   I'll go and check on the bodies {lanza_stitch_first ==1:now|first}. # player #PlayerDBox #0
    ->check_apartment
    *->
    {lanza_stitch_first == 1:Enough talking|Come here!} # otherCharacter # NewCharacterSprite #5
    We need to go Rosenthal. I'll take you to your office # otherCharacter
    ->end_apartment
    
    
/*--------------------------------------------------------------------------------

	Discussing with Lanza

--------------------------------------------------------------------------------*/
    
=lanza_dialogue

TODO Dialogue with Lanza (retakes)
-   (lanza_convo)
# jump # NewCharacterSprite #5
*   What can you say about Giovanni? # player #PlayerDBox #0
    He was made capo some months ago. Poor kid... # otherCharacter # NewCharacterSprite #5
    The Administration chose him to take the place of Lima during the trial. # otherCharacter
    # jump
    **  You mean that you decided to throw him under the bus. # player
        No, we'll take care of his family. What he was gonna do for us was essential. # otherCharacter
        Now we're in deep shit... # otherCharacter #NewNarrativeLog #4
        ->lanza_convo
    **  What did he do to deserve it? # player
        Stop insinuating that we wanted him killed. He was not made for this job. # otherCharacter
        # jump
        *** Is it your opinion? # player
            It is mine, and Abati's too. Even Morello agreed. # otherCharacter
            And he's the fucking Consigliere, taking care of our soldiers and shit. # otherCharacter
            We agreed to tend for his family, at least his mother. # otherCharacter
            We could not find anyone else. # otherCharacter
            # jump
            ****    Where does she live? # player
                    She's in Sicily. The boy came here on his own. Fuckin' American dream. # otherCharacter #NewNarrativeLog #5
                    ->lanza_convo
*   Who could want to harm him? # player #PlayerDBox #0
    Nobody that I would know of. # otherCharacter
    # jump
    **  Not even the girl's pimp? # player
        I can't even say for sure that she's a prostitute. # otherCharacter
        Only a... how could you say... an educated guess Rosenthal. # otherCharacter
        You knew Tommy a little. Always a gentleman with the ladies. # otherCharacter #NewNarrativeLog #6
    ->lanza_convo
*   Who's that girl? # player #PlayerDBox #0
    Can't say for sure, that's your job now. Nobody ever saw Tommy with her. # otherCharacter
    But we were not following him day and night. # otherCharacter
    # jump
    **  You mean you tailed him sometimes? # player
        Don't be stupid. I keep a close watch on everyone. # otherCharacter #NewNarrativeLog #7
    ->lanza_convo
*   What is happening with the organization? # player #PlayerDBox #0
    As you should know now, Lima was sent to jail, with a trial coming. # otherCharacter
    Abati took his place, and I second him now. # otherCharacter
    # jump
    **  Congratulations[!], Sotto-Capi. Is that your title now? # player
        That was meant to be temporary. We should have exonerated Lima. # otherCharacter
        But you know your little complication with Giovanni. # otherCharacter
    **  Too much complication to take the power. # player
        Again, stop your accusations. We were close to freeing the Boss. # otherCharacter
    --  Now Lima is taking full power, and I'll second him the best I can. # otherCharacter
    # jump
    **  What about Morello? # player
        The Consigliere? He's advising us. He approved of Giovanni's designation. # otherCharacter
        I think he's trying to take some distance with the game. # otherCharacter
        What can we say about it? That poor bastard earned to rest now. # otherCharacter
        We're putting the Family back on its feet, so we're focusing on greater matters.
    ->lanza_convo
*   ->
~lanza_stitch_first = 1
->lobby_apartment 

/*--------------------------------------------------------------------------------

	Checking the apartment

--------------------------------------------------------------------------------*/

=check_apartment
#NewCharacterSprite #0 # NewBackground #0 #SFXPlay #16
<i>(I enter the apartment, only to find a first body. Poor Tommy.)</i> # player #PlayerDBox #1
<i>(Ok, let's focus now and do that methodically, ain't I a damn detective?)</i> # player
*	[Check the livingroom] <i>(Nice apartment.)</i> # player
	<i>(Nothing special here, except some furnitures.)</i> # player
	<i>(And obviously, Giovanni's body.)</i> # player
	**	[Check Giovanni] <i>(He's lying near the door.)</i> # player
		<i>(Poor boy.)</i>  # player # NewNoBackground #16
		<i>(His skull is completely shattered, which is caused by the obvious bullet hole on his forehead.)</i> # player
		<i>(Skull and brain pieces are all over the wall behind.)</i> # player 
	    <i>(What does this poker chip do here?)</i> # player #NewNarrativeLog #8
	    # NewDocument #2
- <i>(So now, where would I search for clues?)</i> # player
- (clues_apartment)
# jump
*	[Check the bathroom] <i>(Maybe there could be something in the bathroom.)</i> # player #SFXPlay #17
	# NewBackground #11 #SFXPlay #18
	<i>(Oh that's right. The second body.)</i> # player
	<i>(The entire floor is covered in blood. Hers, obviously.)</i> # player
	**	[Look at the body] <i>(Who do we have here?)</i> # player
		<i>(She really seems to be a prostitute.)</i> # player
		<i>(The way she's dressed, her make-up...)</i> # player
		<i>(But not a lot of prostitutes have a deep cut across their throats.)</i> # player
		<i>(She didn't come with a lot of stuff. Except for a small lady bag.)</i> # player
	    # jump
		***	[Open the purse] <i>(Maybe I'll find more about her here.)</i> # player
			<i>(No papers, only a key for a hotel room.)</i> # player #NewNarrativeLog #9
		# jump # NewDocument #0
		->clues_apartment
*	[Check the bedroom] <i>(Did they have enough time to use the bedroom?)</i> # player #SFXPlay #17
	#NewBackground #21 #SFXPlay #18
	<i>(It's tidy, with not a lot of furnitures.)</i> # player
	<i>(Only a bed, an empty desk, and a closet.)</i> # player
	<i>(The bed is still made. Giovanni didn't sleep here so much, or at least not yesterday.)</i> # player
	<i>(There's the closet then.)</i> # player
	# jump
	**	[Open the closet] <i>(What a freakin' creaking door!)</i> # player
		<i>(But I know seems logical, the closet being nearly empty.)</i> # player
		<i>(Only a few suits, some underwear.)</i> # player
		<i>(Not enough for someone to live here.)</i> # player
		<i>(Unless that person is a slog. But Tommy was not.)</i> # player #NewNarrativeLog #10
	->clues_apartment
*->
<i>(Nothing more for me in that apartment.)</i> # player 
# NewBackground #12 # DisablePlayer
~lanza_stitch_first = 0
->lobby_apartment

/*--------------------------------------------------------------------------------

	End of the Capo's apartment scene

--------------------------------------------------------------------------------*/

=end_apartment

<i>(Lanza, always keeping an eye on me, brought me back to my office on Broadway Street.)</i> # player #PlayerDBox #1
<i>(That day could not have been worse.)</i> # player #NewInvestigation
->END

=== DefaultStory
#NewCharacterSprite #0
<i>(I have nothing to do here.)</i> # player #PlayerDBox #1
->END

/*--------------------------------------------------------------------------------
    Prostitute's apartment
    First lead followed by Bugsy: visit the prostitute’s room in a motel

--------------------------------------------------------------------------------*/

===prostitute_motel===
//Background Motel's Street, no interlocutor
#NewNoBackground #5 # NewCharacterSprite #0
<i>(That should be where the girl lived.)</i> # player #PlayerDBox #1
<i>(What a shady motel...)</i> # player
<i>(I wouldn't stay here for a million bucks!)</i> # player
<i>(But I have to go in now.)</i> # player
# jump
*   [Enter the motel] <i>(Let's go.)</i> # player #SFXPlay #17
    ->motel_lobby

=motel_lobby
//Background Motel's lobby, no interlocutor at first
#NewBackground #14
<i>(That place could use some light.)</i> # player
<i>(The only client that come here exit the place immediately, only passing by.)</i> # player
<i>(There's only this man. Seems to be the manager.)</i> # player #NewCharacterLog #4
*   [Go to the desk] <i>(Let's talk to him, that's my only lead.)</i> # player
//Interlocutor: Motel's manager
- Hello sir, what do you want? # otherCharacter # NewCharacterSprite #4
*   [Ask about the room] Good day fellow, I want to access one of your room. # player #PlayerDBox #0
- Alright, do you have the key? # otherCharacter
# jump
*   [Give him the key] Here it is, room 237. # player 
- <i>(He seems puzzled, as if I wanted to penetrate the White House.)</i> # player #PlayerDBox #1 #NewNarrativeLog #11
- And why would you want to go in there man? # otherCharacter
# jump
*   [I'm investigating] And why would you want to interfere in an ongoing investigation. # player  #PlayerDBox #0
    <i>(I see the fear in his eyes. That's not someone that want to challenge the police.)</i> # player
    <i>(I could swear that he immediately peed is pants.)</i> # player
    Oh... hum, ok sir, if you would follow me. # otherCharacter #NewNarrativeLog #12
	-- (threat_manager)
*   [She's my girlfriend] She's my girl, but I don't have news from her since two days... # player #PlayerDBox #0
    Your girl... Why does she live alone then? # otherCharacter
    **  [Not your business] Why don't mind your damn business? # player
        Alright, calm down... So many weirdos here... # otherCharacter
    **  [To keep our independance] We're a free couple. # player
        A what? #otherCharacter
        We do what we want. # otherCharacter
        But she's your girl? # otherCharacter
        Yes she is. # player
        The one and only? # otherCharacter
        Seems that I can't fool you... # player
        Oh... I see... Alright old sport follow me. # otherCharacter
- <i>(That slow man finally seems to trust me, and leads me through the maze that is this shitty motel.)</i> # player #PlayerDBox #1 
->motel_room

=motel_room
//Background Motel's room, no interlocutor
# NewBackground #3 # DisablePlayer #SFXPlay #16 #NewCharacterSprite #4
I'll have to stay with you man, company's policy. # otherCharacter
No problem, just let me search for a few things # player #PlayerDBox #0
# NewCharacterSprite #0
<i>(That's not what you would call a lady bedroom.)</i> # player #PlayerDBox #1
<i>(Nearly no furniture. No personal effects. Only some clothes on the bed.)</i> # player
<i>(That could make sense if she's a prostitute, she wouldn't have much time to sleep here.)</i> # player
<i>(But maybe I'll find something anyway.)</i> # player
- (motel_search)
# jump #PlayerDBox #1
*	[Search around the bed] <i>(The bed takes nearly the entire room space.)</i> # player
	<i>(There's nothing worth noticing, except for the bedside table and a large closet.)</i> # player
	# jump
	-- (room_search)
	**	[Go to the bedside table] <i>(This table is really the only remarkable object here.)</i> # player
		---	(bedside_search) # jump
		***	[Look on the top] <i>(Above the table, there's a little lamp.)</i> # player
	        <i>(Beside it, an ashtray, full of cigarettes, and a little matchbox.)</i> # player
	        # NewDocument #1
	        <i>(What club could this fine lady go to?)</i> # player
	        ->bedside_search
	    ***	[Open a drawer] <i>(What could we find on the drawer?)</i> # player #SFXPlay #1
	        <i>(Only a bible. What a joke...)</i> # player
	        <i>(Is it a bookmark in it?)</i> # player
	        # jump
	        ****  [Open the bible] <i>(Let's see...)</i> # player #SFXPlay #4
	        <i>(Oh. 100 dollars. Big cuts only. Definitely a prostitute.)</i> # player
	        <i>(But thanks for the money darling, you won't need it now.)</i> # player
	        ->bedside_search
	    ***	->
	    	->room_search
	**	[Open the closet] <i>(So now lady, what are you hiding in here?)</i> # player #SFXPlay #16
    	<i>(Only clothes, normal ones, but also some... enticing outfits.)</i> # player
    	<i>(This only assures me that she's a working girl.)</i> # player
    	->room_search
	**	->
		->motel_search
*	[Check the desk] <i>(She seems to often use that desk given the amount of papers.)</i> # player
    <i>(What are those? Seems to be letters from a relative.)</i> # player
    <i>(I'll check that at the office.)</i> # player
    # NewDocument #3
    ->motel_search
// *   [Go in the kitchen] <i>No space</i> # player
//     <i>Nothing interesting in the kitchen, but it is small. You return in the room.</i> # player
//     ->motel_search
*   [Ask questions to the manager] Alright fellow, could I ask you something? # player #PlayerDBox #0 # NewCharacterSprite #4
//Interlocutor: Motel's manager
    How can I help? # otherCharacter
	-- (manager_questions)
	# jump
    **	[Ask about her occupation] Do you know what she did for a living? # player
    	Didn't ask. I didn't have a reason to. # otherCharacter
    	If you have what's necessary to pay your room, you can stay. # otherCharacter
    	But she often went outside at night. Sometime someone would wait for her outside. # otherCharacter
    ->manager_questions
    **	[Ask her name] Don't you happen to know her name? # player
		Not a lot of people come here under their real name. # otherCharacter
        So I don't bother to ask. # otherCharacter
        I heard someone call her Margaret one time. # otherCharacter
        But I would not give it too much thoughts. # otherCharacter
    # NewCharacterSprite #0
    ->manager_questions
    **	->
    	->motel_search
*->
I'm done here. # player #PlayerDBox #0
<i>(I leave without giving him a chance to believe that I'm not with the police.)</i> # player #PlayerDBox #1 #SFXPlay #17
# NewCharacterSprite #0 #NewBackground #5
<i>(Not too much about that girl... I'll check what I found at the office.)</i> # player
<i>(Where she worked, what's her last name... I need something.)</i> # player #NewNarrativeLog #13
# NewInvestigation
->END


/*--------------------------------------------------------------------------------
    Condor Club
    A bar hiding a casino and brothel. The prostitute used to work here.
--------------------------------------------------------------------------------*/

===condor_club===
{condor_entrance && knowledge_prostitute_name == 1 : ->condor_madam2}
{condor_entrance && knowledge_prostitute_name == 0 : ->condor_fail}
//Background Condor Club's street, no interlocutor
# NewNoBackground #13
<i>You arrive in front of the Condor Club and exit the taxi.</i> # player #PlayerDBox #1 #SFXPlay #14
<i>The club has a large colorful neon sign, which matches the signs of the other places of the street.</i> # player
<i>The Condor's seems to attract a lot of fancy people, men for the most part.</i> # player
<i>They don't seem to be here for the drinks.</i> # player
# jump
*   [Enter the Condor Club] <i>You enter the bar.</i> # player #SFXPlay #9
    ->condor_entrance

=condor_entrance
//Background Condor Club's entrance, no interocutor
# NewBackground #6 #MusicPlay #2
<i>Bugsy is in the club. The place is crowded.</i> # player
<i>There's hardly any stool left empty at the bar.</i> # player
<i>The rest of the room is packed, with music and agitation.</i> # player
# jump
*   [Go to the bar] <i>You approach the bar.</i> # player
- <i>You find an empty stool.</i> # player
<i>The barman does not seem to be interested in you. He's serving the other customers.</i> # player
~ temp number_drinks = 0
- (ordering_drink)
# jump
+   [{number_drinks == 0 : Put a bill on the bar | Put another bill on the counter}] <i>You put {|another} 10 dollars bill on the bar.</i> # player
    <i>{The barman immediately comes, seizes it, and a glass of whiskey appears. You didn't order anything.|The barman immediately comes, seizes it, and a glass of whiskey appears. You didn't order anything.| Again, same dance, he snaps the bill and pours you a drink, but seems more inclined to talk with you.}</i> # player
    <i>{He doesn't seem to consider you interesting (or rich) enough to engage with you.| Don't fool yourself, 20 dollars is not enough to look like a big shot in this city.|30 bucks, nearly all your weekly money. That should be enough.|You're really running low on money now...}</i> # player 
    ~ number_drinks = number_drinks + 1
    ->ordering_drink
+   [Hail the barman] {Boy I need to talk to you.|Are you deaf boy?|Can I talk to you?} # player #PlayerDBox #0
    {number_drinks >= 3 : ->condor_madam}
    <i>{&He doesn't even raise on eye on you.|He sees you, but prefer to go back to cleaning his glasses. Asshole.| MAybe if you get several drinks he would be forced to interact with you!}</i> # player #PlayerDBox #1
    ->ordering_drink

=condor_madam
<i>After a few drinks, the barman finally gets to you.</i> # player
//Interlocutor: Barman
TODO: Create barman sprite
What do you want? # otherCharacter #NewCharacterSprite #3
# jump
*   [Tell him who you are] You tell him your name. # player #PlayerDBox #0
- The barman tells you that he knows who you are. # otherCharacter
So does the woman that sits next to you. # otherCharacter #NewCharacterLog #6
//Interlocutor: Madam
# jump
*   [Tell her what you do] You begin to explain what you're doing here. # player
    She interupts you: she already knows it. # otherCharacter # NewCharacterSprite #6
    You're  a lonely fellow, coming here to enjoy the booze and music, without attracting any problem. # otherCharacter
    You say that you're not the one searching for problems here. # otherCharacter
*   [Hit on her] You try to seduce her. # player
    She stops you. Never with a client. It's a serious business here. # otherCharacter # NewCharacterSprite #6
    You say that it is not so serious, even from whorehouse's standards. # player
-
# jump
*   [Ask her about a missing girl] You ask her if a girl is missing. # player
-   She starts to get mad: she doen't run a brothel. # otherCharacter
She threatens to get you out of the club. # otherCharacter
# jump
*   {knowledge_prostitute_name == 1} [Tell her the name of the girl] You tell her that you found the body of Margaret O'Bannon. # player
    She immediately stops to speak. # otherCharacter
    She says to you to follow her. # otherCharacter
    She leads you through a door. # otherCharacter
    # jump # NewBackground #7 # Demo
    ->END
*   {knowledge_prostitute_name == 0} [Calm the situation] You try to calm her. You're not here to accuse anybody. # player
    You just want to know if anything happened. # player
    She gets angrier. Nothing happened, because there's no girl here. # otherCharacter
    She calls security. # otherCharacter
    <i>You're dragged to the exit.</i> # player #PlayerDBox #1
    //Background: Condor Club's street
    <i>You will have to come here again once you have the name of the girl.</i> # player
    ->END
*   {knowledge_prostitute_name == 0} [Threaten her] You threathen her to call the police if she does not cooperate. # player
    She's not impressed. Even a little amused. # otherCharacter
    You work for Abati, you couldn't even speak to the police. # otherCharacter
    She thinks that you should leave the club immediately. # otherCharacter
    You agree to leave. # player
    # NewCharacterSprite #0
    //Background: Condor Club's street
    <i>You will have to come here again once you have the name of the girl.</i> # player #PlayerDBox #1
    ->END

=condor_madam2
//Background: Condor Club's street, no interlocutor
# NewNoBackground #13
<i>You go back to the Condor Club. This time, you're sure. The girl's name is Margaret O'Bannon.</i> # player #PlayerDBox #1
<i>You enter the club.</i> # player #SFXPlay #9
//Background: Condor Club's entrance
# NewBackground #6
<i>You're in the club. As soon as you enter, the barman sees you.</i> # player
<i>He comes immediately to you, with a baseball bat.</i> # player
# jump # NewCharacterSprite #3
*   [Tell the girl's nickname] Before the man hits you, you tell him that you know what happened to Cara. # player #PlayerDBox #0
    <i>He immediately stops.</i> # player #PlayerDBox #1
    <i>You see the madam in the back, entering from an hidden door.</i> # player
    <i>The barman goes to her, whispers something to her.</i> # player
    <i>She's now closely watching you.</i> # player
    <i>The barman comes back.</i> # player
    He tells you to follow him. # otherCharacter
    He opens the door and tells you to enter. # otherCharacter
    <i>He closes the door behind you.</i> # player
    # NewBackground #7
    <i>You're now in the main part of the club: an hidden casino/brothel.</i> # player
    <i>The madam is waiting for you.</i> # player
    She asks you about Margaret. She hasn't seen her from two days. # otherCharacter 
    # jump # Demo
    ->END

=condor_fail
//Background: Condor Club's street, no interlocutor
# NewNoBackground #13
<i>You go back to the Condor Club.</i> # player #PlayerDBox #1
<i>But you're not so sure about what you could say without getting shot.</i> # player
<i>You decide to go back and work a better approach.</i> # player
->END