using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalSceneManager : MonoBehaviour
{
    public enum SceneState : int 
    {
        SPLASHSCREEN = 0,
        MAINMENU,
        LOADINGSCREEN,
        SETTINGS,
        INSTRUCTIONS,
        CREDITS
    }

    private SceneState CurrentSceneState;
    public SceneState GetCurrentSceneState() => CurrentSceneState;

    public static event Action<SceneState> OnSceneChanged;
    private static readonly Lazy<GlobalSceneManager> _lazyLoadedSceneManager = new Lazy<GlobalSceneManager>(() => new GlobalSceneManager());
    protected GlobalSceneManager(){}
    protected void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static GlobalSceneManager Instance
    {
        get
        {
            return _lazyLoadedSceneManager.Value;
        }
    }

    // Update for later key mapping for demoing game levels / parts if something breaks or doesn't work.
    private void Update(){}

    public void UpdateSceneState(SceneState newSceneState)
    {
        CurrentSceneState = newSceneState;

        try 
        {
            string sceneName = Enum.GetName(typeof(SceneState), newSceneState);
            if(!SceneManager.GetSceneByName(sceneName).IsValid()) 
            {
                throw new SceneNotLoadedException(SceneNotLoadedException.DefaultMessage);
            }

            SceneManager.LoadScene(sceneName);
            OnSceneChanged?.Invoke(newSceneState);
        } 
        catch (SceneNotLoadedException ex)
        {
            #if UNITY_EDITOR
                Debug.LogException(ex, this);
            #endif
            throw; // continue stack propigation for trace
        }
    }
}

// JM - I created an exception class because SceneManager.LoadScene() does not THROW and is VOID!
// Should pop this into a different file at some point, but is only used here atm.
[System.Serializable]
public class SceneNotLoadedException : System.Exception 
{
    public static readonly string DefaultMessage = "Scene not found, please double check that the scene exists and is included in the build index settings.";
    public SceneNotLoadedException() { }
    public SceneNotLoadedException(string message) : base(message) { }
    public SceneNotLoadedException(string message, System.Exception inner) : base(message, inner) { }
    protected SceneNotLoadedException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
