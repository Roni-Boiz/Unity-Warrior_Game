using UnityEngine;
using UnityEngine.SceneManagement;

public class Lo : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(1);
        }
    }
}
