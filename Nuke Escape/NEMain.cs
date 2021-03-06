﻿using Smod2;
using Smod2.Attributes;
using Nuke_Escape.Commands;

/*
Overview: Everyone will spawn as either D class or an SCP, you are currently in a facility with some of the most well known SCP's, and 079 knows this as well. 
He has currently taken control of the facility to release his fellow SCPs, as well as destroy the facility with the alpha warhead in order to damage the foundation. 
After waking up you find yourself with an old pistol and an all access key card to the facility, you find a post it note on the back of the card; scribbled onto it is a simple word, RUN.
\\\\\\\\\\\\\\\\
Goal: Escape however you want, work together with your fellow prisoners, or be unlucky enough to have a psycho for a cellmate who cares for nothing else but watching the world burn. Don't slow down, you don't know when the nuke will go off, so GTFO ASAP!
\\\\\\\\\\\\\\\\
GAME SETTINGS
FF is ON, everyone will spawn with an 05 card and a pistol, the nuke is locked by 079 and cannot be turned off the only way out is the exit.
*/

namespace Nuke_Escape
{
	[PluginDetails(
		author = "Mith",
		name = "Nuke Escape",
		description = "Everyone will spawn as either D class or an SCP, you are currently in a facility with some of the most well known SCP's, and 079 knows this as well.  He has currently taken control of the facility to release his fellow SCPs, as well as destroy the facility with the alpha warhead in order to damage the foundation. After waking up you find yourself with an old pistol and an all access key card to the facility, you find a post it note on the back of the card; scribbled onto it is a simple word, RUN.",
		id = "Mith.gamemode.NukeEscape",
		version = "1.0.0",
		SmodMajor = 3,
		SmodMinor = 4,
		SmodRevision = 0
		)]
	class NEMain : Plugin
	{

		public override void OnDisable()
		{
			Info($"{this.Details.name}(Version:{this.Details.version}) has been disabled.");
		}

		public override void OnEnable()
		{
			Info($"{this.Details.name}(Version:{this.Details.version}) has been enabled.");
		}

		public override void Register()
		{
			AddEventHandlers(new NEEventHandler(this));
			
			AddCommand("ne_version", new NEVersion(this));
			AddCommand("ne_disable", new NEDisable(this));

			
			AddConfig(new Smod2.Config.ConfigSetting("ne_disable", false, true, $"Enables or disables {Details.name}."));
			AddConfig(new Smod2.Config.ConfigSetting("ne_broadcast", true, true, "Should player's be broadcasted to when the roundstarts."));
			AddConfig(new Smod2.Config.ConfigSetting("ne_dclassinfammo", false, true, "Should D-Class be given infinite ammo?"));
			AddConfig(new Smod2.Config.ConfigSetting("ne_nukeshow", true, true, "Should a message be broadcasted when the nuke starts?"));

			AddConfig(new Smod2.Config.ConfigSetting("ne_broadcastmessage", "You have NUKETIME seconds till the nuke starts! GET OUT OF HERE! FF is on after SPAWNPROTECT seconds and watch out for SCPs!", true, "The broadcasted message"));
			AddConfig(new Smod2.Config.ConfigSetting("ne_nukemessage", "079 has forced to nuke to be on! You CANNOT turn it off! RUN!", true, "The message player's are shown when the nuke starts."));
			AddConfig(new Smod2.Config.ConfigSetting("ne_welcomemessage", "Welcome to Nuke-Escape!", true, "The message player's are shown before the round starts."));
			
			AddConfig(new Smod2.Config.ConfigSetting("ne_spawnprotect", 10, true, "Time till d-class can murder eachother."));

			AddConfig(new Smod2.Config.ConfigSetting("ne_dclassitems", new int[] { 11,13 }, true, "What items D-Class get when they're spawned in."));
			AddConfig(new Smod2.Config.ConfigSetting("ne_dclassammo", new int[] { 0, 0, 36 }, true, "Sets ammo of D-Class on spawn. 1st number = 5.56mm, 2nd number = 7.62mm, 3rd number = 9mm"));
			AddConfig(new Smod2.Config.ConfigSetting("ne_nuketime", new int[] { 90 }, true, "Forces the nuke to be on at this time, if more then one number is present it will randomly select one. It takes 100 seconds for nuke to explode (10 seconds for announcement and 90 for the alarm phase) so at 190 seconds in the round for the default the nuke will explode"));

			GamemodeManager.GamemodeManager.RegisterMode(this,"40444404444044440444");
		}
	}
}