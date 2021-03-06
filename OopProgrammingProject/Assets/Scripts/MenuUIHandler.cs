using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject focalPoint;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private GameObject instructionsPanel;
    [SerializeField]
    private GameObject highScorePanel;
    [SerializeField]
    private InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void Update()
    {
        focalPoint.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
    public void Instructions()
    {
        instructionsPanel.SetActive(!instructionsPanel.activeSelf);
    }
    public void HighScores()
    {
        highScorePanel.SetActive(!highScorePanel.activeSelf);
    }

    public void StartNew()
    {
        DataManager.Instance.inputName = inputField.text;
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
             Application.Quit(); // original code to quit Unity player
        #endif
    }
}
