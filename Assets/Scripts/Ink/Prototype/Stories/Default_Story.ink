VAR knowledge_Spaghetti = 0
// DEBUG mode adds a few shortcuts - remember to set to false in release!
VAR DEBUG = false
{DEBUG:
	IN DEBUG MODE!
	*	[Start]	-> start_capo_apartment
	*	[Discussing with Lanza in the Lobby] -> start_capo_apartment.lobby_apartment
	*	[Deeper discussion with Lanza] -> start_capo_apartment.lanza_dialogue
	*   [Checking the apartment] -> start_capo_apartment.check_apartment
- else:
	// First diversion: where do we begin?
 ->start_capo_apartment.start_office
 }
 
===start_capo_apartment===

/*--------------------------------------------------------------------------------

	Start the story!

--------------------------------------------------------------------------------*/

=start_office 
VAR lanza_stitch_first = 0

TODO Tags (see with Killian)
TODO Intro capo's apartment (retakes)
# DisableDiscussion
This night couldn't have ended in a better way. # player 
I mean for a night where I'll have to meet a corpse, it could be worse. # player
I could be the dead man. # player
You understand me Rosenthal? Come here immediately. # otherCharacter # ActivateDiscussion # NewCharacterSprite #3
    * What's the address again? # player

TODO Find the capo's address (confirm)
TODO Find Bugsy's address
TODO Find dead capo's name

- Lived on 98 Francisco Street . Hurry up, fucking stinks here. # otherCharacter 
    *   [Hang up] # player
    *   Don't tell me to hurry[] boy, it's the middle of the fucking night. # player
- I hung up and got out of bed, my eyes still blurred by that short night. # player # DisableDiscussion
Just saw what time it is. 4 AM. # player
For fuck's sake, that man is not gonna rise from the dead now. # player
    *   [Go to Francisco Street] # player
