using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EinTech.Api.Contracts.Entities;
using EinTech.Api.Contracts.Interfaces.Repositories;
using EinTech.Api.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EinTech.Controllers
{
    public class PersonsController : Controller
    {
        private readonly IPersonRepository _personRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public PersonsController(IPersonRepository personRepository, IGroupRepository groupRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public IActionResult Index(string searchString = null)
        {
            ViewBag.SearchString = searchString;
            return View();
        }

        public async Task<IActionResult> Create()
        {
            await PopulateGroupList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(include: "Name, GroupId")]CreatePersonModel create)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _personRepository.AddPerson(_mapper.Map<PersonEntity>(create));
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Saving failed");
            }
            await PopulateGroupList(create.GroupId);
            return View(create);
        }

        private async Task PopulateGroupList(object selectedGroup = null)
        {
            var groups = await _groupRepository.GetGroups();
            var groupModels = _mapper.Map<List<GroupModel>>(groups.ToList());
            ViewBag.GroupId = new SelectList(groupModels, "Id", "Name", selectedGroup);
        }
    }
}