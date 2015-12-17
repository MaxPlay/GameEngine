using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Attributes;
using GameEngine.Components.Rendering;
using GameEngine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Extra;

namespace GameEngine.Components.UI
{
    [RequireComponent(typeof(UIRenderer))]
    public abstract class UIBase : Component
    {
        public string Text;

        protected UIRenderer renderer;
        protected Rectangle AABB;
        protected bool hover;

        public bool Hover { get { return this.hover; } }

        public event UIEventHandler MouseIn;
        public event UIEventHandler MouseOut;
        public event UIEventHandler Clicked;
        public event UIEventHandler DoubleClicked;

        public delegate void UIEventHandler(EngineObject obj, EventArgs e);

        public UIBase(GameObject gameObject)
            : base(gameObject)
        {

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

        public abstract override void Reset();

        protected void OnDoubleClicked()
        {
            if (DoubleClicked != null)
                DoubleClicked(this, new EventArgs());
        }

        protected void OnClicked()
        {
            if (Clicked != null)
                Clicked(this, new EventArgs());
        }

        private void MouseInput_DoubleClick(MouseEventArgs e)
        {
            if (hover)
                OnDoubleClicked();
        }

        private void MouseInput_Left(MouseEventArgs e)
        {
            if (hover)
                OnClicked();
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
            if (renderer != null)
            {
                if (renderer.State == UIState.none)
                {
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        renderer.State = UIState.click;
                    }
                    else
                    {
                        renderer.State = UIState.hover;
                    }
                }
            }

            if (MouseIn != null)
                MouseIn(this, new EventArgs());
        }

        protected void OnMouseOut()
        {
            if (renderer != null)
            {
                renderer.State = UIState.none;
            }

            if (MouseOut != null)
                MouseOut(this, new EventArgs());
        }

    }
}
