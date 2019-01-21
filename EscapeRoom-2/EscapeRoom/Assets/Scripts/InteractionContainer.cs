using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionContainer : MonoBehaviour
{
    public InteractionTarget Target;

    public interface InteractionTarget
    {
        void Interact();
    }
}
