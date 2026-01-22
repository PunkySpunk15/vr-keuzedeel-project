using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    public enum CharacterDialogue
    {
        Guide,
        Informant,
        Outlaw
    }

    private readonly string[] _guideDialogue = new[] {
        "Howdy, wanna practice yer aim?",
        "I'll go easy on ya, but I will shoot after yer headstart of 2 seconds is up.",
        "Alright, remember to only shoot when the timer hits zero! No playin' dirty 'round these parts."
        };

    private readonly string[] _informantDialogue = new[] {
        "Hey there, seems like yer the new sheriff.. wanna know a few things?",
        "Right there on the table in front of ya is a paper with that wanted outlaw's face on 't.",
        "Take a look, yer gonna need to watch out fer him.",
        "Folks say he's comin' to town to stir the pot once again..",
        "Hey, listen..",
        "I'd like to see what yer made of, how's 'bout we duel outside fer a minute?"
    };

    private readonly string[] _outlawDialogue = new[] {
        "I see the new sheriff is gettin' all roostered up in the midday!",
        "Ha, y' look like you've been rode hard 'n put up wet!",
        "I'll make y' hallow, cowboy."
    };

    public CharacterDialogue character;
    public TextMeshProUGUI textElement;
    public Button button;
    public TextMeshProUGUI buttonTextElement;
    public EnableDisable ed;
    private int _index = 0;

    public void StartDialogue()
    {
        textElement.text = character switch
        {
            CharacterDialogue.Guide => _guideDialogue[_index],
            CharacterDialogue.Informant => _informantDialogue[_index],
            CharacterDialogue.Outlaw => _outlawDialogue[_index],
            _ => ""
        };
    }

    public void NextDialogue()
    {
        if (_index < 0)
        {
            button.transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120f);
            button.transform.GetComponent<Image>().color = new Color(0, 239, 255, 100); // BLUE
            buttonTextElement.text = "Take up the offer >>";

            _index = 0;

            switch (character)
            {
                case CharacterDialogue.Guide:
                    //Send player to duel
                    break;
                case CharacterDialogue.Informant:
                    //Send player to duel
                    break;
                case CharacterDialogue.Outlaw:
                    //Send player to duel
                    break;
            }

            ed.Disable();
            return;
        }

        _index++;

        textElement.text = character switch
        {
            CharacterDialogue.Guide => _guideDialogue[_index],
            CharacterDialogue.Informant => _informantDialogue[_index],
            CharacterDialogue.Outlaw => _outlawDialogue[_index],
            _ => ""
        };
    }

    public void ResetIndex() => _index = 0;
}
