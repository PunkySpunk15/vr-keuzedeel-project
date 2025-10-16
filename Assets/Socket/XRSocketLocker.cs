using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class SocketLocker : MonoBehaviour
{
    [Header("Lock Settings")]
    [Tooltip("If true, object cannot be removed from socket once placed.")]
    public bool lockObjectInSocket = false;

    private XRSocketInteractor socket;
    private XRGrabInteractable currentSocketedObject;

    void Start()
    {
        socket = GetComponent<XRSocketInteractor>();

        if (socket != null)
        {
            socket.selectEntered.AddListener(OnObjectEntered);
            socket.selectExited.AddListener(OnObjectExiting);
        }
    }

    void OnObjectEntered(SelectEnterEventArgs args)
    {
        // Track the current socketed object
        currentSocketedObject = args.interactableObject as XRGrabInteractable;

        if (lockObjectInSocket)
        {
            Debug.Log("Object locked in socket");
        }
    }

    void OnObjectExiting(SelectExitEventArgs args)
    {
        if (lockObjectInSocket)
        {
            // Prevent removal by re-attaching
            StartCoroutine(ReattachToSocket());
            Debug.Log("Attempted to remove locked object - blocking removal");
        }
        else
        {
            currentSocketedObject = null;
        }
    }

    IEnumerator ReattachToSocket()
    {
        yield return new WaitForEndOfFrame();

        if (currentSocketedObject != null && socket != null)
        {
            socket.StartManualInteraction(currentSocketedObject);
        }
    }

    // Public method to lock/unlock at runtime
    public void SetLocked(bool locked)
    {
        lockObjectInSocket = locked;
        Debug.Log($"Socket lock set to: {locked}");
    }

    // Public method to check if locked
    public bool IsLocked()
    {
        return lockObjectInSocket;
    }

    void OnDestroy()
    {
        if (socket != null)
        {
            socket.selectEntered.RemoveListener(OnObjectEntered);
            socket.selectExited.RemoveListener(OnObjectExiting);
        }
    }
}