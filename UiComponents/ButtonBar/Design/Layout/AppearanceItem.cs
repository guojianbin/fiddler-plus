﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using ButtonBarsControl.Control;
using ButtonBarsControl.Design.Enums;
using ButtonBarsControl.Design.Generics;
using ButtonBarsControl.Design.Utility;

namespace ButtonBarsControl.Design.Layout
{
    /// <summary>
    /// Class responsible to store information of a <see cref="BarItem"/>
    /// </summary>
    [TypeConverter(typeof (GenericConverter<AppearanceItem>))]
    public class AppearanceItem : ICloneable, IXmlSerializable
    {
        #region Private Fields

        private readonly AppearenceText appearenceText;
        private readonly ColorPair backStyle;
        private readonly ColorPair clickStyle;
        private readonly ColorPair disabledStyle;
        private readonly ColorPair hoverStyle;
        private readonly ColorPair selectedHoverStyle;
        private readonly ColorPair selectedStyle;
        private Color disabledBorder;
        private Color disabledForeGround;
        private int gradient;
        private Color hoverBorder;
        private Color hoverForeGround;
        private Color normalBorder;
        private Color normalForeGround;
        private Color selectedBorder;
        private Color selectedForeGround;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize new instance of <see cref="AppearanceItem"/>.
        /// </summary>
        public AppearanceItem()
        {
            backStyle = new ColorPair();
            backStyle.AppearanceChanged += OnAppearanceChanged;
            clickStyle = new ColorPair();
            clickStyle.AppearanceChanged += OnAppearanceChanged;
            hoverStyle = new ColorPair();
            hoverStyle.AppearanceChanged += OnAppearanceChanged;
            selectedHoverStyle = new ColorPair();
            selectedHoverStyle.AppearanceChanged += OnAppearanceChanged;
            selectedStyle = new ColorPair();
            selectedStyle.AppearanceChanged += OnAppearanceChanged;
            disabledStyle = new ColorPair();
            disabledStyle.AppearanceChanged += OnAppearanceChanged;
            appearenceText = new AppearenceText();
            appearenceText.AppearenceChanged += OnAppearanceChanged;
            Reset();
        }

        #endregion

        #region Public Property

