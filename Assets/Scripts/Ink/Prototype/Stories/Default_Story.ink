VAR knowledge_Spaghetti = 0

-> Yass

=== StartExempleStory

- I have nothing to do here # player
- ->END 


=== Yass
- Bonjour #player
- ->END



===first_story=== //test de débloquage de dialogue avec un autre personnage
Hello Borletti, I was looking for you # player
{knowledge_Spaghetti == 1: ->end_normal_discussion}
Still annoying me Rosenthal... What do you want? # otherCharacter
->first_discussion
= first_discussion
	+Just wanted to check on you[], see what you were up to. # player
	{Ain't nothing to hide man.|Mind your own business.|You're getting on my nerves...} # otherCharacter
	->first_discussion
	*Do you know where Capone is? # player
	Can't say that I know, that man is a weasel. You should not hunt him. # otherCharacter
	Maybe Spaghetti know where he is. He's probably drunk at the Fisherman's Wharf. # otherCharacter 
	 ~knowledge_Spaghetti = 1 

	-> end_first_discussion

=end_first_discussion
Now get the hell out of here old man. # otherCharacter # NewInvestigation # NewDocument #3
-> END

=end_normal_discussion
Ain't nothin' to tell you anymore, get the fuck out. # otherCharacter
-> END