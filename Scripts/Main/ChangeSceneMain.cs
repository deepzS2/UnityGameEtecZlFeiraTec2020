using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneMain : MonoBehaviour
{
    /// <summary>
    /// Apenas muda a cena
    /// </summary>
    /// <param name="sceneName">Nome da cena</param>
    public void _changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
