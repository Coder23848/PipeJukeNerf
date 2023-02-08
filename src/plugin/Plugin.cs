using BepInEx;
using UnityEngine;

namespace PipeJukeNerf
{
    [BepInPlugin("com.coder23848.pipejukenerf", PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private void OnEnable()
        {
            // Plugin startup logic
            On.ShortcutHandler.Update += ShortcutHandler_Update;
            //On.Tracker.ctor += Tracker_ctor;
        }

        /*private void Tracker_ctor(On.Tracker.orig_ctor orig, Tracker self, ArtificialIntelligence AI, int seeAroundCorners, int maxTrackedCreatures, int framesToRememberCreatures, float ghostSpeed, int ghostPush, int ghostPushSpeed, int ghostDismissalRange)
        {
            orig(self, AI, seeAroundCorners, maxTrackedCreatures, framesToRememberCreatures, ghostSpeed, ghostPush, ghostPushSpeed, ghostDismissalRange);
            self.visualize = true;
        }*/

        private void ShortcutHandler_Update(On.ShortcutHandler.orig_Update orig, ShortcutHandler self)
        {
            orig(self);
            for (int i = 0; i < self.transportVessels.Count; i++)
            {
                for (int j = 0; j < self.transportVessels.Count; j++)
                {
                    if (i != j && self.transportVessels[i].pos.FloatDist(self.transportVessels[j].pos) <= 1) // It is, in fact, two different creatures, and the creatures are next to each other in the short cut.
                    {
                        if (!(self.transportVessels[i].creature == null || self.transportVessels[i].creature.abstractCreature == null || self.transportVessels[i].creature.abstractCreature.abstractAI == null || self.transportVessels[i].creature.abstractCreature.abstractAI.RealAI == null || self.transportVessels[i].creature.abstractCreature.abstractAI.RealAI.tracker == null || self.transportVessels[j].creature == null || self.transportVessels[j].creature.abstractCreature == null))
                        {
                            try
                            {
                                self.transportVessels[i].creature.abstractCreature.abstractAI.RealAI.tracker.SeeCreature(self.transportVessels[j].creature.abstractCreature);
                            }
                            catch (System.Exception ex)
                            {
                                Debug.Log("Possible issue with PipeJukeNerf's plugin: " + ex.Message);
                            }
                        }
                    }
                }
            }
        }
    }
}