using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    public enum Character
    {
        Guide,
        Informant,
        Outlaw
    }

    private readonly List<Dialogue> _guideDialogue = new() {
        new("Howdy, wanna practice yer aim?", false),
        new("I'll go easy on ya, but I will shoot after yer headstart of 2 seconds is up.", true),
        new("Alright, remember to only shoot when the timer hits zero! No playin' dirty 'round these parts.", false),
        new("Ya got me, good work!", false),
        new("Head over to the saloon, y' earned a gut warmer.", false)
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
    public Character character;
    public TextMeshProUGUI textElement;
    public Button button;
    public TextMeshProUGUI buttonTextElement;
    public EnableDisable duelCanvas;

    //Objects
    public GameObject characterObject;
    public GameObject player;
    public GameObject spawnPoint;

    //Misc
    public EnableDisable ed;
    public Duel duel;
    public SitDown sd;
    private int _index = 0;

    //Colors
    private Color _blue = new(0, 239, 255, 100);
    private Color _green = new(0, 255, 154, 100);
    private Color _purple = new(136, 0, 255, 100);

    public void StartDialogue()
    {
        ResetIndex();
        textElement.text = character switch
        {
            Character.Guide => _guideDialogue[_index].Text,
            Character.Informant => _informantDialogue[_index].Text,
            Character.Outlaw => _outlawDialogue[_index].Text,
            _ => ""
        };

        buttonTextElement.text = "Next >>";
    }

    public void NextDialogue()
    {
        List<Dialogue> dialogue = character switch
        {
            Character.Guide => _guideDialogue,
            Character.Informant => _informantDialogue,
            Character.Outlaw => _outlawDialogue
        };

        _index++;

        if (_index + 1 > dialogue.Count)
        {
            ed.Disable();
            characterObject.SetActive(false);
            return;
        }

        if (buttonTextElement.text is "Start the duel >>")
        {
            duel.StartDuel();
            ed.Disable(true);
            duelCanvas.Enable();

            if (character == Character.Outlaw)
            {
                //No dialogue after duel with outlaw
                ResetIndex();
            }

            return;
        }

        if (buttonTextElement.text is "Take up the offer >>" or "Face the outlaw ..")
        {
            //Send player to duel location
            CharacterController cc = player.GetComponent<CharacterController>();
            Destroy(characterObject.GetComponent<CheckplayerDistance>());

            switch (character)
            {
                case Character.Informant:
                case Character.Guide:
                    characterObject.transform.SetPositionAndRotation(new Vector3(spawnPoint.transform.position.x, characterObject.transform.position.y, spawnPoint.transform.position.z - 5f), spawnPoint.transform.rotation);
                    canvas.transform.SetPositionAndRotation(new Vector3(spawnPoint.transform.position.x - 1.5f, canvas.transform.position.y, spawnPoint.transform.position.z - 4f), spawnPoint.transform.rotation);
                    break;
                case Character.Outlaw:
                    characterObject.transform.SetPositionAndRotation(new Vector3(spawnPoint.transform.position.x - 5f, characterObject.transform.position.y, spawnPoint.transform.position.z), spawnPoint.transform.rotation);
                    canvas.transform.SetPositionAndRotation(new Vector3(spawnPoint.transform.position.x - 5f, canvas.transform.position.y, spawnPoint.transform.position.z + 1.5f), spawnPoint.transform.rotation);
                    break;
            }

            if (cc != null)
                cc.enabled = false;

            player.transform.SetPositionAndRotation(spawnPoint.transform.position, spawnPoint.transform.rotation);

            if (cc != null)
                cc.enabled = true;

            if (character is Character.Outlaw)
            {
                ed.Disable();
                return;
            }

            buttonTextElement.text = "Start the duel >>";
            button.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100f);
            button.GetComponent<Image>().color = _purple;
        }

        textElement.text = dialogue[_index].Text;

        if (dialogue[_index].StartsDuel)
        {
            button.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120f);
            button.GetComponent<Image>().color = character == Character.Outlaw
                ? _purple
                : _blue;
            buttonTextElement.text = character == Character.Outlaw
                ? "Face the outlaw .."
                : "Take up the offer >>";
        }
    }

    public void StartAfterDuelDialogue()
    {
        if (character == Character.Outlaw)
            return; //Shouldnt get here

        duel.active = false;
        duelCanvas.Disable();

        List<Dialogue> dialogue = character switch
        {
            Character.Guide => _guideDialogue,
            Character.Informant => _informantDialogue,
            Character.Outlaw => _outlawDialogue
        };

        ed.Enable(true);

        characterObject.AddComponent<CheckplayerDistance>().minDistanceMoved = 2;

        textElement.text = dialogue[_index].Text;

        buttonTextElement.text = "Next >>";
        button.transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 60f);
        button.transform.GetComponent<Image>().color = _green;
    }

    public void ResetIndex() => _index = 0;

    public void TriggerOutlawEntrance()
    {
        sd.RotatePlayer();

        //Then enable sounds and finally regular dialogue handling.
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
