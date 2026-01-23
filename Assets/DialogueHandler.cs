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

    //UI elements
    public Canvas canvas;
    public CharacterDialogue character;
    public TextMeshProUGUI textElement;
    public Button button;
    public TextMeshProUGUI buttonTextElement;

    //Objects
    public GameObject characterObject;
    public GameObject player;
    public GameObject spawnPoint;

    //Misc
    public EnableDisable ed;
    public Duel duel;
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

        _index++;

        if (_index + 1 > dialogue.Count)
        {
            duel.StartDuel();
            ed.Disable();
            ResetIndex();
            return;
        }

        textElement.text = dialogue[_index].Text;

        if (buttonTextElement.text is "Take up the offer >>" or "Face the outlaw ..")
        {
            //Send player to duel location
            CharacterController cc = player.GetComponent<CharacterController>();

            switch (character)
            {
                case CharacterDialogue.Informant:
                case CharacterDialogue.Guide:
                    characterObject.transform.SetPositionAndRotation(new Vector3(spawnPoint.transform.position.x, characterObject.transform.position.y, spawnPoint.transform.position.z - 5f), spawnPoint.transform.rotation);
                    canvas.transform.SetPositionAndRotation(new Vector3(spawnPoint.transform.position.x - 1.5f, canvas.transform.position.y, spawnPoint.transform.position.z - 4f), spawnPoint.transform.rotation);
                    break;
                case CharacterDialogue.Outlaw:
                    characterObject.transform.SetPositionAndRotation(new Vector3(spawnPoint.transform.position.x - 5f, characterObject.transform.position.y, spawnPoint.transform.position.z), spawnPoint.transform.rotation);
                    canvas.transform.SetPositionAndRotation(new Vector3(spawnPoint.transform.position.x - 5f, canvas.transform.position.y, spawnPoint.transform.position.z + 1.5f), spawnPoint.transform.rotation);
                    break;
            }

            if (cc != null)
                cc.enabled = false;

            player.transform.SetPositionAndRotation(spawnPoint.transform.position, spawnPoint.transform.rotation);

            if (cc != null)
                cc.enabled = true;

            if (character == CharacterDialogue.Outlaw)
            {
                ed.Disable();
                ResetIndex();
                return;
            }

            buttonTextElement.text = "Start the duel >>";
            button.transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100f);
            button.transform.GetComponent<Image>().color = new Color(136, 0, 255, 100); // PURPLE
        }

        if (dialogue[_index].StartsDuel)
        {
            button.transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120f);
            button.transform.GetComponent<Image>().color = character == CharacterDialogue.Outlaw
                ? new Color(136, 0, 255, 100) // PURPLE
                : new Color(0, 239, 255, 100); // BLUE
            buttonTextElement.text = character == CharacterDialogue.Outlaw
                ? "Face the outlaw .."
                : "Take up the offer >>";
        }
    }

    public void ResetIndex() => _index = 0;

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
