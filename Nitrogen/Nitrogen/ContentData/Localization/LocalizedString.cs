using Nitrogen.Core.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Nitrogen.Core.ContentData.Localization
{
    public class LocalizedString
        : ISerializable<EndianStream>
    {
        private Dictionary<Language, string> table;

        public LocalizedString(LanguageTable map)
        {
            Map = map;
            this.table = new Dictionary<Language, string>();
        }

        public LocalizedString(LanguageTable map, string value)
            : this(map)
        {
            Set(value);
        }

        protected LanguageTable Map { get; private set; }

        public virtual void Set(string value)
        {
            Contract.Requires<ArgumentNullException>(value != null);

            foreach (var language in Map)
                Set(language, value);
        }

        public virtual void Set(Language language, string value)
        {
            Contract.Requires<ArgumentNullException>(value != null);
            Contract.Requires<ArgumentException>(Map.LanguageSupported(language));

            this.table[language] = value;
        }

        public virtual string Get(Language language)
        {
            Contract.Requires<ArgumentException>(Map.LanguageSupported(language));
            return this.table[language];
        }

        public virtual void Serialize(EndianStream s)
        {

        }
    }
}
