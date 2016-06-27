﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepveUOW.Data.UnitOfWork;
using RepveUOW.Data.Repository;
using RepveUOW.Data.Model;
using RepveUOW.Data;

namespace RepveUOW.Presentation.UnitTest
{
    [TestClass]
    public class EntityTest
    {
        // Entity framework için geliştirmiş olduğumuz context. Farklı ORM veya Veritabanı içinde bu context'i değiştirebiliriz.
        private EFBlogContext _dbContext;

        private IUnitOfWork _uow;
        private IRepository<User> _userRepository;
        private IRepository<Category> _categoryRepository;
        private IRepository<Article> _articleRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _dbContext = new EFBlogContext();

            // EFBlogContext'i kullanıyor olduğumuz için EFUnitOfWork'den türeterek constructor'ına
            // ilgili context'i constructor injection yöntemi ile inject ediyoruz.
            _uow = new EFUnitOfWork(_dbContext);
            _userRepository = new EFRepository<User>(_dbContext);
            _categoryRepository = new EFRepository<Category>(_dbContext);
            _articleRepository = new EFRepository<Article>(_dbContext);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _dbContext = null;
            _uow.Dispose();
        }


        [TestMethod]
        public void AddUser()
        {
            User user = new User
            {
                FirstName = "Erkan",
                LastName = "GÜZELKÜÇÜK",
                CreatedDate = DateTime.Now,
                Email = "guzelkucukerkan@gmail.com",
                Password = "11111"
            };

            _userRepository.Add(user);
            int process = _uow.SaveChanges();

            Assert.AreNotEqual(-1, process);
        }

        [TestMethod]
        public void GetUser()
        {
            User user = _userRepository.GetById(1);

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void UpdateUser()
        {
            User user = _userRepository.GetById(1);

            user.FirstName = "Mehmet";

            _userRepository.Update(user);
            int process = _uow.SaveChanges();

            Assert.AreNotEqual(-1, process);
        }

        [TestMethod]
        public void DeleteUser()
        {
            User user = _userRepository.GetById(1);

            _userRepository.Delete(user);
            int process = _uow.SaveChanges();

            Assert.AreNotEqual(-1, process);
        }
    }
}
