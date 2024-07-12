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

    public Button enterButton;

    public Button dressButton;

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
}
