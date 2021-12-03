using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHighlighter : MonoBehaviour
{
    public static TileHighlighter Instance { get; set; }

    public GameObject highlightPrefab;
    private List<GameObject> highlights;


    private void Start()
    {
        Instance = this;
        highlights = new List<GameObject>();
    }

    private GameObject GetHightLight()
    {
        GameObject gameObject = highlights.Find(j => !j.activeSelf);
        if (gameObject == null)
        {
            gameObject = Instantiate(highlightPrefab);
            highlights.Add(gameObject);
        }
        return gameObject;
    }

    public void HighlightPossible(bool[,] moves)
    {
        for (int i = 0; i < 8; i++)
        {

            for (int j = 0; j < 8; j++)
            {
                if (moves[i, j])
                {
                    GameObject gameObject = GetHightLight();
                    gameObject.SetActive(true);
                    gameObject.transform.position = new Vector3(i + 0.5f, 0, j + 0.5f);
                }
            }
        }
    }

    public void HideHighights()
    {
        foreach (GameObject gameObject in highlights)
        {
            gameObject.SetActive(false);
        }
    }
}
