# arena-battler
This is the source code for Protect the Princess, an arcade-style game where basic attacking functions are split into two characters: attacking on the right, and defending on the left.

The game is available on Unity Play (https://play.unity.com/mg/other/protect-the-princess), where it placed first in the February Showcase.


The princess holds all health-based functions. Her healthbar controls the state of the game; when she dies, the game is over. She is controlled with the WASD keys.

The knight holds all attack-based functions. He moves towards the mouse-position, attacks with the left mouse button, and can sprint with the mouse-wheel. However, actions cost stamina, and the knight will be forced to walk without attacking if the stamina bar is depleted, leaving the princess open to attack.

There are three enemy varieties in the game, each with different health and movement speed values.

