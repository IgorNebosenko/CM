#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CM.Maze.Editors
{
    [CustomEditor(typeof(MazeFragment))]
    public class MazeFragmentEditor : Editor
    {
        private SerializedProperty _connectionProperty;

        private SerializedProperty ConnectionProperty => _connectionProperty 
            ??= serializedObject.FindProperty("connectionData");

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Allowed Zones");
            var dataProp = ConnectionProperty.FindPropertyRelative("data");
            DrawColumnsIndexes(MazeFragment.SizeZ);
            
            for (var i = 0; i < MazeFragment.SizeX; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(new GUIContent(i.ToString()), GUILayout.MaxWidth(12));
                var stringProp = dataProp.GetArrayElementAtIndex(i);
                for (var j = 0; j < MazeFragment.SizeZ; j++)
                {
                    var itemProp = stringProp.FindPropertyRelative("lineData").GetArrayElementAtIndex(j);
                    EditorGUILayout.PropertyField(itemProp, GUIContent.none, GUILayout.MaxWidth(16));
                }
                EditorGUILayout.LabelField(new GUIContent(i.ToString()), GUILayout.MaxWidth(12));
                EditorGUILayout.EndHorizontal();
            }
            DrawColumnsIndexes(MazeFragment.SizeZ);

            EditorGUILayout.LabelField("Spawn points");
            var spawnPointsProp = ConnectionProperty.FindPropertyRelative("spawnPoints");
            if (GUILayout.Button("+"))
                spawnPointsProp.InsertArrayElementAtIndex(spawnPointsProp.arraySize);

            for (var i = 0; i < spawnPointsProp.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();
                
                EditorGUILayout.PropertyField(spawnPointsProp.GetArrayElementAtIndex(i));
                if (GUILayout.Button("x"))
                    spawnPointsProp.DeleteArrayElementAtIndex(i);

                EditorGUILayout.EndHorizontal();
            }

            serializedObject.ApplyModifiedProperties();
        }
        

        private void DrawColumnsIndexes(int size)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(" ", GUILayout.MaxWidth(16));
            
            for (var i = 0; i < size; i++)
            {
                EditorGUILayout.LabelField(i.ToString(), GUILayout.MaxWidth(16));
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}
#endif