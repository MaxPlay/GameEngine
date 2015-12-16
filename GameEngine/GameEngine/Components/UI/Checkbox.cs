using GameEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Components.UI
{
    public enum CheckBoxState
    {
        Unchecked,
        Checked
    }

    public class Checkbox : Button
    {
        public CheckBoxState State;

        public Checkbox()
            : base()
        {
            this.State = CheckBoxState.Unchecked;
        }

        public Checkbox(GameObject gameObject)
            : base(gameObject)
        {
            this.State = CheckBoxState.Unchecked;
        }

        protected override void OnClicked()
        {
            this.State = (this.State == CheckBoxState.Unchecked) ? CheckBoxState.Checked : CheckBoxState.Unchecked;

            base.OnClicked();
        }
    }
}
