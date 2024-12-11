namespace Domain.Models
{
    public class AppliedPromotion
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Condition { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is AppliedPromotion p)
            {
                return Id == p.Id
                    && Name == p.Name
                    && Description == p.Description
                    && Condition == p.Condition;
            }
            return false;
        }
    }
}
