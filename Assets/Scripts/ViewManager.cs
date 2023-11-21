using Cinemachine;
using UnityEngine;

public class ViewManager : MonoBehaviour
{

    private static ViewManager _instance;

    public CinemachineVirtualCamera currentCamera;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void ChangeCamera(CinemachineVirtualCamera newCamera)
    {
        _instance.currentCamera.gameObject.SetActive(false);
        newCamera.gameObject.SetActive(true);
        _instance.currentCamera = newCamera;
    }
    
}
