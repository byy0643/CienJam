using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    int resendCount;
    public GameObject _timer;
    Timer timerScript;
    public GameObject _background;
    Background backgroundScript;
    public GameObject _letter;
    Letter letterScript;
    public GameObject _answerDrawer;
    Transform[] drawers;
    List<AnswerDrawer> answerDrawerScript = new List<AnswerDrawer>();


    AnswerDrawer choosen;

    public Player _player = new Player();
    public TextMeshProUGUI gold;

    public GameObject inGameUI;
    public GameObject gameOverUI;
    public TextMeshProUGUI correctText;
    public TextMeshProUGUI wrongText;
    public TextMeshProUGUI resendText;
    public RawImage medal;
    public TextMeshProUGUI moneyText;

    public Texture[] medals;
    public Button resend;

    bool isGameOver;
    // Start is called before the first frame update
    private void Awake()
    {
        isGameOver = false;
        resend.onClick.AddListener(() => Resend());
        resendCount = 0;
        inGameUI.SetActive(true);
        gameOverUI.SetActive(false);
        timerScript = _timer.GetComponent<Timer>();
        backgroundScript = _background.GetComponent<Background>();
        letterScript = _letter.GetComponent<Letter>();
        drawers = _answerDrawer.GetComponentsInChildren<Transform>();
    }
    void Start()
    {
        Time.timeScale = 1.0f;
        for (int i = 0; i < drawers.Length; i++)
        {
            if (drawers[i].GetComponent<AnswerDrawer>() != null)
            {
                Debug.Log(drawers[i].GetComponent<AnswerDrawer>());
                answerDrawerScript.Add(drawers[i].GetComponent<AnswerDrawer>()) ;
            }

        }
        SetAnswerDrawer();
    }

    // Update is called once per frame
    void Update()
    {
        gold.text = _player.curMoney.ToString();
        if (timerScript.minuteAngle <= 3 && timerScript.minuteAngle > 1)
        {
            backgroundScript.now = Background.dayTime.PM;
        } 
        else if (timerScript.minuteAngle <= 1)
        {
            backgroundScript.now = Background.dayTime.NIGHT;
        }
        else
        {
            backgroundScript.now = Background.dayTime.AM;
        }

        if((IsFocus() && Input.GetMouseButtonUp(0) && letterScript.isPicked))
        {
            _letter.SetActive(false);
            _letter.transform.position = new Vector3(-2.98f, -2.53f, -5.671076f);
            _letter.SetActive(true);
            if (choosen.Correct())
            {
                _player.curMoney += 1000;
                _player.corCount++;
            }
            else
            {
                if(_player.curMoney >= 1000) {
                    _player.curMoney -= 1000;
                }
                else
                {
                    _player.curMoney = 0;
                }
                _player.wroCount++;
            }
            SetAnswerDrawer();
        }

        if(timerScript.minuteAngle == 0 || !letterScript.hasQuiz)
        {
            GameOver();
        }
        if(isGameOver && Input.anyKeyDown)
        {
            SceneManager.LoadScene("Main");
        }
    }

    public void Resend()
    {
        Debug.Log("Button Click");
        _letter.SetActive(false);
        _letter.transform.position = new Vector3(-2.98f, -2.53f, -5.671076f);
        _letter.SetActive(true);
        if (_player.curMoney >= 500)
        {
            _player.curMoney -= 500;
        }
        else
        {
            _player.curMoney = 0;
        }
        resendCount++;
        SetAnswerDrawer();
    }

    bool IsFocus()
    {
        for(int i = 0; i<answerDrawerScript.Count; i++)
        {
            if (answerDrawerScript[i].isActivated)
            {
                choosen = answerDrawerScript[i];
                return true;
            }
        }
        return false;
    }

    void SetAnswerDrawer()
    {
        int ansIndex;
        List<int> ansIndexList = new List<int>();
        List<string> answers = letterScript.GetAnswers();
        for(int i = 0; i<4; i++)
        {
            ansIndex = Random.Range(0, 4);
            if (ansIndexList.Contains(ansIndex))
            {
                i--;
                continue;
            }
            else
            {
                ansIndexList.Add(ansIndex);
                answerDrawerScript[i].SetAnswer(answers[ansIndex]);
                if(ansIndex == 0)
                {
                    answerDrawerScript[i].isAnswer = true;
                }
            }
        }
    }

    public void GameOver()
    {
        correctText.text = "X " + _player.corCount.ToString();
        wrongText.text ="X " + _player.wroCount.ToString();
        resendText.text = "X " + resendCount.ToString();
        if(_player.curMoney > 20000)
        {
            medal.texture = medals[2];
        }else if(_player.curMoney<=20000 && _player.curMoney > 10000)
        {
            medal.texture = medals[1];
        }
        else
        {
            medal.texture = medals[0];
        }
        moneyText.text = _player.curMoney.ToString() + "원";
        inGameUI.SetActive(false);
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        isGameOver = true;
    }

    
}
