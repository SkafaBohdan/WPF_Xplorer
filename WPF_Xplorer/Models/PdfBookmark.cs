using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Xplorer.Models
{ 
    public class PdfBookmark : NotifyPropertyChanged 
    {
        private int id = 0;
        private string title;
        private int page;

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Title
        {
            get => title;
            set
            {
                Id++;
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public int Page
        {
            get => page;
            set
            {
                page = value;
                OnPropertyChanged(nameof(Page));
            }
        }

        public string DisplayTitleAndPage =>  $"{Title} => GoTo Page {Page}";
    }
}
