using Smod2.Commands;
using Smod2;

namespace Nuke_Escape.Commands
{
	class NEToggle : ICommandHandler
	{
		private Plugin plugin;

		public NEEventHandler NEHandler;

		public NEToggle(Plugin plugin, NEEventHandler handler)
		{
			this.plugin = plugin;
			NEHandler = handler;
		}

		public string GetCommandDescription()
		{
			return $"Toggles {plugin.Details.name}.";
		}

		public string GetUsage()
		{
			return "ne_toggle";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			NEHandler.NE_Config.NE_Toggled = !NEHandler.NE_Config.NE_Toggled;
			if (NEHandler.NE_Config.NE_Toggled)
			{
				return new string[] { $"{plugin.Details.id} will be active next round!" };
			}
			else
			{
				return new string[] { $"{plugin.Details.id} will NOT be active next round!" };
			}
		}
	}
}