        /// <summary>
        /// Gets Back style of Item in <see cref="State.Normal"/> state.
        /// </summary>
        [Editor(typeof (ColorPair.ColorPairEditor), typeof (UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ColorPair BackStyle
        {
            get { return backStyle; }
        }

        /// <summary>
        /// Gets Back style of Item in <see cref="State.Pressed"/> state. 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ColorPair ClickStyle
        {
            get { return clickStyle; }
        }

        /// <summary>
        /// Gets Back style of Item in <see cref="State.Hover"/> state.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ColorPair HoverStyle
        {
            get { return hoverStyle; }
        }

        /// <summary>
        /// Gets Back style of Item in <see cref="State.Selected"/> state.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ColorPair SelectedStyle
        {
            get { return selectedStyle; }
        }

        /// <summary>
        /// Gets Back style of Item in <see cref="State.Disabled"/> state.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ColorPair DisabledStyle
        {
            get { return disabledStyle; }
        }

        /// <summary>
        /// Gets Back style of Item in <see cref="State.SelectedHover"/> state.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ColorPair SelectedHoverStyle
        {
            get { return selectedHoverStyle; }
        }

        /// <summary>
        /// Gets or Sets border color of Item in <see cref="State.Normal"/> state.
        /// </summary>
        public Color NormalBorder
        {
            get { return normalBorder; }
            set
            {
                if (normalBorder == value)
                    return;
                normalBorder = value;
                OnAppearanceChanged();
            }
        }

        /// <summary>
        /// Gets or Sets border color of Item in <see cref="State.Hover"/> state.
        /// </summary>
        public Color HoverBorder
        {
            get { return hoverBorder; }
            set
            {
                if (hoverBorder == value)
                    return;
                hoverBorder = value;
                OnAppearanceChanged();
            }
        }

        /// <summary>
        /// Gets or Sets border color of Item in <see cref="State.Selected"/> state.
        /// </summary>
        public Color SelectedBorder
        {
            get { return selectedBorder; }
            set
            {
                if (selectedBorder == value)
                    return;
                selectedBorder = value;
                OnAppearanceChanged();
            }
        }

        /// <summary>
        /// Gets or Sets text color of Item in <see cref="State.Normal"/> state.
        /// </summary>
        public Color NormalForeGround
        {
            get { return normalForeGround; }
            set
            {
                if (normalForeGround == value)
                    return;
                normalForeGround = value;
                OnAppearanceChanged();
            }
        }

        /// <summary>
        /// Gets or Sets text color of Item in <see cref="State.Hover"/> state.
        /// </summary>
        public Color HoverForeGround
        {
            get { return hoverForeGround; }
            set
            {
                if (hoverForeGround == value)
                    return;
                hoverForeGround = value;
                OnAppearanceChanged();
            }
        }

        /// <summary>
        /// Gets <see cref="AppearenceText"/> of the item.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public AppearenceText AppearenceText
        {
            get { return appearenceText; }
        }

        /// <summary>
        /// Gets or Sets text color of Item in <see cref="State.Selected"/> state.
        /// </summary>
        public Color SelectedForeGround
        {
            get { return selectedForeGround; }
            set
            {
                if (selectedForeGround == value)
                    return;
                selectedForeGround = value;
                OnAppearanceChanged();
            }
        }

        /// <summary>
        /// Gets or Sets border color of Item in <see cref="State.Disabled"/> state.
        /// </summary>
        public Color DisabledBorder
        {
            get { return disabledBorder; }
            set
            {
                if (disabledBorder == value)
                    return;
                disabledBorder = value;
                OnAppearanceChanged();
            }
        }

        /// <summary>
        /// Gets or Sets text color of Item in <see cref="State.Disabled"/> state.
        /// </summary>
        public Color DisabledForeGround
        {
            get { return disabledForeGround; }
            set
            {
                if (disabledForeGround == value)
                    return;
                disabledForeGround = value;
                OnAppearanceChanged();
            }
        }

        /// <summary>
        /// Gets or Sets Gradient of Item BackColor. This will updating this will update Gradient for all State.
        /// </summary>
        [DefaultValue(90)]
        public int Gradient
        {
            get { return gradient; }
            set
            {
                if (gradient == value)
                    return;
                gradient = value;
                SetGradient();
                OnAppearanceChanged();
            }
        }

        /// <summary>
        /// Indicates current object is Empty or not.
        /// </summary>
        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                return BackStyle.IsEmpty && ClickStyle.IsEmpty && Gradient == 90 &&
                       HoverBorder.IsEmpty && HoverForeGround.IsEmpty && HoverStyle.IsEmpty && NormalBorder.IsEmpty &&
                       NormalForeGround.IsEmpty && SelectedBorder.IsEmpty && SelectedForeGround.IsEmpty &&
                       DisabledBorder.IsEmpty && DisabledForeGround.IsEmpty &&
                       SelectedHoverStyle.IsEmpty && SelectedStyle.IsEmpty && AppearenceText.IsEmpty &&
                       DisabledStyle.IsEmpty;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when properties related to drawing has been modified.
        /// </summary>
        public event GenericEventHandler<AppearanceAction> AppearanceChanged;

        #endregion

        #region Private Methods

        private void SetGradient()
        {
            SelectedHoverStyle.Gradient = gradient;
            ClickStyle.Gradient = gradient;
            HoverStyle.Gradient = gradient;
            SelectedStyle.Gradient = gradient;
            DisabledStyle.Gradient = gradient;
            BackStyle.Gradient = gradient;
        }

        #endregion

        #region Virtual Method

        /// <summary>
        /// Fires <see cref="AppearanceChanged"/> event
        /// </summary>
        /// <param name="sender">Event source</param>
        /// <param name="tArgs">Object containing Event data.</param>
        protected virtual void OnAppearanceChanged(object sender, GenericEventArgs<AppearanceAction> tArgs)
        {
            if (AppearanceChanged != null)
            {
                AppearanceChanged(this, tArgs);
            }
        }

        /// <summary>
        /// Fires <see cref="AppearanceChanged"/> event
        /// </summary>
        protected virtual void OnAppearanceChanged()
        {
            if (AppearanceChanged != null)
            {
                AppearanceChanged(this, new GenericEventArgs<AppearanceAction>(AppearanceAction.Repaint));
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Resets all properties to default value.
        /// </summary>
        public void Reset()
        {
            ResetBackStyle();
            ResetClickStyle();
            ResetGradient();
            ResetHoverBorder();
            ResetHoverForeGround();
            ResetHoverStyle();
            ResetNormalBorder();
            ResetNormalForeGround();
            ResetSelectedBorder();
            ResetSelectedForeGround();
            ResetDisabledBorder();
            ResetDisabledForeGround();
            ResetSelectedHoverStyle();
            ResetSelectedStyle();
            ResetDisabledStyle();
            ResetAppearenceText();
        }

        /// <summary>
        /// Assigns Values of supplied <see cref="AppearanceItem"/> to current object.
        /// </summary>
        /// <param name="app"><see cref="AppearanceItem"/> object whose value is to be assigned.</param>
        public void Assign(AppearanceItem app)
        {
            backStyle.Assign(app.BackStyle);
            clickStyle.Assign(app.ClickStyle);
            gradient = app.Gradient;
            hoverBorder = app.HoverBorder;
            hoverForeGround = app.HoverForeGround;
            hoverStyle.Assign(app.HoverStyle);
            normalBorder = app.NormalBorder;
            normalForeGround = app.NormalForeGround;
            selectedBorder = app.SelectedBorder;
            selectedForeGround = app.SelectedForeGround;
            disabledBorder = app.DisabledBorder;
            disabledForeGround = app.disabledForeGround;
            selectedHoverStyle.Assign(app.SelectedHoverStyle);
            selectedStyle.Assign(app.SelectedStyle);
            disabledStyle.Assign(app.DisabledStyle);
            appearenceText.Assign(app.AppearenceText);
        }

        /// <summary>
        /// Indicated default property value has been changed or not.
        /// </summary>
        /// <returns>Returns true if default values are changed.</returns>
        public bool DefaultChanged()
        {
            return ShouldSerializeBackStyle() || ShouldSerializeClickStyle() || ShouldSerializeGradient() ||
                   ShouldSerializeHoverBorder() || ShouldSerializeHoverForeGround() ||
                   ShouldSerializeHoverStyle() || ShouldSerializeNormalBorder() || ShouldSerializeNormalForeGround() ||
                   ShouldSerializeSelectedBorder() || ShouldSerializeSelectedForeGround() ||
                   ShouldSerializeDisabledBorder() || ShouldSerializeDisabledForeGround() ||
                   ShouldSerializeSelectedHoverStyle() || ShouldSerializeSelectedStyle() ||
                   ShouldSerializeDisabledStyle() || ShouldSerializeAppearenceText();
        }

        #endregion

        #region Reset Implementation

        /// <summary>
        /// Resets <see cref="AppearenceText"/> to default value.
        /// </summary>
        protected virtual void ResetAppearenceText()
        {
            AppearenceText.Reset();
        }

        /// <summary>
        /// Resets <see cref="BackStyle"/> to default value.
        /// </summary>
        protected virtual void ResetBackStyle()
        {
            backStyle.Reset();
        }

        /// <summary>
        /// Resets <see cref="ClickStyle"/> to default value.
        /// </summary>
        protected virtual void ResetClickStyle()
        {
            clickStyle.Reset();
        }

        /// <summary>
        /// Resets <see cref="HoverStyle"/> to default value.
        /// </summary>
        protected virtual void ResetHoverStyle()
        {
            hoverStyle.Reset();
        }

        /// <summary>
        /// Resets <see cref="SelectedStyle"/> to default value.
        /// </summary>
        protected virtual void ResetSelectedStyle()
        {
            selectedStyle.Reset();
        }

        /// <summary>
        /// Resets <see cref="DisabledStyle"/> to default value.
        /// </summary>
        protected virtual void ResetDisabledStyle()
        {
            disabledStyle.Reset();
        }

        /// <summary>
        /// Resets <see cref="NormalBorder"/> to default value.
        /// </summary>
        protected virtual void ResetNormalBorder()
        {
            normalBorder = Color.Empty;
        }

        /// <summary>
        /// Resets <see cref="SelectedHoverStyle"/> to default value.
        /// </summary>
        protected virtual void ResetSelectedHoverStyle()
        {
            selectedHoverStyle.Reset();
        }

        /// <summary>
        /// Resets <see cref="HoverBorder"/> to default value.
        /// </summary>
        protected virtual void ResetHoverBorder()
        {
            hoverBorder = Color.Empty;
        }

        /// <summary>
        /// Resets <see cref="SelectedBorder"/> to default value.
        /// </summary>
        protected virtual void ResetSelectedBorder()
        {
            selectedBorder = Color.Empty;
        }

        /// <summary>
        /// Resets <see cref="NormalForeGround"/> to default value.
        /// </summary>
        protected virtual void ResetNormalForeGround()
        {
            normalForeGround = Color.Empty;
        }

        /// <summary>
        /// Resets <see cref="HoverForeGround"/> to default value.
        /// </summary>
        protected virtual void ResetHoverForeGround()
        {
            hoverForeGround = Color.Empty;
        }

        /// <summary>
        /// Resets <see cref="SelectedForeGround"/> to default value.
        /// </summary>
        protected virtual void ResetSelectedForeGround()
        {
            selectedForeGround = Color.Empty;
        }

        /// <summary>
        /// Resets <see cref="DisabledBorder"/> to default value.
        /// </summary>
        protected virtual void ResetDisabledBorder()
        {
            disabledBorder = Color.Empty;
        }

        /// <summary>
        /// Resets <see cref="DisabledForeGround"/> to default value.
        /// </summary>
        protected virtual void ResetDisabledForeGround()
        {
            disabledForeGround = Color.Empty;
        }

        /// <summary>
        /// Resets <see cref="Gradient"/> to default value.
        /// </summary>
        protected virtual void ResetGradient()
        {
            gradient = 90;
        }

        #endregion

        #region Should Serialize implementation

        /// <summary>
        /// Indicates wether <see cref="AppearenceText"/> needs to be serialized by designer or not.
        /// </summary>
        /// <returns>true if designer needs to serialize</returns>
        protected virtual bool ShouldSerializeAppearenceText()
        {
            return AppearenceText.DefaultChanged();
        }

        /// <summary>
        /// Indicates wether <see cref="BackStyle"/> needs to be serialized by designer or not.
        /// </summary>
        /// <returns>true if designer needs to serialize</returns>
        protected virtual bool ShouldSerializeBackStyle()
        {
            return backStyle.DefaultChanged();
        }

        /// <summary>
        /// Indicates wether <see cref="ClickStyle"/> needs to be serialized by designer or not.
        /// </summary>
        /// <returns>true if designer needs to serialize</returns>
        protected virtual bool ShouldSerializeClickStyle()
        {
            return clickStyle.DefaultChanged();
        }

        /// <summary>
        /// Indicates wether <see cref="HoverStyle"/> needs to be serialized by designer or not.
        /// </summary>
        /// <returns>true if designer needs to serialize</returns>
        protected virtual bool ShouldSerializeHoverStyle()
        {
            return hoverStyle.DefaultChanged();
        }

        /// <summary>
        /// Indicates wether <see cref="SelectedStyle"/> needs to be serialized by designer or not.
        /// </summary>
        /// <returns>true if designer needs to serialize</returns>
        protected virtual bool ShouldSerializeSelectedStyle()
        {
            return SelectedStyle.DefaultChanged();
        }

        /// <summary>
        /// Indicates wether <see cref="DisabledStyle"/> needs to be serialized by designer or not.
        /// </summary>
        /// <returns>true if designer needs to serialize</returns>
        protected virtual bool ShouldSerializeDisabledStyle()
        {
            return DisabledStyle.DefaultChanged();
        }

        /// <summary>
        /// Indicates wether <see cref="NormalBorder"/> needs to be serialized by designer or not.
        /// </summary>
        /// <returns>true if designer needs to serialize</returns>
        protected virtual bool ShouldSerializeNormalBorder()
        {
            return normalBorder != Color.Empty;
        }

        /// <summary>
        /// Indicates wether <see cref="SelectedHoverStyle"/> needs to be serialized by designer or not.
        /// </summary>
        /// <returns>true if designer needs to serialize</returns>
        protected virtual bool ShouldSerializeSelectedHoverStyle()
        {
            return selectedHoverStyle.DefaultChanged();
        }

        /// <summary>
        /// Indicates wether <see cref="HoverBorder"/> needs to be serialized by designer or not.
        /// </summary>
        /// <returns>true if designer needs to serialize</returns>
        protected virtual bool ShouldSerializeHoverBorder()
        {
            return hoverBorder != Color.Empty;
        }

        /// <summary>
        /// Indicates wether <see cref="SelectedBorder"/> needs to be serialized by designer or not.
        /// </summary>
        /// <returns>true if designer needs to serialize</returns>
        protected virtual bool ShouldSerializeSelectedBorder()
        {
            return selectedBorder != Color.Empty;
        }

        /// <summary>
        /// Indicates wether <see cref="NormalForeGround"/> needs to be serialized by designer or not.
        /// </summary>
        /// <returns>true if designer needs to serialize</returns>
        protected virtual bool ShouldSerializeNormalForeGround()
        {
            return normalForeGround != Color.Empty;
        }

        /// <summary>
        /// Indicates wether <see cref="HoverForeGround"/> needs to be serialized by designer or not.
        /// </summary>
        /// <returns>true if designer needs to serialize</returns>
        protected virtual bool ShouldSerializeHoverForeGround()
        {
            return hoverForeGround != Color.Empty;
        }

        /// <summary>
        /// Indicates wether <see cref="SelectedForeGround"/> needs to be serialized by designer or not.
        /// </summary>
        /// <returns>true if designer needs to serialize</returns>
        protected virtual bool ShouldSerializeSelectedForeGround()
        {
            return selectedForeGround != Color.Empty;
        }

        /// <summary>
        /// Indicates wether <see cref="DisabledForeGround"/> needs to be serialized by designer or not.
        /// </summary>
        /// <returns>true if designer needs to serialize</returns>
        protected virtual bool ShouldSerializeDisabledForeGround()
        {
            return disabledForeGround != Color.Empty;
        }

        /// <summary>
        /// Indicates wether <see cref="DisabledBorder"/> needs to be serialized by designer or not.
        /// </summary>
        /// <returns>true if designer needs to serialize</returns>
        protected virtual bool ShouldSerializeDisabledBorder()
        {
            return disabledBorder != Color.Empty;
        }

        /// <summary>
        /// Indicates wether <see cref="Gradient"/> needs to be serialized by designer or not.
        /// </summary>
        /// <returns>true if designer needs to serialize</returns>
        protected virtual bool ShouldSerializeGradient()
        {
            return gradient != 90;
        }

        #endregion

        #region Implementation of ICloneable

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public object Clone()
        {
            var app = new AppearanceItem();
            app.BackStyle.Assign((ColorPair) BackStyle.Clone());
            app.ClickStyle.Assign((ColorPair) ClickStyle.Clone());
            app.DisabledBorder = DisabledBorder;
            app.DisabledForeGround = DisabledForeGround;
            app.DisabledStyle.Assign((ColorPair) DisabledStyle.Clone());
            app.Gradient = Gradient;
            app.HoverBorder = HoverBorder;
            app.HoverForeGround = HoverForeGround;
            app.HoverStyle.Assign((ColorPair) HoverStyle.Clone());
            app.NormalBorder = NormalBorder;
            app.NormalForeGround = NormalForeGround;
            app.SelectedBorder = SelectedBorder;
            app.SelectedForeGround = SelectedForeGround;
            app.SelectedHoverStyle.Assign((ColorPair) SelectedHoverStyle.Clone());
            app.SelectedStyle.Assign((ColorPair) SelectedStyle.Clone());
            app.AppearenceText.Assign((AppearenceText) AppearenceText.Clone());
            return app;
        }

        #endregion

        #region Implementation of IXmlSerializable

        /// <summary>
        /// This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute"/> to the class.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema"/> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"/> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"/> method.
        /// </returns>
        public XmlSchema GetSchema()
        {
            return new XmlSchema();
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"/> stream from which the object is deserialized. </param>
        public void ReadXml(XmlReader reader)
        {
            var doc = new XmlDocument();
            doc.Load(reader);

            if (doc.GetElementsByTagName("BackStyle").Count > 0)
            {
                var xml = "<ColorPair>" + doc.GetElementsByTagName("BackStyle")[0].InnerXml + "</ColorPair>";
                BackStyle.ReadXml(new XmlTextReader(xml, XmlNodeType.Document, null));
            }
            if (doc.GetElementsByTagName("ClickStyle").Count > 0)
            {
                var xml = "<ColorPair>" + doc.GetElementsByTagName("ClickStyle")[0].InnerXml + "</ColorPair>";
                ClickStyle.ReadXml(new XmlTextReader(xml, XmlNodeType.Document, null));
            }
            if (doc.GetElementsByTagName("HoverStyle").Count > 0)
            {
                var xml = "<ColorPair>" + doc.GetElementsByTagName("HoverStyle")[0].InnerXml + "</ColorPair>";
                HoverStyle.ReadXml(new XmlTextReader(xml, XmlNodeType.Document, null));
            }
            if (doc.GetElementsByTagName("SelectedStyle").Count > 0)
            {
                var xml = "<ColorPair>" + doc.GetElementsByTagName("SelectedStyle")[0].InnerXml + "</ColorPair>";
                SelectedStyle.ReadXml(new XmlTextReader(xml, XmlNodeType.Document, null));
            }
            if (doc.GetElementsByTagName("DisabledStyle").Count > 0)
            {
                var xml = "<ColorPair>" + doc.GetElementsByTagName("DisabledStyle")[0].InnerXml + "</ColorPair>";
                DisabledStyle.ReadXml(new XmlTextReader(xml, XmlNodeType.Document, null));
            }
            if (doc.GetElementsByTagName("SelectedHoverStyle").Count > 0)
            {
                var xml = "<ColorPair>" + doc.GetElementsByTagName("SelectedHoverStyle")[0].InnerXml + "</ColorPair>";
                SelectedHoverStyle.ReadXml(new XmlTextReader(xml, XmlNodeType.Document, null));
            }


            if (doc.GetElementsByTagName("NormalBorder").Count > 0)
                NormalBorder = PaintUtility.GetColor(doc.GetElementsByTagName("NormalBorder")[0].InnerText);
            if (doc.GetElementsByTagName("HoverBorder").Count > 0)
                HoverBorder = PaintUtility.GetColor(doc.GetElementsByTagName("HoverBorder")[0].InnerText);
            if (doc.GetElementsByTagName("SelectedBorder").Count > 0)
                SelectedBorder = PaintUtility.GetColor(doc.GetElementsByTagName("SelectedBorder")[0].InnerText);
            if (doc.GetElementsByTagName("NormalForeGround").Count > 0)
                NormalForeGround = PaintUtility.GetColor(doc.GetElementsByTagName("NormalForeGround")[0].InnerText);
            if (doc.GetElementsByTagName("HoverForeGround").Count > 0)
                HoverForeGround = PaintUtility.GetColor(doc.GetElementsByTagName("HoverForeGround")[0].InnerText);

            if (doc.GetElementsByTagName("AppearenceText").Count > 0)
            {
                var xml = "<AppearenceText>" + doc.GetElementsByTagName("AppearenceText")[0].InnerXml +
                          "</AppearenceText>";
                AppearenceText.ReadXml(new XmlTextReader(xml, XmlNodeType.Document, null));
            }

            if (doc.GetElementsByTagName("SelectedForeGround").Count > 0)
                SelectedForeGround = PaintUtility.GetColor(doc.GetElementsByTagName("SelectedForeGround")[0].InnerText);
            if (doc.GetElementsByTagName("DisabledBorder").Count > 0)
                DisabledBorder = PaintUtility.GetColor(doc.GetElementsByTagName("DisabledBorder")[0].InnerText);
            if (doc.GetElementsByTagName("DisabledForeGround").Count > 0)
                DisabledForeGround = PaintUtility.GetColor(doc.GetElementsByTagName("DisabledForeGround")[0].InnerText);
            if (doc.GetElementsByTagName("Gradient").Count > 0)
                Gradient = Convert.ToInt32(doc.GetElementsByTagName("Gradient")[0].InnerText);
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized. </param>
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("BackStyle");
            BackStyle.WriteXml(writer);
            writer.WriteEndElement();

            writer.WriteStartElement("ClickStyle");
            ClickStyle.WriteXml(writer);
            writer.WriteEndElement();

            writer.WriteStartElement("HoverStyle");
            HoverStyle.WriteXml(writer);
            writer.WriteEndElement();

            writer.WriteStartElement("SelectedStyle");
            SelectedStyle.WriteXml(writer);
            writer.WriteEndElement();

            writer.WriteStartElement("DisabledStyle");
            DisabledStyle.WriteXml(writer);
            writer.WriteEndElement();

            writer.WriteStartElement("SelectedHoverStyle");
            SelectedHoverStyle.WriteXml(writer);
            writer.WriteEndElement();

            writer.WriteElementString("NormalBorder", PaintUtility.GetString(NormalBorder));
            writer.WriteElementString("HoverBorder", PaintUtility.GetString(HoverBorder));
            writer.WriteElementString("NormalForeGround", PaintUtility.GetString(NormalForeGround));
            writer.WriteElementString("SelectedBorder", PaintUtility.GetString(SelectedBorder));
            writer.WriteElementString("HoverForeGround", PaintUtility.GetString(HoverForeGround));

            writer.WriteStartElement("AppearenceText");
            AppearenceText.WriteXml(writer);
            writer.WriteEndElement();

            writer.WriteElementString("SelectedForeGround", PaintUtility.GetString(SelectedForeGround));
            writer.WriteElementString("DisabledBorder", PaintUtility.GetString(DisabledBorder));
            writer.WriteElementString("DisabledForeGround", PaintUtility.GetString(DisabledForeGround));

            writer.WriteElementString("Gradient", Gradient.ToString());
        }

        #endregion
    }
}