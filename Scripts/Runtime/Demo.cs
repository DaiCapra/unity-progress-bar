using UnityEditor;
using UnityEngine;
using Progress = ProgressBar.Scripts.Runtime.Progress;

namespace ProgressBar.Example
{
    public class Demo : MonoBehaviour
    {
        [SerializeField] private Progress progress;

        public void Change(float value)
        {
            if (progress == null)
            {
                return;
            }

            progress.SetFill(progress.Value + value);
        }

        public void Reset()
        {
            progress?.SetFill(1);
        }
    }
#if (UNITY_EDITOR)
    [CustomEditor(typeof(Demo))]
    public class DemoEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var instance = (Demo) target;
            if (GUILayout.Button("Reduce"))
            {
                instance.Change(-0.1f);
            }

            if (GUILayout.Button("Reset"))
            {
                instance.Reset();
            }
        }
    }
#endif
}