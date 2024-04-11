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
        [Display(Name = "Course Name")]
        public string name { get; set; }
        [Display(Name = "Course Category")]
        [ForeignKey("Category")]
        public int category_id {  get; set; }
        public virtual Category Category { get; set; }
        [Required]
        [Display(Name = "Course Description")]
        public string Content { get; set; }
        public byte[] Image { get; set; }
        [Display(Name = "Price")]
        public float Price { get; set; }
    }
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ca_id { get; set; }
        [Required]
        [Display(Name = "Category")]
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
        [Display(Name = "Class Name")]
        public string class_name { get; set; }
        [Display(Name = "Class Description")]
        public string content { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Lichhoc> Lichhocs { get; set; }
        public virtual ICollection<ClassMember> ClassMembers { get; set; }
    }
    public class Lichhoc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [ForeignKey("Lophoc")]
        public int lophoc_id { get; set; }
        public virtual Lophoc Lophoc { get; set; }
        [Display(Name = "Day in Week")]
        public string Ngayhoc1 { get; set; }
        [Display(Name = "Period")]
        public string Tiet_1 { get; set; }
        [Display(Name = "Day in Week")]
        public string Ngayhoc2 { get; set; }
        [Display(Name = "Period")]
        public string Tiet_2 { get; set; }
        [Display(Name = "Day in Week")]
        public string Ngayhoc3 { get; set; }
        [Display(Name = "Period")]
        public string Tiet_3 { get; set; }
    }
    public class RollOut
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        [Display(Name = "Lecture Description")]
        public string Lec_Content { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Date-only")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime? date { get; set; }
        [Display(Name = "Absent")]
        public bool? excuted { get; set; }
        [Display(Name = "Total Absent")]

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("Lophoc")]
        public virtual int lophoc_id { get; set; }
        public virtual Lophoc Lophoc { get; set; }
        public string Note { get; set; }
    }
    public class Post_Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Date-only")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Public")]
        public DateTime? DateCreated { get; set; }
        public virtual ICollection<Main_Comment> main_Comments { get; set; }
        public enum PostStatus
        {
            Pending,
            Approved,
            Denied
        }
        public PostStatus Status { get; set; }
    }
    public class Main_Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("post_Posts")]
        public int post_id { get; set; }
        public virtual Post_Post post_Posts { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string comments { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Date-only")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateComment { get; set; }
        public virtual ICollection<Sub_Comment> sub_Comments { get; set; }
    }
    public class Sub_Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("main_Comment")]
        public virtual int main_id { get; set; }
        public virtual Main_Comment main_Comment { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string comments { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Date-only")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateComment { get; set; }
    }
}