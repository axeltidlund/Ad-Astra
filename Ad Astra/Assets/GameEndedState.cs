using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndedState : State
{
    public Animator uiAnimator;

    public override void Enter()
    {
        uiAnimator.SetBool("Ended", true);
        Time.timeScale = 0.1f;
    }

    public override void Do()
    {
        
    }
    public override void Exit()
    {
        uiAnimator.SetBool("Ended", false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        if (isComplete) return;
        isComplete = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        if (isComplete) return;
        isComplete = true;

        Application.Quit();
    }
}
