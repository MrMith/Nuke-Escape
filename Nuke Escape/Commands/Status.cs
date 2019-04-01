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
			if (NEHandler.NE_Config.NE_HasServerStarted)
			{
				if (NEHandler.NE_Config.NE_Toggled)
				{
					return new string[] { $"{plugin.Details.id} will be active next round! Currently active:{NEHandler.NE_Config.NE_Active}" };
				}
				else
				{
					return new string[] { $"{plugin.Details.id} will NOT be active next round! Currently active:{NEHandler.NE_Config.NE_Active}" };
				}
			}
			else
			{
				if (NEHandler.NE_Config.NE_Toggled)
				{
					return new string[] { $"{plugin.Details.id} will be active this round! Currently active:{NEHandler.NE_Config.NE_Active}" };
				}
				else
				{
					return new string[] { $"{plugin.Details.id} will NOT be active this round! Currently active:{NEHandler.NE_Config.NE_Active}" };
				}
			}
		}
	}
}
