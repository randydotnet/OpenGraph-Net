namespace OpenGraphNet.Metadata
{
    using OpenGraphNet.Namespaces;
    using HtmlAgilityPack;

    /// <summary>
    /// Represents an Open Graph meta element
    /// </summary>
    public abstract class Metadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Metadata" /> class.
        /// </summary>
        /// <param name="ns">The ns.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        protected Metadata(Namespace ns, string name, string value) 
        {
            this.Namespace = ns;
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the namespace.
        /// </summary>
        /// <value>
        /// The namespace.
        /// </value>
        public Namespace Namespace { get; set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public string Value { get; }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Metadata"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator string(Metadata element)
        {
            return element.Value;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.Value))
            {
                return string.Empty;
            }

            HtmlDocument doc = this.CreateDocument();

            return doc.DocumentNode.OuterHtml;
        }

        /// <summary>
        /// Creates the document.
        /// </summary>
        /// <returns>The HTML snippet that represents this element</returns>
        protected internal virtual HtmlDocument CreateDocument()
        {
            var doc = new HtmlDocument();

            var meta = doc.CreateElement("meta");
            meta.Attributes.Add("property", string.Concat(this.Namespace.Prefix, ":", this.Name));
            meta.Attributes.Add("content", this.Value);
            doc.DocumentNode.AppendChild(meta);

            return doc;
        }
    }
}