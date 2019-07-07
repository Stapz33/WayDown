VAR knowledge_Spaghetti = 0
VAR knowledge_prostitute_name = 0
VAR madam2 = 0
VAR client_seen = 0
VAR driverapp_seen = 0
VAR drugstore_seen = 0
VAR docker_seen = 0
VAR drugstore_firsttime = 0
// DEBUG mode adds a few shortcuts - remember to set to false in release!
VAR DEBUG = true
TODO: Remove Debug
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
	*	Condor Club[] #NewInvestigation #NewInvestigation #InspectorNameUnlock
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
<i>(Can't sleep tonight. Cars, gun shots, everybody in town wants to drive me crazy.)</i> # player #SFXPlay #23
<i>(And now the fuckin' phone is ringing.)</i> # player #SFXPlay #8
<i>(Don't even wants to answer, fuck it.)</i> # player 
- (phone) {<i>(The phone rings.)</i>|<i>(The phone doesn't stop.)</i>|<i>(It continues to ring.)</i>} # player 
# jump
	*	[Answer] <i>(This damn phone ain't gonna stop itself.)</i> # player
	+	[Try to sleep]{<i>(Ain't no time to call somebody, they'll wait tomorrow.)</i>|<i>(Could be a business call... Who am I kidding?)</i>|<i>(Twenty-four sheeps. Twenty-five sheeps. Twenty-six sheeps...)</i>|} # player #SFXPlay #23
		->phone
- <i>(I decide to get up and finally answer it.)</i> # player #SFXStop #8 #SFXPlay #28
# jump
	*	Are you out of your mind?[] Did you fucking see the time ? # player #PlayerDBox #0
- It's <color=red>James Lanza</color>. We had a problem with <color=red>Giovanni</color>. #otherCharacter #NewCharacterSprite #7 #NewCharacterLog #2
You need to come here immediately. # otherCharacter 
# jump
	*	What time is it[?] for fuck's sake? # player 
- 3AM, but we pay you to come even when you're shitting. #otherCharacter 
You understand me Rosenthal? Come here right now. # otherCharacter 
# jump
    *	What's the address[?] <color=red>Lanza</color>? # player 
- 98 Francisco Street . Hurry up, fucking stinks here. # otherCharacter 
# jump
    *   [Hang up] <> # player #NewCharacterSprite #0
    *   Don't tell me to hurry[] boy, it's the middle of the fucking night. # player
- <i>(I hung up and got out of bed.)</i> # player #PlayerDBox #1  #NewCharacterSprite #0 #SFXPlay #23
<i>(For fuck's sake, what did they do again?)</i> # player
# jump
    *   [Go to Francisco Street] <> # player # NewBigBackground #15 #SFXPlay #33
    #MusicPlay #1
-  (cab){<i>(Not even the time for a coffee, I put on my hat and go outside to find a cab.)</i>|} # player
# jump
    +   [{Hail a cab|Hail a cab again|Try to hail a cab}] <> # player #SFXPlay #15 #SFXPlay #35
        {<i>(That prick doesn't even slow down.)</i>-> cab|<i>(Am I covered in shit?)</i>-> cab|} # player
- <i>(As I'm slowly starting to lose my temper and head back to the office, a 47' Cadilloc slows by.)</i> # player #SFXPlay #36
Mr. Rosenthal? # otherCharacter # NewCharacterSprite #8 #SFXPlay #42
# jump  #PlayerDBox #0
    *   Ain't no Yid here kid[], get lost. # player 
        Ain't the time to joke Mr. Rosenthal. # otherCharacter
    *   Who's askin? # player
- Mr. <color=red>Lanza</color> sent me to get you, could you please get in the car? # otherCharacter 
# jump
    *   Alright, I'm coming[]. Couldn't he warn me that you were going to fetch me? # player
    *   Not so much of a choice[], am I right? # player
- <i>(I step into the car.)</i># player # NewBackground #10 #NewCharacterSprite #0 #PlayerDBox #1 #SFXPlay #14 #MusicPlay #3 #SFXStop #42
<i>(As soon as I seat, my nocturnal driver starts the engine and takes me to Francisco Street.)</i> # player SFXPlay #37
<i>(He rides smoothly and hardly ever speaks. Francisco is not so far, but he seems to take quite a lot of detours.)</i> # player #NewNarrativeLog #0
- (car_convo)
# jump # NewCharacterSprite #8
    *   Are you new in town, kid? # player #PlayerDBox #0 #SFXPlay #35
        Came here 6 months ago from Sicily. # otherCharacter 
        # jump
        **  <color=red>Lanza</color> got you in here? # player #SFXPlay #40
            Let's say that he needed the skills that I'm able to provide for his protection. # otherCharacter 
            # jump
        **  More Italians?[] Is there any place left in North Beach? # player
            I'm not here to settle down, Mr. <color=red>Lanza</color> asked me to come to ensure his protection. # otherCharacter #SFXPlay #37
        -- 
        **	What is he afraid of?[] He's not been linked to <color=red>Lima</color>, has he? # player #SFXPlay #38
        Not that I know of, the boss was smart enough to make sure that the organization would not be too much harmed. # otherCharacter
        So who's going after him? # player
        I can't say anything, but I'm sure Mr. <color=red>Lanza</color> will talk to you about it. # otherCharacter #NewNarrativeLog #1 #SFXPlay #37
        ->car_convo
    *   Can't you go directly to the apartment? # player #PlayerDBox #0 #SFXPlay #34
        Must make sure that we're not tailed sir # otherCharacter # NewCharacterSprite #8 #SFXPlay #40
        # jump
        **  Who would follow me?[] Am I a Jewish Marilyn? # player #SFXPlay #37
        //revoir 
            Let's say that we caught a lot of heat from the trial. # otherCharacter #SFXPlay #41
            # jump
        **  I ain't mixed with all that[] kid, <color=red>Jimmy</color> should have told you so. I'm a simple detective that helps some friends in need. # player
            Not you personnally, but we must stay on our guard with the trial. # otherCharacter #SFXPlay #41
        --  I don't get it, your boss <color=red>Lima</color> is judged, couldn't you find someone to take the blame? # player
            I don't know, that's not something that I was told. # otherCharacter #SFXPlay #37
        # jump
        **  Are you throwing me under the bus?[] I'll not go down easily motherfucker. # player #SFXPlay #38
            Don't be stupid, Mr. <color=red>Lima</color> cannot be saved by you. # otherCharacter
            So what am I doing here? # player #SFXPlay #40
        **  <color=red>Lima</color> didn't request my help for the trial[], what could I possibly do? # player
            You're not here to help on that matter. The Administration is working on it. # otherCharacter #SFXPlay #41
            So where do I fit in your plan now? # player
        --  <color=red>Giovanni</color> is dead. # otherCharacter #SFXPlay #35
        # jump
        **  Fuck... What happened to him? # player #SFXPlay #37
        //à changer
            You will discover it soon enough. # otherCharacter #NewNarrativeLog #2
            ->car_convo
    *->
- <i>(The ride continued in a complete silence.)</i> # player #NewCharacterSprite #0 #PlayerDBox #1 #SFXPlay #38
<i>(<color=red>Giovanni</color> is dead. Poor kid. And more Italians flooding North Beach.)</i> # player
<i>(Something's wrong. And I'm gettin' mixed in all that.)</i> # player
<i>(We finally park in Francisco Street.)</i> # player #MusicPlay #1 #SFXPlay #36
I let you go by yourself. Mr. <color=red>Lanza</color> is waiting for you inside. # otherCharacter # NewCharacterSprite #8 #NewCharacterLog #0 #SFXPlay #42 #SFXPlay #44
# jump
    *   Thank you kid # player #PlayerDBox #0 # NewCharacterSprite #8 #SFXPlay #14
    *   [Just a question...] Before I leave kid, can I ask you something? # player #PlayerDBox #0 #SFXPlay #14
        Go ahead. # otherCharacter
        Am I being set up? Are they trying to fuck me over to save their heads? # player
        Mr. Rosenthal, you're not important enough to worry yourself. # otherCharacter
        <i>(And the young prick leaves.)</i> # player #PlayerDBox #1 # NewCharacterSprite #0 #SFXPlay #37
- <i>(I cross the threshold of the building and go to the second floor. Apartment 237.)</i> # player #PlayerDBox #1 # NewCharacterSprite #0 #MusicPlay #0 #SFXStop #42

# NewBigBackground #12
<i>(<color=red>Jimmy Lanza</color> is waiting for me, near to the door.)</i> # player # NewCharacterSprite #5
For fuck's sake, ain't all Jew boys supposed to arrive on time? # otherCharacter 
# jump #PlayerDBox #0
*   Never when there are [Italians]greaseballs like you waiting for me at a crime scene. # player
    I'm not here to joke around, not with two bodies waiting two feet away. # otherCharacter
*   Fuck you <color=red>Lanza</color>[], I don't have to come like a dog whenever you fuck things up! # player
    You think I'm happy with this shit? I could have done without two more bodies. # otherCharacter
*   Calm down[], we have more important to do. Where is he? # player
    He? You mean them. We found a girl in the bathroom. # otherCharacter
- # jump
*   What do you mean?[] I thought <color=red>Giovanni</color> was the only one. # player
-   You gessed wrong. # otherCharacter
    We found a dead girl in the bathroom. # otherCharacter 
    # jump
*   For fuck's sake[...], you people really can't keep it in your pants. # player
    Show some respect, Rosenthal. We're not gonna always feed you. # otherCharacter
*   [The fuck was that good?] The fuck was so good that they both died? # player
    Stop jokin' around. # otherCharacter
- Someone killed them both, or they killed each other, but I don't think so. # otherCharacter
 But you're the expert here! So what you gonna do? # otherCharacter #NewNarrativeLog #3
-> lobby_apartment

/*--------------------------------------------------------------------------------

	Hub choice: discussing with Lanza or checking the apartment

--------------------------------------------------------------------------------*/

= lobby_apartment
#ActivateDiscussion
# jump
    *   I have some questions for you <color=red>Lanza</color>. # player #PlayerDBox #0
    ->lanza_dialogue
    *   I'll go and check on the bodies {lanza_stitch_first ==1:now|first}. # player #PlayerDBox #0
    ->check_apartment
    *->
    # NewCharacterSprite #5
    {lanza_stitch_first == 1:Enough talking|Come here!} # otherCharacter
    We need to go Rosenthal. I'll take you to your office # otherCharacter
    ->end_apartment
    
    
/*--------------------------------------------------------------------------------

	Discussing with Lanza

--------------------------------------------------------------------------------*/
    
=lanza_dialogue
-   (lanza_convo)
# jump # NewCharacterSprite #5
*   What can you say about <color=red>Giovanni</color>? # player #PlayerDBox #0
    He was made capo some months ago. Poor kid... # otherCharacter # NewCharacterSprite #5
    The Administration chose him to take the place of <color=red>Lima</color> during the trial. # otherCharacter
    So he was the one that was going to take the blame? # player
    It is what was agreed. #otherCharacter
    # jump
    **  You mean that you decided to throw him under the bus. # player
        No, we'll take care of his family. What he was gonna do for us was essential. # otherCharacter
        Now we're in deep shit... # otherCharacter #NewNarrativeLog #4
        ->lanza_convo
    **  What did he do to deserve it? # player
        Stop insinuating that we wanted him killed. He was not made for this job. # otherCharacter
        # jump
        *** Is it your opinion? # player
            It is mine, and Abati's too. Even <color=red>Morello</color> agreed. # otherCharacter
            And he's the fucking Consigliere, taking care of our soldiers and shit. # otherCharacter
            We agreed to tend for his family, at least for his mother. # otherCharacter
            We could not find anyone else. # otherCharacter
            # jump
            ****    Where does she live? # player
                    She's in Sicily. The boy came here on his own. Fuckin' American dream. # otherCharacter #NewNarrativeLog #5
                    ->lanza_convo
*   Who could want to harm him? # player #PlayerDBox #0
    Nobody that I would know of. # otherCharacter
    # jump
    **  Not even the girl's boyfriend? # player
        How can you say that she had a boyfriend? # otherCharacter
        <color=red>Giovanni</color> is Italian. You people really can't keep it in your pants. # player
        Even more when you can get in trouble by doing so. # player
        You knew <color=red>Tommy</color> a little. A good looking boy. # otherCharacter
        They would fall in his arms in a flash. #otherCharacter
        But always a gentleman with the ladies.  #NewNarrativeLog #6
    ->lanza_convo
*   So who's that girl? # player #PlayerDBox #0
    Can't say for sure, that's your job now. Nobody ever saw <color=red>Tommy</color> with her. # otherCharacter
    But we were not following him day and night. # otherCharacter
    # jump
    **  You mean you tailed him sometimes? # player
        Don't be stupid. I keep a close watch on everyone. # otherCharacter #NewNarrativeLog #7
    ->lanza_convo
*   What is happening with the organization? # player #PlayerDBox #0
    As you should know now, <color=red>Lima</color> was sent to jail, with a trial coming. # otherCharacter
    Abati took his place, and I second him now. # otherCharacter
    # jump
    **  Congratulations[!], Sotto-Capi. Is that your title now? # player
        That was meant to be temporary. We should have exonerated <color=red>Lima</color>. # otherCharacter
        But now all is going south # otherCharacter
    **  Too much complication to take the power. # player
        Again, stop your accusations. We were close to freeing the Boss. # otherCharacter
    --  Now <color=red>Lima</color> is taking full power, and I'll second him the best I can. # otherCharacter
    # jump
    **  What about <color=red>Morello</color>? # player
        The Consigliere? He's advising us. # otherCharacter
        But to be honest, I think that he's trying to take some distance with the game. # otherCharacter
        What can we say about it? That poor bastard earned to rest now. # otherCharacter
        We're putting the Family back on its feet, so we're focusing on greater matters. #otherCharacter
    ->lanza_convo
*   ->
~lanza_stitch_first = 1
->lobby_apartment 

/*--------------------------------------------------------------------------------

	Checking the apartment

--------------------------------------------------------------------------------*/

=check_apartment
#NewCharacterSprite #0 # NewBackground #0 #SFXPlay #16
<i>(I enter the apartment, only to find a first body. Poor <color=red>Tommy</color>.)</i> # player #PlayerDBox #1
<i>(Ok, let's focus now and do that methodically, ain't I a damn detective?)</i> # player 
# jump

*	[Check the livingroom] <i>(Nice apartment.)</i> # player
	<i>(Nothing special here, except some furnitures.)</i> # player
	<i>(And obviously, <color=red>Giovanni</color>'s body.)</i> # player 
	# jump
	**	[Check <color=red>Giovanni</color>] <i>(He's lying near the door.)</i> # player
		<i>(Poor boy.)</i>  # player # NewNoBackground #16
		<i>(His skull is completely shattered, which is caused by the obvious bullet hole on his forehead.)</i> # player
		<i>(Skull and brain pieces are all over the wall behind.)</i> # player 
	    # NewDocument #2
	    <i>(What does this poker chip doing here?)</i> # player #NewNarrativeLog #8
- <i>(So now, where would I search for clues?)</i> # player
- (clues_apartment)
# jump
*	[Check the bathroom] <i>(Maybe there could be something in the bathroom.)</i> # player #SFXPlay #17
	# NewBackground #11
	<i>(Oh that's right. The second body.)</i> # player
	<i>(The entire floor is covered in blood. Hers, obviously.)</i> # player 
	# jump
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
	#NewBackground #21
	<i>(It's tidy, with not a lot of furnitures.)</i> # player
	<i>(Only a bed, an empty desk with only one paper on it, and a closet.)</i> # player
	<i>(A love letter ?)</i> # player #NewDocument #14 #NewNarrativeLog #34
	<i>(The bed is still made. <color=red>Giovanni</color> didn't sleep here so much, or at least not yesterday.)</i> # player
	<i>(There's the closet then.)</i> # player
	# jump
	**	[Open the closet] <i>(What a freakin' creaking door!)</i> # player #SFXPlay #16 //change
		<i>(But I know seems logical, the closet being nearly empty.)</i> # player
		<i>(Only a few suits, some underwear.)</i> # player
		<i>(Not enough for someone to live here.)</i> # player
		<i>(Unless that person is a slog. But <color=red>Tommy</color> was not.)</i> # player #NewNarrativeLog #10
	->clues_apartment
	# jump
*->
<i>(Nothing more for me in that apartment.)</i> # player #NewCharacterSprite #0 #SFXPlay #20
# NewBackground #12 # DisablePlayer
~lanza_stitch_first = 0
->lobby_apartment

/*--------------------------------------------------------------------------------

	End of the Capo's apartment scene

--------------------------------------------------------------------------------*/

=end_apartment

<i>(<color=red>Lanza</color>, always keeping an eye on me, brought me back to my office on Broadway Street.)</i> # player #PlayerDBox #1
<i>(That day could not have been worse.)</i> # player #NewBackground #9 # NewCharacterSprite #0
<i>(What a night...)</i> #player
<i>(This freakin' mob is quite a mess.)</i> #player
<i>(Let's check if I have all the hierarchy in order.)</i> #player
<i>(So the boss <color=red>Lima</color> was emprisoned. His second, Abati, took his place.)</i> #player
<i>(<color=red>Abati</color> is now at the head of the entire family.)</i> #player
<i>(<color=red>Jimmy Lanza</color> is just behind him. As the underboss, he overlooks all the capi.)</i> #player
<i>(Couldn't they say captains like every american?)</i> #player
<i>(So each capo have soldiers under their orders.)</i> #player
<i>(And then there is all the associates. Nice way to call all the Jews, Russians and scum working for them.)</i> #player
<i>(And then there's <color=red>Morello</color>.)</i> #player
<i>(The elder, the Consigliere.)</i> #player
<i>(Quite out of the game. He's only advising, never getting his hands dirty.)</i> #player
<i>(What a mess... I should keep a diagram.)</i> #player
TODO: Create the hierarchy diagram
<i>(So now, let's check what we have here.)</i> #player
<i>(A mafioso that couldn't keep it in his pants, and got shot for it.)</i> #player
<i>(<color=red>Lima</color> eliminated, and the entire organization turned upside down by it.)</i> #player
<i>(And above that, this girl. We have nothing on her.)</i> #player
<i>(Maybe that's what I should work on.)</i> #player
<i>(Find who she is, where she lived.)</i> #player
<i>(Ok, let's get to work Bugsy.)</i> #player #NewInvestigation # Introspection
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
#NewNoBackground #5 # NewCharacterSprite #0 #MusicPlay #1
<i>(That should be where the girl lived.)</i> # player #PlayerDBox #1
<i>(What a shady motel...)</i> # player
<i>(I wouldn't stay here for a million bucks!)</i> # player
<i>(But I have to go in now.)</i> # player
# jump
*   [Enter the motel] <i>(Let's go.)</i> # player #SFXPlay #17 #SFXPlay #9
    ->motel_lobby

=motel_lobby
//Background Motel's lobby, no interlocutor at first
#NewBackground #14
<i>(That place could use some light.)</i> # player #MusicPlay #0
<i>(The only client that come here exit the place immediately, only passing by.)</i> # player
<i>(There's only this man. Seems to be the manager.)</i> # player #NewCharacterLog #4
# jump
*   [Go to the desk] <i>(Let's talk to him, that's my only lead.)</i> # player
//Interlocutor: Motel's manager
- Hello sir, what do you want? # otherCharacter # NewCharacterSprite #4
# jump
*   [Ask about the room] Good day fellow, I want to access one of your room. # player #PlayerDBox #0
- Alright, do you have the key? # otherCharacter
# jump
*   [Give him the key] Here it is, room 237. # player #SFXPlay #18
- <i>(He seems puzzled, as if I wanted to penetrate the White House.)</i> # player #PlayerDBox #1 #NewNarrativeLog #11
- And why would you want to go in there man? # otherCharacter
# jump
*   [I'm investigating] And why would you want to interfere in an ongoing investigation. # player  #PlayerDBox #0
    <i>(I see the fear in his eyes. That's not someone that wants to challenge the police.)</i> # player
    <i>(I could swear that he immediately peed his pants.)</i> # player
    Oh... hum, ok sir, if you would follow me. # otherCharacter #NewNarrativeLog #12
	-- (threat_manager)
*   [She's my girlfriend] She's my girl, but I don't have news from her since two days... # player #PlayerDBox #0
    Your girl... Why does she live alone then? # otherCharacter
    # jump
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
- <i>(That slow man finally seems to trust me, and leads me through the maze that is this shitty motel.)</i> # player #PlayerDBox #1 #SFXPlay #20
->motel_room

=motel_room
//Background Motel's room, no interlocutor
# NewBackground #3 # DisablePlayer #NewCharacterSprite #4
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
	**	[Go to the bedside table] <i>(This table is really the only remarkable object here.)</i> # player # NewCharacterSprite #0
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
*	[Check the desk] <i>(She seems to often use that desk given the amount of papers.)</i> # player # NewCharacterSprite #0
    <i>(What are those? Seems to be letters from a relative.)</i> # player
    <i>(I'll check that at the office.)</i> # player
    # jump # NewDocument #3
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
        I heard someone call her <color=red>Margaret</color> one time. # otherCharacter
        But I would not give it too much thoughts. # otherCharacter
        # NewCharacterSprite #0
    ->manager_questions
    **	->
    	->motel_search
*->
I'm done here. # player #PlayerDBox #0
<i>(I leave without giving him a chance to believe that I'm not with the police.)</i> # player #PlayerDBox #1 #SFXPlay #17
# NewCharacterSprite #0 #NewBigBackground #9 #MusicPlay #1
<i>(Not too much about that girl... At least I know where she lived.)</i> # player
<i>(But I still don't know anything else about her, not even her name. Let's work on that.)</i> # player #NewNarrativeLog #13 //change SFX (Taxi)#NewBackground #9

<i>(Maybe Gibbs could help me on that.)</i>#player
<i>(I hate to go to a cop, but let's face it, it as much a good cop as Lanza is.)</i>#player
<i>(He could even betray his own mother.)</i>#player
<i>(But he's useful. Let's go check on him.)</i>#player
#NewBackground #4
What do you want from me this time Mr Rosenthal?#otherCharacter
I couldn't find much on your wife, she's keeping a low profile right now.#otherCharacter
I'm not here for that Gibbs, I work for Lanza for now.#player
I need to find the name of a girl, she was found with one of Abati's men.#player
Could you give me anything on her?#player
Jesus, those Italians are always all guns out.#player
I could check on our records, but I'll need her full name.#otherCharacter
Why didn't you call the police?#otherCharacter
...#player
Gibbs, do you even think some times?#player
Do you see Lanza calling cops?#player
Yeah you're right...#otherCharacter
Anyway, I'll get you the name of that girl.#player
I'll help you with anybody Mr Rosenthal, feel free to come by.#otherCharacter
#NewBackground #9 #Introspection #NewCharacterSprite #0 #PlayerDBox #1
<i>(So now, where could I find that girl's name?)</i>#player
# NewInvestigation #InspectorNameUnlock 
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
<i>(So that's where the girl had her habits...)</i> # player #PlayerDBox #1
<i>(Doesn't seem like a place where a girl could enjoy herself.)</i> # player
<i>(And the men coming here should enjoy something different than their drinks.)</i> # player
<i>(Let's check this joint.)</i> # player
# jump
*   [Enter the Condor Club] <i>(Let's go.)</i> # player #SFXPlay #9
    ->condor_entrance

=condor_entrance
//Background Condor Club's entrance, no interocutor
# NewBackground #6 #MusicPlay #2
<i>(Wow.)</i> # player
<i>(I'm kinda disappointed.)</i> # player
<i>(I expected some people, drinks.)</i> # player
<i>(But it's just a nearly empty club.)</i> # player
<i>(Only some sad men at the counter, and a barman cleaning its glasses.)</i> # player
<i>(He seess me, and doesn't say a single word.)</i> # player
<i>(The glasses seem more important.)</i> # player
<i>(Maybe I can get his attention.)</i> # player #NewNarrativeLog #14
# jump
*   [Go to the bar] <i>(There's an empty stool left.)</i> # player #SFXPlay #17
<i>(He will not approach me. Maybe I look too much like a cop.)</i> # player
<i>(But I'll look like a fool if I leave now.)</i> # player
~ temp number_drinks = 0
- (ordering_drink)
# jump
+   {number_drinks <=4}[{number_drinks <=3 : Put {a|another} bill on the bar | Put my last bill on the counter}] <i>({10 bucks should be enough to make him talk.|Another bill, come on man.|This guy will drain me...|40 freakin'bucks, I'm completely bankrupt now.})</i> # player #PlayerDBox #1
    {Sir, can I have a word with you?|Ok boy now listen...|I want to speak to you!|Vodka I guess?} # player #PlayerDBox #0
    <i>({Not even a look at me.|It's like I'm just a robot handing bucks.|I want to crack that vodka bottle on his head so bad.|And vodka it is.})</i> # player #PlayerDBox #1
    <i>({He comes and snaps my bill.|My 10 bucks just... disappear.|30 freaking bucks in 5 minutes.|As I see my last bill going away, I'm quite eager now for that drink.})</i> # player
    <i>({Only to replace it with a glass of vodka.|Another vodka.|And another one.|That doesn't taste so bad after all.})</i> # player
    <i>({I don't even like vodka.|Not a word. Just a half-full glass.|I'm not drunk, but I'm not sober too.|I think I love vodka.})</i> # player
    {!<i>(But I won't waste a drink...)</i>|<i>(If my ex-wife was here...)</i>}  # player
    # jump
    ++	[Drink] <i>({I hate vodka...|It freakin' burns!|Not so bad... I think my throat is completely anesthetized|Nasdrovia!})</i> # player #SFXPlay #12
    ~ number_drinks = number_drinks + 1
    ->ordering_drink
+   [Hail the barman] {&Boy I need to talk to you.|Are you deaf boy?|Can I talk to you?|Excuse me man...} # player #PlayerDBox #0
    {number_drinks >= 3 : ->condor_madam}
    <i>({&He doesn't even raise on eye on me.|He sees me, but prefer to go back to cleaning his glasses. Asshole.| Is it a way to make me pay? It seems super effective.})</i> # player #PlayerDBox #1
    ->ordering_drink

=condor_madam
<i>(Now I'm not sober at all.)</i> # player #NewNarrativeLog #18
<i>(But he finally raises his eyes on me.)</i> # player
//Interlocutor: Barman
What do you want, sir? # otherCharacter #NewCharacterSprite #3
# jump
*   [Tell him who I am] I'm Bugsy Rose- # player #PlayerDBox #0
-	I know who you are. What do you want? # otherCharacter
# jump
*   [Tell him about the dead girl] I'm searching for a girl, kinda cute, she might have worked here. # player
-	It doesn't ring a bell for me sir. # otherCharacter
	You don't seem to be very cooperative... # player
	And you don't seem to be at your place here, please get out sir. # otherCharacter
	# jump
*	[Smash my drink on his head] You'll see if I'm not in my place... # player
	<i>(As soon as I put my hand on the glass, a woman puts a firm hand on my arm.)</i> # player # PlayerDBox #1
*	[Insult the barman] You fuckin' moron... # player
	<i>(But I don't have the time to finish, a woman appears and put her hand on my arm.)</i> # player # PlayerDBox #1
-	I'm sure this gentleman will remain calm now. # otherCharacter  # NewCharacterSprite #6
	You can go back to your work Dean. # otherCharacter #NewCharacterLog #6
//Interlocutor: Madam
# jump
*   [Tell her what you do] Hello miss, I'm searching for- # PlayerDBox #0
-	I know what you're here for darling. # otherCharacter
# jump
*	[Really?] Really, do you? # player
	Of course. # otherCharacter
	Like everyone else, you're here to enjoy the booze and the calm. # otherCharacter
	You'll whisper your sorrows to your drink, and you'll go back to your wife. # otherCharacter
	Without causing any problem here. # otherCharacter
	But I'm not the one searching for problems here. # player
*	[I'm here for you] Do you? Cause I could swear that I'm here for you now darling. # player
	I don't think you are. # otherCharacter
	I prefer to keep things professionnal here. # otherCharacter
	I'm proud of my business, and I desire to keep it serious. # otherCharacter
	First time I hear about a serious brothel... # player
-
# jump
*   [Ask her about a missing girl] Speaking of that, don't you happen to have a missing girl? # player
-   Excuse me sir? # otherCharacter
	I think you heard me well darling. # player
	And I think you don't understand something. # otherCharacter
	I'm not running a brothel. # otherCharacter
	Sure you're not. # player
	You're starting to get on my nerves man. # otherCharacter
	I might ask you to get out. # otherCharacter #NewNarrativeLog #22
# jump
*   [You had a girl working here under the name of...] #Validation #0
*   [Calm the situation] I'm not here to make any accusation. # player
	    I just want to know what happened here. # player
	    Nothing happened, it's a freakin'bar! # otherCharacter
	    I'll ask you to get the fuck out of here. #otherCharacter
	    Dean, throw this man out of here. # otherCharacter
	    Don't bother, I'm leaving. # player
	    <i>(I don't even let that Dean put his hands on me.)</i> # player #PlayerDBox #1
	    <i>(With the madam still looking at me, I exit the joint.)</i> # player #SFXPlay #9 #NewNarrativeLog #24
	    #NewCharacterSprite #0
	    //Background: Condor Club's street
	    #NewBackground #13 #MusicStop
*   [Threaten her] I'm not getting out. Or if so, I'll come back with a bunch of cops. # player
	    You're a pain in the ass but you're funny. # otherCharacter
	    As if someone working with Abati so publicly could even go near a cop! # otherCharacter
	    So now get the fuck out of here. # otherCharacter
	    Dean, could you send this man out? # otherCharacter
	    <i>(I don't even let that Dean put his hands on me.)</i> # player #PlayerDBox #1
	    <i>(With the madam still looking at me, I exit the joint.)</i> # player #SFXPlay #9 #NewNarrativeLog #25
	    #NewCharacterSprite #0
	    #NewBigBackground #9 #MusicStop
-   <i>(I'm lost. Nothing to link her to this place, except for a poor matchbox.)</i> # player
    <i>(I really need to dig more stuff on her.)</i> # player
    <i>(What could I find now?)</i> # player
    #Introspection
    ->END

= condor_back
	I... I'm not sure... Could you give me a moment? # player
	What a jock... # otherCharacter
	Dean, could you send this man out? # otherCharacter
    <i>(I don't even let that Dean put his hands on me.)</i> # player #PlayerDBox #1
    <i>(With the madam still looking at me, I exit the joint.)</i> # player #SFXPlay #9
    #NewCharacterSprite #0
    //Background: Condor Club's street
    #NewBigBackground #9 #MusicStop
-   <i>(I'm really not sure about anything.)</i> # player
    <i>(Is it already time for a drink?)</i> # player
    <i>(That's tempting...)</i> # player
    <i>(But I better have to work on that girl? Who were you <color=red>Margaret</color>?)</i> # player #NewNarrativeLog #26
    #Introspection
    ->END
 = condor_bad
 	I don't know anyone working here under that name, man or girl. # otherCharacter
 	And to be honest, that's not a typical prostitute name, if it's what your still insinuating. # otherCharacter
 	Now, you go man. # otherCharacter
	Dean, could you send this man out? # otherCharacter
    <i>(I don't even let that Dean put his hands on me.)</i> # player #PlayerDBox #1
    <i>(With the madam still looking at me, I exit the joint.)</i> # player #SFXPlay #9
    #NewCharacterSprite #0
    #NewBigBackground #9 #MusicStop
-   <i>(Did I miss something?)</i> # player
    <i>(Wasn't she working there under the name of <color=red>Margaret</color>?)</i> # player
    <i>(That being said, that's not a really appealing prostitute name.)</i> # player #NewNarrativeLog #27
    <i>(If that even means something...)</i> # player #Introspection
    ->END

= condor_good
	{madam2 == true :  ->Condor_Good_2Time}
	Cara, is that it? Of course it is. # player
    ... # otherCharacter
    You're a customer? # otherCharacter
    No, first time here. # player
    ... # otherCharacter
    Follow me please. # otherCharacter
    <i>(She gets close to a wall and push on it.)</i> # player # PlayerDBox #1 #SFXPlay #16
    <i>(An hidden door. What a good detective I am...)</i> # player #NewCharacterSprite #0
    <i>(I follow her, and get to enter in the real business of the joint.)</i> # player
    # NewBackground #7 #MusicStop
    <i>(The brothel.)</i> # player
    # NewCharacterSprite #6
    Do you know where's Cara? #otherCharacter
    I haven't seen her in two days. # otherCharacter
    ->condor_secondpart_madam

 = Condor_Good_2Time
	Cara! Don't you dare put your hands on me! # player #PlayerDBox #0
    <i>(I see in his eyes that I found it.)</i> # player #PlayerDBox #1
    Wait here for me, sir. # otherCharacter
    <i>(He goes to a wall and knocks on it.)</i> # player #SFXPlay #25
    <i>(The wall pivots, and he seems to whisper something to the person that is behind.)</i> # player #SFXPlay #16
    Sir, could you follow me please. # otherCharacter
    <i>(He nearly drags me through the door.)</i> # player #SFXPlay #17
    <i>(Now I'm really in the Condor Club)</i>
    #NewCharacterSprite #0
    # NewBackground #7 #MusicStop
    <i>(I haven't been to a lot of brothel. But this one is beyond anything else.)</i> # player
    <i>(And this gal is waiting for me.)</i> # player
    #NewCharacterSprite #6
    What happened to Cara? # otherCharacter 
    ->condor_secondpart_madam

=condor_madam2
 ~ madam2 = true
//Background: Condor Club's street, no interlocutor
# NewNoBackground #13
<i>({Now I'm sure. <color=red>Margaret</color> worked here!|Back at it. Let's try to avoid that gorilla-barman.})</i> # player #PlayerDBox #1
<i>({Let's see what that woman has to say about it.|Let's see what that woman has to say about <color=red>Margaret</color>.})</i> # player
# jump
+   [Enter the Condor Club] <i>(Let's go back in here.)</i> # player #SFXPlay #9
//Background: Condor Club's entrance
# NewBackground #6 #MusicPlay #2
-	<i>(I don't have time to cross the threshold, and that Dean is already on me.)</i> # player # NewCharacterSprite #3
	<i>(With a freakin' baseball bat.)</i> # player
# jump
+   [The girl worked here under the name of...] #Validation #0
-> END

=condor_secondpart_madam
#PlayerDBox #1
#NewCharacterSprite #6
<i>(She can't be serious.)</i> #player
<i>(No, she really seems to not know what happened to Cara.)</i> #player
So what happened to her Mr Rosenthal? #otherCharacter #NewNarrativeLog #28
- (whathappened) #jump
*   [She's dead] We found her dead at one of Abati's man's apartment. With the said man also dead. #player #PlayerDBox #0
        Jesus Christ... Poor girl, that will be bad for business. #otherCharacter
        So she worked in your brothel! #player
        Of course she worked here. She was the jewel of this establishment. #otherCharacter
        Any man that entered here would want to have her, at least for some hours. #otherCharacter
        That could lead to some kind of tension among the clients... But Dean would handle it in most cases. #otherCharacter
        ->whathappened
*   [How do you know my name?] {whathappened==2:But let's back up a second, how do you know who I am?| Wait, how do you know my name?} #player #PlayerDBox #0
        Don't be silly, you're like the Jewish dog of the mob. #otherCharacter
        Always preceding them. #otherCharacter
        I wouldn't be surprised if some dagoes came through that door now. #otherCharacter
        Don't worry, I'm alone here. <color=red>Lanza</color> gave me some leverage on this. #player
        Allow me to doubt it. They trust no one. #otherCharacter
        {whathappened<2:But you're not here to talk about me. What happened to Cara?} #otherCharacter
        ->whathappened
*	->
-   Can I ask you something Mr Rosenthal? #otherCharacter
    #jump
    *   [Call me Bugsy] Please darling, call me Bugsy. #player
    *   Go ahead. #player
-   Did your Italian fellow kill Cara? #otherCharacter
    I can't say for sure, but the bullet that he took in the head would have prevented that. #player
    So who killed them? #otherCharacter
    That's what I was hoping for you to help me with. #player
#jump
*   [Why was she outside?]What was she doing outside of the club last night. #player
-   Let's say that we provide a kind of premium service for long time customers... #otherCharacter
    We have some drivers that bring our girls directly to them. #otherCharacter
    They wait there, and then fetch them back here, or directly to there place. #otherCharacter
    Wait, so someone brought her to where she was found? #player
    Usually yes, but she was supposed to go join a client that we know of. #otherCharacter #NewNarrativeLog #29
-   (clientdriver)
#jump
*   [Who's the driver?] Do you know her driver for that night? #player #PlayerDBox #0
        Of course, he works with us for several months. #otherCharacter
        Did he come back here since last night? #player
        Now that you mention it, no he didn't. #otherCharacter
        You think that he's the killer? #otherCharacter
        Doesn't seem like the kind of man to do that. #otherCharacter
        I suppose that you want to check on him? #otherCharacter
        #jump
        **  [You bet] Of course, he's my prime suspect for now. #player
                Where can I find him? #player
                He lives in the Lombard Condominiums. #otherCharacter
                I'll check on him then. #player #NewNarrativeLog #23
        **  [Should I?] Do you think that I should? #player
                I don't know. Maybe. #otherCharacter
                This business really puts a pressure on you. #otherCharacter
                Maybe it's time for me to leave it. #otherCharacter
                I should try politics. #otherCharacter
                It's nearly the same work, yes. #player
                Where can I find your driver? #player
                In the Lombard Condominiums, near the Piers. #otherCharacter
                I'll give it a look then. #NewNarrativeLog #23
        --  ->clientdriver
*   [Who's the client?] So who was she supposed to meet last night? #player #PlayerDBox #0
        Some regular, quite fond of her. #otherCharacter
        He usually waits for her in a hotel room near the Fisherman's Wharf. He's staying at the Sheraton. #otherCharacter
            #jump
            **  [Any problem with him?] Did she have any problem with him? #player
                    Not at all, he was quite lovely. #otherCharacter
                    A little too pushy at times. #otherCharacter
                    He would often ask her to leave her... occupation, and marry him. #otherCharacter
                    #jump
                    *** A whore's lover?[] That's cute. #player
                            Only someone respecting women. #otherCharacter
                            Not a dog jumping on everyone. #otherCharacter
                            Does it apply to me? #player
                            Don't know, you tell me darling. #otherCharacter
                    *** [Is he that rich?] How much does it cost to have a full time hooker? #player
                            I don't know, how much is your wife charging you? #otherCharacter
                            #jump
                            **** [Fuck you madam] No offense, but fuck you. #player
                            **** [Try to make a joke] How ironic for you to say that! #player
                                    Why? #otherCharacter
                                    Because hum... #player
                                    You see... #player
                                    Ok nevermind, let's get back to business. #player
                                    Freakin' whore. #player #PlayerDBox #1
            --  So anyway, no problem with him, but he would often do strange things when he couldn't have her. #otherCharacter #PlayerDBox #0
                I see, a needy client. #player
                Right, something like that. #otherCharacter #NewNarrativeLog #30
                But Cara could handle it. She was good with that. #otherCharacter
                ->clientdriver
*	->
-   I don't think that I could give you anything else Mr Rosenthal. #otherCharacter
    Cara was really dear to me. I'll have to announce it to the other girls. #otherCharacter
    Can I ask you a last thing? #player
    Of course. #otherCharacter
#jump
*   [Do you know <color=red>Giovanni</color>?] Do you know <color=red>Tommy Giovanni</color>? #player
        I've only seen him once or twice, isn't he one of your men? #otherCharacter
-   He's the one who was found with Cara, at his place. #player
#jump
*   [Do they knew each other?] Did he ever come here to see her? #player
        Not that I know. Only came here for the drinks. #otherCharacter #NewNarrativeLog #33
        I don't think he was really... "interested" in what we offer. #otherCharacter
-   I'll leave you now Mr. Rosenthal. #otherCharacter
    Please find what happened to Cara. #otherCharacter
    She didn't deserve that. #otherCharacter
    ->condor_secondpart_morello
    
=condor_secondpart_morello
    <i>(She leads me to the bar, and goes back to the brothel.)</i>#player #PlayerDBox #1 #NewCharacterSprite #0 #MusicPlay #2
    <i>(Beautiful. But tough woman.)</i>#player # NewBackground #6
    <i>(Dean is still beyond his glasses, scrapping them, and keeping an eye on me.)</i>#player
    <i>(But someone else is.)</i>#player
    <i>(Small, with a constant grin on his face.)</i>#player
    <i>(Someone easily likeable, even if you're a little compelled to do so.)</i>#player
    Mr <color=red>Morello</color>... #player #PlayerDBox #0 #NewCharacterSprite #10
#jump
*   [Enjoying yourself here?] I didn't know that you appreciated the services of the club. #player
        Always enjoying a nice drink in every good place of this town. #otherCharacter
        Especially when our famous investigator is in there... #otherCharacter
        Only wanted to have a drink with you, that's all! #otherCharacter
        This would be a first time since I work for the family. #player
*   [What a coincidence!] How odd it is to find you here! Are you also keeping an eye on me? #player
        Why are you saying it like that? Why can't I only come for a friendly drink with you? #otherCharacter
        Cause you never did so since I work for the family? #player
-   I see... Let's say that it's a friendly drink between two workers in the family. #otherCharacter
#jump
*   [What do you want?] So what do you want from me Mr <color=red>Morello</color>? #player
-   Only for you to find what happened to <color=red>Giovanni</color>, like anyone else. #otherCharacter
    It's a great blow to our organization, and something needs to be done. #otherCharacter
    I'm not usually advising for a power showdown, but now we need to be tough. #otherCharacter
#jump
*   [Is the family that weak?] Is the Italian mob in so much trouble. #player
        We're not. But no one takes on the family. #otherCharacter
*   [Strange choice from you] I would not have thought that you would be the one advising for the strong method. #player
        I would not in normal times, but now we need to show that nobody can attack us. #otherCharacter
-   How can you be so sure that it is the work of someone else? #player
    And not only a hookup that went amiss? #player
    That's really not the type of <color=red>Giovanni</color>. #otherCharacter
    He was key to our organization. #otherCharacter
    We worked closely on a business together. #otherCharacter
#jump
*   [What was it?] What was this company? #player
-   Castelbueno Oil Company. #otherCharacter
    There's always oil somewhere with Italians... #player
    It's just something for the family to pour money into. #otherCharacter
    Actually, we had very little to do with <color=red>Giovanni</color>. #otherCharacter
    And even less money for ourselves. #otherCharacter
    So why would he be targeted? #player
    Because he was the future. #otherCharacter
    The bright young face of the family. #otherCharacter
    Maybe someone that would bury us all. #otherCharacter
#jump
*   [A threat?] Maybe a threat then? #player
-   More like a renewed family. #otherCharacter
    But anyway, now we'll try to repair that. #otherCharacter
#jump
*   [What's the name of the company?] What's the name of the company again? #player
    Castelbueno Oil Company. #otherCharacter
    Here, take a card. #otherCharacter #NewDocument #5
    But don't expect to order any oil from it. #otherCharacter #NewNarrativeLog #35
-   I'll leave you now. #otherCharacter
    Try to find who wants to harm us. #otherCharacter
    What else would I try to find? #player
    I don't know, but remember who's paying you Bugsy. #otherCharacter
    Then we'll be grateful, and know how to show it. #otherCharacter
    Goodbye then, until next time. #otherCharacter
    <i>(That old snake...)</i>#player #PlayerDBox #1 #NewCharacterSprite #0
    <i>(It's not surprising that he survived this long in that city.)</i>#player
    <i>(Let's get back to the office.)</i>#player
    # NewBigBackground #9 #MusicStop #2
    <i>(What a strange day.)</i>#player
    <i>(Now I have to lead to follow.)</i>#player
    <i>(The driver, and the client-lover.)</i>#player
    <i>(Quite a nice casting.)</i>#player
    <i>(The driver seems quite suspicious.)</i>#player
    <i>(Even more after disappearing.)</i>#player
    <i>(And the client could be the killer, a jealous lover that could not stand to see her with another.)</i>#player
    <i>(So he wacked'em both.)</i>#player
    <i>(Maybe the driver was killed trying to defend her.)</i>#player
    <i>(So many 'maybes'.)</i>#player
    <i>(It's time to dig on that and start to have some certainties.)</i>#player # NewInvestigation #Introspection
->END

=condor_fail
//Background: Condor Club's street, no interlocutor
# NewNoBackground #13
<i>(What am I doing here?)</i> # player #PlayerDBox #1
<i>(I don't even know the name of that girl!)</i> # player
# NewBigBackground #9
<i>(I might as well put my gun in my mouth myself.)</i> # player
<i>(Let's back up a bit.)</i> # player
<i>(Where could I find her name?)</i> # player #Introspection
->END

/*--------------------------------------------------------------------------------
    Driver Appartment
    
--------------------------------------------------------------------------------*/

===driver_appartment===
=driver_entrance
#NewCharacterSprite #0 #NewNoBackground #24 #PlayerDBox #1
{
	- driverapp_seen == 1: 
		->driver_alreadydone
 	- else: 
 		~ driverapp_seen = 1
}
#PlayerDBox #1 #NewBackground #24
{client_seen==true:<i>(So our killer lives here...)</i>|<i>(So that's where the driver lives...)</i>}#player
{client_seen==true:<i>(I'm kinda disappointed.)</i>|<i>(Nice place for a worker.)</i>}#player
{client_seen==true:<i>(I didn't picture a professionnal killer living in a place like that.)</i>|<i>(Maybe I could end up in a place like that.)</i>}#player
{client_seen==true:<i>(Anyway, let's check his place.)</i>|<i>(Stop dreaming Bugsy, time to work.)</i>}#player
#jump
*   [Enter the building] <i>(Let's go in there, he's at the 375.)</i>#player #NewBackground #22 #SFXPlay #17
-   <i>(This place is really calm.)</i>#player
    <i>(I would have expected a little more people in here.)</i>#player
    <i>(Here is 375. Let's check if he's here.)</i>#player
#jump
*   [Knock on the door]<i>(Will I be lucky?)</i>#player #SFXPlay #24
-   <i>(...)</i>#player
    <i>(It seems that I won't.)</i>#player
#jump
*   [Knock again]Is there someone in here?#player #PlayerDBox #0 #SFXPlay #24
-   <i>(Still no sound.)</i>#player #PlayerDBox #1
    You won't find anybody here sir.#otherCharacter #NewCharacterSprite #11
    Jesus Christ, who are you? #player #PlayerDBox #0
    Why do you creep on people like that?#player
    Sorry, I was quite afraid that the man from the other night was coming again.#otherCharacter
    Who are you talking about?#player
    The man that lived here?#player
    No, the man that came for him.#otherCharacter
#jump
*   [Which man?] Another man? Someone that used to come here often?#player
    No, first time that I've seen him.#otherCharacter
    Didn't look like the type of workers that live here.#otherCharacter
    Not like Cooper, the man that lives in that apartment.#otherCharacter
*   {client_seen==false}[Stop joking around] What are you saying? It was the man that lived there that you saw, don't you?#player
    No, it was someone else. I know the man that lives here.#otherCharacter
    Quite an handsome man.I think his name was Cooper. #otherCharacter
-   The man that came banged on the door.#otherCharacter
    Was not really trying to be discreet.#otherCharacter
    Then Cooper opened the door, and started to yell.#otherCharacter
    But he quickly stopped when the other man flashed a gun.#otherCharacter
*   [What did he look like?] Did you see his face?#player
-   No, I couldn't. He had a hat deeply set on his head.#otherCharacter
    But I heard his voice. Sounded quite Russian, or Slavic.#otherCharacter
    Anyway, a fellow from the east.#otherCharacter
    I also saw a mark of his arm when he drew his gun.#otherCharacter
    Something like a tattoo I think.#otherCharacter
*   [What happened then?] So what happened after he drew his gun? #player
    And how didn't he see you?#player
    But let's back up a little... who the hell are you?#player
    I live here, down the hall.#otherCharacter
    **  [Strange place for a girl like you] What is a gorgeous girl like you doing here?#player
        Let's say that the other tenants don't mind the traffic in my apartment.#otherCharacter
        Even less when they are part of this traffic.#otherCharacter
        By the way, if you want to come by someday, feel free to call me...#otherCharacter
    **  [Live here with your husband?] Ain't your husband at home?#player
        I live alone here, sir.#otherCharacter
        Not even a roommate?#player
        Oh I have quite some company stopping by. Even some of the men from the other apartments.#otherCharacter
        By the way, if you want to come by someday, feel free to call me...#otherCharacter
-   Anyway, so I was just opening my door to go and get the mail when I heard the shouts.#otherCharacter
    But what could a poor woman like me do between those two men?#otherCharacter
    Maybe go back to your apartment?#player
    Or I could hear what they were saying and help a handsome inspector passing by after that, don't you think?#otherCharacter
    So I stayed on my doorstep, and just took a peep sometimes.#otherCharacter
    They exited the building and took Cooper's car, an old grey pick-up truck parked outside.#otherCharacter #NewNarrativeLog #17
    Anything else useful about them?#player
    Don't think so, but you should be careful.#otherCharacter
    I heard that those Russian guys are quite wild.#otherCharacter
    I wouldn't want you to be hurt darling.##otherCharacter
*   How thoughtful[] from you, thank you miss.#player
-   I should get on his tail now.#player
    Have a good day.#player
{
	- client_seen == 1: 
		->driver_newinvestigation 
	- else: 
		->END
}
=driver_newinvestigation
#PlayerDBox #1 #NewCharacterSprite #0
<i>(What a woman.)</i>#player
<i>(I really seem to have an attraction with prostitutes.)</i>#player
<i>(Quite easy if you ask me.)</i>#player
<i>(Strip Bugsy from his coat and a few bucks, and see if I attract as many women.)</i>#player
<i>(Let's focus on what's important now.)</i>#player #NewBackground #24
<i>(Seems that the driver was a victim too.)</i>#player
<i>(But where is he now?)</i>#player 
#NewBackground #9
<i>(My guess would be that he's with the car, or ditched in a alley, with a hole in his head.)</i>#player
<i>(I cannot go on every corner of the city, so the car is the better lead to follow now.)</i>#player
<i>(But where would it be?)</i>#player
<i>(If I was the killer, I would get rid of it.)</i>#player
<i>(Maybe in an auto wreck yard, or a parking lot?)</i>#player #NewNarrativeLog #36
<i>(Time to do some boring investigator work.)</i>#player #Introspection
#NewInvestigation
->END

=driver_introspection
#PlayerDBox #1 #NewCharacterSprite #0
<i>(What a woman.)</i>#player
<i>(I really seem to have an attraction with prostitutes.)</i>#player
<i>(Quite easy if you ask me.)</i>#player
<i>(Strip Bugsy from his coat and a few bucks, and see if I attract as many women.)</i>#player
<i>(Let's focus on what's important now.)</i>#player #NewBackground #24
<i>(That Cooper was not the killer then.)</i>#player
<i>(But why a Russian?)</i>#player
<i>(Is there a beef between the families that I don't know of?)</i>#player 
#NewBackground #9
<i>(But I shouldn't try to get mixed with all this.)</i>#player
<i>(Let's focus on what matters now.)</i>#player
<i>(Who's the killer, and why was he at Giovanni's place.)</i>#player
<i>(There's still that shady last client, I shouldn't forget that.)</i>#player #Introspection
->END

=driver_alreadydone
<i>(The driver is not here anymore, that will not help.)</i> #player #PlayerDBox #1
->END

/*--------------------------------------------------------------------------------
    Client Hotel
    
--------------------------------------------------------------------------------*/

===client_hotel===
=client_entrance
{
	- client_seen == 1: 
		->client_alreadydone
 	- else: 
 		~ client_seen = 1
}
TODO: Sheraton Background (ASK TONY)
#PlayerDBox #1
<i>(So that's where Cara was supposed to come.)</i>#player
<i>(Let's see what her lover did to them.)</i>#player
//Elevator Background
<i>(So that woman told me that he stays at the 7th floor.)</i>#player #NewBackground #25
<i>(I'm not paid enough to take the stairs.)</i>#player
#jump
*   [Call the elevator]
-   <i>(...)</i>#player
<i>(...)</i>#player
<i>(It takes quite a long time...)</i>#player
<i>(...)</i>#player
TODO: SFX elevator ring
<i>(Finally!)</i>#player
<i>(Nobody in the corridor.)</i>#player #NewBackground #23
<i>(There never seems to be anybody in those fancy hotels.)</i>#player
<i>(But wait for a door to open and you'll see where those rich bastards are hiding.)</i>#player
<i>(So what was the room number again? I believe it's 738.)</i>#player
<i>(Let's check that.)</i>#player
<i>(...)</i>#player
<i>(Should be this one.)</i>#player
#jump
*   [Knock at the door] <i>(Is he still in here?)</i>#player #SFXPlay #25
-   <i>(...)</i>#player
    <i>(I can't hear anything.)</i>#player
    <i>(At least nothing discernable.)</i>#player
    <i>(There's kind of hushered sounds.)</i>#player
#jump
*   [Knock again] <i>(Let's try something.)</i>#player #SFXPlay #25
    #jump
    **  [Room service!] Hello sir, it's the room service, you ordered something.#player #PlayerDBox #0
        <i>(At least some sounds! He's getting close to the door.)</i>#player #PlayerDBox #1
        Oh that was quite fast, I ordered like a minute ago. #otherCharacter
        <i>(Stupid schmuck.)</i>#player #PlayerDBox #1
        #jump
        *** [Kick down the door] Enjoy your meal you shithead! #player #PlayerDBox #0
            What is... Oh God!#otherCharacter #NewCharacterSprite #1
    **  [Police sir, open up!] Sir, I'll need you to open right now, it's the police!#player #PlayerDBox #0
        W-w-wait, i-I haven't done anything sir!#otherCharacter
        <i>(Idiot, he's getting closer to the door, I can hear him.)</i>#player #PlayerDBox #1
        #jump
        *** [Kick down the door] No, but you're gonna take this door in the face! #player #PlayerDBox #0
            What is... Jesus!#otherCharacter #NewCharacterSprite #1
-   <i>(So that's our suspect...)</i>#player #PlayerDBox #1 #NewBackground #2
    <i>(An obese fellow rolling on the floor.)</i>#player
    <i>(Can't say that I'm not a little disappointed.)</i>#player
    Wh-who are you? What h-have I done?#otherCharacter
    Alright let me tell you a story fellow. #player #PlayerDBox #0
    Once upon a time, a man fell for a cute Irish prostitute.#player
    But seeing that she was not really receptive, he followed her.#player
    And saw her being 'friendly' with another.#player
    So he decided to eliminate the competition.#player
    What are you talking about?? #otherCharacter
    I haven't left the room since Cara left!#otherCharacter
#jump
*   So she was here?[] You didn't follow her after? #player
-   Not with a gun pointed at me, no!#otherCharacter
#jump
*   [What gun?] Did she have a gun?#player
-   Not her. Her freakin' driver!#otherCharacter
    I paid the Condor to have her for the whole night, but then after half an hour, he came to the door.#otherCharacter
    Saying that she had another client to see.#otherCharacter
    Can't believe it, the Condor used to be more serious!#otherCharacter
#jump
*   [Where did she go to?] Do you know where was this other client? #player
-   Didn't have the time to ask! #otherCharacter
    He took her by the arm, but she didn't seem to want to go with him.#otherCharacter
    So I tried to take her back to the room. #otherCharacter
    That's when he drew his gun. #otherCharacter
    Geez... #player
    Did something happen to her? #otherCharacter
#jump
*   [Can you describe him?] Did you see what he looked like? #player
-   Looked like a normal fellow, not skinny but not fat.#otherCharacter
    But he was quite large, about one or two heads above me. #otherCharacter
    And his eyes...#otherCharacter
    What about them?#player
    So cold... A pair of pale green eyes.#otherCharacter
    They could kill you, even without a gun under your nose.#otherCharacter #NewNarrativeLog #15
#jump
*   [Grow a pair man] Why don't you act like a man? #player
    Wait to be in front of him!#otherCharacter
    And then what?#player
    Then you'll see what man you are.#otherCharacter
*   [The devil himself?] Ain't that the devil himself?#player
    Don't know if he's the devil, but for sure he's gonna join him in the end.#otherCharacter
-   Do you know what he did to Cara?#otherCharacter
*   [Tell him the truth] She was killed with another man, most likely by the man that you saw last night.#player
    R-really?#otherCharacter
    Yes, fellow.#player
    **  [I'm sorry] Sorry for your loss.#player
        <i>(Let's leave this man alone, I cannot do anything else for him.)</i>#player #PlayerDBox #1
    **  [Stop whipping] Won't you stop weeping for a freakin' hooker? Jesus.#player
        <i>(For Christ's sake, why does he can't stop crying?)</i>#player #PlayerDBox #1
        <i>(Let's get the hell outta here.)</i>#player
*   [Lie to him] Hm... she was sent by the Condor Club to another client, like the man said.#player
    What can I say, she was popular.#player
    I can't argue with that.#otherCharacter
    But that's not realy professionnal, the club will hear from me.#otherCharacter
    If you say so...#player
    <i>(Let this man hope a little, maybe he'll forget her.)</i>#player #PlayerDBox #1
-	{
	- driverapp_seen == 1: 
		->client_newinvestigation 
	- else: 
		->client_introspection
	}
=client_newinvestigation
-   <i>(So our man went here, after taking the driver's car.)</i>#player #NewCharacterSprite #0 #NewBackground #23
-   <i>(But where's the driver now?)</i>#player 
	#NewBackground #9
    <i>(Obviously killed.)</i>#player
    <i>(But you cannot kill a car.)</i>#player
    <i>(He must have got rid of it somewhere. Too recognizable.)</i>#player
    <i>(A parking lot? No, most surely a auto wreck yard.)</i>#player #NewNarrativeLog #36
    <i>(Where could I find one?)</i>#player #Introspection
    #NewInvestigation
->END

=client_introspection
-   <i>(Who's that driver?)</i>#player #NewCharacterSprite #0 #NewBackground #23
    <i>(I didn't picture a freakin' cold blooded killer.)</i>#player
    #NewBigBackground #9
    <i>(I should check where he's living. But I guess that he'll not be there. Obviously.)</i>#player #Introspection
->END

=client_alreadydone
<i>(I can't do anything else for that man.)</i> #player #PlayerDBox #1
->END

/*--------------------------------------------------------------------------------
    Wreck Yard
    
--------------------------------------------------------------------------------*/

===wreckyard===
=wreckyard_entrance
#NewNoBackground #28 #PlayerDBox #1
<i>(I couldn't imagine a place like that in San Francisco.)</i>#player
<i>(It looks like I'm not in the city anymore.)</i>#player
<i>(That's quite nice. Calm.)</i>#player
<i>(No gun shots, no police sirens...)</i>#player
<i>("Rosenthal Auto Repair")</i>#player
<i>(I'll think about it.)</i>#player
<i>(Let's check if they saw the car.)</i>#player
#jump
*   [Enter the wreck yard]  <i>(Is there anybody?)</i>#player
-   Hello? #player #PlayerDBox #0 #NewBackground # 26
    ... #player
    Is anybody here?#player
    Yeah yeah, just wait a second.#otherCharacter
    Ok, I'll wait outside.#player
    ...#player
    Morning mister, what do you want?#otherCharacter #NewCharacterSprite #15
#jump
*   [Looking for a car] Did someone drop a car here last night? Like an old grey pick up truck, or something like that.#player
    Indeed someone quite shady did that.#otherCharacter
    Came here nearly banging the gate.#otherCharacter
    It was my shift last night, I thought the communists were bombing us. Freakin' pinkoes.#otherCharacter
    Did you see who the man was?#player
    Only saw him when he escaped the facility.#otherCharacter
    He was running pretty fast.#otherCharacter
    Any chance that you managed to take a look at his face.#player
    Could only see his hair. Grey ones, with a kind of medium cut.#otherCharacter
*   [Looking for someone] I'm looking for someone that could have come here last night, does it ring a bell?#player
    Indeed there was a man that came here.#otherCharacter
    He nearly banged the gate with a car, like a grey rusty pick up truck.#otherCharacter
    Is that the one you're looking for?#otherCharacter
    Seems to be. Tall guy? With tattoos?#player
    He seemed tall yes, but I couldn't see if he had tattoos or not.#otherCharacter
    I could only see the back of his head when he escaped the wreck yard.#otherCharacter
    Something like a medium cut, with grey hair.#otherCharacter #NewNarrativeLog #16
-   So he left on his feet?#player
    Yeah, could have dropped the car anywhere in the yard, it's a hell of a jungle here.#otherCharacter
#jump
*   [Can you guess where it is?] Can you guess where he could have dropped the car?#player
    I don't have weeks to find him.#player
-   Follow me, I'll help you.#otherCharacter
    Nothing else to do today, might as well help a police officer.#otherCharacter
#jump
*   [I'm not with the police] I don't work with the police.#player
    So I'll ask you to stay discreet about what we could find.#player
    Exactly what a cop would say, don't worry sir.#otherCharacter
    Follow me sir.#otherCharacter #NewBackground #27
-   Is this the car?#player
    I think so, it was not here yesterday.#otherCharacter
    Pretty damaged don't you think.#player
    I know, but that could easily be done by someone trying to cover his track, don't yo think officer?#otherCharacter
    I'm not a c...#player
    Yes you're right.#player
#jump
*   [Approach the car] Let's see what we can find in here.#player 
	#NewBackground #33
-   What the...#otherCharacter
    Yes, he's one of the men that I was hoping to find.#player
    <i>(So our killer wacked the driver, and put him in the trunk.)</i>#player #PlayerDBox #1
    <i>(That some Sicilian-level of savagery.)</i>#player
    <i>(Or Russian, I really need to find our man, before he causes more damage to the organization.)</i>#player
    Officer?#otherCharacter #PlayerDBox #0
    Yes?#player
    Am I in trouble?#otherCharacter
    Never had a dead body in my yard.#otherCharacter
    Don't worry, I'll send somone to get rid of the body.#player
    Ok...#otherCharacter
#jump
*   [Look in the car] I'll take a look inside, see if our man dropped something.#player #NewCharacterSprite #0
-   <i>(...)</i>#player #PlayerDBox #1
    <i>(Pretty neat interior for a wrecked car. That man is right, it was faked.)</i>#player #NewDocument #9
    <i>(What's this?)</i>#player
    <i>(Could have been left by the killer.)</i>#player
    <i>(I need to get out of here, before real cops show up.)</i>#player
    Alright sir, my work here is done, I'll go back to the precinct.#player #PlayerDBox #0
    But what do I do with the body?#otherCharacter #NewCharacterSprite #15
    I'll send someone, I told you.#player
    Stay by it please.#player
    O-ok officer...#otherCharacter
    #NewBackground #28 #PlayerDBox #1 #NewCharacterSprite #0
    <i>(What a fool.)</i>#player
    <i>(Those people these days. That listen too much to the radio.)</i>#player
    <i>(They think that they can do police work.)</i>#player
    <i>(I'm curious to know how long that dumb man will stay near the body.)</i>#player
    #NewBackground #9
    <i>(So let's go over what we have.)</i>#player
    <i>(A killer that vanished in the nature. Most likely Russian.)</i>#player
    <i>(A driver, with a bullet in the head. Most likely dead.)</i>#player
    <i>(Not a really good lead to follow.)</i>#player
    <i>(All the people that I talked to saw some bits of the killer.)</i>#player
    <i>(Maybe I could try to compose a complete profile and go check on Gibbs.)</i>#player
    <i>(Those lazy cops need to get some work.)</i>#player
    #Introspection #NewInvestigation #CSUnlock
->END

/*--------------------------------------------------------------------------------
    Killer Appartment
    
--------------------------------------------------------------------------------*/

===killer_appartment===
=killer_entrance
#NewBackground #1 #PlayerDBox #1 #NewCharacterSprite #0
<i>(Quite easy to enter Valery's apartment.)</i>#player
<i>(I'm even a little disappointed.)</i>#player
<i>(What killer would keep his place so easily accessible?)</i>#player
<i>(Anyway, it makes my job easier.)</i>#player
<i>(So what do we have here?)</i>#player
<i>(Quite a nice place!)</i>#player
<i>(Only a little messy.)</i>#player
<i>(There's a lot of trash everywhere.)</i>#player
#jump
*   [Examine the bookcase] <i>(There's really a lot of books.)</i>#player
-   <i>(Books on everything: history, politics, comics...)</i>#player
<i>(Most of them in Russian. How did he manage to get so much of them?)</i>#player
<i>(I don't think that you can purchase them easily nowadays.)</i>#player
<i>(What else do we have...)</i>#player
<i>(What are those?)</i>#player #NewDocument #8
<i>(Dozens of receipts.))</i>#player
<i>(With the same order everytime...)</i>#player
<i>(What else...)</i>#player
#jump
*   [Look below the couch] <i>(What do we have here?)</i>#player #NewDocument #6
-   <i>(Again, dozens of what looks like... restaurant menus?)</i>#player
    <i>(Quite weird.)</i>#player
    <i>(What's the meaning of all that?)</i>#player
    <i>(The rest of the apartment is quite clean.)</i>#player
    <i>(And still no traces of Chtcherbina.)</i>#player
    <i>(But again, not really surprising, this guy wouldn't come back after killing a guy.)</i>#player
    <i>(Especially after leaving so many clues.)</i>#player
    <i>(Time to go back to the office.)</i>#player 
    #NewBackground #9
    <i>(So basically, I have nothing on Chtcherbina.)</i>#player
    <i>(I don't know where he is.)</i>#player
    <i>(Who did he spend time with, and where.)</i>#player
    <i>(I need to locate him.)</i>#player
    <i>(Did I miss something?)</i>#player
	#NewInvestigation #Introspection
->END

/*--------------------------------------------------------------------------------
    Drugstore
    
--------------------------------------------------------------------------------*/

===drugstore===
=drugstore_entrance
#NewBackground #17 #PlayerDBox #1 #NewCharacterSprite #0
{
    -drugstore_seen ==1: ->drugstore_alreadydone
    -drugstore_firsttime ==1:->drugstore_validation
}
<i>(So... a drugstore.)</i>#player
<i>(Given the amount of receipts that I found at his apartment, Valery really spent a lot of time here.)</i>#player
<i>(But there's nothing really interesting in this shop.)</i>#player
<i>(No client, only one man behind the counter....)</i>#player
<i>(He said nothing when I entered, but he keeps a close eye on me.)</i>#player
<i>(I can feel it...)</i>#player
<i>(He isn't the type of fellow that blends well in a drugstore.)</i>#player
<i>(That's odd...)</i>#player
#jump
*   [Go to the counter] <i>(Let's see what this man is doing here.)</i>#player
#NewCharacterSprite #2 #PlayerDBox #0
-   Hi, could you help me with something?#player
    Hello sir, of course, do you want to buy something?#otherCharacter
    ~drugstore_firsttime=1
#jump
*   [Yes please] Of course, what else would I want in a drugstore?#player
    ->drugstore_validation
*   [Just want some information] Actually I was hoping to you somet...#player
    I'm afraid that I cannot help you sir, have a good day.#otherCharacter
    But I...#player
    Have a good day, sir.#otherCharacter #PlayerDBox #1
    <i>(But... He's chasing me out of the store!)</i>#player #NewCharacterSprite #0
    #NewBackground #9 #Introspcetion
    <i>(This place is strange...)</i>#player
    <i>(They're hiding something.)</i>#player
    <i>(But what could that be? It must be linked to Chtcherbina's activities.)</i>#player
    <i>(How can I make this clerk talk?)</i>#player
->END

=drugstore_validation
So what do you want to buy sir?#otherCharacter #Validation #1
->END

=drugstore_validation_wrong
Sorry, but I don't think that we have that in store.#otherCharacter
Have a good day.#otherCharacter #PlayerDBox #1
<i>(Without even letting me say a word, he ushers me out of the store. What the hell...)</i>#player
#NewBackground #9 #Introspection
<i>(Did I miss something?)</i>#player
->END

=drugstore_validation_cancel
I... I don't have anything specific in mind.#player
Then I think that I will not be able to help you sir.#otherCharacter
Have a good day.#otherCharacter #PlayerDBox #1
<i>(Without even letting me say a word, he ushers me out of the store. What the hell is this place...)</i>#player
#NewBackground #9 #Introspection
<i>(Do I miss something?)</i>#player
->END

=drugstore_validation1_good
I'd like a bottle of Irdin vodka please.#player
Alright, we just received a shipment of that brand.#otherCharacter
What else will you need?#otherCharacter #Validation #2
->END

=drugstore_validation2_good
Do you have some Jack's corns?#player
I'm starving.#player
Right away sir.#otherCharacter
Anything else?#otherCharacter #Validation #3
->END

=drugstore_validation3_good
I'll have a pack of Jack's cigarettes please.#player
Sir, we don't have that in store.#otherCharacter
I know.#player
Alright, could you follow me please?#otherCharacter
Oh... yeah sure.#player
->drugstore_backroom

=drugstore_backroom
#NewCharacterSprite #0 #PlayerDBox #1
<i>(I knew that this guy was hiding something!)</i>#player #NewBackground #18
<i>(A full ammunation.)</i>#player
<i>(My guess is that it was Valery's first supplier.)</i>#player
#NewCharacterSprite #2
Welcome to the Emporium sir.#otherCharacter #PlayerDBox #0
I can't remember your face, although you know the password.#otherCharacter
How did you came accross our store?#otherCharacter
#jump
*   [Friend of mine, Valery] A good pal, Valery Chtcherbina, couldn't stop talking about you.#player
    I had to see it with my own eyes.#player
    This name doesn't ring a bell.#otherCharacter
*   [Ad in a magazine?] Aren't you advertised in magazines?#player
    I could swear that I could see a 'Two for one' coupon for your store!#player
    As you would understand, we like to keep a low profile.#otherCharacter
    Always in the interest of our clients, of course.#otherCharacter
    Clients like Valery Chtcherbina?#player
    Can't tell you sir.#otherCharacter
-   Really? You want to do this?#player
#jump
*   [Menace him] Aren't you on Abati's territory?#player
    I suppose that Lanza would love to come and visit this shop...#player
    Cut the crap pal.#otherCharacter
    I managed to put this business on his feet, without any problem.#otherCharacter
    I want to keep it that way.#otherCharacter
    Tell me what you know about Chtcherbina then.#player
    He's a regular customer.#otherCharacter
    I think that I'm his only supplier, given the amount of weapon that he buys.#otherCharacter
    When was the last time that he came here?#player
    About two days ago. Wanted a simple handgun and ammos.#otherCharacter
    Not the most singular order.#otherCharacter
    Did he came alone?#player
    Yes, always.#otherCharacter
    Came with his car, a really noticeable Chebrillet Deluxe.#otherCharacter #NewNarrativeLog #19
    Maybe a little too noticeable.#otherCharacter
    That's how you find his track?#otherCharacter
    Not really, he killed one of Abati's capi.#player
    For God's sake...#otherCharacter
    Can't say that it won't be good for business.#otherCharacter
    All those Italians and Russians shooting each other...#otherCharacter
    But now I'm afraid that I won't be of any help in your hunt.#otherCharacter
    I don't want to take side in this.#otherCharacter
    That's not what I asked. I'll leave your shop, and you won't hear about me again.#player
    Have a good day then.#otherCharacter
    ~drugstore_seen = 1
    {
    	- docker_seen == 1: 
    		->drugstore_newinvestigation 
    	- else: 
    		->drugstore_introspection
    }


->END

=drugstore_alreadydone
<i>(I will not find Chtcherbina here.)</i>#player
<i>(I need to dig somewhere else.)</i>#player
->END

=drugstore_newinvestigation
#NewCharacterSprite #0 #PlayerDBox #1
<i>(Chtcherbina, you Russian bastard.)</i>#player
<i>(They really seem to be behind this.)</i>#player 
#NewBackground #9
<i>(But... it doesn't really hold up.)</i>#player
<i>(Why would they want to attack their allies?)</i>#player
<i>(The two organization are really close since the war.)</i>#player
<i>(Anyway, all the evidences point to Chtcherbina.)</i>#player
<i>(But still, I can't find him.)</i>#player
<i>(Where could he be?)</i>#player
#NewInvestigation #Introspection
->END

=drugstore_introspection
#NewCharacterSprite #0 #PlayerDBox #1
<i>(This really stinks.)</i>#player
<i>(Those Russian seems to appear quite often in the picture.)</i>#player #NewBackground #9
<i>(But that still will not give me the location of Chtcherbina.)</i>#player #Introspection
<i>(Where could I find his track again?)</i>#player
<i>(Time to go back to work.)</i>#player
->END

/*--------------------------------------------------------------------------------
    Restaurant
    
--------------------------------------------------------------------------------*/

===restaurant===
=restaurant_entrance
#NewBackground #30 #PlayerDbox #1 #NewCharacterSprite #0
<i>(Given all junk that I found in his place, Chtcherbina seemed to be a regular customer of this place.)</i>#player
<i>(More Russian stuff. That stinks.)</i>#player
<i>(Maybe he just liked the food here?)</i>#player
<i>(Or it's a front for the mob...)</i>#player
<i>(Please, let's hope that it's just the food.)</i>#player
#jump
*   [Enter the diner] <i>(Let's get a taste of this place.)</i>#player
#NewBackground #8
-   <i>(There's quite a lot of people in here.)</i>#player
<i>(No sign of Chtcherbina, obviously.)</i>#player
<i>(Would have been a strange time for a pie.)</i>#player
#jump
*   [Go to a table] <i>(Maybe I can ask something to a waitress...)</i>#player
-   <i>(...)</i>#player
<i>(Do I smell bad?)</i>#player
<i>(Nobody seems to be interested in taking my order.)</i>#player
I'm happy to finally meet you, Mr Rosenthal.#otherCharacter
<i>(Who's talk...)</i>#player
#NewCahracterSprite #15 #PlayerDBox #0
Mind if I join you?#otherCharacter
#jump
*   [Please, be my guest] Of course, but I really enjoy knowing who sits at my table.#player
    My restaurant, my rules. You won't need my name.#otherCharacter
*   Do I have the choice?[] It doesn't really seems so.#player
    Of course. Chatting with me, of getting killed.#otherCharacter
    You're quite a straightforward person, Mr...#player
    You won't need my name.#otherCharacter
-   If you say so...#player
    So why are you going after Valery?#otherCharacter
#jump
*   I think you already know it[], sir.#player
    Actually I don't.#otherCharacter
    But I know that you're searching for him in the entire city.#otherCharacter
    You even went to his apartment.#otherCharacter
    So don't try to make me angrier.#otherCharacter
*   Am I? Are you sure?[] What could make you believe that?#player
    Maybe the fact that you're looking for him all over the city.#otherCharacter
    You even went to his apartment.#otherCharacter
    So don't try to make me angrier than I already am.#otherCharacter
-  But why would I tell you what you already know?#player
That you sent Chtcherbina to wack one of Abati's capi.#player
...#otherCharacter
#jump
*   [Nothing to say?] Ain't got nothing to say to that?#player
    Or will you just threaten me?#player
    What difference will it make?#otherCharacter
    In either case, the war with Abati seems inevitable.#otherCharacter
    You wouldn't believe me if I told you that we have nothing to do with it.#otherCharacter
    #jump
    **  [Why would I?]  Hard to believe in this.#player
-   I presume that you're with the Russian mob.#player
    You have everything to win from a weaker Italian family.#player
    And you're aware of that.#player
    Of course.#otherCharacter
    But for the moment, we don't really need to take over.#otherCharacter
    What Valery did, if he really did so, was not under my orders.#otherCharacter
    Or anyone else that I could know of.#otherCharacter
    Valery did this on his own, he was used to do that.#otherCharacter
    Allows him to make an extra buck or two.#otherCharacter
    But the deal was that it would never impact us.#otherCharacter
    Which is clearly not the case here.#otherCharacter
    I allowed him to conduct his business in a safe environment like this one.#otherCharacter
    As a matter of fact, he encountered someone here some days ago.#otherCharacter
#jump
*   [Who was it?] Did you see the man that came to see him?#player
-   As I said, I don't want to be involved, so I leave him alone.#otherCharacter
    But that needs to change obviously.#otherCharacter
    I only know that he operated from the docks, near the The Embarcadero. 
    As a gesture of good faith to you and Abati, I'll let you chase him.#otherCharacter #NewNarrativeLog #32
    Under what condition?#player
    Don't kill him.#otherCharacter
    He's a brother, as everyone here.#otherCharacter
    We'll deal with him.#otherCharacter
    Now you'll have to excuse me, I have a restaurant to run.#otherCharacter
    I don't expect to see you too much again Mr Rosenthal.#otherCharacter #PlayerDBox #1
    <i>(So the Russian mob is involved.)</i>#player
    <i>(Or is it?)</i>#player
    <i>(It was quite a peaceful time in San Francisco, why trying to jeopardize that?)</i>#player
    <i>(Let's hope that I won't get caught in the middle of the fire.)</i>#player #NewBackground #9
    <i>(If that man said the truth, Chtcherbina worked on his own.)</i>#player
    <i>(But why? Or for who?)</i>#player
    <i>(Maybe I will find his track again at the docks.)</i>#player #Introspection #NewInvestigation
->END

/*--------------------------------------------------------------------------------
    Dockers
    
--------------------------------------------------------------------------------*/

===dockers===
=dockers_entrance
#NewNoBackground #19 #PlayerDBox #1 #NewCharacterSprite #0
{
	- docker_seen == 1: 
		->dockers_alreadydone
 	- else: 
 		~ docker_seen = 1
}
<i>(So that should be where Chtcherbina came.)</i>layer
<i>(On one of the longest dock in the world...)</i>#player
<i>(Everybody seems to want to give me a hard time.)</i>#player
<i>(Let's try with the entrance first.)</i>#player
#jump
*   [Go to the entrance] Hello, is there anyone?#player #PlayerDBox #0
-   Hello sir, how can I help you?#otherCharacter #NewCharacterSprite #12
#jump
*   [Looking for someone] I'm looking for a fellow that came here, tall guy, very Russian-type, does it ring a bell?#player
-   I'm sorry sir, but the piers are almost 3 miles long.#otherCharacter
    Quite difficult to spot anyone that come and goes.#otherCharacter
    Don't you keep a record?#player
    Of course, but only of the cars entering and exiting.#otherCharacter
    You don't really see people walking in here.#otherCharacter
    Are you a cop?#otherCharacter
#jump
*   Yes sir[], I'm asking for your cooperation on that investigation.#player
    Of course sir, right away.#otherCharacter
*   [Only looking for a friend]No I'm not, I was looking for my pal Valery, he told me to met him here.#player
    But he didn't gave me the pier, and I'm not gonna run across the entire 3 miles.#player
    Strange place to meet someone...#otherCharacter
    He loves the sea.#player
    But it's the San Francisco's Bay.#otherCharacter
    Anyway, can I have them? #player
    If you want.#otherCharacter
-   Which days?#otherCharacter
    What?#player
    Which day's records do you want?#otherCharacter
    We keep all the books since 1912.#otherCharacter
    The couple last days should be enough.#player 
    So here's the first page.#otherCharacter #NewDocument #10
    Let me check a second... #otherCharacter
    And here's the second.#otherCharacter #NewDocument #11
    Will bring them back quickly?#otherCharacter
    As soon as I find what I'm looking for.#player
    Which will take...#otherCharacter
    Goodbye sir, and thanks for your help.#player
    <i>(What a good fellow.)</i>#player #NewCharacterSprite #0 #PlayerDBox #1
    <i>(Just dumb enough.)</i>#player
    {
    	- drugstore_seen == 1: 
    		->dockers_newinvestigation 
    	- else: 
    		->dockers_introspection
    }
=dockers_alreadydone
#NewNoBackground #19 #PlayerDBox #1 #NewCharacterSprite #0
<i>(They won't help me here, I need to find the killer by myself.)</i> #player
<i>(And they say that I don't have a tough job...)</i>#player
->END

=dockers_newinvestigation
<i>(So now I know that Chtcherbina came here not long ago.)</i>#player
<i>(But he could be anywhere in those piers...)</i>#player #NewBackground #9
<i>(I need to think, and fast.)</i>#player #Introspection
<i>(He could disappear anytime soon.)</i>#player
<i>(Let's find if someone gave me useful information to track him.)</i>#player #NewInvestigation
->END

=dockers_introspection
<i>(So now I know that Chtcherbina came here not long ago.)</i>#player
<i>(But he could be anywhere in those piers...)</i>#player #NewBackground #9
<i>(I need to think, and fast.)</i>#player #Introspection
<i>(He could disappear anytime soon.)</i>#player
<i>(I have to find more about him, what he did, who did he talk to...)</i>#player
->END

/*--------------------------------------------------------------------------------
    Pier 35
    
--------------------------------------------------------------------------------*/

===pier35===
=pier35_entrance
#NewBackground #20 #NewCharacterSprite #0 #PlayerDBox #1
<i>(If I read the records correctly, Chtcherbina's car is still here.)</i>#player
<i>(Finally, I think that I found him.)</i>#player
<i>(Let's hope that it will end here.)</i>#player
#jump
*   [Search for the vehicule] <i>(Let's find Valer-...)</i>#player
TODO: SFX gunshot
-   <i>(What was that?)</i>#player
<i>(Holy crap...)</i>#player
#jump
*   [Run toward the sound] <i>(Am I the only one after Chtcherbina?)</i>#player
-   #NewBackground #29
<i>(What the...)</i>#player
<i>(That's Valery.)</i>#player
<i>(He won't be a threat now.)</i>#player
<i>(But who did this?)</i>#player
TODO: SFX Car speeding up
<i>(He's escaping!)</i>#player
<i>(I won't be able to pursue him... Crap!)</i>#player
<i>(Better get his plate at least.)</i>#player #NewNarrativeLog #31
<i>(I'll try to run it by Gibbs, see if he can find something.)</i>#player
<i>(What happened here?)</i>#player
#jump
*   [Look at the corpse] <i>(So at least he won't escape me this time.)</i>#player
-   <i>(He took the bullet in the forehead.)</i>#player
<i>(Dead right as it crushed his skull.)</i>#player
<i>(What did he had on him?)</i>#player
#jump
*   [Empty his pockets] <i>(Let's see...)</i>#player #NewDocument #12
-   <i>(So he was meeting someone here...)</i>#player
<i>(His client?)</i>#player
<i>(Then why killing him, with the job already done?)</i>#player
<i>(That's odd...)</i>#player
TODO:SFX gunshot (Russians)
<i>(What the hell!!)</i>#player
<i>(They speak Russian... so that bastard sent someone to follow me after all.)</i>#player
<i>(How am I going to get out of -...)</i>#player
TODO: SFX gunshot (Soldier, closer)
<i>(...)</i>#player
<i>(They're getting close.)</i>#player
#NewCharacterSprite #8
Mr Rosenthal, take a cover, I handle it.#otherCharacter
<i>(What the...)</i>#player
<i>(That's Lanza's man/)</i>#player
<i>(Everybody is following me it seems.)</i>#player
Alright, I think I got one, the other escaped. Freakin' russians.#otherCharacter
Are you ok sir?#otherCharacter #PlayerDBox #0
Y-yeah, it seems so...#player
I'll get you back home. Who's this guy?#otherCharacter
The man that killed Giovanni.#player
Russian too?#otherCharacter
Yeah, I think they followed me.#player
So they are after us...#otherCharacter
I need to inform Lanza.#otherCharacter
But someone killed this guy!#player
That could be the man that called the hit!#player
I wouldn't give it a lot of thoughts.#otherCharacter
That's what happen it this field.#otherCharacter
I'll get you back right away.#otherCharacter
We have a war to prepare.#otherCharacter
#NewBackground #10 #PlayerDBox #1
<i>(Maybe he's right. Maybe the war is the only ending.)</i>#player
<i>(But I can't shake off the feeling that it stinks.)</i>#player
<i>(It's too simple.)</i>#player
#NewBackground #9
<i>(Now it's been like 3 days since the shooting.)</i>#player
<i>(Given what I heard, Abati's and his men are on the path to war.)</i>#player
<i>(Why does it bother me?)</i>#player
<i>(They put me back in the closet. I'm not a fighter.)</i>#player
<i>(And not an Italian.)</i>#player
<i>(...)</i>#player
TODO:SFX phone
<i>(Please, make it only be a jealous husband...)</i>#player
#jump #PlayerDBox #0
*   [Answer the phone] Bugsy Rosenthal, private eye, what can I do for you?#player
-   Hello, it's Abati.#otherCharacter
Oh hum, hello Mr Abati.#player
I talked to Rossini, Lanza's man.#otherCharacter
He told me that you had some reservations about what we're about to do.#otherCharacter
I don't know if I'm the best to advise you.#player
I'm the one asking you to.#otherCharacter
I don't think that the Russians are behind this.#player
Or at least not entirely.#player
You're right.#otherCharacter
I know for a fact that we have a traitor.#otherCharacter
How can you be so sure about it?#player
Only a few persons knew about the fact that Giovanni would take the blame for Lima.#otherCharacter
I happen to be one of them.#otherCharacter
Who were the others?#player
Only two people.#otherCharacter
First one is Lanza.#otherCharacter
What the...#player
And the second one is Morello.#otherCharacter
But... how can one of them betray the family?#player
I really don't know. That's why I need you.#otherCharacter
You'll need to find if one of them is linked with all this.#otherCharacter
But don't tell anyone.#otherCharacter
When you find something, come to The Little Door Restaurant. I'll meet you there.#otherCharacter #NewNarrativeLog #31
But I'll ask for proofs.#otherCharacter
This war will cost us a lot, I cannot afford another mistake.#otherCharacter
You'll still attack the Russians? It's madness!#player
It's too late for second thought.#otherCharacter
Let's hope that we'll at least eliminate one threat.#otherCharacter
See you soon, Bugsy.#otherCharacter #PlayerDBox #1
#NewCharacterSprite #0
<i>(Jesus Christ...)</i>#player
<i>(So one of the members of the administration is behind that.)</i>#player
<i>(But... why?)</i>#player
<i>(Let's find out what's hidden here.)</i>#player
<i>(Right now I only have a licence plate, and a pile of objects and documents.)</i>#player
<i>(And an even bigger pile of bodies.)</i>#player
<i>(What's the big picture behind all that?)</i>#player #Introspection #NewInvestigation
->END


/*--------------------------------------------------------------------------------
    Ending
    
--------------------------------------------------------------------------------*/

===ending===
=start_ending
#NewBackground #31 #PlayerDBox #1
<i>(That's where it all should end.)</i>#player
<i>(In a freakin' Italian restaurant.)</i>#player
<i>(Why do I end up in this?)</i>#player
<i>(But I guess it's too late now.)</i>#player
Hello again, Mr Rosenthal. Can you follow me please?#otherCharacter #PlayerDBox #0
Alright. Let's end this.#player
#NewBackground #32 #NewCharacterSprite #9
Welcome Bugsy.#otherCharacter
Sorry that I couldn't meet you sooner.#otherCharacter
Please have a seat.#otherCharacter
You can go now, Rossini. I'll call you if we need anything.#otherCharacter
Here we are Bugsy. On the verge of war with the Russians.#otherCharacter
But most important, we're at war with ourselves.#otherCharacter
-   (choice_badguy) Who betrayed us?#otherCharacter
And why?#otherCharacter
What did you find?#otherCharacter
#jump #endgame
->END

=badchoice_proofs
That really doesn't make any sense.#otherCharacter
We pay you good money Bugsy, don't try to fuck me.#otherCharacter
Let's try it one more time, but I swear, I'll kill you if you don't take it seriously.#otherCharacter
#jump #endgame
->END

=good_ending
Jesus... Morello, really?#otherCharacter
I'm positive, yes.#player
But why?#otherCharacter
So you see that letter that we found at Giovanni's apartment?#player
I first thought it was from a woman.#player
Turns out it had the same handwriting as the note that we found on the killer's body.#player
It makes sense that whoever wrote that letter, also saw the killer, and possibly asked him to kill Giovanni.#player
But how is this connected to Morello?#otherCharacter
See the partial phone number, just under the tear on the tearing on the killer's note?#player
It's the same on that business card...#player
Castelbueno...#otherCharacter
Yes, the company controlled by Morello and Giovanni.#player
I think that they were lovers.#player
...#otherCharacter
Does anyone else know this?#otherCharacter
Not that I know of.#player
I intend to keep it that way.#otherCharacter
Mr. Abati...#player
Yes Bugsy?#otherCharacter
#jump
*   [What will happen to him?] What will you do to Morello?#player
    I'm pretty sure that you have an idea about that.#otherCharacter
    He's dead to us.#otherCharacter
    We can't tolerate such a disgrace in our ranks.#otherCharacter
    Even if he's one of the founders of our family.#otherCharacter
*   Can I talk to him[?] one last time?#player
    I don't like to be taken for a schmuck, and it seems that Morello only saw me like that.#player
    I think that I can give you that.#otherCharacter
    Rossini, bring Morello.#otherCharacter
    He's already here?#player
    I called a meeting of the administration.#otherCharacter
    Your two suspects were on another room, they came just after you.#otherCharacter
    So that conclude our collaboration for now Bugsy.#otherCharacter
    It's not that I don't like you, but when I see you it's after a tragedy.#otherCharacter
    And I would like to prevent that.#otherCharacter
    I completly agree.#player
    No offense, but when I'm around Italians, I can't stop watching over my shoulder now.#player
    Goodbye then Mr Rosenthal.#otherCharacter #PlayerDBox #1
    ->discussion_morello
-   But you don't have to worry about it anymore.#otherCharacter
Your work here is done.#otherCharacter
Finally! I can't survive around Italians it seems.#player
Always a witty comment. I really don't know how you managed to survive.#otherCharacter
I'll let you go now.#otherCharacter
Goodbye Mr Rosenthal.#otherCharacter #PlayerDBox #1
<i>(So that where it ends.)</i>#player #NewCharacterSprite #0
<i>(Without a big win, or an epic climax.)</i>#player
<i>(Only old gangsters, killing each other.)</i>#player
<i>(I could swear that I heard a gunshot when I left.)</i>#player #NewBackground #9
<i>(Here we go again.)</i>#player
<i>(Same office. Same files.)</i>#player
<i>(I'm not a winner in this.)</i>#player
<i>(But I ain't dead either.)</i>#player
<i>(So that's a win.)</i>#player
TODO:SFX phone
<i>(What's this...)</i>#player
#jump
*   [Answer the phone] <i>(I didn't expected to find a new work so soon.)</i>#player
-   #PlayerDBox #0
Bugsy Rosenthal, private eye, what can I do for you?#player #NewCharacterSprite #7
Really professionnal Bugsy, that's surprising.#otherCharacter
Jesus Lanza, not you again...#player
Don't say that, you'll miss me.#otherCharacter
What do you want?#player
Just wanted to thank you. You really pulled this off.#otherCharacter
I hate to say this, but you saved us on this.#otherCharacter
Otherwise we would have been in a really ugly war.#otherCharacter
I think those methods are better left in the past.#otherCharacter
Anyway, I'll stop following you from now.#otherCharacter
You can go back to sleep, I won't bother you at 3AM from now.
#jump
*   Fuck you Lanza[].#player
TODO: SFX hang up the phone
-   #NewCharacterSprite #0 #Demo
->END

=discussion_morello
#NewBackgroundSprite #9 #PlayerDBox #1
<i>(Abati leaves, without even looking at me.)</i>#player
<i>(I'm already in the past for him.)</i>#player
<i>(Can't blame him.)</i>#player
<i>(He's replaced by an old man, pale, but still proud.)</i>#player
<i>(Yet, only the shadow of who Morello was.)</i>#player #NewCharacterSprite #10
So we meet one last time it seems, Bugsy.#otherCharacter #PlayerDBox #0
Indeed. I got you.#player
I know. And to be honest, I'm kinda relieved.#otherCharacter
You won't get out of this.#player
Yes, without any doubt.#otherCharacter
But it was quite a life.#otherCharacter
Life of murder?#player
No, it was the first time for me.#otherCharacter
Why Giovanni then?#player
I think that you know it.#otherCharacter
I loved him.#otherCharacter
I still do.#otherCharacter
He asked me to change the administration decision about sending him in prison to save Lima.#otherCharacter
But I couldn't, it would draw too much suspicion over us.#otherCharacter
So he threatened to go public about our relation.#otherCharacter
I couldn't let him do that.#otherCharacter
How is this situation relieving then? You got caught.#player
Because I had to hid. I felt like a prey, never able to live my life.#otherCharacter
Now it doesn't matter.#otherCharacter
I only hope that Giovanni will forgive me.#otherCharacter
#NewBackground #9 #NewCharacterSprite #0 #PlayerDBox #1
<i>(I knew leaving that room that nobody would see Morello alive anymore.)</i>#player
<i>(In fact, I think that I heard a muffled gunshot when I left the place.)</i>#player
<i>(Here we go again.)</i>#player
<i>(Same office. Same files.)</i>#player
<i>(I'm not a winner in this.)</i>#player
<i>(But I ain't dead either.)</i>#player
<i>(So that's a win.)</i>#player
TODO:SFX phone
<i>(What's this...)</i>#player
#jump
*   [Answer the phone] <i>(I didn't expected to find a new work so soon.)</i>#player
-   #PlayerDBox #0
Bugsy Rosenthal, private eye, what can I do for you?#player #NewCharacterSprite #7
Really professionnal Bugsy, that's surprising.#otherCharacter
Jesus Lanza, not you again...#player
Don't say that, you'll miss me.#otherCharacter
What do you want?#player
Just wanted to thank you. You really pulled this off.#otherCharacter
I hate to say this, but you saved us on this.#otherCharacter
Otherwise we would have been in a really ugly war.#otherCharacter
I think those methods are better left in the past.#otherCharacter
Anyway, I'll stop following you from now.#otherCharacter
You can go back to sleep, I won't bother you at 3AM from now.
#jump
*   Fuck you Lanza[].#player
TODO: SFX hang up the phone
-   #NewCharacterSprite #0 #Demo
->END

=bad_ending
How disappointing...#otherCharacter
You're just a drunk, tired, and useless man.#otherCharacter
Get the hell out of here.#otherCharacter
Rossini? Deal with him.#otherCharacter #PlayerDBox #1
#NewCharacterSprite #0
<i>(I never saw the shot coming.)</i>#player
<i>(Only thing I saw was the face of Rossini.)</i>#player #NewCharacterSprite #8
<i>(No anger in his eyes. No shame either.)</i>#player
<i>(Only disappointement.)</i>#player
<i>(I don't know if the death coming over me was blowing up my brain, but I could swear that he tried to tell me something.)</i>#player
<i>(But what's the point now?)</i>#player #NewCharacterSprite #0
<i>(It all ends here.)</i>#player
<i>(At least for me.)</i>#player
#jump #Demo
->END

=lanza_ending
So it was Jimmy...#otherCharacter
It seems so.#player
Alright, that's all I needed.#otherCharacter
What a waste...#otherCharacter
Go back home Bugsy. You won't have to see such a disgrace.#otherCharacter #PlayerDBox #1
<i>(I could swear that what I saw in Abati's eyes was sadness.)</i>#player
<i>(And disappointment.)</i>#player
#NewBackground #9
<i>(Back to the beginning.)</i>#player
<i>(Except that Abati and the others seem to avoid talking to me.)</i>#player
<i>(Of course, nobody saw Lanza ever again.)</i>#player
<i>(It should be a victory. For me, for the family.)</i>#player
<i>(But this victory tasted like shit.)</i>#player
<i>(...)</i>#player
<i>(Did... did I miss something?)</i>#player
#jump #Demo
->END