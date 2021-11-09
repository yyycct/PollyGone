using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartPageController : MonoBehaviour
{
    [SerializeField] private Animation anim;
    [SerializeField] private Animation cutsceneAnim;
    public void StartButtonClicked()
    {
        anim.Play();
        StartCoroutine(NextScene());
        
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(anim.clip.length);
        cutsceneAnim.Play();
    }
}
