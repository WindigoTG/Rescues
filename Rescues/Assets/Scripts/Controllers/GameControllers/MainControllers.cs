﻿namespace Rescues
{
    public sealed class MainControllers : Controllers
    {
        #region ClassLifeCycles
        
        public MainControllers(GameContext context, Services services)
        {
            Add(new LevelController(context, services));
            Add(new InitializeCharacterController(context, services));
            // Add(new InitializeEnemyController(context, services));
            Add(new TimeRemainingController());
            Add(new ItemActiveController(context, services));
            Add(new MainPuzzleController(context, services));
            Add(new HidingPlaceController(context, services));
            // Add(new EnemyVisionController(context, services));
            // Add(new EnemyMoveController(context, services));
            // Add(new StandController(context, services));
            // Add(new EnemyVisionController(context, services));
            // Add(new EnemyMoveController(context, services));
            Add(new InitializeGameMenuController(context, services));
            Add(new InputController(context, services));
            Add(new ActivatorController());
        }

        #endregion
    }
}
