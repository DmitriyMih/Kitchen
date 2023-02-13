using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class LayerFootstesSoundSO : ScriptableObject
{
    public LayerSound defaultLayerSound;

    [Space] 
    public List<LayerSound> layerSounds;
}

[Serializable]
public class LayerSound
{
    public LayerMask layerMask;
    public List<AudioClip> layerFootstepsSound;
}