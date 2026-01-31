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
        new("Howdy, wanna practice yer aim?", false, 0),
        new("I'll go easy on ya, but I will shoot after yer headstart of 3 seconds is up.", true, 0),
        new("Alright, remember to only shoot when the timer hits zero! No playin' dirty 'round these parts.", false, 1),
        new("Ya got me, good work!", false, 3),
        new("Head over to the saloon, y' earned a gut warmer.", false, 4)
    };

    private readonly List<Dialogue> _informantDialogue = new() {
        new("Hey there, seems like yer the new sheriff.. wanna know a few things?", false, 0),
        new("Folks say he's comin' to town to stir the pot once again.. y'know.. the outlaw?", false, 0),
        new("Maybe y've seen a paper with that wanted outlaw's face on 't laying around.", false, 0),
        new("Take a look, yer gonna need to watch out fer him.", false, 0),
        new("Hey, listen..", false, 0),
        new("I'd like to see what yer made of, how's 'bout we duel outside fer a minute?", true, 1),
        new("Good ol' fashioned duel, pull the trigger when it's time.", false, 1),
        new("Woah, guess y' got what 't takes..", false, 3)
    };

    private readonly List<Dialogue> _outlawDialogue = new() {
        new("...", false, 0),
        new("I see the new sheriff is gettin' all roostered up in the midday!", false, 1),
        new("Ha, y' look like you've been rode hard 'n put up wet!", false, 1),
        new("I'll make y' hallow, cowboy.", true, 1)
    };

    //UI elements
    public Canvas canvas;
    public Character character;
    public TextMeshProUGUI textElement;
    public Button button;
    public TextMeshProUGUI buttonTextElement;
    public EnableDisable duelCanvas;

    //Objects
    public List<GameObject> characterObjects;
    public GameObject player;
    public GameObject spawnPoint;
    public GameObject outlawSpawnPoint;
    public GameObject grabToMove;

    //Misc
    public Duel duel;
    public SitDown sd;
    public GameObject lastCharacterObject;
    public EnableDisable zaraEd;
    public EnableDisable zaraTableEd;
    public EnableDisable zaraHorseEd;
    private int _index = 0;
    private EnableDisable _ed;

    //Colors
    private Color _blue = new(0, 239, 255, 100);
    private Color _green = new(0, 255, 154, 100);
    private Color _purple = new(136, 0, 255, 100);

    public void StartDialogue()
    {
        ResetIndex();
        List<Dialogue> dialogue = character switch
        {
            Character.Guide => _guideDialogue,
            Character.Informant => _informantDialogue,
            Character.Outlaw => _outlawDialogue
        };

        textElement.text = dialogue[_index].Text;

        buttonTextElement.text = "Next >>";

        int index = dialogue[_index].CharacterObjectIndex;
        lastCharacterObject = characterObjects[index];
        _ed = lastCharacterObject.GetComponent<EnableDisable>();
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

        if (_index + 1 > dialogue.Count && character is not Character.Outlaw)
        {
            _ed.Disable();
            lastCharacterObject.SetActive(false);
            return;
        }

        if (buttonTextElement.text is "Start the duel >>")
        {
            duel.StartDuel();
            _ed.Disable(true);
            duelCanvas.Enable();

            return;
        }

        if (buttonTextElement.text is "Take up the offer >>" or "Face the outlaw ..")
        {
            CharacterController cc = player.GetComponent<CharacterController>();

            foreach (GameObject characterObject in characterObjects)
                Destroy(characterObject.GetComponent<CheckplayerDistance>());

            switch (character)
            {
                case Character.Informant:
                case Character.Guide:
                    foreach (GameObject characterObject in characterObjects)
                        characterObject.transform.SetPositionAndRotation(
                            new Vector3(spawnPoint.transform.position.x, (
                            character is Character.Informant
                                ? spawnPoint.transform.position.y
                                : characterObject.transform.position.y), spawnPoint.transform.position.z - 5f),
                            new Quaternion(characterObject.transform.rotation.x, 0f, characterObject.transform.rotation.z, characterObject.transform.rotation.w)
                           );

                    canvas.transform.SetPositionAndRotation(new Vector3(spawnPoint.transform.position.x - 1.5f, canvas.transform.position.y - 0.5f, spawnPoint.transform.position.z - 3f), spawnPoint.transform.rotation);
                    break;
                case Character.Outlaw:
                    foreach (GameObject characterObject in characterObjects)
                        characterObject.transform.SetPositionAndRotation(
                            new Vector3(spawnPoint.transform.position.x - 2f, spawnPoint.transform.position.y, spawnPoint.transform.position.z),
                            outlawSpawnPoint.transform.rotation);

                    canvas.transform.SetPositionAndRotation(new Vector3(spawnPoint.transform.position.x - 3f, canvas.transform.position.y - 0.5f, spawnPoint.transform.position.z + 1.5f), spawnPoint.transform.rotation);
                    break;
            }

            if (cc != null)
                cc.enabled = false;

            player.transform.SetPositionAndRotation(spawnPoint.transform.position, spawnPoint.transform.rotation);

            if (cc != null)
                cc.enabled = true;

            grabToMove.SetActive(false);

            if (character is Character.Outlaw)
            {
                duel.StartDuel();
                canvas.GetComponent<EnableDisable>().Disable();
                duelCanvas.Enable();

                zaraTableEd.Disable();
                zaraEd.Enable();
                zaraHorseEd.Enable();
                lastCharacterObject = SetCharacterObject(2);

                return;
            }

            buttonTextElement.text = "Start the duel >>";
            button.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 100f);
            button.GetComponent<Image>().color = _purple;
        }

        textElement.text = dialogue[_index].Text;
        lastCharacterObject = SetCharacterObject(dialogue[_index].CharacterObjectIndex);

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
        duel.StopDuel();

        if (character is Character.Outlaw)
        {
            HandleWinOutlawDuel();
            return;
        }

        duelCanvas.Disable();

        List<Dialogue> dialogue = character switch
        {
            Character.Guide => _guideDialogue,
            Character.Informant => _informantDialogue,
            Character.Outlaw => _outlawDialogue
        };

        _ed.Enable(true);
        grabToMove.SetActive(true);

        textElement.text = dialogue[_index].Text;
        lastCharacterObject = SetCharacterObject(dialogue[_index].CharacterObjectIndex);
        foreach (GameObject characterObject in characterObjects)
            characterObject.AddComponent<CheckplayerDistance>().minDistanceMoved = 2;

        buttonTextElement.text = "Next >>";
        button.transform.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 60f);
        button.transform.GetComponent<Image>().color = _green;
    }

    private void HandleWinOutlawDuel()
    {
        TextMeshProUGUI text = duelCanvas.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI timerText = duelCanvas.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();

        text.text = "You won!!";
        timerText.text = string.Empty;

        EnableDisable button = duelCanvas.transform.GetChild(2).gameObject.GetComponent<EnableDisable>();
        button.Enable();
    }

    public void ResetIndex() => _index = 0;

    public GameObject SetCharacterObject(int index)
    {
        GameObject newCharacterObject = characterObjects[index];

        lastCharacterObject.SetActive(false);
        newCharacterObject.SetActive(true);
        _ed = lastCharacterObject.GetComponent<EnableDisable>();

        return newCharacterObject;
    }

    public void TriggerOutlawEntrance()
    {
        sd.RotatePlayer();

        //Then enable sounds and finally regular dialogue handling.
    }

    class Dialogue
    {
        public string Text { get; set; }
        public bool StartsDuel { get; set; }
        public int CharacterObjectIndex { get; set; }

        public Dialogue(string text, bool startsDuel, int characterObjectIndex)
        {
            Text = text;
            StartsDuel = startsDuel;
            CharacterObjectIndex = characterObjectIndex;
        }
    }
}
