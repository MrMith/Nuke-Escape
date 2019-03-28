using Smod2.Commands;
using Smod2;

namespace Nuke_Escape.Commands
{
	class NEDisable : ICommandHandler
	{
		private Plugin plugin;

		public NEDisable(Plugin plugin)
		{
			this.plugin = plugin;
		}

		public string GetCommandDescription()
		{
			return $"Enables or disables {plugin.Details.name}.";
		}

		public string GetUsage()
		{
			return "ne_disable";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			Smod2.PluginManager.Manager.DisablePlugin(plugin.Details.id);
			return new string[] { "Disabled " + plugin.Details.id };
		}
	}
}
