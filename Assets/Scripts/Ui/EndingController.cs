using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingController : MonoBehaviour
{
    [SerializeField] private Animation anim;
    [SerializeField] private Animation creditAnim;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NextScene());

    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(anim.clip.length);
        creditAnim.Play();
    }

    public void RestartButtonClicked()
    {
        SceneManager.LoadScene("Cutscene");
    }

    public void QuitClicked()
    {
        Application.Quit();
    }

    private IEnumerator CursorAppear()
    {
        yield return new WaitForSeconds(creditAnim.clip.length - 3f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
