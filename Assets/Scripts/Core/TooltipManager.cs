using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance;

    public GameObject tooltipPanel;
    public TextMeshProUGUI tooltipText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
            
        tooltipPanel.SetActive(false);
    }

    public void Show(string content, Vector3 mousePosition)
    {
        tooltipPanel.SetActive(true);
        tooltipText.text = content;
        tooltipPanel.transform.position = mousePosition + Vector3.up * 10f + Vector3.right * 10f;
    }

    public void Hide()
    {
        tooltipPanel.SetActive(false);
    }
}