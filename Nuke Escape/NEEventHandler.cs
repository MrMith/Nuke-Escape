﻿using Smod2;
using Smod2.API;
using Smod2.Events;
using Smod2.EventHandlers;
using Smod2.EventSystem.Events;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Nuke_Escape.Manager;

namespace Nuke_Escape
{
	class NEEventHandler : IEventHandlerRoundStart , IEventHandlerSetRole, IEventHandlerWaitingForPlayers, IEventHandlerPlayerHurt, IEventHandlerWarheadStopCountdown, IEventHandlerReload, IEventHandlerPlayerJoin, IEventHandlerSummonVehicle, IEventHandlerWarheadStartCountdown, IEventHandlerSetSCPConfig
	{
		public Plugin plugin;

		public NE_Config NE_Config = new NE_Config();

		public NEEventHandler(Plugin plugin)
		{
			this.plugin = plugin;
		}

		public void OnPlayerHurt(PlayerHurtEvent ev)
		{
			if (GamemodeManager.GamemodeManager.GetCurrentMode().Equals(plugin))
			{
				if (ev?.Attacker?.TeamRole.Role == Role.CLASSD && ev.Player.TeamRole.Role == Role.CLASSD)
				{
					if (plugin.Server.Round.Duration < NE_Config.NE_SpawnProtect)
					{
						ev.Damage = 0;
					}
				}
			}
		}

		public void OnPlayerJoin(PlayerJoinEvent ev)
		{
			if (GamemodeManager.GamemodeManager.GetCurrentMode().Equals(plugin))
			{
				if (!NE_Config.NE_HasServerStarted)
				{
					ev.Player.PersonalBroadcast(40, NE_Config.NE_WelcomeMessage, true);
				}
				else if(NE_Config.NE_Broadcast)
				{
					ev.Player.PersonalBroadcast(30, NE_Config.NE_BroadcastMessage, true);
				}

				if(plugin.Server.Round.Duration > 0 && plugin.Server.Round.Duration <= NE_Config.NE_LateSpawn)
				{
					if (plugin.Server.NumPlayers >= GamemodeManager.GamemodeManager.SpawnQueue.Count)
					{
						MakeSurePlayerSpawnIn((UnityEngine.GameObject)ev.Player.GetGameObject(), Role.CLASSD);
					}
					else
					{
						if(GamemodeManager.GamemodeManager.CurrentQueue[plugin.Server.NumPlayers] == Smod2.API.Team.SCP)
						{
							MakeSurePlayerSpawnIn((UnityEngine.GameObject)ev.Player.GetGameObject(),GetRandomSCP());
						}
						else
						{
							MakeSurePlayerSpawnIn((UnityEngine.GameObject)ev.Player.GetGameObject(), Role.CLASSD);
						}
					}
				}
			}
		}

		public async void MakeSurePlayerSpawnIn(UnityEngine.GameObject gameObject, Role role)
		{
			await Task.Delay(250);
			ServerMod2.API.SmodPlayer playa = new ServerMod2.API.SmodPlayer(gameObject);
			playa.ChangeRole(role);
			if(role == Role.CLASSD)
			{
				gameObject.GetComponent<WeaponManager>().NetworkfriendlyFire = true;
			}
		}

		public void OnReload(PlayerReloadEvent ev)
		{
			if (GamemodeManager.GamemodeManager.GetCurrentMode().Equals(plugin) && ev.Player.TeamRole.Role == Role.CLASSD && NE_Config.NE_InfiniteAmmo)
			{
				ev.AmmoRemoved = 0;
			}
		}

		public void OnRoundStart(RoundStartEvent ev)
		{
			if (GamemodeManager.GamemodeManager.GetCurrentMode().Equals(plugin))
			{
				NE_Config.NE_HasServerStarted = true;
				plugin.Server.Map.ClearBroadcasts();
				if (NE_Config.NE_Broadcast)
				{
					plugin.Server.Map.Broadcast(30, NE_Config.NE_BroadcastMessage, true);
				}
				AlphaWarheadController.host.ScheduleDetonation(NE_Config.NE_NukeTime+1, true);// + 1 because if its 0 then it won't do anything :(

				foreach(Smod2.API.Player playa in plugin.Server.GetPlayers())
				{
					if(playa.TeamRole.Team != Smod2.API.Team.SCP)
					{
						((UnityEngine.GameObject)playa.GetGameObject()).GetComponent<WeaponManager>().NetworkfriendlyFire = true;
					}
				}
			}
		}

		public void OnSetRole(PlayerSetRoleEvent ev)
		{
			if(ev.Player.TeamRole.Role == Smod2.API.Role.CLASSD &&GamemodeManager.GamemodeManager.GetCurrentMode().Equals(plugin))
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

		public void OnStartCountdown(WarheadStartEvent ev)
		{
			if (GamemodeManager.GamemodeManager.GetCurrentMode().Equals(plugin))
			{
				if (NE_Config.NE_NukeShouldMessage)
				{
					plugin.Server.Map.Broadcast(20, NE_Config.NE_NukeMessage, true);
				}
			}
		}

		public void OnStopCountdown(WarheadStopEvent ev)
		{
			if (GamemodeManager.GamemodeManager.GetCurrentMode().Equals(plugin))
			{
				ev.Cancel = false;
			}
		}

		public void OnSummonVehicle(SummonVehicleEvent ev)
		{
			if (GamemodeManager.GamemodeManager.GetCurrentMode().Equals(plugin))
			{
				ev.AllowSummon = false;
			}
		}

		public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
		{
			if (plugin.GetConfigBool("ne_disable"))
			{
				plugin.PluginManager.DisablePlugin(plugin);
				return;
			}
			NE_Config.SetupConfig(plugin);
		}

		public Role GetRandomSCP()
		{
			Random rand = new Random();

			Dictionary<Role, int> CheckTeams = new Dictionary<Role, int>();

			foreach(var role in (Role[])Enum.GetValues(typeof(Role)))
			{
				CheckTeams[role] = 0;
			}

			foreach(Player playa in plugin.Server.GetPlayers())
			{
				CheckTeams[playa.TeamRole.Role]++;
			}

			int SCPToRandom;
			int alive = 0;
			for (;;)
			{
				SCPToRandom = NE_Config.NE_SCPsToRandom[rand.Next(0, NE_Config.NE_SCPsToRandom.Length - 1)];

				foreach (var role in (Role[])Enum.GetValues(typeof(Role)))
				{
					if (CheckTeams[(Role)SCPToRandom] == alive && (Role)SCPToRandom == role)
					{
						return (Role)SCPToRandom;
					}
				}

				alive++;
			}
		}

		public void OnSetSCPConfig(SetSCPConfigEvent ev)
		{
			if (GamemodeManager.GamemodeManager.GetCurrentMode().Equals(plugin))
			{
				ev.Ban079 = true;
				ev.Ban049 = false;
				ev.Ban096 = false;
				ev.Ban106 = false;
				ev.Ban173 = false;
				ev.Ban939_53 = false;
				ev.Ban939_89 = false;
			}
		}
	}
}