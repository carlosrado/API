namespace API.DTOs.FixQty{
    public class GetFixQtyDTO{
                public int Id { get; set; } = 0;
                public string? Reference { get; set; } ="";
                public int IdUser { get; set; } = 0;
                public string? Responsible { get; set; } ="";
                public EReason Reason { get; set; } = EReason.Other;
                public int ReasonValue { get; set; } =0;
                public int FixQty { get; set; } =1000;
                public int Average { get; set; }=19;
                public DateTime ExpirationDate { get; set; } = DateTime.Now;
                public DateTime DeathDate { get; set; } = DateTime.Now;
                public int FixQtyCMMS { get; set; } = 1000;
                public int PackagingSize { get; set; } = 199;
                public char PartStatus { get; set; } ='C';
                public ErrorEnum Error { get; set; } =ErrorEnum.None;
                public EState State { get; set; } = EState.ToSupervise;
                public string? Comments { get; set; }="";
    }
}