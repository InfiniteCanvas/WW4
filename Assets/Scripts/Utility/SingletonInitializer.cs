using UnityEngine;

namespace WW4.Utility
{
    public class SingletonInitializer : MonoBehaviour
    {
        private void Awake()
        {
            InitializeAllControlledComponents();
        }

        private void InitializeAllControlledComponents()
        {
            var controlledComponents = GetComponents<InitializerControlled>();
            foreach (var c in controlledComponents)
            {
                if (c.Initialize())
                    Debug.Log($"{c.GetClass()} successfully initialized.");
                else
                    Debug.LogWarning($"{c.GetClass()} was not initialized.");
            }
        }
    }
}