using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EinTech.Api.Contracts.Entities;
using EinTech.Api.Contracts.Interfaces.Repositories;
using EinTech.Api.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace EinTech.ViewComponents
{
    public class ListViewComponent : ViewComponent
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public ListViewComponent(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync(string searchString = null)
        {
            IList<PersonEntity> persons;
            if (String.IsNullOrEmpty(searchString))
            {
                persons = await _personRepository.GetPersons();
            }
            else
            {
                persons = await _personRepository.GetPersonsByNameOrGroup(searchString);
            }


            var dt = _mapper.Map<List<PersonModel>>(persons.ToList());

            return View("Default", dt);
        }
    }
}
