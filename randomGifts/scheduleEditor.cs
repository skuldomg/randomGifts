using System;
using StardewModdingAPI;
using StardewValley;

namespace randomSchedules
{
    internal class scheduleEditor : IAssetEditor
    {
        ModEntry mod;
        IMonitor monitor;

        public scheduleEditor(ModEntry mod, IMonitor monitor)
        {
            this.mod = mod;
            this.monitor = monitor;
        }
        public bool CanEdit<T>(IAssetInfo asset)
        {
            return asset.AssetNameEquals(@"Characters\schedules\Abigail") || asset.AssetNameEquals(@"Characters\schedules\Alex") || asset.AssetNameEquals(@"Characters\schedules\Caroline")
                || asset.AssetNameEquals(@"Characters\schedules\Clint") || asset.AssetNameEquals(@"Characters\schedules\Demetrius") || asset.AssetNameEquals(@"Characters\schedules\Elliott")
                || asset.AssetNameEquals(@"Characters\schedules\Emily") || asset.AssetNameEquals(@"Characters\schedules\Evelyn") || asset.AssetNameEquals(@"Characters\schedules\George")
                || asset.AssetNameEquals(@"Characters\schedules\Haley") || asset.AssetNameEquals(@"Characters\schedules\Harvey") || asset.AssetNameEquals(@"Characters\schedules\Jas")
                || asset.AssetNameEquals(@"Characters\schedules\Jodi") || asset.AssetNameEquals(@"Characters\schedules\Kent") || asset.AssetNameEquals(@"Characters\schedules\Leah")
                || asset.AssetNameEquals(@"Characters\schedules\Lewis") || asset.AssetNameEquals(@"Characters\schedules\Linus") || asset.AssetNameEquals(@"Characters\schedules\Marnie")
                || asset.AssetNameEquals(@"Characters\schedules\Maru") || asset.AssetNameEquals(@"Characters\schedules\Pam") || asset.AssetNameEquals(@"Characters\schedules\Penny")
                || asset.AssetNameEquals(@"Characters\schedules\Pierre") || asset.AssetNameEquals(@"Characters\schedules\Robin") || asset.AssetNameEquals(@"Characters\schedules\Sam")
                || asset.AssetNameEquals(@"Characters\schedules\Sebastian") || asset.AssetNameEquals(@"Characters\schedules\Shane") || asset.AssetNameEquals(@"Characters\schedules\Vincent")
                || asset.AssetNameEquals(@"Characters\schedules\Willy");
        }

        public void Edit<T>(IAssetData asset)
        {            
            String[] assetArr = asset.AssetName.Split('\\');
            String npc = assetArr[2];

            monitor.Log("Editing schedule for '" + npc + "'...");

            // TODO: All characters -- check complete week for Abby first
            asset.AsDictionary<string, string>().ReplaceWith(mod.generateSchedule(npc));
        }

    }
}
