using System;
using Xunit;

namespace QAToolKit.Engine.Probes.Test
{
    public class IgnoreOnGithubFact : FactAttribute
    {
        public IgnoreOnGithubFact()
        {
            if (IsGitHubAction())
            {
                Skip = "Ignore the test when run in Github agent.";
            }
        }

        private static bool IsGitHubAction()
            => Environment.GetEnvironmentVariable("GITHUB_ACTION") != null;
    }
}
