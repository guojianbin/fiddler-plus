using System;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using ButtonBarsControl.Design.Enums;
using ButtonBarsControl.Design.Generics;

namespace ButtonBarsControl.Design.Layout
{
    /// <summary>
    /// Class responsible to hold information related to Border.
    /// </summary>
    [TypeConverter(typeof (GenericConverter<AppearanceBorder>))]
    public class AppearanceBorder : ICloneable, IDisposable, IXmlSerializable
    {
        private DashStyle borderLineStyle = DashStyle.Solid;
        private ToolStripStatusLabelBorderSides borderVisibility = ToolStripStatusLabelBorderSides.All;
        private CornerShape cornerShape;

        /// <summary>
        /// Create new instance of <see cref="AppearanceBorder"/>
        /// </summary>
        public AppearanceBorder()
        {
            cornerShape = new CornerShape();
        }

        /// <summary>
        /// <see cref="Layout.CornerShape"/> object containing corner shape information of borders.
        /// </summary>
        [TypeConverter(typeof (GenericConverter<CornerShape>))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CornerShape CornerShape
        {
            get { return cornerShape; }
        }

        /// <summary>
        /// Gets or Sets Border Style to be used.
        /// </summary>
        public DashStyle BorderLineStyle
        {
            get { return borderLineStyle; }
            set
            {
                if (borderLineStyle != value)
                {
                    borderLineStyle = value;
                    OnAppearanceChanged(new GenericEventArgs<AppearanceAction>(AppearanceAction.Recreate));
                }
            }
        }

        /// <summary>
        /// Gets or Sets On which sides border shouldbe shown.
        /// </summary>
        public ToolStripStatusLabelBorderSides BorderVisibility
        {
            get { return borderVisibility; }
            set
            {
                if (borderVisibility != value)
                {
                    borderVisibility = value;
                    OnAppearanceChanged(new GenericEventArgs<AppearanceAction>(AppearanceAction.Recreate));
                }
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
                return borderLineStyle == DashStyle.Solid && borderVisibility == ToolStripStatusLabelBorderSides.All &&
                       cornerShape.IsEmpty;
            }
        }

        #region ICloneable Members

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public virtual object Clone()
        {
            var appearanceBorder = new AppearanceBorder();
            appearanceBorder.CornerShape.Assign((CornerShape) cornerShape.Clone());
            appearanceBorder.BorderLineStyle = borderLineStyle;
            appearanceBorder.BorderVisibility = borderVisibility;
            return appearanceBorder;
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public virtual void Dispose()
        {
            if (cornerShape != null)
            {
                cornerShape.BorderCornerChanged -= OnBorderCornerChanged;
            }
        }

        #endregion

        /// <summary>
        /// Occurs when properties related to drawing has been modified.
        /// </summary>
        public event GenericEventHandler<AppearanceAction> AppearanceChanged;

        /// <summary>
        /// Get wether default values of the object has been modified.
        /// </summary>
        /// <returns>Returns true if default values has been modified for current object.</returns>
        public virtual bool DefaultChanged()
        {
            return borderLineStyle != DashStyle.Solid || borderVisibility != ToolStripStatusLabelBorderSides.All ||
                   CornerShape.DefaultChanged();
        }

        /// <summary>
        /// Fires <see cref="AppearanceChanged"/> event
        /// </summary>
        /// <param name="sender">Event source.</param>
        /// <param name="e">Object containing event data.</param>
        protected void OnBorderCornerChanged(object sender, GenericEventArgs<AppearanceAction> e)
        {
            OnAppearanceChanged(e);
        }

        /// <summary>
        /// Fires <see cref="AppearanceChanged"/> event
        /// </summary>
        /// <param name="e">Object containing event data.</param>
        protected virtual void OnAppearanceChanged(GenericEventArgs<AppearanceAction> e)
        {
            if (AppearanceChanged != null)
            {
                AppearanceChanged(this, e);
            }
        }

        /// <summary>
        /// Resets current object to default values.
        /// </summary>
        public virtual void Reset()
        {
            cornerShape = new CornerShape();
            borderLineStyle = DashStyle.Solid;
            borderVisibility = ToolStripStatusLabelBorderSides.All;
        }

        /// <summary>
        /// Copies values of supplied <see cref="AppearanceBorder"/> to current object.
        /// </summary>
        /// <param name="appearanceBorder"><see cref="AppearanceBorder"/> object whose value is to be copied.</param>
        public void Assign(AppearanceBorder appearanceBorder)
        {
            CornerShape.Assign((CornerShape) appearanceBorder.cornerShape.Clone());
            BorderLineStyle = appearanceBorder.borderLineStyle;
            BorderVisibility = appearanceBorder.borderVisibility;
        }

        #region Should serialize implementation used by designer

        /// <summary>
        /// Indicates wether designer needs to serialize <see cref="CornerShape"/> property.
        /// </summary>
        /// <returns>Returns true if <see cref="CornerShape"/> property needs to be serialized.</returns>
        protected bool ShouldSerializeCornerShape()
        {
            return cornerShape.DefaultChanged();
        }

        /// <summary>
        /// Indicates wether designer needs to serialize <see cref="BorderLineStyle"/> property.
        /// </summary>
        /// <returns>Returns true if <see cref="BorderLineStyle"/> property needs to be serialized.</returns>
        protected bool ShouldSerializeBorderLineStyle()
        {
            return borderLineStyle != DashStyle.Solid;
        }

        /// <summary>
        /// Indicates wether designer needs to serialize <see cref="BorderVisibility"/> property.
        /// </summary>
        /// <returns>Returns true if <see cref="BorderVisibility"/> property needs to be serialized.</returns>
        protected bool ShouldSerializeBorderVisibility()
        {
            return borderVisibility != ToolStripStatusLabelBorderSides.All;
        }

        #endregion

        #region Reset implementation used by designer

        /// <summary>
        /// Resets value of <see cref="CornerShape"/> to default value.
        /// </summary>
        protected void ResetCornerShape()
        {
            cornerShape = new CornerShape();
        }

        /// <summary>
        /// Resets value of <see cref="BorderLineStyle"/> to default value.
        /// </summary>
        protected void ResetBorderLineStyle()
        {
            borderLineStyle = DashStyle.Solid;
        }

        /// <summary>
        /// Resets value of <see cref="BorderVisibility"/> to default value.
        /// </summary>
        protected void ResetBorderVisibility()
        {
            borderVisibility = ToolStripStatusLabelBorderSides.All;
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
            if (doc.GetElementsByTagName("BorderLineStyle").Count > 0)
                BorderLineStyle =
                    (DashStyle) Enum.Parse(typeof (DashStyle), doc.GetElementsByTagName("BorderLineStyle")[0].InnerText);
            if (doc.GetElementsByTagName("BorderVisibility").Count > 0)
                BorderVisibility =
                    (ToolStripStatusLabelBorderSides)
                    Enum.Parse(typeof (ToolStripStatusLabelBorderSides),
                               doc.GetElementsByTagName("BorderVisibility")[0].InnerText);
            if (doc.GetElementsByTagName("CornerShape").Count > 0)
            {
                string xml = "<CornerShape>" + doc.GetElementsByTagName("CornerShape")[0].InnerXml + "</CornerShape>";
                CornerShape.ReadXml(new XmlTextReader(xml, XmlNodeType.Document, null));
            }
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized. </param>
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteElementString("BorderLineStyle", BorderLineStyle.ToString());
            writer.WriteElementString("BorderVisibility", BorderVisibility.ToString());
            writer.WriteStartElement("CornerShape");
            CornerShape.WriteXml(writer);
            writer.WriteEndElement();
        }

        #endregion
    }
}