using BepInEx;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;

namespace VoidlingEscape
{
  [BepInPlugin("com.Nuxlar.VoidlingEscape", "VoidlingEscape", "1.0.0")]

  public class VoidlingEscape : BaseUnityPlugin
  {
    SpawnCard voidRaidCrabPhase2 = Addressables.LoadAssetAsync<SpawnCard>("RoR2/DLC1/VoidRaidCrab/cscMiniVoidRaidCrabPhase2.asset").WaitForCompletion();
    public void Awake()
    {
      On.EntityStates.Missions.BrotherEncounter.BossDeath.OnEnter += BossDeath_OnEnter;
    }
    private void BossDeath_OnEnter(On.EntityStates.Missions.BrotherEncounter.BossDeath.orig_OnEnter orig, EntityStates.Missions.BrotherEncounter.BossDeath self)
    {
      DirectorPlacementRule placementRule = new DirectorPlacementRule();
      placementRule.placementMode = DirectorPlacementRule.PlacementMode.Direct;
      DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(voidRaidCrabPhase2, placementRule, Run.instance.runRNG);
      directorSpawnRequest.teamIndexOverride = new TeamIndex?(TeamIndex.Monster);
      GameObject spawnedVoidRaidCrabPhase2 = voidRaidCrabPhase2.DoSpawn(new Vector3(386.5f, -174.8f, 462.8f), Quaternion.identity, directorSpawnRequest).spawnedInstance;
      NetworkServer.Spawn(spawnedVoidRaidCrabPhase2);
      orig(self);
    }
  }
}