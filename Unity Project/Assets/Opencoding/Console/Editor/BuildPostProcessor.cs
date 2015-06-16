using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using Opencoding.XCodeEditor;
using UnityEngine;

namespace Opencoding.Console.Editor
{
	static class BuildPostProcessor
	{
		[PostProcessBuild(200)]
		public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
		{
			if (target != BuildTarget.iPhone)
				return;

			// Unity 5 can copy files in sub folders of Plugins without this
			if (Application.unityVersion.StartsWith("4"))
			{
				var project = new XCProject(pathToBuiltProject);
				project.ApplyMod(Application.dataPath, "Assets/Opencoding/Console/Editor/fixup.projmods");
				project.Save();
			}
		}

		[PostProcessScene]
		public static void OnPostprocessScene()
		{
			if (EditorApplication.isPlaying)
				return;

			var debugConsoles = UnityEngine.Object.FindObjectsOfType<DebugConsole>();
			if(debugConsoles.Length > 1)
				throw new InvalidOperationException("More than one debug console in the scene " + EditorApplication.currentScene);

			if (debugConsoles.Length == 0)
				return;

			var debugConsole = debugConsoles[0];
			if(debugConsole.Settings.OnlyInDevBuilds && !EditorUserBuildSettings.development)
				UnityEngine.Object.DestroyImmediate(debugConsole.gameObject);

			if(!String.IsNullOrEmpty(debugConsole.Settings.DisableIfDefined) && EditorUserBuildSettings.activeScriptCompilationDefines.Contains(debugConsole.Settings.DisableIfDefined))
				UnityEngine.Object.DestroyImmediate(debugConsole.gameObject);

			if (debugConsole.Settings.AutoSetVersion)
			{
				debugConsole.Settings.GameVersion = PlayerSettings.bundleVersion;
			}
		}
	}
}