using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Core
{
	public class EventManager
    {
        public static event UnityAction<float> OnChangePlayerHealth;
        public static event UnityAction<float> OnChangePlayerPotions;
        public static event UnityAction<TextAsset> OnInitiateDialogue;
        public static void RaiseChangePlayerHealth(float newHealthPoints) =>
            OnChangePlayerHealth?.Invoke(newHealthPoints);
        public static void RaiseChangePlayerPotions(float newHealthPotions) =>
            OnChangePlayerPotions?.Invoke(newHealthPotions);
            public static void RaiseInitiateDialogue(TextAsset inkJson) =>
            OnInitiateDialogue?.Invoke(inkJson);
    }

}

