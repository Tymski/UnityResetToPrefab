using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Tymski.ResetToPrefab
{
    public static class ResetRectTransformToPrefab
    {
        [MenuItem("CONTEXT/RectTransform/Reset to Prefab")]
        public static void ResetToPrefab(MenuCommand command)
        {
            RectTransform rectTransform = (RectTransform)command.context;
            GameObject prefab = PrefabUtility.GetCorrespondingObjectFromSource(rectTransform.gameObject);
            if (prefab == null) return;
            RectTransform prefabRectTransform = prefab.transform.GetComponent<RectTransform>();

            string[] propsToReset =
            {
                "anchorMin",
                "anchorMax",
                "anchoredPosition",
                "anchoredPosition3D",
                "pivot",
                "sizeDelta",
                "offsetMin",
                "offsetMax",
                "localPosition",
                "localEulerAngles",
                "localScale",
            };

            PropertyInfo[] props = rectTransform.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty);

            foreach (string propName in propsToReset)
            {
                var prop = props.Where(p => p.Name == propName).First();
                prop.SetValue(rectTransform, prop.GetValue(prefabRectTransform));
            }
        }
    }
}