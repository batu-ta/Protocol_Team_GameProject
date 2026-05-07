using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuKontrol : MonoBehaviour
{
    public void OyunaBasla()
    {
        SceneManager.LoadScene("Outside"); 
    }

    public void OyundanCik()
    {
        Application.Quit();
    }
}