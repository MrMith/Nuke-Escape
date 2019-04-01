using Smod2;
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
		id = "Mith.NukeEscape",
		version = "0.01",
		SmodMajor = 3,
		SmodMinor = 3,
		SmodRevision = 0
		)]
	class NEMain : Plugin
	{
		public NEEventHandler NEHandler;

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
			AddEventHandlers(NEHandler = new NEEventHandler(this));

			AddCommand("ne_version", new NEVersion(this));
			AddCommand("ne_toggle", new NEToggle(this, NEHandler));
			AddCommand("ne_status", new NEStatus(this, NEHandler));
			AddCommand("ne_disable", new NEDisable(this));
			AddCommand("ne_forcestop", new NEForceStop(this, NEHandler));

			AddConfig(new Smod2.Config.ConfigSetting("ne_disable", false, Smod2.Config.SettingType.BOOL, true, $"Enables or disables {this.Details.name}."));
			AddConfig(new Smod2.Config.ConfigSetting("ne_defaulttoggle", false, Smod2.Config.SettingType.BOOL, true, $"Should this gamemode be toggled on or off by default."));
			AddConfig(new Smod2.Config.ConfigSetting("ne_broadcast", true, Smod2.Config.SettingType.BOOL, true, $"Should player's be broadcasted to when the roundstarts."));
			AddConfig(new Smod2.Config.ConfigSetting("ne_dclassinfammo", false, Smod2.Config.SettingType.BOOL, true, $"Should D-Class be given infinite ammo?"));

			AddConfig(new Smod2.Config.ConfigSetting("ne_spawnqueue", "40444", Smod2.Config.SettingType.STRING, true, $"What role spawns in what order. This repeats so if you had a server limit of 22 there would be 4 scps and 18 dclass."));
			AddConfig(new Smod2.Config.ConfigSetting("ne_broadcastmessage", "You have NUKETIME seconds till the nuke starts! GET OUT OF HERE! FF is on after SPAWNPROTECT seconds and watch out for SCPs!", Smod2.Config.SettingType.STRING, true, $"The broadcasted message"));

			AddConfig(new Smod2.Config.ConfigSetting("ne_nuketime", 90, Smod2.Config.SettingType.NUMERIC, true, $"Forces the nuke to be on at this time. It takes 100 seconds for nuke to explode (10 seconds for announcement and 90 for the alarm phase) so at 190 seconds in the round for the default the nuke will explode"));
			AddConfig(new Smod2.Config.ConfigSetting("ne_latespawn", 15, Smod2.Config.SettingType.NUMERIC, true, $"How long into the round people who join late will be spawned as D-Class."));
			AddConfig(new Smod2.Config.ConfigSetting("ne_spawnprotect", 10, Smod2.Config.SettingType.NUMERIC, true, $"Time till d-class can murder eachother."));

			AddConfig(new Smod2.Config.ConfigSetting("ne_dclassitems", new int[] { 11,13 }, Smod2.Config.SettingType.NUMERIC_LIST, true, $"What items D-Class get when they're spawned in."));
			AddConfig(new Smod2.Config.ConfigSetting("ne_dclassammo", new int[] { 0, 0, 36 }, Smod2.Config.SettingType.NUMERIC_LIST, true, $"Sets ammo of D-Class on spawn. 1st number = 5.56mm, 2nd number = 7.62mm, 3rd number = 9mm"));
		}
	}
}