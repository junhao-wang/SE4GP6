using UnityEngine;
//Part of the Darkest Dungeon scripts
public abstract class BaseSlot : MonoBehaviour
{
    protected RectTransform RectTransform { get { return rectTran ?? (rectTran = GetComponent<RectTransform>()); } }

    private RectTransform rectTran;
}
