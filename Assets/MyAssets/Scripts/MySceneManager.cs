using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    /// <summary>�V�[���J��</summary>
    /// <param name="sceneName">�Y���V�[����</param>
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
