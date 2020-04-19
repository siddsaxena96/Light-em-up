using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialSystem : MonoBehaviour
{
    [SerializeField] private GameObject fadePanel = null;
    [SerializeField] private GameObject tutorialPanel = null;
    [SerializeField] private TutorialData tutorialData = null;
    [SerializeField] private TMP_Text tutorialText = null;

    [Space]
    [SerializeField] private PlayerController playerController = null;
    [SerializeField] private MirrorBehaviour firstMirror = null;

    private bool clickToNext = false;
    private int tutorialStep = 0;


    private void Awake()
    {
        playerController.enabled = false;
        firstMirror.StopRotation();
        StartTutorialSequence();
    }

    private void StartTutorialSequence()
    {
        tutorialStep = 0;
        if (tutorialData != null)
        {
            LoadTutorialStep();
        }
    }

    private void LoadTutorialStep()
    {
        if (tutorialStep == 2)
            firstMirror.allowRotate = true;
        tutorialText.text = string.Format(tutorialData.tutorialSteps[tutorialStep].instuction);
        fadePanel.SetActive(tutorialData.tutorialSteps[tutorialStep].isClickable);
        Time.timeScale = tutorialData.tutorialSteps[tutorialStep].isClickable ? 0f : 1f;
    }

    public void LoadNextStep()
    {
        tutorialStep++;
        LoadTutorialStep();
    }

    private void Update()
    {
        if (tutorialData.tutorialSteps[tutorialStep].isClickable)
        {
            if (Input.GetMouseButton(0))
            {
                LoadNextStep();
            }
        }

        if (clickToNext)
        {
            if (Input.GetMouseButton(0))
            {
                Time.timeScale = 1.0f;
                clickToNext = false;
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Level1");
            }
        }
    }

    public void VampireTutorial()
    {
        playerController.StopPlayer();
        playerController.enabled = false;
        StartCoroutine(VampireTutorialCoroutine());
    }

    private IEnumerator VampireTutorialCoroutine()
    {
        yield return new WaitForSeconds(1);
        playerController.enabled = true;
        LoadNextStep();
    }

    public void TutorialComplete()
    {
        playerController.enabled = false;
        Time.timeScale = 0f;
        tutorialText.text = "You've completed the tutorial.<br> Q and E to rotate the mirrors and Click to use torch.<br> click on the screen to start the main level";
        clickToNext = true;
    }
}
