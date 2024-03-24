using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Depressed.Models
{
    /*public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int stu_id {  get; set; }
        [Required]
        public string name { get; set; }
        public int age { get; set; }
        [Required]
        public string main_subject { get; set; }
        public string address { get; set; }
        public int phone_number { get; set; }
        public DateTime BirthDate { get; set; }
        public byte[] Image { get; set; }
        public virtual ICollection<ClassMember> ClassMembers { get; set; }
        public virtual ICollection<RollOut> RollOuts { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int te_id { get; set; }
        [Required]
        public string name { get; set; }
        public int age { get; set; }
        [Required]
        public string main_subject { get; set; }
        public string address { get; set; }
        public int phone_number { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte[] Image { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ICollection<Khoahoc> Khoahocs {  get; set; }
        public virtual ICollection<Thongbao> Thongbaos { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }*/
    public class Khoahoc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int kh_id { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string name { get; set; }
        [ForeignKey("Category")]
        public int category_id {  get; set; }
        public virtual Category Category { get; set; }
        [Required]
        public string Content { get; set; }
        public byte[] Image { get; set; }
        public float Price { get; set; }
    }
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ca_id { get; set; }
        [Required]
        public string ca_name { get; set; }
        public virtual ICollection<Khoahoc> Khoahocs { get; set; }
    }
    public class ClassMember
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [ForeignKey("Lophoc")]
        public int lophoc_id { get; set; }
        public virtual Lophoc Lophoc { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
    public class Lophoc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int class_id { get; set; }
        public string class_name { get; set; }
        public string content { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Lichhoc> Lichhoc { get; set; }
    }
    public class Lichhoc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [ForeignKey("Lophoc")]
        public int lophoc_id { get; set; }
        public  virtual Lophoc Lophoc { get; set; }
        public DateTime? Ngayhoc1 {  get; set; }
        public string Tiet_1 {  get; set; }
        public DateTime? Ngayhoc2 { get; set; }
        public string Tiet_2 { get; set; }
        public DateTime? Ngayhoc3 { get; set; }
        public string Tiet_3 { get; set; }
    }
    public class RollOut
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [ForeignKey("Lichhoc")]
        public int lh_id { get; set; }
        public virtual Lichhoc Lichhoc { get; set; }
        [Required]
        public string Lec_Content { get; set; }
        public DateTime? date { get; set; }
        public bool? excuted { get; set; }
        public int total_absent { get; set; }
        public float total_percent { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("Lophoc")]
        public virtual int lophoc_id { get; set; }
        public virtual Lophoc Lophoc { get; set; }
        public string Note { get; set; }
    }
    public class Thongbao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int tb_id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string content { get; set; }
        public DateTime? Date_Public { get; set; }
    }
}