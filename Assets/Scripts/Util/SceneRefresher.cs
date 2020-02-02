using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRefresher : MonoBehaviour
{
    [SerializeField] private KeyCode refreshKey = KeyCode.R;

    private void Update()
    {
        if (Input.GetKeyDown(refreshKey)) {
            int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(activeSceneIndex);
        }
    }
}
