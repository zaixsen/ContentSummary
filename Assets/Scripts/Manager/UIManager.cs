using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoSinleton<UIManager>
{
    public GameObject createRoleUI;

    public GameObject mainUI;

    public GameObject dressUI;

    public GameObject rotate3DPlayers;

    public GameObject dialogGo;

    public Button enterButton;

    public Button dressButton;

    public Text text;
    private void Start()
    {
        DontDestroyOnLoad(this);
        rotate3DPlayers.SetActive(false);
        enterButton.gameObject.SetActive(false);
        enterButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Game");
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        });
        dressButton.onClick.AddListener(() =>
        {
            dressUI.SetActive(true);
            mainUI.SetActive(false);
        });
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        SetMainUI(true);
        enterButton.gameObject.SetActive(false);
    }

    public void SetCreateRoleUI(bool flag)
    {
        createRoleUI.SetActive(flag);
        rotate3DPlayers.SetActive(true);
        enterButton.gameObject.SetActive(true);
    }
    public void SetMainUI(bool flag)
    {
        mainUI.SetActive(flag);
    }

    public void PrintDialog(string content)
    {
        text.text = content;
    }

    public void SetDialogActive(bool flag)
    {
        dialogGo.SetActive(flag);
        text.text = "";
        mainUI.SetActive(!flag);
    }
}
