﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StartMenu : MonoBehaviour
{
    public bool inMainMenu;
    private Animator MainAnimator;
    private CanvasGroup CanvasGroup;
    float waitTime = 0;
    void Start()
    {
        inMainMenu = false;
        MainAnimator = this.GetComponent<Animator>();
        CanvasGroup = this.GetComponent<CanvasGroup>();
        StartCoroutine(fadein());
    }
    void Update()
    {
        if (inMainMenu) //判斷是否切換畫面
        {
            StartCoroutine(fadein());
        }
        if (Input.GetKeyDown(KeyCode.Space) && CanvasGroup.blocksRaycasts)
        {
            StartCoroutine(Connected());
        }
    }
    private IEnumerator fadein() //淡入畫面
    {

        MainAnimator.SetTrigger("fade");
        inMainMenu = false;
        yield return new WaitForSeconds(0.5f);
        CanvasGroup.blocksRaycasts = true;
    }
    private IEnumerator fadeout() //淡出畫面       
    {
        CanvasGroup.blocksRaycasts = false;
        MainAnimator.SetTrigger("fade");
        GameObject.Find("TranPageAnimation").GetComponent<Animator>().SetTrigger("change");
        yield return new WaitForSeconds(0.5f);
        if (PlayerPrefs.HasKey("username") && PlayerPrefs.HasKey("password"))
        {
            GameObject.Find("GameMenu").GetComponent<GameMenu>().inGameMenu = true;
        }
        else
        {
            GameObject.Find("LoginMenu").GetComponent<Login>().inLoginMenu = true;
        }

    }

    IEnumerator Connected()
    {
        GameObject LM = GameObject.Find("LoadingMenu");
        if (PhotonNetwork.IsConnected)
        {
            LM.GetComponent<CanvasGroup>().alpha = 0;
            LM.GetComponent<CanvasGroup>().blocksRaycasts = false;
            StartCoroutine(fadeout());
        }
        else
        {
            if (LM.GetComponent<CanvasGroup>().alpha == 0)
            {
                LM.GetComponent<CanvasGroup>().alpha = 1;
            }
            yield return new WaitForSeconds(0.1f);
            waitTime += 0.1f;
            if(waitTime > 10f && !LM.transform.Find("Reload").gameObject.activeSelf)
            {
                LM.transform.Find("Reload").gameObject.SetActive(true);
                LM.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            //PhotonNetwork.ConnectUsingSettings();  //開啟連線
            StartCoroutine(Connected());
        }
    }
    public void Reload()
    {
        SceneManager.LoadScene(0);
    }
    public void PlayGame() //如果點擊畫面
    {
        if (CanvasGroup.blocksRaycasts)
        {
            StartCoroutine(Connected());
        }
    }
    public void QuitGame() //如果按離開按鈕
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
