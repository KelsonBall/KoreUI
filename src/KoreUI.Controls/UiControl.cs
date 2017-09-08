using System;
using System.Collections.Generic;
using System.Collections;
using OpenTK.Graphics;
using Processing.OpenTk.Core.Math;
using Processing.OpenTk.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KoreUI.Controls
{
    public class UiControl : IEnumerable<UiControl>
    {
        #region Backing Dictionary

        private readonly Dictionary<string, object> _attributes = new Dictionary<string, object>();
        protected static readonly Dictionary<Type, Dictionary<string, Action<object, object>>> _setters = new Dictionary<Type, Dictionary<string, Action<object, object>>>();

        public IEnumerable<KeyValuePair<string, object>> Attributes => _attributes;

        public object this[string attribute]
        {
            get
            {
                if (_attributes.ContainsKey(attribute))                
                    return _attributes[attribute];
                return null;
            }
            set
            {
                _attributes[attribute] = value;
            }
        }

        protected void Set<T>(T value, [CallerMemberName] string name = null)
        {
            if (name != null)
                _attributes[name] = value;
            var type = GetType();
            if (_setters.ContainsKey(type))
                if (_setters[type].ContainsKey(name))
                    _setters[type][name](this, value);
        }
        
        protected static void RegisterSetter<TControl, TValue>(string name, Action<TControl, TValue> setter)
        {
            var type = typeof(TControl);
            if (!_setters.ContainsKey(type))
                _setters[type] = new Dictionary<string, Action<object, object>>();
            _setters[type][name] = (o,v) => setter((TControl)o, (TValue)v);            
        }

        protected T Get<T>([CallerMemberName] string name = null) => (T)this[name];

        #endregion

        #region Ctor
        public UiControl() : this(c => { }) { }

        public UiControl(Action<UiControl> setup)
        {
            DefaultStyle();
            setup(this);
        }

        protected virtual void DefaultStyle()
        {
            Name = GetType().Name;
            Background = Color4.White;
        }
        #endregion

        #region Properties

        public INotifyPropertyChanged DataContext { get; set; }

        public string Name { get => Get<string>(); set => Set(value); }

        public Color4 Background { get => Get<Color4>(); set => Set(value); }


        #endregion

        #region Events

        public bool IsMouseOver { get; internal set; }

        public bool Encloses(PVector point) => ActualX < point.X && (ActualX + ActualWidth) > point.X && ActualY < point.Y && (ActualY + ActualHeight) > point.Y;

        public Action<UiControl, InputEventArgs> MouseMoved;
        protected virtual void BeforeMouseMoved(InputEventArgs args) { Redraw = true; }

        public Action<UiControl, InputEventArgs> MouseEntered;
        protected virtual void BeforeMouseEntered(InputEventArgs args) { Redraw = true; }

        public Action<UiControl, InputEventArgs> MouseLeft;
        protected virtual void BeforeMouseLeft(InputEventArgs args) { Redraw = true; }

        public Action<UiControl, InputEventArgs> MousePressed;
        protected virtual void BeforeMousePressed(InputEventArgs args) { Redraw = true; }

        public Action<UiControl, InputEventArgs> KeyPressed;
        protected virtual void BeforeKeyPressed(InputEventArgs args) { Redraw = true; }

        public Action<UiControl, InputEventArgs> KeyReleased;
        protected virtual void BeforeReleased(InputEventArgs args) { Redraw = true; }

        public Action<UiControl, InputEventArgs> Focused;
        protected virtual void BeforeFocused(InputEventArgs args) { Redraw = true; }


        #endregion

        #region Position

        public (int offset, double scale) Width
        {
            set
            {
                Coordinates.SizeOffsetX = value.offset;
                Coordinates.SizeScaleX = value.scale;
            }
        }

        public (int offset, double scale) Height
        {
            set
            {
                Coordinates.SizeOffsetY = value.offset;
                Coordinates.SizeScaleY = value.scale;
            }
        }

        public ((int offset, double scale) width, (int offset, double scale) height) Size
        {
            set
            {
                Width = value.width;
                Height = value.height;
            }
        }

        public (int offset, double scale) Left
        {
            set
            {
                Coordinates.PositionOffsetX = value.offset;
                Coordinates.PositionScaleX = value.scale;
            }
        }

        public (int offset, double scale) Top
        {
            set
            {
                Coordinates.PositionOffsetY = value.offset;
                Coordinates.PositionScaleY = value.scale;
            }
        }

        public ((int offset, double scale) left, (int offset, double scale) top) Position
        {
            set
            {
                Left = value.left;
                Top = value.top;
            }
        }


        public UiCoordinate Coordinates { get; private set; } = new UiCoordinate();

        public double ActualWidth
        {
            get
            {
                if (Parent != null)
                    return (Parent.ActualWidth * Coordinates.SizeScaleX) + Coordinates.SizeOffsetX;
                return Coordinates.SizeOffsetX;
            }
        }

        public double ActualHeight
        {
            get
            {
                if (Parent != null)
                    return (Parent.ActualHeight * Coordinates.SizeScaleY) + Coordinates.SizeOffsetY;
                return Coordinates.SizeOffsetY;
            }
        }

        public double LocalX
        {
            get
            {
                if (Parent != null)
                    return (Parent.ActualWidth * Coordinates.PositionScaleX) + Coordinates.PositionOffsetX;
                return 0;
            }
        }

        public double LocalY
        {
            get
            {
                if (Parent != null)
                    return (Parent.ActualHeight * Coordinates.PositionScaleY) + Coordinates.PositionOffsetY;
                return 0;
            }
        }

        public double ActualX
        {
            get
            {
                if (Parent != null)
                    return Parent.ActualX + (Parent.ActualWidth * Coordinates.PositionScaleX) + Coordinates.PositionOffsetX;
                return 0;
            }
        }

        public double ActualY
        {
            get
            {
                if (Parent != null)
                    return Parent.ActualY + (Parent.ActualHeight * Coordinates.PositionScaleY) + Coordinates.PositionOffsetY;
                return 0;
            }
        }

        #endregion

        #region Drawing        

        internal bool Redraw { get; set; } = true;

        public virtual void Draw(Canvas canvas)
        {
            canvas.WithStyle(() => {
                canvas.Fill = Background;
                canvas.Rectangle((0, 0), (ActualWidth, ActualHeight));
            });
            DrawChildren(canvas);
        }

        public void SetToRedraw()
        {
            Redraw = true;
            foreach (var descendent in Descendents)
                descendent.Redraw = true;
        }

        #endregion

        #region Hierarchy

        public IEnumerator<UiControl> GetEnumerator() => Children.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Children.GetEnumerator();

        public Application Root
        {
            get
            {
                UiControl parent = this;
                while ((parent = parent.Parent) != null);
                return (Application)parent;
            }
        }

        public IEnumerable<UiControl> Children
        {
            get
            {
                foreach (var child in _children)
                    yield return child;
            }
        }

        public IEnumerable<UiControl> Descendents
        {
            get
            {
                foreach (var child in Children)
                {
                    yield return child;
                    foreach (var descendent in child.Descendents)
                        yield return descendent;
                }
            }
        }

        public IEnumerable<UiControl> Ancestors
        {
            get
            {
                var ancestor = this;
                while ((ancestor = ancestor.Parent) != null)
                    yield return ancestor;
            }
        }

        public IEnumerable<UiControl> Siblings
        {
            get
            {
                foreach (var sibling in Parent)
                    if (sibling != this)
                        yield return sibling;
            }
        }

        public UiControl Parent { get; private set; }

        protected void DrawChildren(Canvas canvas)
        {
            foreach (var child in this)
                canvas.WithBoundry((child.LocalX, child.LocalY), (child.ActualWidth, child.ActualHeight), () => child.Draw(canvas));
        }

        private readonly List<UiControl> _children = new List<UiControl>();

        public virtual UiControl Add(UiControl control)
        {
            if (control.Parent != null)
                control.Parent.Remove(control);
            control.Parent = this;
            _children.Add(control);
            Layout(control);
            return this;
        }

        public virtual UiControl Add(IEnumerable<UiControl> controls)
        {
            foreach (var control in controls)
                Add(control);
            return this;
        }

        public virtual UiControl Remove(UiControl control)
        {
            control.Parent = null;
            _children.Remove(control);
            return this;
        }

        public virtual UiControl Remove(IEnumerable<UiControl> controls)
        {
            foreach (var control in controls)
                Remove(control);
            return this;
        }

        public virtual void Layout(UiControl control)
        {

        }

        #endregion


    }
}
