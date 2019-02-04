VAR first_discussion_passed = false
->second_story

===second_story=== //test d'unlock de deuxieme discussion après une première discussion réussie
{
	- first_discussion_passed==true:
		->second_discussion
	- first_discussion_passed==false:
		->first_discussion
}
=first_discussion
How are you Antonio? # player
Nothing new, you're still working on Lotto's death? # otherCharacter
+ I'm stuck... # player
	Don't give a fuck # otherCharacter
* Do you know that? # player
	Maybe... Kidding, of course I know. Come see me later # otherCharacter
	~first_discussion_passed = true
- Now get the fuck out. # otherCharacter
+ [Continue?]
	-> second_story
* [Quit?]
	-> END


=second_discussion
TADADADA # otherCharacter
->END