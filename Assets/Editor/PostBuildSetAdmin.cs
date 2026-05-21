using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class PostBuildSetAdmin : IPostprocessBuildWithReport {
  public int callbackOrder => 0;

  public void OnPostprocessBuild(BuildReport report) {
    var exePath = report.summary.outputPath;
    var manifestPath = System.IO.Path.Combine(
      System.IO.Path.GetDirectoryName(exePath),
      System.IO.Path.GetFileNameWithoutExtension(exePath) + ".exe.manifest"
    );
    var srcManifest = System.IO.Path.Combine(
      Application.dataPath, "HyperStudio.manifest"
    );
    if (System.IO.File.Exists(srcManifest)) {
      System.IO.File.Copy(srcManifest, manifestPath, true);
      Debug.Log($"Manifest copied to: {manifestPath}");
    }
  }
}
