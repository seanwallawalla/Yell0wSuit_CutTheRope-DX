using CutTheRopeDX.Framework.Core;
using CutTheRopeDX.Framework.Physics;
using CutTheRopeDX.GameMain;

using Xunit;

namespace CutTheRopeDX.Tests
{
    public class CandyCapabilitiesTests
    {
        [Fact]
        public void Candy_DefaultCapabilitiesMatchCurrentCandyBehavior()
        {
            CandyCapabilities candy = CandyCapabilities.Candy;

            Assert.True(candy.CanCollectStars);
            Assert.True(candy.CanOpenMouth);
            Assert.True(candy.CanBeEaten);
            Assert.True(candy.CanLoseLevelWhenOffScreen);
            Assert.True(candy.CanBeGrabbedBySpider);
            Assert.True(candy.CanBeGrabbedByMouse);
            Assert.True(candy.CanBeGrabbedByHand);
            Assert.True(candy.CanEnterTransport);
            Assert.True(candy.CanFloatInWater);
            Assert.True(candy.CanBeDraggedBySnail);
        }

        [Fact]
        public void LightBulb_IsPhysicalButNotCandyConsumable()
        {
            CandyCapabilities bulb = CandyCapabilities.LightBulb;

            Assert.False(bulb.CanCollectStars);
            Assert.False(bulb.CanOpenMouth);
            Assert.False(bulb.CanBeEaten);
            Assert.False(bulb.CanLoseLevelWhenOffScreen);
            Assert.False(bulb.CanBeGrabbedBySpider);
            Assert.False(bulb.CanBeGrabbedByMouse);
            Assert.False(bulb.CanBeGrabbedByHand);
            Assert.True(bulb.CanEnterTransport);
            Assert.False(bulb.CanFloatInWater);
            Assert.False(bulb.CanBeDraggedBySnail);
        }

        [Fact]
        public void Axe_IsPhysicalHazardButNotCandyConsumable()
        {
            CandyCapabilities axe = CandyCapabilities.Axe;

            Assert.False(axe.CanCollectStars);
            Assert.False(axe.CanOpenMouth);
            Assert.False(axe.CanBeEaten);
            Assert.False(axe.CanLoseLevelWhenOffScreen);
            Assert.False(axe.CanBeGrabbedBySpider);
            Assert.False(axe.CanBeGrabbedByMouse);
            Assert.False(axe.CanBeGrabbedByHand);
            Assert.False(axe.CanEnterLantern);
            Assert.True(axe.CanEnterTransport);
            Assert.True(axe.CanBindRocket);
            Assert.False(axe.CanAttachAnts);
            Assert.False(axe.CanCollideWithCandyBodies);
            Assert.False(axe.CanBeBrokenByHazards);
            Assert.False(axe.CanFloatInWater);
            Assert.False(axe.CanBeDraggedBySnail);
        }

        [Fact]
        public void CandyContext_ToView_PreservesCapabilities()
        {
            CandyContext ctx = new()
            {
                point = new ConstraintedPoint
                {
                    pos = new Vector(1f, 2f)
                },
                Capabilities = CandyCapabilities.LightBulb
            };

            CandyView view = ctx.ToView();

            Assert.False(view.Capabilities.CanBeEaten);
            Assert.False(view.Capabilities.CanCollectStars);
        }

        [Fact]
        public void CandyInteraction_GatesLightBulbCandyOnlyInteractions()
        {
            CandyContext bulb = new()
            {
                Capabilities = CandyCapabilities.LightBulb
            };

            Assert.False(CandyInteraction.CanCollectStar(bulb));
            Assert.False(CandyInteraction.CanBeGrabbedByHand(bulb));
            Assert.False(CandyInteraction.CanAttachAnts(bulb));
            Assert.False(CandyInteraction.CanBeBrokenByHazards(bulb));
        }

        [Fact]
        public void CandyInteraction_GatesAxeCandyOnlyInteractions()
        {
            CandyContext axe = new()
            {
                Capabilities = CandyCapabilities.Axe
            };

            Assert.False(CandyInteraction.CanCollectStar(axe));
            Assert.False(CandyInteraction.CanBeGrabbedByHand(axe));
            Assert.False(CandyInteraction.CanAttachAnts(axe));
            Assert.False(CandyInteraction.CanBeBrokenByHazards(axe));
        }
    }
}
