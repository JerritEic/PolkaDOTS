using Unity.Entities;
using UnityEngine;

namespace PolkaDOTS.Debugging
{
    /// <summary>
    ///  Captures screenshots at a configurable interval. Useful for debugging headless deployments.
    /// </summary>
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    [WorldSystemFilter(WorldSystemFilterFlags.Presentation)]
    public partial struct TakeScreenshotSystem : ISystem
    {
        private int number;
        private double lastUpdate;
        public void OnCreate(ref SystemState state)
        {
            number = 0;
            lastUpdate = -1.0;
            if (!ApplicationConfig.TakeScreenshots)
            {
                state.Enabled = false;
            }
        }

        public void OnUpdate(ref SystemState state)
        {
            if (state.World.Time.ElapsedTime - lastUpdate < ApplicationConfig.TakeScreenshotsInterval)
            {
                return;
            }
            lastUpdate = state.World.Time.ElapsedTime;
            
            ScreenCapture.CaptureScreenshot($"{ApplicationConfig.ScreenshotFolder}/screenshot_{number}.png");
            number++;
        }
        
    }
}