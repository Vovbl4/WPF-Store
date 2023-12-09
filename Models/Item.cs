using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FinalTask.Models
{
    internal class Item : INotifyPropertyChanged
    {


        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int Price { get; set; }

        [NotMapped]
        public string DisplayImagePath
        {
            get
            {
                return "/imgs/" + ImagePath;
            }
        }

        public Item() {}

        public Item(string name, string description, string img_path, int price)
        {
            ImagePath = img_path;
            Name = name;
            Description = description;
            Price = price;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
