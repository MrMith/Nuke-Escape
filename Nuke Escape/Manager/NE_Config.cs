namespace Nuke_Escape.Manager
{
	class NE_Config
	{
		public int[] NE_DClassitems { get; private set; }
		public int[] NE_DClassammo { get; private set; }

		public int NE_SpawnProtect { get; private set; }
		public int NE_NukeTime { get; private set; }
		public int NE_LateSpawn { get; private set; }

		public bool NE_Active { get; set; }
		public bool NE_InfiniteAmmo { get; set; }
		public bool NE_NukeActive { get; set; }
		public bool NE_Toggled { get; set; }
		public bool NE_Broadcast { get; set; }
		public bool NE_HasServerStarted { get; set; }

		public string NE_BroadcastMessage { get; set; }
		public string NE_SpawnQueue { get; set; }

		public void SetupConfig(Smod2.Plugin plugin)
		{
			NE_DClassitems = plugin.GetConfigIntList("ne_dclassitems");
			NE_DClassammo = plugin.GetConfigIntList("ne_dclassammo");

			NE_SpawnProtect = plugin.GetConfigInt("ne_spawnprotect");
			NE_NukeTime = plugin.GetConfigInt("ne_nuketime");
			NE_LateSpawn = plugin.GetConfigInt("ne_latespawn");

			NE_InfiniteAmmo = plugin.GetConfigBool("ne_dclassinfammo");
			NE_Broadcast = plugin.GetConfigBool("ne_broadcast");

			if (!NE_Toggled)
			{
				NE_Toggled = plugin.GetConfigBool("ne_defaulttoggle");
			}

			NE_BroadcastMessage = plugin.GetConfigString("ne_broadcastmessage");
			NE_BroadcastMessage = NE_BroadcastMessage.Replace("NUKETIME", NE_NukeTime.ToString());
			NE_BroadcastMessage = NE_BroadcastMessage.Replace("SPAWNPROTECT", NE_SpawnProtect.ToString());
			NE_SpawnQueue = plugin.GetConfigString("ne_spawnqueue");

			NE_Active = false;
			NE_NukeActive = false;
			NE_HasServerStarted = false;
		}
	}
}
