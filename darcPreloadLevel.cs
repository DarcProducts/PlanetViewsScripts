using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class darcPreloadLevel : MonoBehaviour
{
    [SerializeField] TMP_Text loadingProgressText = null;
    AsyncOperation asyncOperation;
    void Start() => StartCoroutine("LoadSolSystem");
    public IEnumerator LoadSolSystem()
    {
        asyncOperation = SceneManager.LoadSceneAsync("darcSolSystem");

        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {

            loadingProgressText.text = $"loading... please wait";

            if (asyncOperation.progress >= 0.9f)
            {
                loadingProgressText.text = "Press any button to continue";
                loadingProgressText.faceColor = new Color(255, 255, 0, 255);

                if (OVRInput.Get(OVRInput.RawButton.Any))
                    asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    [ContextMenu("Go to loaded scene")]
    public void GoToLoadedScene() => asyncOperation.allowSceneActivation = true;
}
