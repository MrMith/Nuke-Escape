using Smod2.Commands;
using Smod2;

namespace Nuke_Escape.Commands
{
	class NEVersion : ICommandHandler
	{
		private Plugin plugin;

		public NEVersion(Plugin plugin)
		{
			this.plugin = plugin;
		}

		public string GetCommandDescription()
		{
			return $"Gets version of {plugin.Details.name} for debugging";
		}

		public string GetUsage()
		{
			return "ne_version";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			return new string[] { plugin.Details.id + " is version " + plugin.Details.version };
		}
	}
}
