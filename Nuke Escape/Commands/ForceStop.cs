using Smod2.Commands;
using Smod2;

namespace Nuke_Escape.Commands
{
	class NEForceStop : ICommandHandler
	{
		private Plugin plugin;

		public NEEventHandler NEHandler;

		public NEForceStop(Plugin plugin, NEEventHandler handler)
		{
			this.plugin = plugin;
			NEHandler = handler;
		}

		public string GetCommandDescription()
		{
			return $"Force stops {plugin.Details.name}.";
		}

		public string GetUsage()
		{
			return "ne_forcestop";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			NEHandler.NE_Config.NE_Active = false;
			AlphaWarheadController.host.CancelDetonation();

			return new string[] { NEHandler.plugin.Details.id + " has been disabled." };
		}
	}
}
