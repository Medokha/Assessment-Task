namespace Assessment.ViewModels.Edu
{
    public class Stumatrial
    {
        public string DocName { get; set; }   // اسم الدكتور
        public string SubName { get; set; }   // اسم المادة
        public string Dep { get; set; }       // القسم
        public int Quest { get; set; }        // درجة السعي
        public int Total { get; set; }        // الدرجة النهائية
        public int Traqomy { get; set; }      // التقدير التراكمي
        public int StageId { get; set; } // الـ ID الخاص بالمرحلة
        public string StageName { get; set; } 
        public string Grade { get; set; }
        public string? FileName { get; set; }
    }
}
