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
	*	Prostitute Motel[]
		**	[Start] ->prostitute_motel
		**	[Motel lobby] ->prostitute_motel.motel_lobby
		**	[Motel room] ->prostitute_motel.motel_room
	*	Condor Club[]
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
Fucking city. # player 
"Come to San Francisco" they said. "It's like a Gold Rush for private detectives". # player 
And I fell for it. # player 
Ain't nobody like Bugsy Rosenthal to look like a damn fool. # player 
Next thing I know, my wife leaves me, and I'm nearly all out of legit businesses. # player 
But everybody has to do something for a living. # player 
And this mob is paying quite well. # player #NewNarrativeLog #0
But they're fucking savages, killing each other and all. # player 
Can't sleep tonight. Cars, gun shots, everybody in town wants to drive me crazy. # player 
And now the fuckin' phone is ringing. # player #SFXPlay #8
Don't even wants to answer, fuck it. # player 
- (phone) {The phone rings.|The phone doesn't stop.|It continues to ring.} # player 
# jump
	*	[Answer] This damn phone ain't gonna stop itself. # player 
	+	[Try to sleep]{Ain't no time to call somebody, they'll wait tomorrow.|Could be a business call... Who am I kidding?|Twenty-four sheeps. Twenty-five sheeps. Twenty-six sheeps...|} # player #NewNarrativeLog #1
		->phone
- I decide to get up and finally answer it. # player #SFXStop #8

TODO Find the capo's address (confirm)
TODO Find Bugsy's address
TODO Find dead capo's name
# jump
	*	Are you out of your mind?[] Did you fucking see the hour? # player #PlayerDBox #0
- It's <color=yellow>James Lanza</color>. We had a problem with Giovanni. #otherCharacter #NewCharacterSprite #7
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
- I hung up and got out of bed. # player #PlayerDBox #1  #NewCharacterSprite #0
For fuck's sake, what did they do again? # player
# jump
    *   [Go to Francisco Street] <> # player
    #MusicPlay #1
- (cab) {Not even the time for a coffee, I put on my hat and go outside to find a cab.|} # player 
# jump # NewBackground #4
    +   [{Hail a cab|Hail a cab again|Try to hail a cab}] <> # player
        {That prick doesn't even slow down.-> cab|Am I covered in shit?-> cab|} # player
- As I'm slowly starting to lose my temper and head back to the office, a 47' Cadillac slows by. # player
Mr. Rosenthal? # otherCharacter # NewCharacterSprite #8
# jump  #PlayerDBox #0
    *   Ain't no Yid here kid[], get lost. # player
        Ain't the time to joke Mr. Rosenthal. # otherCharacter
    *   Who's askin? # player
- Mr. Lanza sent me to get you, could you please get in the car? # otherCharacter
    *   Alright, I'm coming[]. Couldn't he warn me that you were going to fetch me? # player
    *   Not so much of a choice[], am I right? # player
- I step into the car. As soon as I seat, my nocturnal driver starts the engine and takes me to Francisco Street. # player #NewCharacterSprite #0 #PlayerDBox #1
He rides smoothly and hardly ever speaks. Francisco is not so far, but he seems to take quite a lot of detours. # player
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
        I can't say anything, but I'm sure Mr. Lanza will talk to you about it. # otherCharacter
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
            You will discover it soon enough. # otherCharacter
            ->car_convo
    *->
- The ride continued in a complete silence. # player #NewCharacterSprite #0 #PlayerDBox #1
Giovanni is dead. Poor kid. And more Italians flooding North Beach. # player
Someting's wrong. And I'm gettin' mixed in all that. # player
We finally park in Francisco Street. # player #MusicPlay #0
I let you go by yourself. Mr. Lanza is waiting for you inside. # otherCharacter # NewCharacterSprite #8
# jump
    *   Thank you kid # player #PlayerDBox #0 # NewCharacterSprite #8
    *   [Just a question...] Before I leave kid, can I ask you something? # player #PlayerDBox #0
        Go ahead. # otherCharacter
        Am I being set up? Are they trying to fuck me over to save their heads? # player
        Mr. Rosenthal, you're not important enough to worry yourself. # otherCharacter
        And the young prick leaves. # player #PlayerDBox #1 # NewCharacterSprite #0
- I cross the threshold of the building and go to the second floor. Apartment 237. # player #PlayerDBox #1 # NewCharacterSprite #0

TODO First dialogue with Lanza (retakes)

# NewBackground #0
Jimmy Lanza is waiting for me, near to the door. # player # NewCharacterSprite #5
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
 But you're the expert here! So what you gonna do? # otherCharacter
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
        Now we're in deep shit... # otherCharacter
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
                    She's in Sicily. The boy came here on his own. Fuckin' American dream. # otherCharacter
                    ->lanza_convo
*   Who could want to harm him? # player #PlayerDBox #0
    Nobody that I would know of. # otherCharacter
    # jump
    **  Not even the girl's pimp? # player
        I can't even say for sure that she's a prostitute. # otherCharacter
        Only a... how could you say... an educated guess Rosenthal. # otherCharacter
        You knew Tommy a little. Always a gentleman with the ladies. # otherCharacter
    ->lanza_convo
*   Who's that girl? # player #PlayerDBox #0
    Can't say for sure, that's your job now. Nobody ever saw Tommy with her. # otherCharacter
    But we were not following him day and night. # otherCharacter
    # jump
    **  You mean you tailed him sometimes? # player
        Don't be stupid. I keep a close watch on everyone. # otherCharacter
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
// BACKGROUND APARTMENT
TODO Checking the apartment (exploration, gathering clues)
#NewCharacterSprite #0
I enter the apartment, only to find a first body. Poor Tommy. # player #PlayerDBox #1
Ok, let's focus now and do that methodically, ain't I a damn detective? # player
- (clues_apartment)
# jump
	*	[Check Giovanni] {clues_apartment > 1: You finally decide to go and check on Giovanni.| You decide to check on Giovanni first.} # player
		Poor boy.  # player
		His skull is completely shattered, which is caused by the obvious bullet hole on his forehead. # player
		Skull and brain pieces are all over the wall behind. # player 
        You find a poker chip on the ground. # player
        # NewDocument #2
		->clues_apartment
	*	[Check the livingroom] You go around the livingroom # player
		Nothing special here, except some old furnitures. # player
		->clues_apartment
	*	[Check the bathroom] You go to check the bathroom # player
		Oh that's right. The second body. # player
		The entire floor is covered in blood. Hers, obviously # player
		TODO: Continue here.
		The prostitute body. I should check her bag. # player
        # jump
		**	[Open the purse] I open that little woman's purse. # player
		Inside, there's a key for a hotelroom. # player 
		# NewDocument #0
		->clues_apartment
	*	[Check the bedroom] You go to the bedroom. # player
		It's tidy, as if it was not often used. # player
		The bed is made. Giovanni didn't sleep here so much. # player
		There's a closet. # player
		# jump
		**	[Open the closet] You open the door of this massive closet. # player
			Only to find out that it is nearly empty. # player
			A few suits, some underwear. # player
			Not enough for someone to live here. # player
			Unless that person is a slog. But Tommy was not. # player
		->clues_apartment
	*->
	Nothing more for me in that apartment. # player
~lanza_stitch_first = 0
->lobby_apartment

/*--------------------------------------------------------------------------------

	End of the Capo's apartment scene

--------------------------------------------------------------------------------*/

=end_apartment

Lanza, always keeping an eye on me, brought me back to my office on Broadway Street. # player #PlayerDBox #1
That day could not have been worse. # player #NewInvestigation
->END

=== DefaultStory
#NewCharacterSprite #0
I have nothing to do here # player #PlayerDBox #1
->END

/*--------------------------------------------------------------------------------
    Prostitute's apartment
    First lead followed by Bugsy: visit the prostitute’s room in a motel

--------------------------------------------------------------------------------*/

===prostitute_motel===
//Background Motel's Street, no interlocutor
#NewBackground #5 # NewCharacterSprite #0
Bugsy arrives in front of the motel and exits the taxi. # player #PlayerDBox #1
Description of the motel. # player
# jump
*   [Enter the motel] Bugsy enters the motel # player
    ->motel_lobby

=motel_lobby
//Background Motel's lobby, no interlocutor at first
Description of the reception. There's a manager. # player
//Interlocutor: Motel's manager
Bugsy asks if he can visit the girl's room. # player #PlayerDBox #0
The manager asks for the key. # otherCharacter # NewCharacterSprite #4
# jump
*   [Give him the key] Bugsy gives him the key. # player
- The manager is reluctant, he doesn't want to let you in. # otherCharacter
# jump
*   [Threaten him] Bugsy threatens him, saying that he should not interfere with the investigation on her death. # player
*   [Convince him] Bugsy tries to convince him that she's his girlfriend, and that he hasn't had news since some days. # player
- The manager leads Bugsy to the room. # otherCharacter
->motel_room

=motel_room
//Background Motel's room, no interlocutor
# NewBackground #3
# NewCharacterSprite #0
Bugsy enters the room alone. and searches for clues about her. # player #PlayerDBox #0
- (motel_search)
# jump
	*	[Search around the bed] You look around the bed. # player
		There's nothing worth noticing, except for the bedside table and a large closet. # player
		# jump
		**	[Go to the bedside table] You approach the bedside table. # player
			---	(bedside_search) # jump
			***	[Look on the top] He checks above the bedside table. # player
		        Bugsy finds a ashtray with a box of matches. # player
		        # NewDocument #1
		        ->bedside_search
		    ***	[Open a drawer] Bugsy opens a drawer of the bedside table. # player
		        Bugsy finds a small bible. # player
		        # jump
		        ****  [Open the bible] He opens the bible. # player
		        There is a wad of notes, with like 100 dollars in small bills. # player
		        ->bedside_search
		    ***	->
		    	->motel_search
		**	[Open the closet] He opens the closet. # player
        	Bugsy finds two kinds of clothes. # player
        	->bedside_search
		**	->
			->motel_search
    *	[Check the desk] He goes to the desk to check it.
        Bugsy finds several letters on the desk.
        # NewDocument #3
        ->motel_search
    *   [Go in the kitchen] He goes in the kitchen. # player
        Nothing interesting in the kitchen, but it is small. Bugsy returns in the room. # player
        ->motel_search
    *   [Ask questions to the manager] He asks to the manager informations about the girl. # player #PlayerDBox #0
    //Interlocutor: Motel's manager
        Bugsy asks the manager about her occupation. # player
        He says that he only know her name, and he didn't have any reason to complain or to ask her for more. #otherCharacter # NewCharacterSprite #4
        # NewCharacterSprite #0
        ->motel_search
    *->
    Nothing more to find here, Bugsy exits and takes the cab back to his office. # player #PlayerDBox #1
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
# NewBackground #6
Bugsy arrives in front of the Condor Club and exits the taxi. # player #PlayerDBox #1
Description of the club's facade. # player
# jump
*   [Enter the Condor Club] Bugsy enters the bar. # player
    ->condor_entrance

=condor_entrance
//Background Condor Club's entrance, no interocutor
Bugsy is in the club. The place is crowded. # player
# jump
*   [Go to the bar] Bugsy goes to the bar. # player
- He finds an empty stool. # player
The barman does not seem to be interested in Bugsy. He's serving the other customers. # player
~ temp number_drinks = 0
- (ordering_drink)
# jump
+   [Put a bill on the bar] You put a 10 dollars bill on the bar. # player
    The barman immediately comes, seizes it, and a glass of whiskey appears. You didn't order it. # player
    ~ number_drinks = number_drinks + 1
    ->ordering_drink
+   [Hail the barman] Boy I need to talk to you. # player #PlayerDBox #0
    {number_drinks == 3 : ->condor_madam}
    He doesn't even raise on eye on you. # player #PlayerDBox #1
    ->ordering_drink

=condor_madam
After a few drinks, the barman finally gets to you. # player
//Interlocutor: Barman
TODO: Create barman sprite
What do you want? # otherCharacter #NewCharacterSprite #3
# jump
*   [Tell him who you are] You tell him your name. # player #PlayerDBox #0
- The barman tells you that he knows who you are. # otherCharacter
So does the woman that seats next to you. # otherCharacter
//Interlocutor: Madam
# jump
*   [Tell her who you are] You tell her your name. # player
    She cuts you: she already knows it. # otherCharacter # NewCharacterSprite #6
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
    # NewBackground #7
    ->END
*   {knowledge_prostitute_name == 0} [Calm the situation] You try to calm her. You're not here to accuse anybody. # player
    You just want to know if anything happened. # player
    She gets angrier. Nothing happened, because there's no girl here. # otherCharacter
    She calls security. # otherCharacter
    You're dragged to the exit. # player #PlayerDBox #1
    //Background: Condor Club's street
    You will have to come here again once you have the name of the girl. # player
    ->END
*   {knowledge_prostitute_name == 0} [Threaten her] You threathen her to call the police if she does not cooperate. # player
    She's not impressed. Even a little amused. # otherCharacter
    You work for Abati, you couldn't even speak to the police. # otherCharacter
    She thinks that you should leave the club immediately. # otherCharacter
    You agree to leave. # player
    # NewCharacterSprite #0
    //Background: Condor Club's street
    You will have to come here again once you have the name of the girl. # player #PlayerDBox #1
    ->END

=condor_madam2
//Background: Condor Club's street, no interlocutor
# NewBackground #6
You go back to the Condor Club. This time, you're sure. The girl's name is Margaret O'Bannon. # player #PlayerDBox #1
You enter the club. # player
//Background: Condor Club's entrance
You're in the club. As soon as you enter, the barman sees you. # player
He comes immediately to you, with a baseball bat. # player
# jump # NewCharacterSprite #3
*   [Tell the girl's name] Before the man hits you, you tell him that you have informations on Margaret O'Bannon. # player #PlayerDBox #0
    He immediately stops. # player #PlayerDBox #1
    You see the madam in the back, entering from an hidden door. # player
    The barman goes to her, whispers something to her. # player
    She's now closely watching you. # player
    The barman comes back. # player
    He tells you to follow him. # otherCharacter
    He opens the door and tells you to enter. # otherCharacter
    He closes the door behind you. # player
    You're know in the main part of the club: an hidden casino/brothel. # player
    The madam is waiting for you. # player
    She asks you about Margaret. She hasn't seen her from two days. # otherCharacter # 
    ->END

=condor_fail
//Background: Condor Club's street, no interlocutor
# NewBackground #6
You go back to the Condor Club. # player #PlayerDBox #1
But you're not so sure about what you could say without getting shot. # player
You decide to go back and work a better approach. # player
->END