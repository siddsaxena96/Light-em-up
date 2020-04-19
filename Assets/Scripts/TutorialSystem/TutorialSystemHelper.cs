using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSystemHelper : MonoBehaviour
{
    [SerializeField] private TutorialSystem tutorialSystem = null;
    private bool once = false;

    public void LoadStep()
    {
        if (!once)
        {
            tutorialSystem.LoadNextStep();
            once = true;
        }
    }

    public void VampireTutorial()
    {
        if (!once)
        {
            tutorialSystem.VampireTutorial();
        }
    }
}
