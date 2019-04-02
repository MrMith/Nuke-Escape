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
			return "ne on | ne off | ne status";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			if(args.Length < 1)
			{
				return new string[] {GetUsage()};
			}

			switch(args[0].ToLower())
			{
				case "on":
					NEHandler.NE_Config.NE_Toggled = true;

					if (!NEHandler.NE_Config.NE_HasServerStarted)
					{
						NEHandler.NE_Config.NE_Active = true;
					}
					goto case "status";
				case "off":
					NEHandler.NE_Config.NE_Toggled = false;

					if (!NEHandler.NE_Config.NE_HasServerStarted)
					{
						NEHandler.NE_Config.NE_Active = false;
					}
					plugin.Server.Map.ClearBroadcasts();
					goto case "status";
				case "status":
					if (NEHandler.NE_Config.NE_Toggled)
					{
						return new string[] { $"Turned {plugin.Details.id} on! Currently active {NEHandler.NE_Config.NE_Active}" };
					}
					else
					{
						return new string[] { $"Turned {plugin.Details.id} off! Currently active {NEHandler.NE_Config.NE_Active}" };
					}
				default:
					return new string[] { GetUsage() };
			}
		}
	}
}
