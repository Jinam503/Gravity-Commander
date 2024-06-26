using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroduceText : MonoBehaviour,IInteractable
{
    [SerializeField] private TextMeshPro introduceText;
    [SerializeField] [TextArea] private string[] introduceTexts;
    [SerializeField] private GameObject mouseClickImage;
    private int curTextIndex;
    private bool canClick;
    

    private void Start()
    {
        introduceText.text = "";
        canClick = false;
        curTextIndex = 0;
        StartCoroutine(WriteNextIntroduce());
    }

    public void Interact()
    {
        if (!canClick || curTextIndex >= introduceTexts.Length) return;
        if(mouseClickImage.activeSelf)mouseClickImage.SetActive(false);
        StartCoroutine(WriteNextIntroduce());
    }

    private IEnumerator WriteNextIntroduce()
    {
        canClick = false;
        for(int i = 0; i <= introduceTexts[curTextIndex].Length; i++)
        {   
            introduceText.text = introduceTexts[curTextIndex][..i];
            yield return new WaitForSeconds(0.1f);
        }

        curTextIndex++;
        canClick = true;
    }
}
