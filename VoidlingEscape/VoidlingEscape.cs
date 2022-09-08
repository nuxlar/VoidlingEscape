using BepInEx;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;

namespace VoidlingEscape
{
  [BepInPlugin("com.Nuxlar.VoidlingEscape", "VoidlingEscape", "1.0.1")]

  public class VoidlingEscape : BaseUnityPlugin
  {
    SpawnCard voidRaidCrabPhase2 = Addressables.LoadAssetAsync<SpawnCard>("RoR2/DLC1/VoidRaidCrab/cscMiniVoidRaidCrabPhase2.asset").WaitForCompletion();
    public void Awake()
    {
      On.RoR2.HoldoutZoneController.Start += SpawnOnCharge;
    }
    private void SpawnOnCharge(On.RoR2.HoldoutZoneController.orig_Start orig, RoR2.HoldoutZoneController self)
    {
      // new Vector3(386.5f, -174.8f, 462.8f) tunnel
      // new Vector3(231.1f, -174.7f, 296) ship open
      // isboundsobjectivetoken OBJECTIVE_MOON_CHARGE_DROPSHIP
      if (self.inBoundsObjectiveToken == "OBJECTIVE_MOON_CHARGE_DROPSHIP")
      {
        DirectorPlacementRule placementRule = new DirectorPlacementRule();
        placementRule.placementMode = DirectorPlacementRule.PlacementMode.Direct;
        DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(voidRaidCrabPhase2, placementRule, Run.instance.runRNG);
        directorSpawnRequest.teamIndexOverride = new TeamIndex?(TeamIndex.Monster);
        GameObject spawnedVoidRaidCrabPhase2 = voidRaidCrabPhase2.DoSpawn(new Vector3(231.1f, -174.7f, 296), Quaternion.identity, directorSpawnRequest).spawnedInstance;
        NetworkServer.Spawn(spawnedVoidRaidCrabPhase2);
      }
      orig(self);
    }
  }
}