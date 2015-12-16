using GameEngine.Attributes;
using GameEngine.Components.Rendering;
using GameEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Extra;
using Microsoft.Xna.Framework;

namespace GameEngine.Components.UI
{
    [RequireComponent(typeof(UIRenderer))]
    class Button : Component
    {
        protected UIRenderer renderer;
        protected Rectangle AABB;
        protected bool hover;

        public bool Hover { get { return this.hover; } }

        public event ButtonEventHandler MouseIn;
        public event ButtonEventHandler MouseOut;
        public event ButtonEventHandler Clicked;
        public event ButtonEventHandler DoubleClicked;

        public delegate void ButtonEventHandler(EngineObject obj, EventArgs e);

        public Button()
            : base(null)
        {
            Initialize();
        }

        public Button(GameObject gameObject)
            : base(gameObject)
        {
            Initialize();
        }

        public override void Initialize()
        {
            renderer = GetComponent<UIRenderer>();
            MouseInput.Moved += MouseInput_Moved;
            MouseInput.Left += MouseInput_Left;
            MouseInput.DoubleClick += MouseInput_DoubleClick;
            if (renderer.BaseImage != null)
                AABB = renderer.BaseImage.Dimension;
            base.Initialize();
        }

        private void MouseInput_DoubleClick(MouseEventArgs e)
        {
            if (hover)
                OnDoubleClicked();
        }

        protected void OnDoubleClicked()
        {
            if (DoubleClicked != null)
                DoubleClicked(this, new EventArgs());
        }

        private void MouseInput_Left(MouseEventArgs e)
        {
            if (hover)
                OnClicked();
        }

        protected void OnClicked()
        {
            if (Clicked != null)
                Clicked(this, new EventArgs());
        }

        private void MouseInput_Moved(MouseEventArgs e)
        {
            if (AABB.Contains(MouseInput.MousePosition) != this.hover)
                if (this.hover)
                {
                    this.hover = false;
                    OnMouseOut();
                }
                else
                {
                    this.hover = true;
                    OnMouseIn();
                }
        }

        protected void OnMouseIn()
        {
            if (MouseIn != null)
                MouseIn(this, new EventArgs());
        }

        protected void OnMouseOut()
        {
            if (MouseOut != null)
                MouseOut(this, new EventArgs());
        }

        public override void Reset()
        {
            if (hover)
                OnMouseOut();
            hover = false;
        }
    }
}
