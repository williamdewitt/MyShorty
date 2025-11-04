using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.PathConstruction;

[GitHubActions(
    "continuous",
    GitHubActionsImage.UbuntuLatest,
    On = new[] { GitHubActionsTrigger.Push },
    InvokedTargets = new[] { nameof(Publish) })]
class Build : NukeBuild
{
  /// Support plugins are available for:
  ///   - JetBrains ReSharper        https://nuke.build/resharper
  ///   - JetBrains Rider            https://nuke.build/rider
  ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
  ///   - Microsoft VSCode           https://nuke.build/vscode

  public static int Main() => Execute<Build>(x => x.Test);

  [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
  readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

  [Solution]
  readonly Solution Solution;

  GitHubActions GitHubActions => GitHubActions.Instance;

  Target Clean => _ => _
      .Before(Restore)
      .Executes(() =>
      {
        DotNetTasks.DotNetClean();
      });

  Target Restore => _ => _
      .Executes(() =>
      {
        DotNetTasks.DotNetRestore();
      });

  Target Compile => _ => _
      .DependsOn(Restore)
      .Executes(() =>
      {
        DotNetTasks.DotNetBuild(o => 
          o.SetConfiguration(Configuration)
        );
      });

  Target Test => _ => _
      .DependsOn(Compile)
      .Executes(() =>
      {
        DotNetTasks.DotNetTest();
      });
  
  Target Publish => _ => _
      .DependsOn(Test)
      .Executes(() =>
      {
        DotNetTasks.DotNetTest();
      });
}
