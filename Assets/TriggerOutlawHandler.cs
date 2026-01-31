using UnityEngine;

public class TriggerOutlawHandler : MonoBehaviour
{
    public EnableDisable outlawEd;
    public EnableDisable canvasEd;
    public DialogueHandler dh;
    public AudioSource audio;

    private bool _isWantedPaperInSocket = false;
    private bool _isWhiskeyInSocket = false;
    private bool _isBadgeInSocket = false;

    private bool _hasBeenTriggered = false;

    public void IsWantedPaperInSocket(bool answer)
    {
        _isWantedPaperInSocket = answer;
        TriggerOutlaw();
    }
    public void IsWhiskeyInSocket(bool answer)
    {
        _isWhiskeyInSocket = answer;
        TriggerOutlaw();
    }
    public void IsBadgeInSocket(bool answer)
    {
        _isBadgeInSocket = answer;
        TriggerOutlaw();
    }

    public void TriggerOutlaw()
    {
        //Only if all items are in socket and the outlaw hasn't been triggered already.
        if (
            (_isWantedPaperInSocket && _isWhiskeyInSocket && _isBadgeInSocket)
            &&
            !_hasBeenTriggered
           )
        {
            outlawEd.Enable();
            audio.Play();
            canvasEd.Enable();
            dh.TriggerOutlawEntrance();
            _hasBeenTriggered = true;
        }
    }
}
