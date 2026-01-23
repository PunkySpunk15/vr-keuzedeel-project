using System.Collections.Generic;
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

    private readonly List<Dialogue> _guideDialogue = new() {
        new("Howdy, wanna practice yer aim?", false),
        new("I'll go easy on ya, but I will shoot after yer headstart of 2 seconds is up.", true),
        new("Alright, remember to only shoot when the timer hits zero! No playin' dirty 'round these parts.", false)
    };

    private readonly List<Dialogue> _informantDialogue = new() {
        new("Hey there, seems like yer the new sheriff.. wanna know a few things?", false),
        new("Right there on the table in front of ya is a paper with that wanted outlaw's face on 't.", false),
        new("Take a look, yer gonna need to watch out fer him.", false),
        new("Folks say he's comin' to town to stir the pot once again..", false),
        new("Hey, listen..", false),
        new("I'd like to see what yer made of, how's 'bout we duel outside fer a minute?", true)
    };

    private readonly List<Dialogue> _outlawDialogue = new() {
        new("I see the new sheriff is gettin' all roostered up in the midday!", false),
        new("Ha, y' look like you've been rode hard 'n put up wet!", false),
        new("I'll make y' hallow, cowboy.", true)
    };

    public CharacterDialogue character;
    public TextMeshProUGUI textElement;
    public Button button;
    public TextMeshProUGUI buttonTextElement;
    public Canvas canvas;
    public GameObject characterObject;
    public GameObject player;
    public EnableDisable ed;
    private int _index = 0;

    public void StartDialogue()
    {
        textElement.text = character switch
        {
            CharacterDialogue.Guide => _guideDialogue[_index].Text,
            CharacterDialogue.Informant => _informantDialogue[_index].Text,
            CharacterDialogue.Outlaw => _outlawDialogue[_index].Text,
            _ => ""
        };
    }

    public void NextDialogue()
    {
        List<Dialogue> dialogue = character switch
        {
            CharacterDialogue.Guide => _guideDialogue,
            CharacterDialogue.Informant => _informantDialogue,
            CharacterDialogue.Outlaw => _outlawDialogue
        };

        if (dialogue[_index].StartsDuel)
        {
            button.transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120f);
            button.transform.GetComponent<Image>().color = new Color(0, 239, 255, 100); // BLUE
            buttonTextElement.text = "Take up the offer >>";

            return;
        }

        if (buttonTextElement.text == "Take up the offer >>")
        {
            //Send player to duel location
            CharacterController cc = player.GetComponent<CharacterController>();

            switch (character)
            {
                case CharacterDialogue.Informant:
                case CharacterDialogue.Guide:
                    characterObject.transform.position = new Vector3(-10.76f, characterObject.transform.position.y, -11.97f);
                    characterObject.transform.rotation = new Quaternion(characterObject.transform.rotation.x, 180f, characterObject.transform.rotation.z, characterObject.transform.rotation.w);

                    canvas.transform.position = new Vector3(-12.27f, canvas.transform.position.y, -11.66f);
                    canvas.transform.rotation = new Quaternion(canvas.transform.rotation.x, 180f, canvas.transform.rotation.z, canvas.transform.rotation.w);

                    if (cc != null)
                        cc.enabled = false;

                    player.transform.position = new Vector3(-10.76f, player.transform.position.y, 1.74f);

                    if (cc != null)
                        cc.enabled = true;
                    break;
                case CharacterDialogue.Outlaw:
                    characterObject.transform.position = new Vector3(6.95f, characterObject.transform.position.y, 3.54f);
                    canvas.transform.position = new Vector3(7.41f, canvas.transform.position.y, 5.25f);

                    if (cc != null)
                        cc.enabled = false;

                    player.transform.position = new Vector3(-13.3f, player.transform.position.y, -7.5f);
                    player.transform.rotation = new Quaternion(player.transform.rotation.x, 90f, player.transform.rotation.z, player.transform.rotation.w);

                    if (cc != null)
                        cc.enabled = true;
                    break;
            }

            return;
        }

        _index++;

        textElement.text = dialogue[_index].Text;
        buttonTextElement.text = "Start the duel >>";
        button.transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100f);
        button.transform.GetComponent<Image>().color = new Color(136, 0, 255, 100); // PURPLE

        if (_index >= dialogue.Count)
        {
            ed.Disable();
            return;
        }
    }

    public void ResetIndex() => _index = 0;

    void Start()
    {
        //TEST OUT CHANGING LOCATION HERE!!!
    }

    class Dialogue
    {
        public string Text { get; set; }
        public bool StartsDuel { get; set; }

        public Dialogue(string text, bool startsDuel)
        {
            Text = text;
            StartsDuel = startsDuel;
        }
    }
}
