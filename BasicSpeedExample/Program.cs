#region

using BasicSpeedExample.ClientBase.KeyBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace BasicSpeedExample
{
    class Program
    {
        static void Main(string[] args)
        {
            MCM.openGame(); // Open game up inside of MCM class
            MCM.openWindowHost();

            new Keymap(); // only define new keymap ONCE. this just starts up the keymap threads so it can catch minecraft key inputs

            Keymap.keyEvent += McKeyPress; // Assign Mc Keymap to function so that functions called when a keys been pressed/held/let go of

            while (true) // infinite loop
            {
            }
        }

        private static void McKeyPress(object sender, KeyEvent e)
        {
            if (e.vkey == VKeyCodes.KeyHeld) // Key is currently being held
            {
                var plrYaw = Game.bodyRots.y; // yaw

                if (Keymap.GetAsyncKeyState(Keys.W))
                {
                    if (!Keymap.GetAsyncKeyState(Keys.A) && !Keymap.GetAsyncKeyState(Keys.D))
                        plrYaw += 90f;
                    else if (Keymap.GetAsyncKeyState(Keys.A))
                        plrYaw += 45f;
                    else if (Keymap.GetAsyncKeyState(Keys.D))
                        plrYaw += 135f;
                }
                else if (Keymap.GetAsyncKeyState(Keys.S))
                {
                    if (!Keymap.GetAsyncKeyState(Keys.A) && !Keymap.GetAsyncKeyState(Keys.D))
                        plrYaw -= 90f;
                    else if (Keymap.GetAsyncKeyState(Keys.A))
                        plrYaw -= 45f;
                    else if (Keymap.GetAsyncKeyState(Keys.D))
                        plrYaw -= 135f;
                }
                else if (!Keymap.GetAsyncKeyState(Keys.W) && !Keymap.GetAsyncKeyState(Keys.S))
                {
                    if (!Keymap.GetAsyncKeyState(Keys.A) && Keymap.GetAsyncKeyState(Keys.D))
                        plrYaw += 180f;
                }

                if (!(Keymap.GetAsyncKeyState(Keys.W) | Keymap.GetAsyncKeyState(Keys.A) | Keymap.GetAsyncKeyState(Keys.S) |
                      Keymap.GetAsyncKeyState(Keys.D))) return;

                var calYaw = plrYaw * ((float)Math.PI / 180f);

                Vector3 vec = Game.velocity;

                vec.x = (float)Math.Cos(calYaw) * 0.7f;
                vec.z = (float)Math.Sin(calYaw) * 0.7f;

                Game.velocity = vec;
            }
        }
    }
}
