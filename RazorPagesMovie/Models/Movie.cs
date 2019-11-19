﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesMovie.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public string Title{ get; set; }

        [Display(Name ="Release Data")]//Display 特性指定要显示的字段名称的内容（本例中应为“Release Date”，而不是“ReleaseDate”）。 
        [DataType(DataType.Date)]//DataType 属性指定数据的类型（日期），使字段中存储的时间信息不会显示。
        public DateTime ReleaseData { get; set; }
        public string Genre { get; set; }

        [Column(TypeName ="decimal(18,2)")]//[Column(TypeName = "decimal(18, 2)")] 数据注释使 Entity Framework Core 可以将 Price 正确映射到数据库中的货币
        public decimal Price { get; set; }


    }
}
