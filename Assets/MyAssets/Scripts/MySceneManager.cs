using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    /// <summary>シーン遷移</summary>
    /// <param name="sceneName">該当シーン名</param>
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
