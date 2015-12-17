using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Core;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Extra;

namespace GameEngine.Components.UI
{
    public class Textbox : UIBase
    {
        private StringBuilder inputText;
        private int cursor;

        public string Text { get { return this.inputText.ToString(); } }

        public Textbox()
            : base(null)
        {

        }

        public Textbox(GameObject gameObject)
            : base(gameObject)
        {

        }

        public override void Reset()
        {
            cursor = 0;
            inputText.Clear();
        }

        public override void Initialize()
        {
            inputText = new StringBuilder();
            KeyInput.D0 += QueryInput;
            KeyInput.D1 += QueryInput;
            KeyInput.D2 += QueryInput;
            KeyInput.D3 += QueryInput;
            KeyInput.D4 += QueryInput;
            KeyInput.D5 += QueryInput;
            KeyInput.D6 += QueryInput;
            KeyInput.D7 += QueryInput;
            KeyInput.D8 += QueryInput;
            KeyInput.D9 += QueryInput;
            KeyInput.A += QueryInput;
            KeyInput.B += QueryInput;
            KeyInput.C += QueryInput;
            KeyInput.D += QueryInput;
            KeyInput.E += QueryInput;
            KeyInput.F += QueryInput;
            KeyInput.G += QueryInput;
            KeyInput.H += QueryInput;
            KeyInput.I += QueryInput;
            KeyInput.J += QueryInput;
            KeyInput.K += QueryInput;
            KeyInput.L += QueryInput;
            KeyInput.M += QueryInput;
            KeyInput.N += QueryInput;
            KeyInput.O += QueryInput;
            KeyInput.P += QueryInput;
            KeyInput.Q += QueryInput;
            KeyInput.R += QueryInput;
            KeyInput.S += QueryInput;
            KeyInput.T += QueryInput;
            KeyInput.U += QueryInput;
            KeyInput.V += QueryInput;
            KeyInput.W += QueryInput;
            KeyInput.X += QueryInput;
            KeyInput.Y += QueryInput;
            KeyInput.Z += QueryInput;
            KeyInput.Space += QueryInput;
            KeyInput.Delete += QueryInput;
            KeyInput.Backspace += QueryInput;
            cursor = 0;
            base.Initialize();
        }

        private void QueryInput(KeyEventArgs e, char keyvalue)
        {
            KeyboardState state = Keyboard.GetState();
            Keys[] keys = state.GetPressedKeys();

            if (keys.Length > 0)
                switch (keys[0])
                {
                    case Keys.Delete:
                        if (inputText.Length > 0 && cursor != inputText.Length)
                        {
                            inputText.Remove(cursor, 1);
                        }
                        return;

                    case Keys.Back:
                        if (inputText.Length > 0)
                        {
                            inputText.Remove(cursor - 1, 1);
                            cursor--;
                        }
                        return;

                    case Keys.Space:
                        inputText.Append(" ");
                        cursor++;
                        return;

                    case Keys.Left:
                        if (cursor > 0)
                            cursor--;
                        return;
                    case Keys.Right:
                        if (cursor < inputText.Length)
                            cursor++;
                        return;
                }
            if ((int)keys[0] < 48 || (int)keys[0] > 90)
                return;

            if ((int)keys[0] >= 65)
            {
                string currentChar = keyvalue.ToString();
                if (e.IsShiftDown)
                    currentChar = currentChar.ToUpper();

                inputText.Append(currentChar);
            }
            else
                inputText.Append(keyvalue);
            cursor++;
        }

        public int CursorPosition { get { return this.cursor; } set { this.cursor = value; } }

    }
}
