using HauntedPSX.RenderPipelines.PSX.Runtime;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace HauntedPSX.RenderPipelines.PSX.Editor
{
    [ExecuteInEditMode]
    internal static class PSXRenderPipelineAssetFactory
    {
        private static readonly string s_PackagePath = "Packages/com.hauntedpsx.render-pipelines.psx/";

        [MenuItem("HauntedPS1/Create HauntedPS1 Render Pipeline Asset", priority = CoreUtils.assetCreateMenuPriority1)]
        private static void CreatePSXRenderPipelineAsset()
        {
            var icon = EditorGUIUtility.FindTexture("ScriptableObject Icon");
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, ScriptableObject.CreateInstance<DoCreateNewAssetPSXRenderPipelineAsset>(), "PSXRenderPipelineAsset.asset", icon, null);
        }

        private class DoCreateNewAssetPSXRenderPipelineAsset : UnityEditor.ProjectWindowCallback.EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var newAsset = CreateInstance<PSXRenderPipelineAsset>();
                newAsset.name = Path.GetFileName(pathName);

                ResourceReloader.ReloadAllNullIn(newAsset, s_PackagePath);

                AssetDatabase.CreateAsset(newAsset, pathName);
                ProjectWindowUtil.ShowCreatedAsset(newAsset);
            }
        }

        [MenuItem("HauntedPS1/Create HauntedPS1 Render Pipeline Resources", priority = CoreUtils.assetCreateMenuPriority1)]
        private static void CreatePSXRenderPipelineResources()
        {
            var icon = EditorGUIUtility.FindTexture("ScriptableObject Icon");
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0, ScriptableObject.CreateInstance<DoCreateNewAssetPSXRenderPipelineResources>(), "PSXRenderPipelineResources.asset", icon, null);
        }

        private class DoCreateNewAssetPSXRenderPipelineResources : UnityEditor.ProjectWindowCallback.EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                var newAsset = CreateInstance<PSXRenderPipelineResources>();
                newAsset.name = Path.GetFileName(pathName);

                ResourceReloader.ReloadAllNullIn(newAsset, s_PackagePath);

                AssetDatabase.CreateAsset(newAsset, pathName);
                ProjectWindowUtil.ShowCreatedAsset(newAsset);
            }
        }
    }
}
