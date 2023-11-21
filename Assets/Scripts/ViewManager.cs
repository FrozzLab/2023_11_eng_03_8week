using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class ViewManager : MonoBehaviour
{

    private static ViewManager _instance;
    
    public CinemachineVirtualCamera currentCamera;
    public AspectViewAssociation[] aspectViewAssociations;
    
    private Camera _mainCamera;

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

        _mainCamera = Camera.main;
        
        SetUpViewByAspectRatio();
    }

    private void SetUpViewByAspectRatio()
    {
        float cameraAspect = _mainCamera.aspect;
        
        if (cameraAspect >= 2.3)
        {
            ActivateViewByTargetRatio(AspectRatio.TwentyOneByNine);
        }
        else if (cameraAspect >= 1.7)
        {
            ActivateViewByTargetRatio(AspectRatio.SixteenByNine);
        }
        else
        {
            ActivateViewByTargetRatio(AspectRatio.FourByThree);
        }
    }

    private void ActivateViewByTargetRatio(AspectRatio targetRatio)
    {
        AspectViewAssociation targetAssociation = 
            aspectViewAssociations.FirstOrDefault(a => a.ratio == targetRatio);

        if (targetAssociation != null)
        {
            foreach (var association in aspectViewAssociations)
            {
                association.assignedView.gameObject.SetActive(false);
            }
            
            targetAssociation.assignedView.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"No view found for aspect ratio {nameof(targetRatio)}. Defaulting to FourByThree");
            
            aspectViewAssociations
                .FirstOrDefault(a => a.ratio == AspectRatio.FourByThree)?
                .assignedView.gameObject.SetActive(true);
        }
    }

    public static void ChangeCamera(CinemachineVirtualCamera newCamera)
    {
        _instance.currentCamera.gameObject.SetActive(false);
        newCamera.gameObject.SetActive(true);
        _instance.currentCamera = newCamera;
    }
    
}
