using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FiveGroup.Models
{
    public class MetaHospital
    {
        [Display(Name ="編號")]
        [Required(ErrorMessage = "此為必填欄位")]
        public string hos_id { get; set; }

        [Display(Name = "中文名稱")]
        [Required(ErrorMessage = "此為必填欄位")]
        public string hos_name { get; set; }

        [Display(Name = "英文名稱")]
        [Required(ErrorMessage = "此為必填欄位")]
        public string hos_eng_name { get; set; }

        [Display(Name = "地址")]
        [Required(ErrorMessage = "此為必填欄位")]
        public string hos_address { get; set; }

        [Display(Name = "連絡電話")]
        [Required(ErrorMessage = "此為必填欄位")]
        public string hos_phone { get; set; }
        
        [Display(Name = "是否顯示")]
        [Required(ErrorMessage = "此為必填欄位")]
        public bool hos_display { get; set; }
    }
    public partial class Metadataingrediant
    {


        [DisplayName("成分編號")]
        [Required(ErrorMessage = "此欄位不得為空白")]
        public string ing_id { get; set; }


        [DisplayName("中文名稱")]
        [Required(ErrorMessage = "此欄位不得為空白")]
        [StringLength(30, ErrorMessage = "此欄位不得超過30字")]
        public string ing_name { get; set; }


        [DisplayName("英文名稱")]
        [Required(ErrorMessage = "此欄位不得為空白")]
        [StringLength(30, ErrorMessage = "此欄位不得超過30字")]
        //[RegularExpression("[A-Z][a-z]", ErrorMessage ="僅能輸入英文")]
        public string ing_eng_name { get; set; }


        [DisplayName("類別")]
        [Required(ErrorMessage = "此欄位不得為空白")]
        [StringLength(20, ErrorMessage = "此欄位不得超過20字")]
        public string ing_category { get; set; }


        [DisplayName("範圍與限制用量")]
        [Required(ErrorMessage = "此欄位不得為空白")]
        [StringLength(500, ErrorMessage = "此欄位不得超過500字")]
        public string ing_restricted { get; set; }


        [DisplayName("使用限制")]
        [StringLength(100, ErrorMessage = "此欄位不得超過100字")]
        public string ing_limitation { get; set; }
    }
    public class MetaDatadoctor
    {

        [DisplayName("醫師編號")]
        [RegularExpression("[D][R][0-9][0-9][0-9]", ErrorMessage = "請輸入正確格式!!【範例:DR001】")]
        public string doc_id { get; set; }

        [DisplayName("醫師姓名")]
        [Required(ErrorMessage = "請輸入醫師名稱")]
        [StringLength(20, ErrorMessage = "醫師名稱不得超過20字")]
        public string doc_name { get; set; }

        [DisplayName("醫師經歴")]
        [Required(ErrorMessage = "請輸入醫師經歴")]
        [StringLength(150, ErrorMessage = "醫師經歴不得超過150字")]
        public string doc_history { get; set; }

        [DisplayName("顯示醫師")]
        [DefaultValue(true)]
        public bool doc_display { get; set; }

    }

    public class MetadataDepSymRef
    {
        [DisplayName("流水號")]
        public long dep_sym_sn { get; set; }
    }

    public class MetadataBodyPart
    {
        [DisplayName("身體部位")]
        public string part_name { get; set; }
    }

    public class MetadataSymptom
    {
        [DisplayName("症狀")]
        public string sym_name { get; set; }
    }

    public class MetadataDepartment
    {
        [DisplayName("科別")]
        public string dep_name { get; set; }
    }
    public class MetaDatafeedback
    {
        [DisplayName("問題編號")]
        public int f_sn { get; set; }

        [DisplayName("問題類別")]
        [Required(ErrorMessage = "請選擇類別!!")]
        public string f_class { get; set; }

        [DisplayName("內容")]
        [Required(ErrorMessage = "請輸入內容!!")]
        [StringLength(150, ErrorMessage = "內容不得超過150字")]
        public string f_content { get; set; }

        [DisplayName("回報時間")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public System.DateTime f_date { get; set; }

        [DisplayName("標示為已讀")]
        public bool f_checked { get; set; }
    }

}