using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TestStore.Models
{
    public class Course
    {
        public int Id { get; set; }

        [DisplayName("課程編號")]
        public string CId { get; set; }

        [DisplayName("課程名稱")]
        public string Name { get; set; }

        [DisplayName("講師名稱")]
        public string Lector { get; set; }

        [DisplayName("課程類別編號")]
        public Nullable<int> CategoryId { get; set; }
        
        [DisplayName("單價")]
        public int Price { get; set; }

        [DisplayName("圖示")]
        public string Img { get; set; }
    }
}
