using FluentAssertions;
using HR.API.Controllers;
using HR.API.DTOs;
using HR.Service;
using Microsoft.AspNetCore.Mvc;
using Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HR.Test
{
    public class CandidatesControllerTest
    {
        private readonly Mock<ICandidatesService> repositoryStub = new Mock<ICandidatesService>();
        private readonly Mock<ISkillsService> skillRepoStub = new Mock<ISkillsService>();
        private readonly Random rand = new();

        [Fact]
        public async Task GetCandidate_WithUnexistingCandidate_ReturnsNotFound()
        {
            // Arrange
            repositoryStub.Setup(repo => repo.GetCandidateAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Candidate)null);

            var controller = new CandidatesController(repositoryStub.Object,skillRepoStub.Object);

            // Act
            var result = await controller.GetCandidateAsync(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetCandidate_WithExistingCandidate_ReturnsOk()
        {
            // Arrange
            var expectedCandidate = CreateRandomCandidate();

            repositoryStub.Setup(repo => repo.GetCandidateAsync(It.IsAny<Guid>()))
                .ReturnsAsync(expectedCandidate);

            var controller = new CandidatesController(repositoryStub.Object,skillRepoStub.Object);

            // Act
            var result = await controller.GetCandidateAsync(Guid.NewGuid());
            var okResult = result.Result as OkObjectResult;

            // Assert
            Assert.IsType<Candidate>(okResult.Value);
            Assert.Equal(expectedCandidate, okResult.Value);
        }

        [Fact]
        public async Task GetCandidatesAsync_WithExistingCandidates_ReturnsOk()
        {
            //arramge
            var expectedCandidates = new[] { CreateRandomCandidate(), CreateRandomCandidate(), CreateRandomCandidate() };
            repositoryStub.Setup(repo => repo.GetCandidatesAsync()).ReturnsAsync(expectedCandidates);

            var controller = new CandidatesController(repositoryStub.Object,skillRepoStub.Object);
            
            //act
            var actualCandidates = await controller.GetCandidatesAsync();

            //assert
            actualCandidates.Should().BeEquivalentTo(expectedCandidates);
        }

        [Fact]
        public async Task PostCandidateAsync_WithCandidateToCreate_ReturnOkCandidate()
        {
            //arrange
            var candidateToCreate = new CandidateDTO
            {

                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                ContactNumber = 1234567890,
                Email = Guid.NewGuid().ToString(),
                DateOfBirth = new DateTime()
            };

            var controller = new CandidatesController(repositoryStub.Object, skillRepoStub.Object);
           
            //act
            var result = await controller.PostCandidateAsync(candidateToCreate);
            var okResult = result.Result as OkObjectResult;

            //assert
            var createdCandidate = okResult.Value as CandidateDTO;

            createdCandidate.Should().BeEquivalentTo(candidateToCreate);
            createdCandidate.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task UpdateCanInfo_WithExistingCandidate_ReturnNoContent()
        {
            //arrange
            Candidate existingCandidate = CreateRandomCandidate();


            repositoryStub.Setup(repo => repo.GetCandidateAsync(It.IsAny<Guid>())).ReturnsAsync(existingCandidate);

            var canId = existingCandidate.Id;
            var canToUpdate = new CandidateDTO()
            {
                Name = Guid.NewGuid().ToString(),
                ContactNumber = 1234567890,
                Email = Guid.NewGuid().ToString(),
                DateOfBirth = new DateTime()
            };


            var controller = new CandidatesController(repositoryStub.Object,skillRepoStub.Object);

            //act
            var result = await controller.UpdateCandidateInfoAsync(canId,canToUpdate);

            //assert
            result.Should().BeOfType<NoContentResult>();
        }



        [Fact]
        public async Task UpdateCanInfo_WithNoExistingCandidate_ReturnNotFound()
        {
            //arrange


            repositoryStub.Setup(repo => repo.GetCandidateAsync(It.IsAny<Guid>())).ReturnsAsync((Candidate)null);

            var canToUpdate = new CandidateDTO()
            {
                Name = Guid.NewGuid().ToString(),
                ContactNumber = 1234567890,
                Email = Guid.NewGuid().ToString(),
                DateOfBirth = new DateTime()
            };


            var controller = new CandidatesController(repositoryStub.Object, skillRepoStub.Object);

            //act
            var result = await controller.UpdateCandidateInfoAsync(Guid.NewGuid(), canToUpdate);

            //assert
            result.Should().BeOfType<NotFoundResult>();
        }


        [Fact]
        public async Task DeleteCanAsync_WithExistingCan_ReturnNoContent()
        {
            Candidate existingCandidate = CreateRandomCandidate();

            repositoryStub.Setup(repo => repo.GetCandidateAsync(It.IsAny<Guid>())).ReturnsAsync(existingCandidate);

            var canId = existingCandidate.Id;



            var controller = new CandidatesController(repositoryStub.Object, skillRepoStub.Object);

            var result = await controller.DeleteCandidateAsync(canId);

            result.Should().BeOfType<NoContentResult>();
        }


        [Fact]
        public async Task DeleteCandidateAsync_WithNoExistingCan_ReturnNoContent()
        {

            repositoryStub.Setup(repo => repo.GetCandidateAsync(It.IsAny<Guid>())).ReturnsAsync((Candidate)null);



            var controller = new CandidatesController(repositoryStub.Object, skillRepoStub.Object);


            var result = await controller.DeleteCandidateAsync(Guid.NewGuid());

            result.Should().BeOfType<NotFoundResult>();
        }


        [Fact]
        public async Task UpdateCandidateSkill_WithExistingCandidateAndSkill_ReturnNoContent()
        {
            //arrange
            Candidate existingCandidate = CreateRandomCandidate();
            Skill existingSkill = CreateRandomSkill();


            repositoryStub.Setup(repo => repo.GetCandidateAsync(It.IsAny<Guid>())).ReturnsAsync(existingCandidate);
            skillRepoStub.Setup(repo => repo.GetSkillAsync(It.IsAny<Guid>())).ReturnsAsync(existingSkill);

            var skillId = existingSkill.Id;
            var canId = existingCandidate.Id;
            
            var canToUpdate = new CandidateDTO()
            {
                Name = Guid.NewGuid().ToString(),
                ContactNumber = 1234567890,
                Email = Guid.NewGuid().ToString(),
                DateOfBirth = new DateTime()
            };

            var skillToUpdate = new SkillDTO()
            {
                Name = Guid.NewGuid().ToString()
            };

            var controller = new CandidatesController(repositoryStub.Object, skillRepoStub.Object);

            //act
            var result = await controller.UpdateCandidateSkill(canId, skillId);

            //assert
            result.Should().BeOfType<NoContentResult>();
        }


        [Fact]
        public async Task UpdateCandidateSkill_WithExistingCandidateAndNoExistingSkill_ReturnNotFound()
        {
            Candidate existingCandidate = CreateRandomCandidate();
            

            repositoryStub.Setup(repo => repo.GetCandidateAsync(It.IsAny<Guid>())).ReturnsAsync(existingCandidate);
            skillRepoStub.Setup(repo => repo.GetSkillAsync(It.IsAny<Guid>())).ReturnsAsync((Skill)null);

            var canId = existingCandidate.Id;

            var canToUpdate = new CandidateDTO()
            {
                Name = Guid.NewGuid().ToString(),
                ContactNumber = 1234567890,
                Email = Guid.NewGuid().ToString(),
                DateOfBirth = new DateTime()
            };

            var controller = new CandidatesController(repositoryStub.Object, skillRepoStub.Object);

            //act
            var result = await controller.UpdateCandidateSkill(canId, Guid.NewGuid());

            //assert
            result.Should().BeOfType<NotFoundResult>();

        }


        [Fact]
        public async Task UpdateCandidateSkill_WithNoExistingCandidateAndExistingSkill_ReturnNotFound()
        {

            Skill existingSkill = CreateRandomSkill();


            repositoryStub.Setup(repo => repo.GetCandidateAsync(It.IsAny<Guid>())).ReturnsAsync((Candidate)null);
            skillRepoStub.Setup(repo => repo.GetSkillAsync(It.IsAny<Guid>())).ReturnsAsync(existingSkill);

            var skillId = existingSkill.Id;

         
            var skillToUpdate = new SkillDTO()
            {
                Name = Guid.NewGuid().ToString()
            };

            var controller = new CandidatesController(repositoryStub.Object, skillRepoStub.Object);

            //act
            var result = await controller.UpdateCandidateSkill(Guid.NewGuid(), skillId);

            //assert
            result.Should().BeOfType<NotFoundResult>();

        }


        [Fact]
        public async Task UpdateCandidateSkill_WithNoExistingCandidateAndNoExistingSkill_ReturnNotFound()
        {
            Candidate existingCandidate = CreateRandomCandidate();
            Skill existingSkill = CreateRandomSkill();


            repositoryStub.Setup(repo => repo.GetCandidateAsync(It.IsAny<Guid>())).ReturnsAsync((Candidate)null);
            skillRepoStub.Setup(repo => repo.GetSkillAsync(It.IsAny<Guid>())).ReturnsAsync((Skill)null);

           
            var canToUpdate = new CandidateDTO()
            {
                Name = Guid.NewGuid().ToString(),
                ContactNumber = 1234567890,
                Email = Guid.NewGuid().ToString(),
                DateOfBirth = new DateTime()
            };
            var skillToUpdate = new SkillDTO()
            {
                Name = Guid.NewGuid().ToString()
            };

            var controller = new CandidatesController(repositoryStub.Object, skillRepoStub.Object);

            //act
            var result = await controller.UpdateCandidateSkill(Guid.NewGuid(), Guid.NewGuid());

            //assert
            result.Should().BeOfType<NotFoundResult>();

        }


        [Fact]
        public async Task SearchCandidate_WithFoundCandidateAndSearchPhraseNotNull_ReturnsOk()
        {
            // Arrange
            var expectedCandidates = new List<Candidate> { CreateRandomCandidate(), CreateRandomCandidate() };

            repositoryStub.Setup(repo => repo.SearchCandidatesAsync(It.IsAny<string>())).ReturnsAsync(expectedCandidates);

            var controller = new CandidatesController(repositoryStub.Object, skillRepoStub.Object);

            // Act
            var result = await controller.SearchCandidateAsync(It.IsAny<string>());
            var okResult = result as OkObjectResult;

            // Assert
            Assert.Equal(expectedCandidates, okResult.Value);
        }
        private Candidate CreateRandomCandidate()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                ContactNumber = 1234567890,
                Email = Guid.NewGuid().ToString(),
                DateOfBirth = new DateTime()
            };
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
