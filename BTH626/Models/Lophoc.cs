using System.ComponentModel.DataAnnotations;
namespace BTH626.Models;
public class Lophoc
{
    [Key]
    [Display(Name ="Tên lớp")]
    public string tenlop{get;set;}


     [Display(Name ="Mã lớp")]
    public string malop{get;set;}
    
    

}