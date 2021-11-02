
using abcApi.Controllers;
using abcApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABCTest
{
    public class Tests
    {
        abcDBContext _context = new abcDBContext();
        [SetUp]
        public void Setup()
        {
            
        }
        public async Task<ActionResult<IEnumerable<TblUser>>> GetTblUsers()
        {
            abcDBContext _context = new abcDBContext();
            return await _context.TblUsers.ToListAsync();
        }
        [Test]
        public  void GetAllUsers()
        {
            var users = new UsersController(_context);
            var res = users.GetTblUsers();
            var contentResult = GetTblUsers();
            Assert.AreEqual(res.Equals(contentResult), contentResult.Equals(res));
        }
        [Test]
        public void GetUser()
        {
            var users = new UsersController(_context);
            var res = users.GetTblUser(1001);
         
            var contentResult = users.GetTblUser(1001);
            Assert.AreEqual(res.Equals(contentResult), contentResult.Equals(res));
        }
        [Test]
        public void CategoryExist()
        {
            var categories = new TblCategoriesController(_context);
            var res = categories.GetTblCategory(1001);
          
            var contentResult = categories.GetTblCategory(1001);
            Assert.AreEqual(res.Equals(contentResult), contentResult.Equals(res));
        }
    }
}