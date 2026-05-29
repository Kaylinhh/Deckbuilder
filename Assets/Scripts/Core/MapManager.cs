using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public enum NodeType
{
    Combat,
    Campfire
}

[System.Serializable]
public class MapNode
{
    public Button button;
    public NodeType type;
    public string enemyName;
    public GameObject enemyPrefab;
}


public class MapManager : MonoBehaviour
{
    public MapNode[] nodes;
    public RectTransform arrowIndicator;

    public Color lockedColor = Color.gray;
    public Color completedColor = Color.green;
    public Color currentColor = Color.white;

        private void Awake()
    {
        GameManager.OnMapShown += HandleMapShown;
    }

    private void OnDestroy()
    {
        GameManager.OnMapShown -= HandleMapShown;
    }

    private void HandleMapShown()
    {
        StartCoroutine(RefreshMapNextFrame());
    }

    private void Start()
    {
        GameManager.Instance.currentEnemyPrefab = nodes[0].enemyPrefab;
        StartCoroutine(RefreshMapNextFrame());
    }

    private IEnumerator RefreshMapNextFrame()
    {
        yield return new WaitForEndOfFrame();
        RefreshMap();
    }

    public void RefreshMap()
    {
        int currentIndex = GameManager.Instance.currentCombatIndex;
        Debug.Log($"RefreshMap — currentCombatIndex: {currentIndex}");

        for (int i = 0; i < nodes.Length; i++)
        {
            if (i < currentIndex)
            {
                // Completed
                nodes[i].button.interactable = false;
                nodes[i].button.image.color = completedColor;
            }
            else if (i == currentIndex)
            {
                // Current
                nodes[i].button.interactable = true;
                nodes[i].button.image.color = currentColor;
                RectTransform nodeRect = nodes[i].button.GetComponent<RectTransform>();
                arrowIndicator.position = nodes[i].button.transform.position + Vector3.up * 60f;
                Debug.Log($"Node pos: {nodeRect.anchoredPosition}, Node world: {nodeRect.position}");
            }
            else
            {
                // Locked
                nodes[i].button.interactable = false;
                nodes[i].button.image.color = lockedColor;
            }
        }
    }

        public void OnNodeClicked(int index)
    {
        MapNode node = nodes[index];
        
        if (node.type == NodeType.Campfire)
        {
            int healAmount = Mathf.RoundToInt(GameManager.Instance.maxHP * 0.2f);
            GameManager.Instance.currentHP = Mathf.Min(
                GameManager.Instance.currentHP + healAmount,
                GameManager.Instance.maxHP
            );
            GameManager.Instance.WinCombat();
            RefreshMap();
        }
        else
        {
            GameManager.Instance.currentEnemyPrefab = nodes[index].enemyPrefab;
            GameManager.Instance.ShowCombat();
        }
    }
}