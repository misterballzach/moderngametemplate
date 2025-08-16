using NUnit.Framework;
using Lex.Sim;
using Lex.Sim.Commands;

namespace Lex.Tests
{
    public class SimulationIntegrationTests
    {
        [Test]
        public void DealDamageCommand_ReducesTargetHealth()
        {
            // Arrange
            var ctx = new SimContext(seed: 1);
            var playerStats = new StatBlock(maxHealth: 100, maxMana: 50);
            var player = ctx.CreateEntity(playerStats);

            int initialHealth = player.Stats.Health;
            int damageAmount = 10;
            var dealDamageCmd = new DealDamage(player.Id, damageAmount);

            // Act
            ctx.Queue.Enqueue(dealDamageCmd);
            ctx.Resolve();

            // Assert
            int finalHealth = player.Stats.Health;
            Assert.AreEqual(initialHealth - damageAmount, finalHealth, "Health should be reduced by the damage amount.");
        }

        [Test]
        public void ChainedCommands_ExecuteInOrder()
        {
            // Arrange
            var ctx = new SimContext(seed: 2);
            var target1 = ctx.CreateEntity(new StatBlock(100, 0));
            var target2 = ctx.CreateEntity(new StatBlock(100, 0));

            var cmd1 = new DealDamage(target1.Id, 10);
            var cmd2 = new DealDamage(target2.Id, 20);
            var cmd3 = new DealDamage(target1.Id, 5);

            // Act
            ctx.Queue.Enqueue(cmd1);
            ctx.Queue.Enqueue(cmd2);
            ctx.Queue.Enqueue(cmd3);
            ctx.Resolve();

            // Assert
            Assert.AreEqual(100 - 10 - 5, target1.Stats.Health);
            Assert.AreEqual(100 - 20, target2.Stats.Health);
        }
    }
}
