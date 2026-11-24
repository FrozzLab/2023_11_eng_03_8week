using System;
using Unity.Cinemachine;


namespace Menu
{
    [Serializable]
    public struct CameraViewAssociation
    {
        public CinemachineCamera camera;
        public ViewType viewType;
    }
}