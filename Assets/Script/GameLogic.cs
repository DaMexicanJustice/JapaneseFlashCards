using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameLogic : MonoBehaviour
{
    public static GameLogic Instance { get; private set; }

    public TextMeshProUGUI tmp;
    public TextMeshProUGUI tmpScore;
    public TextMeshProUGUI tmpHighscore;
    public TMP_InputField inputField;
    public TMP_InputField tmpInputField;

    int score = 0;
    public string jsonFileName;

    LetterList letterList;
    public List<Question> questions;
    public Question chosenQuestion;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        PanelManager.Instance.EnablePanel("Main Panel");
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        questions = new List<Question>();
        PropagateLetters();
        PickNewLetter();
        tmpScore.text = score + "/" + (letterList.letters.Length * 2);
    }

    // Update is called once per frame
    void Update()
    {
        tmpHighscore.text = "Personal best: " + GetHighscore();
        Debug.Log(questions.Count);
    }

    public void ConfirmAnswer()
    {
        if (string.Equals(chosenQuestion.letter.romaji, tmpInputField.text, StringComparison.OrdinalIgnoreCase))
        {
            audioSource.clip = Resources.Load("right") as AudioClip;
            score++;
            tmpScore.text = score + "/" + (letterList.letters.Length * 2);
            tmpInputField.text = "";
            RemoveLetterFromQuestion();
            PickNewLetter();
        } else
        {
            audioSource.clip = Resources.Load("wrong") as AudioClip;
        }
        SetHighscore();
        audioSource.Play();
    }

    public void Reload()
    {
        SceneManager.LoadScene(0);
    }

    public void RevealAnswer()
    {
        if (chosenQuestion != null)
        {
            tmp.text = chosenQuestion.letter.romaji;
        }
    }

    public void PickNewLetter()
    {
        try
        {
            chosenQuestion = questions[UnityEngine.Random.Range(0, questions.Count)];
            if (CheckIfNewQuestion(chosenQuestion) == true)
            {
                tmp.text = chosenQuestion.HiraganaUsed() ? chosenQuestion.letter.katakana : chosenQuestion.letter.hiragana;
            } else
            {
                questions.Remove(chosenQuestion);
                PickNewLetter();
            }
        } catch (Exception e)
        {
            Debug.Log(e);
            PanelManager.Instance.DisablePanel("Main Panel");
            PanelManager.Instance.EnablePanel("Toast Panel");
        }

    }

    public void Cheat()
    {
        if (Application.isEditor)
        {
            inputField.text = chosenQuestion.letter.romaji;
            ConfirmAnswer();
        }
    }

    

    void RemoveLetterFromQuestion()
    {
        if (chosenQuestion.HiraganaUsed() == false)
        {
            tmp.text = chosenQuestion.letter.hiragana;
            questions[questions.IndexOf(chosenQuestion)].UseHiragana();
        }
        else
        {
            tmp.text = chosenQuestion.letter.katakana;
            questions[questions.IndexOf(chosenQuestion)].UseKatakana();
        }
    }

    private bool CheckIfNewQuestion(Question q)
    {
        if (q.HiraganaUsed() == true && q.KatakanaUsed() == true)
        {
            return false;
        } else
        {
            return true;
        }
    }
         
    private void PropagateLetters()
    {
        letterList = new LetterList();
        letterList = JsonUtility.FromJson<LetterList>(PrepareJSON());

        foreach (Letter l in letterList.letters)
        {
            questions.Add(new Question(l));
        }
    }

    private string PrepareJSON()
    {
        TextAsset textAsset = Resources.Load(jsonFileName) as TextAsset;
        return textAsset.text;
    }

    public bool CheckAnswer(string answer)
    {

        return false;
    }

    void SetHighscore()
    {
        if (score > GetHighscore())
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
    }

    int GetHighscore()
    {
        return PlayerPrefs.GetInt("Highscore");
    }

}
