using LedDashboard.Modules.BasicAnimation;
using LedDashboard.Modules.Common;
using LedDashboard.Modules.LeagueOfLegends.Model;
using SharpDX.RawInput;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LedDashboard.Modules.LeagueOfLegends.ChampionModules
{
    class VelKozModule : ChampionModule
    {
        
        // Variables

        AnimationModule animator; // Animator module that will be useful to display animations
        AbilityKey SelectedAbility = AbilityKey.None; // Currently selected ability (for example, if you pressed Q but you haven't yet clicked LMB to cast the ability, PressedKey = 'q' 
        
        // Champion-specific Variables

        bool qCastInProgress = false;
        bool rCastInProgress = false; // this is used to make the animation for Vel'Koz's R to take preference over other animations

        /// <summary>
        /// Creates a new champion instance.
        /// </summary>
        /// <param name="ledCount">Number of LEDs in the strip</param>
        /// <param name="playerInfo">Player information data</param>
        public static VelKozModule Create(int ledCount, ActivePlayer playerInfo, LightingMode preferredMode)
        {
            return new VelKozModule(ledCount, playerInfo, "Velkoz", preferredMode);
        }

        private VelKozModule(int ledCount, ActivePlayer playerInfo, string championName, LightingMode preferredMode) : base(championName, playerInfo, preferredMode)
        {
            // Initialization for the champion module occurs here.
            // Preload all the animations you'll want to use. MAKE SURE that each animation file
            // has its Build Action set to "Content" and "Copy to Output Directory" is set to "Always".

            animator = AnimationModule.Create(ledCount);
            animator.PreloadAnimation(@"Animations/Vel'Koz/q_start.txt");
            animator.PreloadAnimation(@"Animations/Vel'Koz/q_recast.txt");
            animator.PreloadAnimation(@"Animations/Vel'Koz/w_cast.txt");
            animator.PreloadAnimation(@"Animations/Vel'Koz/w_close.txt");

            ChampionInfoLoaded += OnChampionInfoLoaded;
        }

        private void OnChampionInfoLoaded(ChampionAttributes champInfo)
        {
            animator.NewFrameReady += (_, ls, mode) => DispatchNewFrame(ls,mode);
            OnMouseClicked += OnMouseClick;
            OnKeyPressed += OnKeyPress;
        }

        /// <summary>
        /// Called when the mouse is clicked.
        /// </summary>
        private void OnMouseClick(object s, MouseEventArgs m) 
        {
            // TODO: Quick cast support

            if (m.Button == MouseButtons.Right)
            {
                SelectedAbility = AbilityKey.None;
            } else if (m.Button == MouseButtons.Left)
            {
                // CODE FOR Q
                if (SelectedAbility == AbilityKey.Q && !qCastInProgress)
                {
                    // Here you should write code to trigger the appropiate animations to play when the user casts Q.
                    // The code will slightly change depending on the champion, since not all champion abilities are cast in the same "Q + Left Click" fashion,
                    // plus you might want to implement custom animation logic for the different champion abilities.

                    // Trigger the start animation.
                    Task.Run(async () =>
                    {
                        await Task.Delay(100);
                        if (!rCastInProgress) animator.RunAnimationOnce(@"Animations/Vel'Koz/q_start.txt", true);
                    });

                    // The Q cast is in progress.
                    qCastInProgress = true;

                    // After 1.15s, if user didn't press Q again already, the Q split animation plays.
                    Task.Run(async () =>
                    {
                        await Task.Delay(1150);
                        if (CanCastAbility(AbilityKey.Q) && !rCastInProgress && qCastInProgress)
                        {
                            animator.RunAnimationOnce(@"Animations/Vel'Koz/q_recast.txt");
                            // Since the ability was cast, start the cooldown timer.
                            StartCooldownTimer(AbilityKey.Q); 
                        }
                        qCastInProgress = false;
                    });

                    // Q was cast, so now there is no ability selected.
                    // Note that this doesn't get triggered after 1.15s (it doesn't wait for the above task to finish).
                    SelectedAbility = AbilityKey.None;
                }

                // CODE FOR W
                if (SelectedAbility == AbilityKey.W)
                {
                    Task.Run(async () =>
                    {
                        animator.RunAnimationOnce(@"Animations/Vel'Koz/w_cast.txt", true);
                        await Task.Delay(1800);
                        if (!rCastInProgress) animator.RunAnimationOnce(@"Animations/Vel'Koz/w_close.txt", false, 0.15f);
                    });
                    StartCooldownTimer(AbilityKey.W);
                    SelectedAbility = AbilityKey.None;
                }

                // CODE FOR E
                if (SelectedAbility == AbilityKey.E)
                {
                    Task.Run(async () =>
                    {
                        await Task.Delay(1000);
                        if (!rCastInProgress) _ = animator.ColorBurst(HSVColor.FromRGB(229, 115, 255), 0.15f);
                    });
                    StartCooldownTimer(AbilityKey.E);
                    SelectedAbility = AbilityKey.None;
                }

                // if (SelectedAbility == AbilityKey.R) { } --- Not needed for vel'koz because vel'koz ult is instant cast and doesn't need a mouse click.
            }
        }

        /// <summary>
        /// Called when a key is pressed;
        /// </summary>
        private void OnKeyPress(object s, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'q')
            {
                if (qCastInProgress)
                {
                    qCastInProgress = false;
                    if(!rCastInProgress) animator.RunAnimationOnce(@"Animations/Vel'Koz/q_recast.txt");
                    StartCooldownTimer(AbilityKey.Q);
                }
                else if (CanCastAbility(AbilityKey.Q))
                {
                    SelectedAbility = AbilityKey.Q;
                }
                   
            }
            if (e.KeyChar == 'w')
            {
                if (CanCastAbility(AbilityKey.W))
                    SelectedAbility = AbilityKey.W;
            }
            if (e.KeyChar == 'e')
            {
                if (CanCastAbility(AbilityKey.E))
                    SelectedAbility = AbilityKey.E;
            }
            if (e.KeyChar == 'r')
            {
                if (rCastInProgress)
                {
                    animator.StopCurrentAnimation();
                    rCastInProgress = false;
                    StartCooldownTimer(AbilityKey.R);
                } else
                {
                    if (CanCastAbility(AbilityKey.R))
                    {
                        animator.StopCurrentAnimation();
                        animator.RunAnimationInLoop(@"Animations/Vel'Koz/r_loop.txt", 2300, 0.15f);
                        rCastInProgress = true;
                        Task.Run(async () =>
                        {
                            await Task.Delay(2300);
                            if(rCastInProgress)
                            {
                                StartCooldownTimer(AbilityKey.R);
                                rCastInProgress = false;
                            }
                        });
                    }
                }     
            }
            if (e.KeyChar == 'f') // TODO: Refactor this into LeagueOfLegendsModule, or a new SummonerSpells module. Also take cooldown into consideration.
            {
                animator.ColorBurst(HSVColor.FromRGB(255, 237, 41), 0.1f);
            }
        }
    }
}
