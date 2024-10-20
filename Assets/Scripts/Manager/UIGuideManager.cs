using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//2024-05-22: CUSTOM UNITY TEMPLATE 

public class UIGuideManager : MonoBehaviour
{
    [SerializeField] private UIManager uimanager = null;
    [SerializeField] private Sprite[] guidepage = null;
    [SerializeField] private Sprite[] keyandvrpage = null; 
    [SerializeField, TextArea] private string[] guidetext = null;
    [SerializeField] private TextMeshProUGUI guidetext_text = null;
    [SerializeField] private Image guideimg = null;

    private Vector2 originalimg = Vector2.zero;
    private Vector2 originaltext = Vector2.zero;
    private Vector2 imgsize = Vector2.zero; 

    public enum GuideStatus { Game, Key, VR}

    private GuideStatus status = GuideStatus.Game;
    private int currentpage = 1; //첫번째 페이지

    private void Awake()
    {
        originalimg = guideimg.transform.position;
        originaltext = guidetext_text.transform.position;
        imgsize = guideimg.GetComponent<RectTransform>().sizeDelta; 
    }

    public void CloseGuideUI()
    {
        gameObject.SetActive(false);
        uimanager.gameObject.SetActive(true);
        Time.timeScale = 1f; 
    }

    public void GoGameGuide()
    {
        status = GuideStatus.Game;
        guideimg.sprite = guidepage[currentpage - 1];
        guidetext_text.text = guidetext[currentpage - 1]; 
    }

    public void GoKeyGuide()
    {
        status = GuideStatus.Key;
        guideimg.sprite = keyandvrpage[0];
        guidetext_text.text = ""; 
    }

    public void GoVRGuide()
    {
        status = GuideStatus.VR;
        guideimg.sprite = keyandvrpage[1];
        guidetext_text.text = ""; 
    }

    public void GuidePageNextBtn() 
    {
        if(status == GuideStatus.Game)
        {
            if(currentpage < 6)
            {
                ++currentpage;
                guideimg.sprite = guidepage[currentpage - 1];
                guidetext_text.text = guidetext[currentpage - 1];
                /*
                if(currentpage == 5 || currentpage == 6)
                {
                    guideimg.GetComponent<RectTransform>().position = new Vector2(0f, 15f);
                    guideimg.GetComponent<RectTransform>().sizeDelta = new Vector2(570f, 300f);
                    guidetext_text.GetComponent<RectTransform>().position = new Vector2(0f, -150f); 
                }
                */ 
            }
        }
    }

    public void GuidePagePreviousBtn()
    {
        if (status == GuideStatus.Game)
        {
            if (currentpage > 1)
            {
                --currentpage;
                guideimg.sprite = guidepage[currentpage - 1];
                guidetext_text.text = guidetext[currentpage - 1];
                /*
                if(currentpage <= 4)
                {
                    guideimg.GetComponent<RectTransform>().position = originalimg;
                    guidetext_text.GetComponent<RectTransform>().position = originaltext;
                    guideimg.GetComponent<RectTransform>().sizeDelta = imgsize; 
                }
                */ 
            }
        }
    }
}
