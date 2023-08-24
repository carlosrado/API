namespace API.DTOs.FixQty{
    public class AddFixQtyDTO{
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
        public void RandomizeValues()
        {
            Random random = new ();

            // Example: Generating a random string for the Reference property
            Reference = Guid.NewGuid().ToString();

            // Example: Generating a random int for the IdUser property
            IdUser = random.Next(1, 1000);

            // Example: Generating a random string for the Responsible property
            Responsible = "randomUser" + random.Next(1, 100)+"@ford.com";

            // Example: Generating a random EReason enum value for the Reason property
            Reason = (EReason)random.Next(0, Enum.GetValues(typeof(EReason)).Length);
            PackagingSize = random.Next(1, 50000);
            Average = random.Next(1, 30000);
            switch (Reason)
            {
                case EReason.PointsOfUse:
                    ReasonValue = random.Next(1, 30);
                    FixQty = ReasonValue*PackagingSize;

                    break;

                case EReason.DaysToCover:
                    ReasonValue = random.Next(1, 30);
                    FixQty = ReasonValue*Average;
                    break;
                case EReason.Other:
                    ReasonValue = 0;
                    FixQty = random.Next(1, 150000);
                    break;
            }
            ExpirationDate = DateTime.Now.AddDays(random.Next(1, 30));
            DeathDate = DateTime.Now.AddDays(random.Next(365, 1095));
            FixQtyCMMS = random.Next(1, 150000);
            char[] partStatusOptions = { 'C', 'N', 'C', 'C' };
            PartStatus = partStatusOptions[random.Next(0, partStatusOptions.Length)];
            Error = GetError();
            State = EState.ToSupervise;
            Comments = "Random comment " + random.Next(1, 1000);
        }
        public ErrorEnum GetError()
        {
            // Check if the ExpirationDate is less than a week from the current date
            if (ExpirationDate < DateTime.Now.AddDays(7))
            {
                return ErrorEnum.EndDate;
            }

            // Check if the ExpirationDate is less than two weeks but more than one week from the current date
            if (ExpirationDate < DateTime.Now.AddDays(14))
            {
                return ErrorEnum.CloseEndDate;
            }

            // Add additional cases for future application here if needed
            // ...

            // Check if FixQty and FixQtyCMMS are different
            if (FixQty != FixQtyCMMS)
            {
                return ErrorEnum.OutStand;
            }

            // If none of the above cases match, return ErrorEnum.OutOfReason (for future application)
            return ErrorEnum.OutReason;
        }
    }

}