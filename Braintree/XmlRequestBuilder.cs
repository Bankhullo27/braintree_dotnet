#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace Braintree
{
    public class XmlRequestBuilder : RequestBuilder
    {
        public XmlRequestBuilder() : base() {}
        public XmlRequestBuilder(string parent) : base(parent) {}

        protected override string Format(object content)
        {
            if (content.GetType().IsSubclassOf(typeof(Request)))
            {
                return ((Request) content).ToInnerXml();
            }
            else
            {
                return SecurityElement.Escape(content.ToString());
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            if (Parent != null) builder.AppendFormat("<{0}>", Parent);
            foreach (KeyValuePair<string, string> node in Nodes) {
                builder.AppendFormat("<{0}>{1}</{0}>", Dasherize(node.Key), node.Value);
            }
            if (Parent != null) builder.AppendFormat("</{0}>", Parent);

            return builder.ToString();
        }

        protected string Dasherize(string value)
        {
            return value.Replace('_', '-');
        }
    }
}