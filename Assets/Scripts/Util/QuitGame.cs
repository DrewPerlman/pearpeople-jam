using UnityEngine;

public class QuitGame : MonoBehaviour
{
    [SerializeField] private KeyCode quitKey = KeyCode.Escape;

    private bool inEditor = false;

    private void Start()
    {
#if UNITY_EDITOR
        inEditor = true;
#endif
    }

    void Update()
    {
        if (Input.GetKeyDown(quitKey)) {
            if (inEditor) {
                UnityEditor.EditorApplication.isPlaying = false;
            }
            else {
                Application.Quit();
            }
        }
    }
}
