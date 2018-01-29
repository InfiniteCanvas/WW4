/*
 * Copyright (c) Simon Josiek
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.VR;

public class Menu : MonoBehaviour
{
    #region Singleton

    public static Menu instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    [HideInInspector] public AsyncOperation operation;
    public GameObject vrMenu;
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject pauseMenu;
    public GameObject tutorialOverlay;
    public GameObject inGameOverlay;
    public Image fadingImage;
    public Image background;

    private bool paused;
    private int currentScene;

    private void Start()
    {
        VRSettings.LoadDeviceByName("");
        VRSettings.enabled = false;
        vrMenu.SetActive(true);
        StartCoroutine(FadingOut(5));
        StartCoroutine(LoadScene(1));
    }

    private void Update()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene != 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !optionsMenu.activeSelf)
            {
                if (tutorialOverlay.activeSelf)
                {
                    tutorialOverlay.SetActive(false);
                }
                else
                {
                    paused = !paused;
                }
            }

            if (Input.GetKeyDown(KeyCode.I) && !paused)
            {
                tutorialOverlay.SetActive(true);
            }

            if (tutorialOverlay.activeSelf || paused)
            {
                inGameOverlay.SetActive(false);
            }
            else if (!paused)
            {
                inGameOverlay.SetActive(true);
            }

            if (paused)
            {
                background.gameObject.SetActive(true);
                if (!optionsMenu.activeSelf)
                {
                    pauseMenu.SetActive(true);
                }               
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                background.gameObject.SetActive(false);
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }  
    }

    public void DesktopMode()
    {        
        vrMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void VRMode()
    {
        VRSettings.LoadDeviceByName("HTC Vive");
        VRSettings.enabled = true;
        tutorialOverlay = null;
        inGameOverlay = null;
    }

    public void NewGame()
    {
        StartCoroutine(ActivateMainScene());
    }

    public void Continue()
    {
        paused = false;
    }

    public void Return()
    {
        optionsMenu.SetActive(false);
        if (currentScene == 0)
        {
            mainMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(true);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            operation.allowSceneActivation = false;
            yield return operation;
        }
    }

    IEnumerator ActivateMainScene()
    {
        StartCoroutine(FadingIn(2));
        yield return new WaitUntil(() => operation.isDone);
        background.sprite = null;
        background.color = new Color(0, 0, 0, 0.5f);
        mainMenu.SetActive(false);
        tutorialOverlay.SetActive(true);
        yield return new WaitForSeconds(2);
        StartCoroutine(FadingOut(2));
    }

    IEnumerator FadingIn(float fadeTime)
    {
        fadingImage.canvasRenderer.SetAlpha(0.0f);
        fadingImage.CrossFadeAlpha(1.0f, fadeTime, true);
        yield return new WaitForSecondsRealtime(fadeTime);
        operation.allowSceneActivation = true;
    }

    IEnumerator FadingOut(float fadeTime)
    {
        fadingImage.canvasRenderer.SetAlpha(1.0f);
        fadingImage.CrossFadeAlpha(0.0f, fadeTime, true);
        yield return new WaitForSecondsRealtime(fadeTime);
    }
}