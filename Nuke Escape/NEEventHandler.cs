using Smod2;
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
	class NEEventHandler : IEventHandlerRoundEnd, IEventHandlerRoundStart , IEventHandlerSetRole, IEventHandlerWaitingForPlayers, IEventHandlerPlayerHurt, IEventHandlerWarheadStopCountdown, IEventHandlerReload, IEventHandlerPlayerJoin, IEventHandlerSummonVehicle, IEventHandlerDecideTeamRespawnQueue, IEventHandlerWarheadStartCountdown
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
			if(NE_Config.NE_Active)
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
			if (NE_Config.NE_Active)
			{
				if(!NE_Config.NE_HasServerStarted)
				{
					ev.Player.PersonalBroadcast(40, NE_Config.NE_WelcomeMessage, true);
				}
				else if(NE_Config.NE_Broadcast)
				{
					ev.Player.PersonalBroadcast(30, NE_Config.NE_BroadcastMessage, true);
				}

				if(plugin.Server.Round.Duration > 0 && plugin.Server.Round.Duration <= NE_Config.NE_LateSpawn)
				{
					int rem = plugin.Server.NumPlayers % 5;
					int role = int.Parse(NE_Config.NE_SpawnQueue[rem + 1].ToString());
					if(role == 0)
					{
						MakeSurePlayerSpawnIn((UnityEngine.GameObject)ev.Player.GetGameObject(), GetRandomSCP());

						return;
					}
					MakeSurePlayerSpawnIn((UnityEngine.GameObject)ev.Player.GetGameObject(), Role.CLASSD);
				}
			}
		}

		public async void MakeSurePlayerSpawnIn(UnityEngine.GameObject gameObject, Role role)
		{
			await Task.Delay(250);
			ServerMod2.API.SmodPlayer playa = new ServerMod2.API.SmodPlayer(gameObject);
			playa.ChangeRole(role);
			return;
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
			NE_Config.NE_HasServerStarted = true;

			if (NE_Config.NE_Toggled)
			{
				NE_Config.NE_Active = true;
			}

			if (NE_Config.NE_Active)
			{
				plugin.Server.Map.ClearBroadcasts();
				if (NE_Config.NE_Broadcast)
				{
					plugin.Server.Map.Broadcast(30, NE_Config.NE_BroadcastMessage, true);
				}
				AlphaWarheadController.host.ScheduleDetonation(NE_Config.NE_NukeTime+1, true);// + 1 because if its 0 then it won't do anything :(
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

		public void OnStartCountdown(WarheadStartEvent ev)
		{
			if (NE_Config.NE_Active)
			{
				if(NE_Config.NE_NukeShouldMessage)
				{
					plugin.Server.Map.Broadcast(20, NE_Config.NE_NukeMessage, true);
				}
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

			if (NE_Config.NE_Toggled)
			{
				NE_Config.NE_Active = true;
			}
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
	}
}