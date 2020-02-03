using UnityEngine;

public class QuitGame : MonoBehaviour
{
    [SerializeField] private KeyCode quitKey = KeyCode.Escape;



    void Update()
    {
        if (Input.GetKeyDown(quitKey)) {
                Application.Quit();
        }
    }
}