- (cab) {Not even the time for a coffee, I put on my hat and go outside to find a cab.|} # player # NewBackground #4
    +   [{Hail a cab|Hail a cab again|Try to hail a cab}] # player
        {That prick doesn't even slow down.-> cab|Am I covered in shit?-> cab|} # player
- As I'm slowly starting to lose my temper and head back to the office, a 47' Cadillac slows by. # player
Mr. Rosenthal? # otherCharacter # NewCharacterSprite #0 # ActivateDiscussion
    *   Ain't no Yid here kid[], get lost. # player
        Ain't the time to joke Mr. Rosenthal. # otherCharacter
    *   Who's askin? # player
- Mr. Lanza sent me to get you, could you please get in the car? # otherCharacter
    *   Alright, I'm coming[]. Couldn't he warn me that you were going to fetch me? # player
    *   Not so much of a choice[], am I right? # player
- I step into the car. As soon as I seat, my nocturnal driver starts the engine and takes me to Francisco Street. # player # DisableDiscussion
He rides smoothly and hardly ever speaks. Francisco is not so far, but he seems to take quite a lot of detours. # player
- (car_convo)
    *   Are you new in town, kid? # player 
        Came here 6 months ago from Sicily. # otherCharacter # ActivateDiscussion
        **  Lanza got you in here? # player
            Let's say that he needed the skills that I'm able to provide for his protection. # otherCharacter
        **  More Italians? Is there any place left in North Beach? # player
            I'm not here to settle down, Mr. Lanza asked me to come to ensure his protection. # otherCharacter
        --  What is he afraid of? He's not been linked to Lima, has he? # player
        Not that I know of, the boss was smart enough to make sure that the organization would not be too much harmed. # otherCharacter
        So who's going after him? # player
        I can't say anything, but I'm sure Mr. Lanza will talk to you about it. # otherCharacter
        ->car_convo
    *   Can't you go directly to the apartment? # player
        Must make sure that we're not tailed sir # otherCharacter # ActivateDiscussion
        **  Who would follow me?[] Am I a Jewish Marilyn? # player 
        //revoir 
            Let's say that we caught a lot of heat from the trial. # otherCharacter
        **  I ain't mixed with all that[] kid, Jimmy should have told you so. I'm a simple detective that helps some friends in need. # player
            Not you personnally, but we must stay on our guard with the trial. # otherCharacter
        --  I don't get it, your boss Lima is judged, couldn't you find a straw man to take his place? # player
            Let's say that it's exactly where you're intervening. # otherCharacter
        **  Are you throwing me under the bus?[] I'll not go down easily motherfucker. # player
            Don't be stupid, Mr. Lima cannot be saved. # otherCharacter
            So what am I doing here? # player
        **  Lima didn't request my help for the trial[], what could I possibly do? # player
            If all had gone accordingly to the plan, you would not have been involved in this. # otherCharacter
            So where do I fit in your plan now? # player
        --  Our straw man is dead. # otherCharacter
        **  Fuck... What happened to Giovanni? # player 
        //à changer
            You will discover it soon enough. # otherCharacter
            ->car_convo
    *->
- The ride continued in a complete silence. # player
Giovanni is dead. Poor kid. And more Italians flooding North Beach. # player
Someting's wrong. And I'm gettin' mixed in all that. # player
We finally park in Francisco Street. # player # NewBackground #0
I let you go by yourself. Mr. Lanza is waiting for you inside. # otherCharacter # ActivateDiscussion
    *   Thank you kid # player
    *   [Just a question...] Before I leave kid, can I ask you something? # player
        Go ahead. # otherCharacter
        Am I being set up? Are they trying to fuck me over to save their heads? # player
        Mr. Rosenthal, you're not important enough to worry yourself. # otherCharacter
        And the young prick leaves. # player
- I cross the threshold of the building and go to the second floor. Apartment 237. # player

TODO First dialogue with Lanza (retakes)

Jimmy Lanza is waiting for me, near to the door. # player
For fuck's sake, ain't all Jew boys supposed to arrive on time? # otherCharacter # NewCharacterSprite #3
*   Never when there are greaseballs like you waiting for me at a crime scene. # player
//Joke, à améliorer
    I'm not here to joke around, not with two bodies waiting two feet away. # otherCharacter
*   Fuck you Lanza, I don't have to come like a dog whenever you fuck things up! # player
//Go crazy 
    You think I'm happy with this shit? I could have done without two more bodies. # otherCharacter
*   Calm down, we have more important to do. Where is he? # player
//Answer calmly 
    He? You mean them. We found a girl in the bathroom. # otherCharacter
-
*   What do you mean?[] I thought Giovanni was the only one. # player
//à changer
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

    *   I have some questions for you Lanza. # player
    ->lanza_dialogue
    *   I'll go and check on the bodies {lanza_stitch_first ==1:now|first}. # player
    ->check_apartment
    *->
    {lanza_stitch_first == 1:Enough talking|Come here!} # otherCharacter # NewCharacterSprite #3
    We need to go Rosenthal. I'll take you to your office # otherCharacter
    ->end_apartment
    
    
/*--------------------------------------------------------------------------------

	Discussing with Lanza

--------------------------------------------------------------------------------*/
    
=lanza_dialogue

TODO Dialogue with Lanza (retakes)
-   (lanza_convo)
*   What can you say about Giovanni? # player
    He was made capo some months ago. Poor kid... # otherCharacter # NewCharacterSprite #3
    The Administration chose him to take the place of Lima during the trial. # otherCharacter
    **  You mean that you decided to throw him under the bus. # player
        No, we'll take care of his family. What he was gonna do for us was essential. # otherCharacter
        Now we're in deep shit... # otherCharacter
        ->lanza_convo
    **  What did he do to deserve it? # player
        Stop insinuating that we wanted him killed. He was not made for this job. # otherCharacter
        *** Is it your opinion? # player
            It is mine, and Abati's too. Even Morello agreed. # otherCharacter
            And he's the fucking Consigliere, taking care of our soldiers and shit. # otherCharacter
            We agreed to tend for his family, at least his mother. # otherCharacter
            We could not find anyone else. # otherCharacter
            ****    Where does she live? # player
                    She's in Sicily. The boy came here on his own. Fuckin' American dream. # otherCharacter
                    ->lanza_convo
*   Who could want to harm him? # player
    Nobody that I would know of. # otherCharacter
    **  Not even the girl's pimp? # player
        I can't even say for sure that she's a prostitute. # otherCharacter
        Only a... how could you say... an educated guess Rosenthal. # otherCharacter
        You knew Tommy a little. Always a gentleman with the ladies. # otherCharacter
// We're putting the Family back on its feet, so we're focusing on greater matters.
    ->lanza_convo
*   Who's that girl? # player
    Can't say for sure, that's your job now. Nobody ever saw Tommy with her. # otherCharacter
    But we were not following him day and night. # otherCharacter
    **  You mean you tailed him sometimes? # player
        Don't be stupid. I keep a close watch on everyone. # otherCharacter
    ->lanza_convo
*   What is happening with the organization? # player
    As you should know now, Lima was sent to jail, with a trial coming. # otherCharacter
    Abati took his place, and I second him now. # otherCharacter
    **  Congratulations[!], Sotto-Capi. Is that your title now? # player
        That was meant to be temporary. We should have exonerated Lima. # otherCharacter
        But you know your little complication with Giovanni. # otherCharacter
    **  Too much complication to take the power. # player
        Again, stop your accusations. We were close to freeing the Boss. # otherCharacter
    --  Now Lima is taking full power, and I'll second him the best I can. # otherCharacter
    **  What about Morello? # player
        The Consigliere? He's advising us. He approved of Giovanni's designation. # otherCharacter
        I think he's trying to take some distance with the game. # otherCharacter
        What can we say about it? That poor bastard earned to rest now. # otherCharacter
    ->lanza_convo
*   ->
~lanza_stitch_first = 1
->lobby_apartment

/*--------------------------------------------------------------------------------

	Checking the apartment

--------------------------------------------------------------------------------*/

=check_apartment

TODO Checking the apartment (exploration, gathering clues)
Test Check Apartment # player

~lanza_stitch_first = 0
->lobby_apartment

/*--------------------------------------------------------------------------------

	End of the Capo's apartment scene

--------------------------------------------------------------------------------*/

=end_apartment

Lanza, always keeping an eye on me, brought me back to my office on Broadway Street. # player
That day could not have been worse. # player #NewInvestigation
->END

=== DefaultStory
# DisableDiscussion # NewBackground #4
I have nothing to do here # player
->END