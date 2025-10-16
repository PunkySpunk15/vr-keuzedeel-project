using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisable : MonoBehaviour
{
    public GameObject objectToToggle;

    public void Toggle()
    {
        objectToToggle.SetActive(!objectToToggle.activeSelf);
    }

    public void Enable()
    {
        objectToToggle.SetActive(true);
    }

    public void Disable()
    {
        objectToToggle.SetActive(false);
    }
}