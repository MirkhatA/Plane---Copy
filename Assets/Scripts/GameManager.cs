using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform field;
    public Transform goalField;
    public GameObject button;

    public TMP_Text scoreTMP;
    public TMP_Text winTMP;

    public GameObject GameUI;
    public GameObject WinUI;

    public float hideDelay = 2f;

    public List<Button> buttons = new List<Button>();
    public List<Button> goalButtons = new List<Button>();

    public List<Sprite> sprites = new List<Sprite>();
    
    private string buttonGoalNames = "";
    private int numOfClicks = 0;
    

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        numOfClicks = 0;
        scoreTMP.text = numOfClicks.ToString();

        GameUI.SetActive(true);
        WinUI.SetActive(false);

        GenerateCells();
        GetButtons();
        GenerateGoalCell();
        AddListeners();

        field.gameObject.SetActive(false);
        goalField.gameObject.SetActive(true);

        StartCoroutine(SwapFieldsAfterDelay());
    }

    private IEnumerator SwapFieldsAfterDelay()
    {
        yield return new WaitForSeconds(hideDelay);

        goalField.gameObject.SetActive(false);
        field.gameObject.SetActive(true);
    }

    private void GenerateGoalCell()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject _button = Instantiate(button);
            _button.name = "" + goalButtons[i].name;
            _button.transform.SetParent(goalField, false);

            Image buttonImage = _button.GetComponent<Image>();
            buttonImage.sprite = sprites[int.Parse(goalButtons[i].name)];
        }
    }

    private void GenerateCells()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject _button = Instantiate(button);
            _button.name = "" + i;
            _button.transform.SetParent(field, false);

            Image buttonImage = _button.GetComponent<Image>();
            buttonImage.sprite = sprites[i];
        }
    }

    private void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("cellBtn");
        
        for (int i = 0; i < objects.Length; i++)
        {
            buttons.Add(objects[i].GetComponent<Button>());
        }

        goalButtons.AddRange(buttons);
        Shuffle(goalButtons);

        if (buttonGoalNames == "")
        {
            buttonGoalNames = string.Join(", ", goalButtons.Select(btn => btn.name).ToArray());
        }
    }

    private void Shuffle(List<Button> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Button value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    private void AddListeners()
    {
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => PickPuzzle());
        }
    }

    private void PickPuzzle()
    {
        GameObject selectedObject = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        string name = selectedObject.name;
        int newName = int.Parse(name) + 1;

        if (newName == 8)
        {
            newName = 0;
        }

        selectedObject.name = newName.ToString();
        Image buttonImage = selectedObject.GetComponent<Image>();
        buttonImage.sprite = sprites[newName];

        string buttonNames = string.Join(", ", buttons.Select(btn => btn.name).ToArray());
        numOfClicks += 1;

        scoreTMP.text = "Steps: " + numOfClicks;

        if (buttonNames.Equals(buttonGoalNames))
        {
            WinUI.SetActive(true);
            GameUI.SetActive(false);
            winTMP.text = "You win. Number of steps: " + numOfClicks;
        }
    }

}
