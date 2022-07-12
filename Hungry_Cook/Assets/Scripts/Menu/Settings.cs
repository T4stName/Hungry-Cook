using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Settings : MonoBehaviour
{
    public void LoadLevel(int number)
    {
        SceneManager.LoadScene(number);
    }
   public void OpenMenu()
   {
    LoadScene(0);
   }
   private void LoadScene(int number)
   {
    SceneManager.LoadScene(number);
   }
}
