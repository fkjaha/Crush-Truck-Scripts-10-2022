using UnityEngine;

public class UIMethods : MonoBehaviour
{
    public void ToggleObject(GameObject toggleObject)
    {
        toggleObject.SetActive(!toggleObject.activeSelf);
    }
}
