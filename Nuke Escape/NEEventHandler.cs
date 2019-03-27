using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using Nuke_Escape.Manager;
using Smod2.EventSystem.Events;
using System.Collections.Generic;

namespace Nuke_Escape
{
	internal class NEEventHandler : IEventHandlerRoundEnd, IEventHandlerRoundStart , IEventHandlerSetRole, IEventHandlerWaitingForPlayers, IEventHandlerPlayerHurt, IEventHandlerWarheadStopCountdown, IEventHandlerReload, IEventHandlerPlayerJoin, IEventHandlerSummonVehicle, IEventHandlerDecideTeamRespawnQueue
	{
		public Plugin plugin;

		public bool CheckFalseRoundEnd = true;

		public NE_Config NE_Config = new NE_Config();

		public NEEventHandler(Plugin plugin)
		{
			this.plugin = plugin;
		}

		public void OnDecideTeamRespawnQueue(DecideRespawnQueueEvent ev)
		{
			string SpawnQueue = "";
			for(int i = 0; i < plugin.Server.MaxPlayers/5;i++)
			{
				SpawnQueue += NE_Config.NE_SpawnQueue;
			}
			char[] spawnCharArray = SpawnQueue.ToCharArray();
			List<Smod2.API.Team> spawnqueueTeam = new List<Smod2.API.Team>();
			for (int i = 0; i <= spawnCharArray.Length-1;i++)
			{
				spawnqueueTeam.Add((Smod2.API.Team)int.Parse(spawnCharArray[i].ToString()));
			}
			ev.Teams = spawnqueueTeam.ToArray();
		}

		public void OnPlayerHurt(PlayerHurtEvent ev)
		{
			if (ev?.Attacker?.TeamRole.Role == Role.CLASSD && ev.Player.TeamRole.Role == Role.CLASSD)
			{
				if(plugin.Server.Round.Duration < NE_Config.NE_SpawnProtect)
				{
					ev.Damage = 0;
				}
			}
		}

		public void OnPlayerJoin(PlayerJoinEvent ev)
		{
			if (NE_Config.NE_Active)
			{
				ev.Player.PersonalBroadcast(15, "Welcome to Nuke Escape!", true);

				if(plugin.Server.Round.Duration > 0 && plugin.Server.Round.Duration <= NE_Config.NE_LateSpawn)
				{
					ev.Player.ChangeRole(Role.CLASSD);
				}
			}
		}

		public void OnReload(PlayerReloadEvent ev)
		{
			if (NE_Config.NE_Active && ev.Player.TeamRole.Role == Role.CLASSD && NE_Config.NE_InfiniteAmmo)
			{
				ev.AmmoRemoved = 0;
			}
		}

		public void OnRoundEnd(RoundEndEvent ev)
		{
			if (NE_Config.NE_Active)
			{
				if (CheckFalseRoundEnd)
				{
					CheckFalseRoundEnd = false;
				}
				NE_Config.NE_Active = false;
			}
		}

		public void OnRoundStart(RoundStartEvent ev)
		{
			if(NE_Config.NE_Toggled)
			{
				NE_Config.NE_Active = true;
			}

			if(NE_Config.NE_Active)
			{
				plugin.Server.Map.ClearBroadcasts();
				plugin.Server.Map.Shake();
				if(NE_Config.NE_Broadcast)
				{
					plugin.Server.Map.Broadcast(30, NE_Config.NE_BroadcastMessage, true);
				}
				AlphaWarheadController.host.ScheduleDetonation(NE_Config.NE_NukeTime+1, true);
			}
		}

		public void OnSetRole(PlayerSetRoleEvent ev)
		{
			if(ev.Player.TeamRole.Role == Smod2.API.Role.CLASSD && NE_Config.NE_Active)
			{
				ev.Items.Clear();
				foreach(var item in NE_Config.NE_DClassitems)
				{
					ev.Items.Add((ItemType)item);
				}

				ev.Player.SetAmmo(AmmoType.DROPPED_5, NE_Config.NE_DClassammo[0]);
				ev.Player.SetAmmo(AmmoType.DROPPED_7, NE_Config.NE_DClassammo[1]);
				ev.Player.SetAmmo(AmmoType.DROPPED_9, NE_Config.NE_DClassammo[2]);
			}
		}

		public void OnStopCountdown(WarheadStopEvent ev)
		{
			if (NE_Config.NE_Active)
			{
				ev.Cancel = false;
			}
		}

		public void OnSummonVehicle(SummonVehicleEvent ev)
		{
			if (NE_Config.NE_Active)
			{
				ev.AllowSummon = false;
			}
		}

		public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
		{
			if (plugin.GetConfigBool("ne_disable"))
			{
				plugin.pluginManager.DisablePlugin(plugin);
				return;
			}

			CheckFalseRoundEnd = true;
			NE_Config.SetupConfig(plugin);
			if(NE_Config.NE_Toggled)
			{
				NE_Config.NE_Active = true;
			}
			plugin.Info(NE_Config.NE_Active +":active:" + NE_Config.NE_Toggled+":toggled");
		}
	}
}