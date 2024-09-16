using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AnswerDrawer : MonoBehaviour
{
    public bool isActivated;

    Transform ansText;
    public bool isAnswer;
    // Start is called before the first frame update
    private void Awake()
    {
        ansText = gameObject.transform.Find("AnswerText");
    }
    void Start()
    {
        isActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position - new Vector3(3f, 0, 0), 2.0f);
        isActivated = true;
    }

    private void OnMouseExit()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(3f, 0, 0), 2.0f);
        isActivated = false;
    }

    public void SetAnswer(string answer)
    {
        ansText.GetComponent<TextMeshPro>().text = answer;
    }

    public bool Correct()
    {
        if(isAnswer)
        {
            isAnswer = false;
            return true;
        }
        return false;
    }
}
