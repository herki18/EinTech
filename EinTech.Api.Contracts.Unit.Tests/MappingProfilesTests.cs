using System;
using AutoMapper;
using EinTech.Api.Contracts.Entities;
using EinTech.Api.Contracts.Mappers;
using EinTech.Api.Contracts.Models;
using Xunit;

namespace EinTech.Api.Contracts.Unit.Tests
{
    public class MappingProfilesTests
    {
        private readonly IMapper _mapper;

        public MappingProfilesTests()
        {
            _mapper = new MapperConfiguration(mappings =>
            {
                mappings.AddProfile<MappingProfiles>();
            }).CreateMapper();
        }

        [Fact]
        public void Should_Map_CreatePersonModel_To_PersonEntity()
        {
            var createPersonModel = new CreatePersonModel()
            {
                GroupId = 1,
                Name = "Test"
            };

            var personEntity = _mapper.Map<PersonEntity>(createPersonModel);

            Assert.Equal(createPersonModel.GroupId, personEntity.GroupId);
            Assert.Equal(createPersonModel.Name, personEntity.Name);
        }

        [Fact]
        public void Should_Map_GroupEntity_To_GroupModel()
        {
            var groupEntity = new GroupEntity()
            {
                Id = 1,
                Name = "GroupA",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            var groupModel = _mapper.Map<GroupModel>(groupEntity);

            Assert.Equal(groupEntity.Id, groupModel.Id);
            Assert.Equal(groupEntity.Name, groupModel.Name);
        }

        [Fact]
        public void Should_Map_PersonEntity_To_PersonModel()
        {
            var personEntity = new PersonEntity()
            {
                Id = 1,
                Name = "Test",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                GroupId = 1,
                Group = new GroupEntity()
                {
                    Name = "GroupA"
                }
            };

            var personModel = _mapper.Map<PersonModel>(personEntity);

            Assert.Equal(personEntity.Name, personModel.Name);
            Assert.Equal(personEntity.Group.Name, personModel.GroupName);
        }
    }
}
