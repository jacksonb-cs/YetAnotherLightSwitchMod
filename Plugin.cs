using BepInEx;
using HarmonyLib;

namespace YetAnotherLightSwitchMod
{
	[BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
	public class Plugin : BaseUnityPlugin
	{
		public const string PLUGIN_GUID = "com.jacksonb-cs.YetAnotherLightSwitchMod";
		public const string PLUGIN_NAME = "YetAnotherLightSwitchMod";
		public const string PLUGIN_VERSION = "1.0.0";

		private void Awake()
		{
			var harmony = new Harmony(PLUGIN_GUID);
			harmony.PatchAll();

			// Plugin startup logic
			Logger.LogInfo($"Plugin {PLUGIN_NAME} is loaded!");
		}
	}

	// After anything shuts the lights off, this immediately turns them back on.
	[HarmonyPatch(typeof(ShipLights))]
	[HarmonyPatch(nameof(ShipLights.SetShipLightsClientRpc))]
	public class ShipLightsTogglePatch
	{
		[HarmonyPostfix]
		public static void SetShipLightsClientRpcPostfix(ShipLights __instance)
		{
			__instance.areLightsOn = true;
			__instance.shipLightsAnimator.SetBool("lightsOn", true);
		}
	}
}
