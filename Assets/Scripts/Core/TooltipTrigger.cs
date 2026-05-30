using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public string content;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager.Instance.Show(content, eventData.position);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        TooltipManager.Instance.Show(content, eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.Hide();
    }
}