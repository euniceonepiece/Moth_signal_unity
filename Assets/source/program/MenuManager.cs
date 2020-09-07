using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    [Header("Loading 文字")]
    public Text TextLoading;
    [Header("Loading 數字")]
    public Text TextLoadingNumb;
    [Header("Loading 吧條")]
    public Image LoadingBar;

    public void StartLoading()
    {
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
       AsyncOperation AO = SceneManager.LoadSceneAsync("gametest");
       AO.allowSceneActivation = false;
        while (AO.progress <=1)
        {
            TextLoadingNumb.text = AO.progress / 0.9f*100+"%";
            LoadingBar.fillAmount = AO.progress / 0.9f;

            if(AO.progress >=0.9f)
            {
                TextLoading.text ="按任意鍵開始遊戲..!!";
                if(Input.anyKey) AO.allowSceneActivation = true ;
            }

            yield return null;
        }
    }


    public void QuitGame()
    {
        Debug.Log("離開遊戲");
        Application.Quit();
    }
    
}