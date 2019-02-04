-> StartExempleStory


LIST OtherCharactersNames = Todd_Smith, Elias_Wilson
LIST PlayerNickName = man, boy, Bugsy
VAR playerActualNick = man
VAR actualOtherCharacter = Todd_Smith

=== function RandomPlayerNickname
{playerActualNick != Bugsy:
    ~ playerActualNick++
    ~ return playerActualNick
    - else:
    ~ playerActualNick = man
    ~ return playerActualNick
}

=== StartExempleStory

- hmm ? {RandomPlayerNickname()}? what do you want from me ? # otherCharacter
- Hello {actualOtherCharacter}, i have some questions for you # player
    * Where were you doing yesterday at 9PM # player
        -- I was at the *Blue Dragon* # otherCharacter
        -- Okay thanks cya <> # player
    * Nothing thank you[] <> # player
- ->END 
