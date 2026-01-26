using UnityEngine;

public class EnableDisable : MonoBehaviour
{
    public GameObject objectToToggle;
    public DialogueHandler dialogueHandler = null;
    public DialogueHandler.Character? character = null;

    public void Toggle()
    {
        if (!objectToToggle.activeSelf)
            Enable();
        else
            Disable();
    }

    public void Enable(bool keepCurrentIndex = false)
    {
        if (objectToToggle.activeSelf)
            return;

        if (dialogueHandler != null && !keepCurrentIndex)
            dialogueHandler.StartDialogue();

        //if (character != null && character == DialogueHandler.Character.Outlaw)
        //{
        //    dialogueHandler.TriggerOutlawEntrance();
        //}

        objectToToggle.SetActive(true);
    }

    public void Disable(bool keepCurrentIndex = false)
    {
        if (!objectToToggle.activeSelf)
            return;

        if (dialogueHandler != null && !keepCurrentIndex)
            dialogueHandler.ResetIndex();

        objectToToggle.SetActive(false);
    }
}