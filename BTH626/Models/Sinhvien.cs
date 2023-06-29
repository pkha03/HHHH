using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTH626.Models;
public class Sinhvien
{
    [Key]
    [Display(Name ="Mã sinh viên")]
    public string masinhvien{get;set;}
    
    [Display(Name ="Tên sinh viên")]
    public string tensinhvien{get;set;}

    [Display(Name ="Mã lớp")]
    public string malop{get;set;}
    
    [ForeignKey("malop")]
    public Lophoc? Lophoc {get;set;}
    
}