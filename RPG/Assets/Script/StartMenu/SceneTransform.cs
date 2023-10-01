using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransform : MonoBehaviour
{
    public Text LoadingPerson;
    public Image LoadingProgresBar;

    private static SceneTransform _instance;
    private static bool _shouldPlayOpeningAnimation = false;

    private Animator componentAnimator;
    private AsyncOperation _loadSceneOperation;

    public static void SwitchToScene(string sceneName)
    {
        _instance.componentAnimator.SetTrigger(name: "Scene Closing");
        _instance._loadSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        _instance._loadSceneOperation.allowSceneActivation = false;
    }

    void Start()
    {
        _instance = this;
        componentAnimator = GetComponent<Animator>();

        if (_shouldPlayOpeningAnimation) componentAnimator.SetTrigger(name: "Scene Opening");
    }

    private void Update()
    {
        if (_loadSceneOperation != null)
        {
            LoadingPerson.text = Mathf.RoundToInt(f: _loadSceneOperation.progress * 100) + "%";
            LoadingProgresBar.fillAmount = _loadSceneOperation.progress;
        }
    }

    public void OnAnimationOver()
    {
        _shouldPlayOpeningAnimation = true;
        _loadSceneOperation.allowSceneActivation = true;
    }
}

