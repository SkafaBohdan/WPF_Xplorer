using System.IO;
using System.Text;

namespace WPF_Xplorer.Models
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
            Info,
            Null,
            Bookmark
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
        public virtual string DisplayType
        {
            get
            {
                if (Type is PdfType.Info)
                    return PdfType.Indirect.ToString();
                else
                    return Type.ToString();
            }
        }

        public virtual string DisplayValue
        {
            get
            {
                return value?.ToString();
            }
        }
        public virtual string DisplayKeyAndValue
        {
            get
            {
                return DisplayKey + " " + Value;
            }
        }

        public string StreamValue
        {
            get
            {
                if (!(Type is PdfType.Stream))
                    return string.Empty;

                byte[] arr;

                var streamValue = (PdfStream)this;
                using (var fileStream = new FileStream(streamValue.Path, FileMode.Open))
                {
                    arr = new byte[fileStream.Length];
                    fileStream.Read(arr, 0, arr.Length);
                }
                return Encoding.Default.GetString(arr);
            }
        }        
    }
}
