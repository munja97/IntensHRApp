using FluentAssertions;
using HR.API.Controllers;
using HR.API.DTOs;
using HR.Service;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace HR.Test
{
    public class SkillsControllerTest
    {
        private readonly Mock<ISkillsService> repoStub = new();

        [Fact]
        public async Task GetSkillAsync_WithUnexistingSkill_RetrunsNotFOund()
        { 
            //arrange
            
            repoStub.Setup(repo => repo.GetSkillAsync(It.IsAny<Guid>())).ReturnsAsync((Skill)null);

            var controller = new SkillsController(repoStub.Object);

            //act

            var result = await controller.GetSkillAsync(Guid.NewGuid());

            //assert

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetSkillAsync_WithExistingSkill_RetrunsOk()
        {
            //arrange
            var expectedSkill = CreateRandomSkill();
            repoStub.Setup(repo => repo.GetSkillAsync(It.IsAny<Guid>())).ReturnsAsync(expectedSkill);

            var controller = new SkillsController(repoStub.Object);

            //act

            var result = await controller.GetSkillAsync(Guid.NewGuid());
            var okResult = result.Result as OkObjectResult;

            //assert

            Assert.IsType<Skill>(okResult.Value);
            Assert.Equal(expectedSkill, okResult.Value);
        }



        [Fact]
        public async Task GetSkillsAsync_WithExistingSkills_ReturnsOkSkills()
        {
            var expectedSkills = new[] { CreateRandomSkill(), CreateRandomSkill(), CreateRandomSkill() };
            repoStub.Setup(repo => repo.GetSkillsAsync()).ReturnsAsync(expectedSkills);


            var controller = new SkillsController(repoStub.Object);

            var actualSkills = await controller.GetSkills();

            actualSkills.Should().BeEquivalentTo(expectedSkills);
        }


        [Fact]
        public async Task PostSkillAsync_WithSkillToCreate_ReturnsOkSkill()
        {
            var skillToCreate = new SkillDTO()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString()

            };

            var controller = new SkillsController(repoStub.Object);

            var result = await controller.PostSkill(skillToCreate);
             var okResult = result.Result as OkObjectResult;

            var createdSkill = okResult.Value as SkillDTO;

            createdSkill.Should().BeEquivalentTo(skillToCreate);
            createdSkill.Id.Should().NotBeEmpty();

        }



        [Fact]
        public async Task UpdateSkillAsync_WithExistingSkill_ReturnNoContent()
        {
            Skill existingSkill = CreateRandomSkill();

            repoStub.Setup(repo => repo.GetSkillAsync(It.IsAny<Guid>())).ReturnsAsync(existingSkill);

            var skillId = existingSkill.Id;
            var skillToUpdate = new SkillDTO()
            {
                Name = Guid.NewGuid().ToString()
            };


            var controller = new SkillsController(repoStub.Object);

            var result = await controller.PutSkillAsync(skillId, skillToUpdate);

            result.Should().BeOfType<NoContentResult>();
        }


        [Fact]
        public async Task UpdateSkillAsync_WithNoExistingSkill_ReturnNotFound()
        {

            repoStub.Setup(repo => repo.GetSkillAsync(It.IsAny<Guid>())).ReturnsAsync((Skill)null);

            var skillToUpdate = new SkillDTO()
            {
                Name = Guid.NewGuid().ToString()
            };


            var controller = new SkillsController(repoStub.Object);

            var result = await controller.PutSkillAsync(Guid.NewGuid(), skillToUpdate);

            result.Should().BeOfType<NotFoundResult>();
        }


        [Fact]
        public async Task DeleteSkillAsync_WithExistingSkill_ReturnNoContent()
        {
            Skill existingSkill = CreateRandomSkill();

            repoStub.Setup(repo => repo.GetSkillAsync(It.IsAny<Guid>())).ReturnsAsync(existingSkill);

            var skillId = existingSkill.Id;
            


            var controller = new SkillsController(repoStub.Object);

            var result = await controller.DeleteSkillAsync(skillId);

            result.Should().BeOfType<NoContentResult>();
        }


        [Fact]
        public async Task DeleteSkillAsync_WithNoExistingSkill_ReturnNoContent()
        {

            repoStub.Setup(repo => repo.GetSkillAsync(It.IsAny<Guid>())).ReturnsAsync((Skill)null);

            


            var controller = new SkillsController(repoStub.Object);

            var result = await controller.DeleteSkillAsync(Guid.NewGuid());

            result.Should().BeOfType<NotFoundResult>();
        }



        private Skill CreateRandomSkill()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString()
            };
        }


    }
}
