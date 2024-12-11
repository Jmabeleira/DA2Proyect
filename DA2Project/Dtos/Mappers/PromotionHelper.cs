using Domain.Models;
using Dtos.Models;

namespace Dtos.Mappers
{
    public static class PromotionHelper
    {
        public static AppliedPromotion FromAppliedPromotionDtoToAppliedPromotion(AppliedPromotionDto appliedPromotion)
        {
            if (appliedPromotion == null)
            {
                return null;
            }

            return new AppliedPromotion
            {
                Id = appliedPromotion.Id,
                Name = appliedPromotion.Name,
                Description = appliedPromotion.Description,
                Condition = appliedPromotion.Condition
            };
        }

        public static AppliedPromotionDto FromAppliedPromotionToAppliedPromotionDto(AppliedPromotion appliedPromotion)
        {
            if (appliedPromotion == null)
            {
                return null;
            }

            return new AppliedPromotionDto
            {
                Id = appliedPromotion.Id,
                Name = appliedPromotion.Name,
                Description = appliedPromotion.Description,
                Condition = appliedPromotion.Condition
            };
        }
    }
}
