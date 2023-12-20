using System.Configuration;

namespace LuxuryAPIv3.Models
{
    public class Category
    {
        private readonly string DEFAULT_IMG_PATH = ConfigurationManager.AppSettings["DEFAULT_IMG_PATH"] + "/Category";

        public int IdCate { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public Category() 
        {
            this.Image = DEFAULT_IMG_PATH;
        }
        public Category(string Name)
        {
            this.Name = Name;
            this.Image = DEFAULT_IMG_PATH;
        }
        public Category(string Name, string Image)
        {
            this.Name = Name;
            this.Image = Image;
        }
        public Category(int IdCate, string Name, string Image)
        {
            this.IdCate = IdCate;
            this.Name = Name;
            this.Image = Image;
        }
    }
}