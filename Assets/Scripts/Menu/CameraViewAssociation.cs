using System;
using Cinemachine;

namespace Menu
{
    [Serializable]
    public struct CameraViewAssociation
    {
        public CinemachineVirtualCamera camera;
        public ViewType viewType;
    }
}