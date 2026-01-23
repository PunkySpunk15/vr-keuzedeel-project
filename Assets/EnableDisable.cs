using UnityEngine;

public class EnableDisable : MonoBehaviour
{
    public GameObject objectToToggle;
    public DialogueHandler dialogueHandler = null;

    public void Toggle()
    {
        if (!objectToToggle.activeSelf)
            Enable();
        else
            Disable();
    }

    public void Enable()
    {
        if (dialogueHandler != null)
            dialogueHandler.StartDialogue();

        objectToToggle.SetActive(true);
    }

    public void Disable()
    {
        if (dialogueHandler != null)
            dialogueHandler.ResetIndex();

        objectToToggle.SetActive(false);
    }
}