using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BACK : MonoBehaviour
{


    public void back()
    {
        SceneManager.LoadSceneAsync("test start");
    }



}