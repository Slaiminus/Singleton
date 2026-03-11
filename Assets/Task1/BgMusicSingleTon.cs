using UnityEngine;

public class BgMusicSingleTon : MonoBehaviour
{
    private static BgMusicSingleTon _instance;
    public static BgMusicSingleTon Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
