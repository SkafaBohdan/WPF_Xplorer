using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingPDFTron.Models
{
    public class PdfObj : NotifyPropertyChanged
    {
        private string key;
        private PdfType type;
        private object value;
        public enum PdfType
        {
            Document,
            Bool,
            String,
            Stream,
            Number,
            Dictionary,
            Indirect,
            Array,
            Name,
            Info
        }

        public string Key
        {
            get => key;
            set
            {
                key = value;
                OnPropertyChanged(nameof(Key));
            }
        }

        public PdfType Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged(nameof(value));
            }
        }

        public object Value
        {
            get => value;
            set
            {
                this.value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public virtual string DisplayKey 
        {
            get
            {

                if (Type is PdfType.Document || Type is PdfType.Stream)
                    return key;
                else
                    return $"/{key}";
            }
        }
        
        public virtual string DisplayValue
        {
            get
            {
                return value.ToString();
            }
        }
        public virtual string DisplayKeyAndValue
        {
            get
            {
                return DisplayKey + " " + Value;
            }
        }


    }
}
