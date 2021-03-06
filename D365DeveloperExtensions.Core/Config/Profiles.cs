﻿using D365DeveloperExtensions.Core.Enums;
using D365DeveloperExtensions.Core.Models;
using D365DeveloperExtensions.Core.Vs;
using EnvDTE;
using System.Collections.Generic;

namespace D365DeveloperExtensions.Core.Config
{
    public static class Profiles
    {
        public static List<string> GetProfiles(Project project, ToolWindowType toolWindowType)
        {
            if (toolWindowType == ToolWindowType.PluginTraceViewer)
                return null;

            SpklConfig spklConfig = ConfigFile.GetSpklConfigFile(project);
            string projectPath = ProjectWorker.GetProjectPath(project);

            switch (toolWindowType)
            {
                case ToolWindowType.PluginDeployer:
                    return GetConfigProfiles(projectPath, spklConfig.plugins);
                case ToolWindowType.SolutionPackager:
                    return GetConfigProfiles(projectPath, spklConfig.solutions);
                case ToolWindowType.WebResourceDeployer:
                    return GetConfigProfiles(projectPath, spklConfig.webresources);
                default:
                    return null;
            }
        }

        public static List<string> GetConfigProfiles<T>(string projectPath, List<T> configs)
        {
            if (configs == null)
                return new List<string>();

            List<string> profiles = new List<string>();

            int i = 1;
            foreach (dynamic config in configs)
            {
                profiles.Add(string.IsNullOrEmpty(config.profile)
                    ? $"{ExtensionConstants.NoProfilesText} {i}"
                    : config.profile);
                i++;
            }

            return profiles;
        }
    }
}