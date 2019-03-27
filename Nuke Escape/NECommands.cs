using Smod2.Commands;
using Smod2;

namespace Nuke_Escape
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

	class NEToggle : ICommandHandler
	{
		private Plugin plugin;

		public NEEventHandler NEHandler;

		public NEToggle(Plugin plugin,NEEventHandler handler)
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
			if(NEHandler.NE_Config.NE_Toggled)
			{
				return new string[] { $"{plugin.Details.id} will be active next round!" };
			}
			else
			{
				return new string[] { $"{plugin.Details.id} will NOT be active next round!" };
			}
		}
	}

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
			return new string[] {NEHandler.plugin.Details.id + " has been disabled." };
		}
	}
}
