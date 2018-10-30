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
            return asset.AssetNameEquals(@"Characters\schedules\Abigail");
        }

        public void Edit<T>(IAssetData asset)
        {
            monitor.Log("Editing schedule(s)...");
            // TODO: All characters
            asset.AsDictionary<string, string>().ReplaceWith(mod.generateSchedule("Abigail"));
        }

    }
}
