# Nuke Escape
Escape however you want, work together with your fellow prisoners, or be unlucky enough to have a psycho for a cellmate who cares for nothing else but watching the world burn.
Don't slow down, you don't know when the nuke will go off, so GET OUT!

## Install Instructions.
Put Nuke Escape.dll under the release tab into sm_plugins folder.


## Config Options.
| Config Option              | Value Type      | Default Value | Description |
|   :---:                    |     :---:       |    :---:      |    :---:    |
| ne_disable                 | Boolean         | False             | Disables or enables the entire plugin. |
| ne_defaulttoggle           | Boolean         | False             | Should this gamemode be toggled on or off by default. |
| ne_broadcast               | Boolean         | True              | Should player's be broadcasted to when the roundstarts. |
| ne_broadcastmessage        | String          | [See this page ](https://github.com/MrMith/Nuke-Escape/wiki/ne_broadcastmessage)| Information about the plugin that is shown in a mapwide broadcast. |
| ne_spawnqueue              | String          | 40444             | Decides the spawn queue. This is multipled so if you had a server size of 22 there would be 4 scps and 18 dclass. |
| ne_nukemessage             | String          | 079 has forced to nuke to be on! You CANNOT turn it off! RUN!  | Message that is broadcasted when nuke starts.|
| ne_welcomemessage          | String          | Welcome to Nuke-Escape!  | Message that is broadcasted before the round starts.|
| ne_spawnprotect            | Integer         | 10                | How long till D-Class can murder eachother. |
| ne_latespawn               | Integer         | 15                | How long into the round people who join late will be spawned as D-Class. |
| ne_dclassitems             | Int List        | 11,13             | What items D-Class spawn with. |
| ne_dclassammo              | Int List        | 0,0,36            | Sets ammo of D-Class on spawn. 1st number = 5.56mm, 2nd number = 7.62mm, 3rd number = 9mm |
| ne_nuketime                | Integer List    | 0,3,5,9,16,17     | Forces the nuke to be on at this time, if more then one number is present it will randomly select one. It takes 100 seconds for nuke to explode (10 seconds for announcement and 90 for the alarm phase) so at 190 seconds in the round for the default the nuke will explode |
| ne_scpslatespawn           | Integer List    | 90                | SCP's to be randomly chosen from during the ne_latespawn time if they're chosen to be an SCP. |

## Commands

| Command(s)                 | Value Type      | Description                              |
|   :---:                    |     :---:       |    :---:                                 |
| ne_version                 | N/A             | Get the version of this plugin           |
| ne_disable                 | N/A             | Disables the entire plugin.              |
| ne                         | String          | ne on turns the plugin on, ne off turns it off and ne status shows current status.|
| ne_forcestop               | N/A             | Forces the plugin to stop. Round will NOT end and everyone will be D-Class/SCP. |
