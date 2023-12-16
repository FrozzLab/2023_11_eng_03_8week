using System;

namespace Menu
{
    [Serializable]
    public struct CameraRegistry
    {
        public AspectViewAssociation aspectViewAssociation;
        public CameraViewAssociation[] cameraViewAssociations;
    }
}