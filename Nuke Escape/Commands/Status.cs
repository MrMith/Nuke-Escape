using Smod2.Commands;
using Smod2;

namespace Nuke_Escape.Commands
{
	class NEStatus : ICommandHandler
	{
		private Plugin plugin;

		public NEEventHandler NEHandler;

		public NEStatus(Plugin plugin, NEEventHandler handler)
		{
			this.plugin = plugin;
			NEHandler = handler;
		}

		public string GetCommandDescription()
		{
			return $"Shows status of {plugin.Details.name}.";
		}

		public string GetUsage()
		{
			return "ne_status";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			if (NEHandler.NE_Config.NE_Active)
			{
				return new string[] { $"{plugin.Details.id} is currently active! Will it be next round:{NEHandler.NE_Config.NE_Toggled}" };
			}
			else
			{
				return new string[] { $"{plugin.Details.id} is NOT currently active! Will it be next round:{NEHandler.NE_Config.NE_Toggled}" };
			}
		}
	}
}
