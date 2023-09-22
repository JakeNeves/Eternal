using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace Eternal.Common.Misc
{
    /// <summary>
    /// A smart HUD element with some code sorced from the Starlight River GitHub Reopsitory
    /// </summary>
    public class SmartHUDLoader : ModSystem
    {
        /// <summary>
		/// The collection of automatically craetaed UserInterfaces for SmartHUDStates.
        /// (Sourced from the Starlight River GitHub Repository)
		/// </summary>
		public static List<UserInterface> UserInterfaces = new();

        /// <summary>
        /// The collection of all automatically loaded SmartUIStates.
        /// (Sourced from the Starlight River GitHub Repository)
        /// </summary>
        public static List<SmartHUDState> HUDStates = new();

        /// <summary>
        /// Uses reflection to scan through and find all types extending SmartHUDState that arent abstract, and loads an instance of them.
        /// (Sourced from the Starlight River GitHub Repository)
        /// </summary>
        public override void Load()
        {
            if (Main.dedServ)
                return;

            UserInterfaces = new List<UserInterface>();
            HUDStates = new List<SmartHUDState>();

            foreach (Type t in Mod.Code.GetTypes())
            {
                if (!t.IsAbstract && t.IsSubclassOf(typeof(SmartHUDState)))
                {
                    var state = (SmartHUDState)Activator.CreateInstance(t, null);
                    var userInterface = new UserInterface();
                    userInterface.SetState(state);
                    state.UserInterface = userInterface;

                    HUDStates?.Add(state);
                    UserInterfaces?.Add(userInterface);
                }
            }
        }

        public override void Unload()
        {
            HUDStates.ForEach(n => n.Unload());
            UserInterfaces = null;
            HUDStates = null;
        }

        /// <summary>
        /// Helper method for creating and inserting a LegacyGameInterfaceLayer automatically
        /// (Sourced from the Starlight River GitHub Repository)
        /// </summary>
        /// <param name="layers">The vanilla layers</param>
        /// <param name="state">the UIState to bind to the layer</param>
        /// <param name="index">Where this layer should be inserted</param>
        /// <param name="visible">The logic dictating the visibility of this layer</param>
        /// <param name="scale">The scale settings this layer should scale with</param>
        public static void AddLayer(List<GameInterfaceLayer> layers, UIState state, int index, bool visible, InterfaceScaleType scale)
        {
            string name = state == null ? "Unknown" : state.ToString();
            layers.Insert(index, new LegacyGameInterfaceLayer("Eternal: " + name,
                delegate
                {
                    if (visible)
                        state.Draw(Main.spriteBatch);

                    return true;
                }, scale));
        }

        /// <summary>
		/// Gets the autoloaded SmartHUDState instance for a given SmartHUDState subclass
        /// (Sourced from the Starlight River GitHub Repository)
		/// </summary>
		/// <typeparam name="T">The SmartUIState subclass to get the instance of</typeparam>
		/// <returns>The autoloaded instance of the desired SmartUIState</returns>
		public static T GetHUDState<T>() where T : SmartHUDState
        {
            return HUDStates.FirstOrDefault(n => n is T) as T;
        }

        /// <summary>
        /// Forcibly reloads a SmartHUDState and it's associated UserInterface
        /// (Sourced from the Starlight River GitHub Repository)
        /// </summary>
        /// <typeparam name="T">The SmartHUDState subclass to reload</typeparam>
        public static void ReloadState<T>() where T : SmartHUDState
        {
            int index = HUDStates.IndexOf(GetHUDState<T>());
            HUDStates[index] = (T)Activator.CreateInstance(typeof(T), null);
            UserInterfaces[index] = new UserInterface();
            UserInterfaces[index].SetState(HUDStates[index]);
        }

        /// <summary>
        /// Handles the insertion of the automatically generated UIs
        /// (Sourced from the Starlight River GitHub Repository)
        /// </summary>
        /// <param name="layers"></param>
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            for (int k = 0; k < HUDStates.Count; k++)
            {
                SmartHUDState state = HUDStates[k];
                AddLayer(layers, state, state.InsertionIndex(layers), state.Visible, state.Scale);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            foreach (UserInterface eachState in UserInterfaces)
            {
                if (eachState?.CurrentState != null && ((SmartHUDState)eachState.CurrentState).Visible)
                    eachState.Update(gameTime);
            }
        }
    }
}
