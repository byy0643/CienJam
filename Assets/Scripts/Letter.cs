using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Letter: MonoBehaviour
{
    int[][][] sudokuArray = new int[12][][];
    List<int> sudokuIndexList = new List<int>();
    List<int> numberIndexList = new List<int>();
    List<int> pictureIndexList = new List<int>();
    string[] numAnswer = new string[8];
    string[] picAnswer = new string[13];
    Transform sudoku;
    MeshRenderer _renderer;
    public bool isPicked;
    public bool hasQuiz;

    string ans;
    int ansIndex;

    Vector2 mousePosition;
    Camera cam;

    public Material paper;
    public Material[] numberQuiz;
    public Material[] pictureQuiz;

    public enum quizType { SUDOKU, NUMBER, PICTURE };
    public quizType curType;
    // Start is called before the first frame update
    private void Awake()
    {
        sudokuArray[0] = new int[][] {
            new int[] {4, 1, 3, 2},
            new int[] {3, 2, 4, 0},
            new int[] {1, 4, 0, 3},
            new int[] {2, 0, 1, 4},
            new int[] {1, 2, 3},
        };
        sudokuArray[1] = new int[][] {
            new int[] {4, 0, 1, 2},
            new int[] {2, 1, 3, 4},
            new int[] {3, 4, 2, 1},
            new int[] {0, 2, 0, 3},
            new int[] {3, 1, 4},
        };

        sudokuArray[2] = new int[][] {
            new int[] {0, 4, 1, 3},
            new int[] {3, 0, 4, 2},
            new int[] {4, 2, 3, 1},
            new int[] {0, 3, 2, 4},
            new int[] {2, 1, 1},
        };

        sudokuArray[3] = new int[][] {
            new int[] {4, 1, 0, 3},
            new int[] {2, 3, 0, 4},
            new int[] {3, 2, 4, 1},
            new int[] {0, 4, 3, 2},
            new int[] {2, 1, 1},
        };

        sudokuArray[4] = new int[][] {
            new int[] {0, 3, 4, 0},
            new int[] {1, 0, 3, 2},
            new int[] {3, 2, 1, 4},
            new int[] {4, 1, 2, 3},
            new int[] {2, 1, 4},
        };

        sudokuArray[5] = new int[][] {
            new int[] {4, 3, 2, 0},
            new int[] {1, 2, 3, 4},
            new int[] {3, 4, 0, 2},
            new int[] {2, 0, 4, 3},
            new int[] {1, 1, 1},
        };

        sudokuArray[6] = new int[][] {
            new int[] {2, 0, 3, 4},
            new int[] {3, 4, 1, 2},
            new int[] {4, 0, 2, 1},
            new int[] {0, 2, 4, 3},
            new int[] {1, 3, 1},
        };

        sudokuArray[7] = new int[][] {
            new int[] {1, 3, 4, 2},
            new int[] {0, 2, 3, 1},
            new int[] {0, 4, 1, 0},
            new int[] {3, 1, 2, 4},
            new int[] {4, 2, 3},
        };

        sudokuArray[8] = new int[][] {
            new int[] {3, 1, 2, 0},
            new int[] {2, 4, 1, 0},
            new int[] {1, 3, 0, 2},
            new int[] {4, 2, 3, 1},
            new int[] {4, 3, 4},
        };

        sudokuArray[9] = new int[][] {
            new int[] {0, 1, 0, 4},
            new int[] {3, 4, 1, 0},
            new int[] {4, 3, 2, 1},
            new int[] {1, 2, 4, 3},
            new int[] {2, 3, 2},
        };

        sudokuArray[10] = new int[][] {
            new int[] {0, 1, 3, 0},
            new int[] {2, 0, 4, 1},
            new int[] {3, 2, 1, 4},
            new int[] {1, 4, 2, 3},
            new int[] {4, 2, 3},
        };

        sudokuArray[11] = new int[][] {
            new int[] { 0, 4, 1, 0 },
            new int[] { 2, 1, 3, 0 },
            new int[] { 4, 3, 2, 1 },
            new int[] { 1, 2, 4, 3 },
            new int[] { 3, 2, 4 },
        };

        numAnswer = new string[] { "0호", "6호", "14호", "100호", "404호", "501호", "11호", "24호"};
        picAnswer = new string[] { "2월호", "가정맹호", "간호", "구미호", "기호", "김정호", "모스부호", "무궁화호", "무야호", "박찬호", "삼인성호","전화번호", "좋을호" };

        sudoku = gameObject.transform.Find("Sudoku");
        sudoku.gameObject.SetActive(false);
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        _renderer = GetComponent<MeshRenderer>();
        hasQuiz = true;
    }

    private void OnEnable()
    {
        int randomType;

        bool isCreatedQuiz = false;
        int loopNum = 0;
        Debug.Log("Create letter");
        while (!isCreatedQuiz)
        {
            randomType = Random.Range(0, 3);
            Debug.Log("randomType: " + randomType);
            switch (randomType)
            {
                case 0:
                    curType = quizType.SUDOKU;
                    if (sudokuIndexList.Count < sudokuArray.Length)
                    {
                        CreateSudoku();
                        isCreatedQuiz=true;
                    }
                    break;

                case 1:
                    curType = quizType.NUMBER;
                    sudoku.gameObject.SetActive(false);
                    if (numberIndexList.Count < numAnswer.Length)
                    {
                        CreateNumberQuiz();
                        isCreatedQuiz = true;
                    }
                    break;

                case 2:
                    curType = quizType.PICTURE;
                    sudoku.gameObject.SetActive(false);
                    if (pictureIndexList.Count < picAnswer.Length)
                    {
                        CreatePictureQuiz();
                        isCreatedQuiz = true;
                    }

                    break;
            }
            if (loopNum++ > 10000)
            {
                hasQuiz = false;
                break;
            }

        }
        
        Debug.Log("is created");
    }
    void Start()
    {
        isPicked = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateSudoku()
    {
        sudoku.gameObject.SetActive (true);
        _renderer.material = paper;
        Debug.Log("Create Sudoku");

        int index;
        do
        {
            index = Random.Range(0, 12);
        } while (sudokuIndexList.Contains(index));

        sudokuIndexList.Add(index);

        for(int i = 0; i<16; i++)
        {
            int row = i / 4;
            int col = i % 4;
            if (sudokuArray[index][row][col] != 0)
            {
                sudoku.gameObject.GetComponentsInChildren<TextMeshPro>()[i].text = sudokuArray[index][row][col].ToString();
            }
            else
            {
                sudoku.gameObject.GetComponentsInChildren<TextMeshPro>()[i].text = "";
            }
        }

        ans = string.Join("", sudokuArray[index][4]);
        ans += "호";
        ansIndex = index;
    }

    void CreateNumberQuiz()
    {
        int index;

        do
        {
            index = Random.Range(0, numAnswer.Length);
        }while(numberIndexList.Contains(index));

        numberIndexList.Add(index);

        _renderer.material = numberQuiz[index];

        ans = numAnswer[index];
        ansIndex = index;
    }

    void CreatePictureQuiz()
    {
        int index;

        do
        {
            index = Random.Range(0, picAnswer.Length);
        } while (pictureIndexList.Contains(index));

        pictureIndexList.Add(index);

        _renderer.material = pictureQuiz[index];

        ans = picAnswer[index];
        ansIndex = index;
    }

    IEnumerator FadeOut()
    {
        float f = 1;
        while (f > 0)
        {
            f -= 0.1f;
            Color colorAlpha = _renderer.material.color;
            colorAlpha.a = f;
            _renderer.material.color = colorAlpha;
            yield return new WaitForSeconds(0.02f);
        }

    }

    IEnumerator FadeIn()
    {
        float f = 0;
        while(f <= 1)
        {
            f += 0.1f;
            Color colorAlpha = _renderer.material.color;
            colorAlpha.a = f;
            _renderer.material.color = colorAlpha;
            yield return new WaitForSeconds(0.02f);
        }
    }

    private void OnMouseDrag()
    {
        if (Input.GetMouseButton(0)) {
            mousePosition = Input.mousePosition;
            mousePosition = cam.ScreenToWorldPoint(mousePosition);
            transform.position = mousePosition;
            isPicked = true;
        }

    }

    public List<string> GetAnswers()
    {
        string answer;
        List<string> ansList = new List<string>();
        List<int> ansIndexList = new List<int>();
        int randomIndex;

        if (ans != null)
        {
            Debug.Log("update ans");
            ansIndexList.Add(ansIndex);
            ansList.Add(ans);
        }
        switch (curType)
        {
            case quizType.SUDOKU:
                for (int i = 0; i < 3; i++)
                {
                    randomIndex = Random.Range(0, sudokuArray.Length);
                    if (ansIndexList.Contains(randomIndex))
                    {
                        i--;
                        continue;
                    }
                    else
                    {
                        ansIndexList.Add(randomIndex);
                        answer = string.Join("", sudokuArray[randomIndex][4]);
                        answer += "호";
                        ansList.Add(answer);
                    }
                }
                break;
            case quizType.NUMBER:
                for(int i =0; i< 3; i++)
                {
                    randomIndex = Random.Range(0, numAnswer.Length);
                    if (ansIndexList.Contains(randomIndex))
                    {
                        i--;
                        continue;
                    }
                    else
                    {
                        ansIndexList.Add(randomIndex);
                        ansList.Add(numAnswer[randomIndex]);
                    }
                }
                break;
            case quizType.PICTURE:
                for (int i = 0; i < 3; i++)
                {
                    randomIndex = Random.Range(0, picAnswer.Length);
                    if (ansIndexList.Contains(randomIndex))
                    {
                        i--;
                        continue;
                    }
                    else
                    {
                        ansIndexList.Add(randomIndex);
                        ansList.Add(picAnswer[randomIndex]);
                    }
                }
                break;
        }

        return ansList;
    }

}
