using System;
using System.Collections.Generic;
using UnityEngine;

public class TextPool : MonoBehaviour
{
    public static TextPool Instance;

    [SerializeField] private List<FlyingText> texts;
    private Queue<FlyingText> _textsQueue = new();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (FlyingText flyingText in texts)
        {
            _textsQueue.Enqueue(flyingText);
        }
    }

    public void PlayString(string text)
    {
        FlyingText textToActivate = _textsQueue.Dequeue();
        textToActivate.gameObject.SetActive(true);
        textToActivate.Activate(text);
        Debug.Log("z");
        _textsQueue.Enqueue(textToActivate);
    }
}
