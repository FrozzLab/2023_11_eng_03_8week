using System;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Screen = UnityEngine.Device.Screen;

namespace Menu
{
    public class ViewManager : MonoBehaviour
    {

        private static ViewManager _instance;
        
        private CameraRegistryContainer _cameraRegistryContainer;
        private AspectViewAssociation _currentAspectViewAssociation;
        private float _currentAspectRatioFloat;
        private ViewType _currentView;

        private readonly ResolutionContainer[] _availableResolutions =
        {
            new(1024, 768),
            new(1280, 720),
            new(1280, 800),
            new(1366, 768),
            new(1440, 900),
            new(1600, 900),
            new(1680, 1050),
            new(1920, 1080),
            new(1920, 1200),
            new(2560, 1080),
            new(2560, 1440),
            new(3440, 1440),
            new(3840, 2160)
        };
        
        private readonly ScreenModeContainer[] _availableModes =
        {
            new(FullScreenMode.Windowed),
            new(FullScreenMode.FullScreenWindow),
            new(FullScreenMode.ExclusiveFullScreen)
        };
        
        public UnityEvent<int> resolutionChanged;
        public UnityEvent<int> screenModeChanged;
        
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
        
        private void Start()
        {
            ChangeScreenMode(2); //FullscreenWindow
            
            // Finding the index of the closest resolution from the list
            // I know it looks intimidating but it's needed to avoid incorrect labelling in the settings view later on
            int closestResolutionIndex = _availableResolutions
                .Select((resolution, index) 
                    => (index, Distance: 
                        Math.Sqrt(Math.Pow(resolution.width - Screen.currentResolution.width, 2) 
                                  + Math.Pow(resolution.height - Screen.currentResolution.height, 2))))
                .OrderBy(tuple => tuple.Distance)
                .First()
                .index;
            
            ChangeResolution(closestResolutionIndex);
            
            _cameraRegistryContainer = CameraRegistryContainer.Instance;
            _currentAspectRatioFloat = Screen.currentResolution.width / (float)Screen.currentResolution.height;
            
            foreach (var registry in _cameraRegistryContainer.cameraRegistries)
            {
                foreach (var association in registry.cameraViewAssociations)
                {
                    association.camera.gameObject.SetActive(false);
                }
            }
            
            SetUpViewByAspectRatio(_currentAspectRatioFloat);

            foreach (var cameraRegistry in _cameraRegistryContainer.cameraRegistries)
            {
                cameraRegistry
                    .cameraViewAssociations
                    .FirstOrDefault(cva => cva.viewType == _cameraRegistryContainer.defaultView)
                    .camera
                    .gameObject.SetActive(true);
            }

            _currentView = _cameraRegistryContainer.defaultView;
        }

        private void OnEnable()
        {
            SceneManager.activeSceneChanged += ChangedActiveScene;
        }

        private void ChangedActiveScene(Scene current, Scene next)
        {
            _cameraRegistryContainer = CameraRegistryContainer.Instance;

            if (_cameraRegistryContainer == null)
            {
                return;
            }
            
            foreach (var registry in _cameraRegistryContainer.cameraRegistries)
            {
                foreach (var association in registry.cameraViewAssociations)
                {
                    association.camera.gameObject.SetActive(false);
                }
            }
            
            SetUpViewByAspectRatio(_currentAspectRatioFloat);
            
            _cameraRegistryContainer
                .cameraRegistries
                .FirstOrDefault(cr => cr.aspectViewAssociation == _currentAspectViewAssociation)
                .cameraViewAssociations
                .FirstOrDefault(cva => cva.viewType == _cameraRegistryContainer.defaultView)
                .camera
                .gameObject.SetActive(true);
        }
        
        private void SetUpViewByAspectRatio(float aspect)
        {
            if (aspect >= 2.3)
            {
                ActivateViewByTargetRatio(AspectRatio.TwentyOneByNine);
            }
            else if (aspect >= 1.7)
            {
                ActivateViewByTargetRatio(AspectRatio.SixteenByNine);
            }
            else if (aspect >= 1.6)
            {
                ActivateViewByTargetRatio(AspectRatio.SixteenByTen);
            }
            else
            {
                ActivateViewByTargetRatio(AspectRatio.FourByThree);
            }
        }

        private void ActivateViewByTargetRatio(AspectRatio targetRatio)
        {
            _currentAspectViewAssociation = 
                _cameraRegistryContainer
                    .cameraRegistries
                    .FirstOrDefault(cr => cr.aspectViewAssociation.ratio == targetRatio)
                    .aspectViewAssociation;

            if (_currentAspectViewAssociation != null)
            {
                foreach (var cameraRegistry in _cameraRegistryContainer.cameraRegistries)
                {
                    cameraRegistry.aspectViewAssociation.assignedView.gameObject.SetActive(false);
                }
            
                _currentAspectViewAssociation.assignedView.gameObject.SetActive(true);
            }
            else
            {
                _cameraRegistryContainer
                    .cameraRegistries
                    .FirstOrDefault(cr => cr.aspectViewAssociation.ratio == AspectRatio.FourByThree)
                    .aspectViewAssociation
                    .assignedView
                    .gameObject.SetActive(true);
            }
        }

        public static void ChangeCamera(ViewTypeContainer newViewContainer)
        {
            foreach (var cameraRegistry in _instance._cameraRegistryContainer.cameraRegistries)
            {
                cameraRegistry
                    .cameraViewAssociations
                    .FirstOrDefault(cva => cva.viewType == _instance._currentView)
                    .camera
                    .gameObject.SetActive(false);
                
                cameraRegistry
                    .cameraViewAssociations
                    .FirstOrDefault(cva => cva.viewType == newViewContainer.viewType)
                    .camera
                    .gameObject.SetActive(true);
            }

            _instance._currentView = newViewContainer.viewType;
        }
        
        public void ChangeResolution(int index)
        {
            Screen.SetResolution
            (
                _availableResolutions[index].width, 
                _availableResolutions[index].height, 
                Screen.fullScreenMode
            );
            
            resolutionChanged.Invoke(index);
            
            SetUpViewByAspectRatio(_availableResolutions[index].width / (float)_availableResolutions[index].height);
        }
        
        public void ChangeScreenMode(int index)
        {
            Screen.SetResolution
            (
                Screen.width, 
                Screen.height, 
                _availableModes[index].mode
            );
            
            screenModeChanged.Invoke(index);
            
            SetUpViewByAspectRatio(Screen.width / (float)Screen.height);
        }
    }
}