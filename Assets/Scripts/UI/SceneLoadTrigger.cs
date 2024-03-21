using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField] private SceneField[] _scenesToLoad;
    [SerializeField] private SceneField[] _scenesToUnload;

    public void Trigger()
    {
        // Trigger

        // LoadScenes();
        // UnloadScene();
    }

    private void LoadScenes()
    {
        for (int i = 0; i < _scenesToLoad.Length; i++)
        {
            bool isSceneLoaded = false;

            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(j);
                
                if(loadedScene.name == _scenesToLoad[i].SceneName)
                {
                    isSceneLoaded = true;
                    break;
                }
            }

            if(!isSceneLoaded)
            {
                SceneManager.LoadSceneAsync(_scenesToLoad[i], LoadSceneMode.Additive);
            }
        }
    }

    private void UnloadScene()
    {
        for (int i = 0; i < _scenesToUnload.Length; i++)
        {
            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(j);
                
                if(loadedScene.name == _scenesToUnload[i].SceneName)
                {
                    SceneManager.UnloadSceneAsync(_scenesToUnload[i]);
                }
            }
        }
    }
}
