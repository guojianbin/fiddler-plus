using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using ButtonBarsControl.Design.Attributes;

namespace ButtonBarsControl.Design.Editors
{
    internal class ImageListIndexEditor : UITypeEditor
    {
        protected ImageList currentImageList;
        protected PropertyDescriptor currentImageListProp;
        protected object currentInstance;

        protected UITypeEditor imageEditor =
            ((UITypeEditor) TypeDescriptor.GetEditor(typeof (Image), typeof (UITypeEditor)));

        private ImageIndexUI imageUI;

        internal UITypeEditor ImageEditor
        {
            get { return imageEditor; }
        }

        /// <summary>
        /// Gets the editor style used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)"/> method.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Drawing.Design.UITypeEditorEditStyle"/> value that indicates the style of editor used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)"/> method. If the <see cref="T:System.Drawing.Design.UITypeEditor"/> does not support this method, then <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle"/> will return <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.None"/>.
        /// </returns>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that can be used to gain additional context information. </param>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        /// <summary>
        /// Edits the specified object's value using the editor style indicated by the <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle"/> method.
        /// </summary>
        /// <returns>
        /// The new value of the object. If the value of the object has not changed, this should return the same object it was passed.
        /// </returns>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that can be used to gain additional context information. </param><param name="provider">An <see cref="T:System.IServiceProvider"/> that this editor can use to obtain services. </param><param name="value">The object to edit. </param>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null)
            {
                var edSvc = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
                if (edSvc == null)
                {
                    return value;
                }
                if (imageUI == null)
                {
                    imageUI = new ImageIndexUI();
                }
                InitializeImageList(context);
                imageUI.Start(edSvc, value, currentImageList);
                edSvc.DropDownControl(imageUI);
                value = imageUI.Value;
                imageUI.End();
            }
            return value;
        }

