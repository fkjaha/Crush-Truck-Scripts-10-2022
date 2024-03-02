using DG.Tweening;
using TMPro;
using UnityEngine;

public class FlyingText : MonoBehaviour
{
    [SerializeField] private float heightChange;
    [SerializeField] private float flyTime;
    [SerializeField] private TextMeshPro thisTextMeshPro;
    
    private Vector3 _startPos;

    private void Start()
    {
        _startPos = transform.localPosition;
    }

    public void Activate(string text)
    {
        thisTextMeshPro.text = text;

        transform.DOKill();
        transform.localPosition = _startPos;
        thisTextMeshPro.DOFade(1f, 0f);
        gameObject.SetActive(true);

        transform.DOLocalMoveY( _startPos.y + heightChange, flyTime).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
        thisTextMeshPro.DOFade(0f, flyTime);
    }
}
