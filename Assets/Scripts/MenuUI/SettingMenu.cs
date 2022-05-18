﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class SettingMenu : MonoBehaviour
{
    [SerializeField] private AudioSource audios;
    [SerializeField] private Slider soundSlider;
    private float volume;
    DatabaseReference reference;
    // Start is called before the first frame update
    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;  //定義資料庫連接
        if (audios != null)
        {
            volume = audios.volume;
            soundSlider.value = volume;
            audios.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //ese叫設定出來
        {
            if (transform.Find("Settings").gameObject.activeSelf)
            {
                transform.Find("Settings").gameObject.SetActive(false);
            }
            else
            {
                SetBtnColor();
                transform.Find("Settings").gameObject.SetActive(true);
            }
        }
        if (audios != null)
        {
            audios.volume = volume;
        }
        // if (transform.Find("Settings").gameObject.activeSelf &&
        // GameObject.Find("GameMenu").GetComponent<CanvasGroup>().blocksRaycasts &&
        // !transform.Find("Settings").Find("LoginOut").gameObject.activeSelf)
        // {
        //      transform.Find("Settings").Find("LoginOut").gameObject.SetActive(true);
        // }
    }

    void SetBtnColor()
    {
        if (!GameObject.Find("ChoosePlayer").GetComponent<CanvasGroup>().blocksRaycasts)
        {
            transform.Find("Settings").Find("LoginOut").GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            transform.Find("Settings").Find("LoginOut").GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 1);
        }
    }

    public void updateVolume(float musicVolume) //音量調節
    {
        volume = musicVolume;
    }

    public void ExitGame() //離開遊戲
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
    }
    public void Back() //關閉設定
    {
        transform.Find("Settings").gameObject.SetActive(false);
    }

    public void LoginOut()
    {
        if (!GameObject.Find("ChoosePlayer").GetComponent<CanvasGroup>().blocksRaycasts)
        {
            PlayerPrefs.DeleteKey("password");
            transform.Find("Settings").gameObject.SetActive(false);
            StartCoroutine(fadeout());
            // reference.Child("Account_Online").Child(PlayerPrefs.GetString("username")).SetValueAsync(null);
        }
    }
    private IEnumerator fadeout() //淡出畫面       
    {
        GameObject.Find("TranPageAnimation").GetComponent<Animator>().SetTrigger("change");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
        // GameObject.Find("LoginMenu").GetComponent<Login>().inLoginMenu = true;
    }
}