        private void InitializeImageList(ITypeDescriptorContext context)
        {
            var instance = context.Instance;
            var imageListProperty = GetImageListProperty(context.PropertyDescriptor, ref instance);
            while ((instance != null) && (imageListProperty == null))
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(instance);
                for (int i = 0; i < properties.Count; i++)
                {
                    if (typeof (ImageList).IsAssignableFrom(properties[i].PropertyType))
                    {
                        imageListProperty = properties[i];
                        break;
                    }
                }
            }
            if (imageListProperty != null)
            {
                currentImageList = (ImageList) imageListProperty.GetValue(instance);
                currentImageListProp = imageListProperty;
                currentInstance = instance;
            }
            else
            {
                currentImageList = null;
                currentImageListProp = imageListProperty;
                currentInstance = instance;
            }
        }

        protected virtual Image GetImage(ITypeDescriptorContext context, int index, string key, bool useIntIndex)
        {
            Image image = null;
            var instance = context.Instance;
            if (!(instance is object[]))
            {
                if ((index < 0) && (key == null))
                {
                    return image;
                }
                InitializeImageList(context);
                if (currentImageList != null)
                {
                    if (useIntIndex)
                    {
                        if ((currentImageList != null) && (index < currentImageList.Images.Count))
                        {
                            index = (index > 0) ? index : 0;
                            image = currentImageList.Images[index];
                        }
                        return image;
                    }
                    return currentImageList.Images[key];
                }
            }
            return null;
        }

        /// <summary>
        /// Indicates whether the specified context supports painting a representation of an object's value within the specified context.
        /// </summary>
        /// <returns>
        /// true if <see cref="M:System.Drawing.Design.UITypeEditor.PaintValue(System.Object,System.Drawing.Graphics,System.Drawing.Rectangle)"/> is implemented; otherwise, false.
        /// </returns>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that can be used to gain additional context information. </param>
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return ((imageEditor != null) && imageEditor.GetPaintValueSupported(context));
        }

        /// <summary>
        /// Paints a representation of the value of an object using the specified <see cref="T:System.Drawing.Design.PaintValueEventArgs"/>.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Drawing.Design.PaintValueEventArgs"/> that indicates what to paint and where to paint it. </param>
        public override void PaintValue(PaintValueEventArgs e)
        {
            if (ImageEditor == null) return;
            Image image = null;
            if (e.Value is int)
            {
                image = GetImage(e.Context, (int) e.Value, null, true);
            }
            else if (e.Value is string)
            {
                image = GetImage(e.Context, -1, (string) e.Value, false);
            }
            if (image != null)
            {
                ImageEditor.PaintValue(new PaintValueEventArgs(e.Context, image, e.Graphics, e.Bounds));
            }
        }

        internal static PropertyDescriptor GetImageListProperty(PropertyDescriptor currentComponent, ref object instance)
        {
            if (instance is object[])
            {
                return null;
            }
            const PropertyDescriptor descriptor = null;
            var component = instance;
            var attribute = currentComponent.Attributes[typeof (ImagePropertyAttribute)] as ImagePropertyAttribute;
            if (attribute != null)
            {
                var strArray = attribute.PropertyName.Split(new[] {'.'});
                for (var i = 0; i < strArray.Length; i++)
                {
                    if (component == null)
                    {
                        return descriptor;
                    }
                    var descriptor2 = TypeDescriptor.GetProperties(component)[strArray[i]];
                    if (descriptor2 == null)
                    {
                        return descriptor;
                    }
                    if (i == (strArray.Length - 1))
                    {
                        if (typeof (ImageList).IsAssignableFrom(descriptor2.PropertyType))
                        {
                            instance = component;
                            return descriptor2;
                        }
                    }
                    else
                    {
                        component = descriptor2.GetValue(component);
                    }
                }
            }
            return descriptor;
        }

        #region Nested type: ImageIndexUI

        private class ImageIndexUI : ListBox
        {
            private IWindowsFormsEditorService edSvc;
            private int value = -1;

            public ImageIndexUI()
            {
                // ReSharper disable DoNotCallOverridableMethodsInConstructor
                ItemHeight = 20;
                Height = 20*5;
                DrawMode = DrawMode.OwnerDrawFixed;
                Dock = DockStyle.Fill;
                // ReSharper restore DoNotCallOverridableMethodsInConstructor
                BorderStyle = BorderStyle.None;
            }

            public int Value
            {
                get { return value; }
            }

            public void End()
            {
                edSvc = null;
                value = -1;
            }

            /// <summary>
            /// Raises the <see cref="E:System.Windows.Forms.Control.Click"/> event.
            /// </summary>
            /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
            protected override void OnClick(EventArgs e)
            {
                base.OnClick(e);
                if (SelectedIndex >= 0)
                    value = SelectedIndex - 1;
                edSvc.CloseDropDown();
            }

            /// <summary>
            /// Raises the <see cref="E:System.Windows.Forms.ListBox.DrawItem"/> event.
            /// </summary>
            /// <param name="die">A <see cref="T:System.Windows.Forms.DrawItemEventArgs"/> that contains the event data. </param>
            protected override void OnDrawItem(DrawItemEventArgs die)
            {
                base.OnDrawItem(die);
                if (die.Index == -1) return;
                var image = Items[die.Index] as Bitmap;
                var s = (die.Index - 1).ToString();
                var font = die.Font;
                Brush brush = new SolidBrush(die.ForeColor);
                die.DrawBackground();
                if (image != null)
                {
                    die.Graphics.DrawRectangle(SystemPens.WindowText, new Rectangle(die.Bounds.X, die.Bounds.Y, 18, 18));
                    die.Graphics.DrawImage(image, new Rectangle(die.Bounds.X + 2, die.Bounds.Y + 2, 16, 16));
                    die.Graphics.DrawString(s, font, brush, die.Bounds.X + 20,
                                            die.Bounds.Y + ((die.Bounds.Height - font.Height)/2));
                }
                else
                {
                    die.Graphics.DrawString("(none)", font, brush, die.Bounds.X + 20,
                                            die.Bounds.Y + ((die.Bounds.Height - font.Height)/2));
                }
                brush.Dispose();
            }

            /// <summary>
            /// Processes a dialog key.
            /// </summary>
            /// <returns>
            /// true if the key was processed by the control; otherwise, false.
            /// </returns>
            /// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys"/> values that represents the key to process. </param>
            protected override bool ProcessDialogKey(Keys keyData)
            {
                if (((keyData & Keys.KeyCode) == Keys.Return) && ((keyData & (Keys.Alt | Keys.Control)) == Keys.None))
                {
                    OnClick(EventArgs.Empty);
                    return true;
                }
                return base.ProcessDialogKey(keyData);
            }

            internal void Start(IWindowsFormsEditorService service, object objectValue, ImageList list)
            {
                edSvc = service;
                value = (int) objectValue;
                Items.Clear();
                Items.Add("(none");
                if (list != null)
                {
                    for (var i = 0; i < list.Images.Count; i++)
                    {
                        Items.Add(list.Images[i]);
                    }
                }

                for (var i = 0; i < Items.Count; i++)
                {
                    if (Items[i] != objectValue) continue;
                    SelectedIndex = i + 1;
                    return;
                }
            }
        }

        #endregion
    }
}