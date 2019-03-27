# Nuke Escape
Escape however you want, work together with your fellow prisoners, or be unlucky enough to have a psycho for a cellmate who cares for nothing else but watching the world burn.
Don't slow down, you don't know when the nuke will go off, so GET OUT!

## Install Instructions.
Put Nuke Escape.dll under the release tab into sm_plugins folder.


## Config Options.
| Config Option              | Value Type      | Default Value | Description |
|   :---:                    |     :---:       |    :---:      |    :---:    |
| ne_disable                 | Boolean         | False             | Disables or enables the entire plugin. |
| ne_defaulttoggle           | Boolean         | False             | Set toggle to this value |
| ne_broadcast               | Boolean         | True              | When round starts should the player be given information aboout the plugin. |
| ne_broadcastmessage        | String          | [See this page ]()| Information about the plugin that is shown in a mapwide broadcast. |
| ne_nuketime                | Integer         | 30                | Time till the nuke turns on. Takes 100 seconds for nuke to explode (10 second announcement and 90 seconnd countdown) |
| ne_spawnprotect            | Integer         | 10                | How long till D-Class can murder eachother. |
| ne_dclassitems             | Int List        | 11,13             | What items D-Class spawn with. |
| ne_dclassammo              | Int List        | 0,0,36            | Sets ammo on spawn. 1st number = 5.56mm, 2nd number = 7.62mm, 3rd number = 9mm|
| ne_spawnqueue              | String          | 40444             | Decides the spawn queue. This is multipled so if you had a server size of 22 there would be 4 scps and 18 dclass. |

## Commands

| Command(s)                 | Value Type      | Description                              |
|   :---:                    |     :---:       |    :---:                                 |
| ne_version                 | N/A             | Get the version of this plugin           |
| ne_disable                 | N/A             | Disables the entire plugin.              |
| ne_status                  | N/A             | Gets the status of this plugin.          |
| ne_toggle                  | N/A             | Toggles if its going to be active next round.  |
| ne_forcestop               | N/A             | Forces the plugin to stop. Round will NOT end and everyone will be D-Class/SCP. |