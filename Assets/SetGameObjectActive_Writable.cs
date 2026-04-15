using UnityEngine;

namespace Gamekit3D.GameCommands
{
    public class SetGameObjectActive_Writable : GameCommandHandler
    {
        public GameObject[] targets;
        public bool isEnabled = true;

        [Header("Wwise Events On Activate")]
        public AK.Wwise.Event OnActivate_Event;

        public override void PerformInteraction()
        {
            foreach (var g in targets)
                g.SetActive(isEnabled);

            if (OnActivate_Event != null)
                OnActivate_Event.Post(gameObject);
        }
    }
}