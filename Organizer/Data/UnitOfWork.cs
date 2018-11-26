using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Organizer.Data
{
    public class UnitOfWork
    {
        private ApplicationDbContext _dbContext;
        private GroupsRepo _groupsRepo;
        private NotesRepo _notesRepo;
        private TODOItemsRepo _TODOItemsRepo;
        private UserEventsRepo _userEventsRepo;
        private UsersRepo _usersRepo;

        public  TODOItemsRepo TODOItemsRepo
        {
            get
            {
                if(_TODOItemsRepo == null)
                {
                    _TODOItemsRepo = new TODOItemsRepo();
                }
                return _TODOItemsRepo;
            }
        }

        public void Complete()
        {
            _dbContext.SaveChanges();
        }

    }
}