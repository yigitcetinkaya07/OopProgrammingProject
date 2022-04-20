using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highScoreEntyTransfomList;
    private int maxScoreCount = 20;
    private void Awake()
    {
        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);
        //HighScores highScores = DataManager.Instance.GetHighScores();
        HighScores highScores = DataManager.Instance.GetHighScores();

        highScoreEntyTransfomList = new List<Transform>();
        if (PlayerPrefs.HasKey("highScoreTable"))
        {
            foreach (HighScoreEntry highScoreEntry in highScores.highScoreEntryList)
            {
                CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highScoreEntyTransfomList);
            }
        }
    }
    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 24f;
        Transform entryTransform = Instantiate(entryTemplate, entryContainer);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, 0 - templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;

        switch (rank)
        {
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
            default: rankString = rank + "TH"; break;
        }
        int score = highScoreEntry.score;
        entryTransform.Find("PosText").GetComponent<Text>().text = rankString;
        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();
        string name = highScoreEntry.name;
        entryTransform.Find("NameText").GetComponent<Text>().text = name;
        entryTransform.Find("BackgroundScore").gameObject.SetActive(rank % 2 == 1);
        transformList.Add(entryTransform);
    }
}
public class HighScores
{
    public List<HighScoreEntry> highScoreEntryList;

}
[System.Serializable]
//This line is required for JsonUtility
public class HighScoreEntry
{
    public int score;
    public string name;
}


