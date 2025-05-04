using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        animator.SetBool("Info", false);
    }
    public void StartGame()
    {
        animator.SetBool("Info", false);
        SceneManager.LoadScene("Main");
    }

    public void Exit()
    {
        animator.SetBool("Info", false);
        Application.Quit();
    }

    public void Info()
    {
        animator.SetBool("Info", true);
    }
}
