using FishingLine;
using UnityEngine;
namespace FishingRod
{
    public class FishingRod : MonoBehaviour
    {
        public static FishingState state;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                switch (state)
                {
                    case FishingState.Idle:
                        FishLine.instance.CastLine();
                        break;
                    case FishingState.Fishing:
                        FishLine.instance.ResetPos();
                        break;
                }
            }
        }
    }
}
