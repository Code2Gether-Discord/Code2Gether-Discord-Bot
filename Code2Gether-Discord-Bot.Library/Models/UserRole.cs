using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code2Gether_Discord_Bot.Library.Models
{
    public class UserRole
    {
        #region Fields
        // Allows the Entity Framework to load a value only when its called (not immediately)
        private readonly ILazyLoader _lazyLoader;
        private Project _project;
        private User _user;
        private ProjectRole _projectRole;
        #endregion

        #region Properties
        [Key]
        public long ID { get; set; }
        [ForeignKey(nameof(Project))]
        public long ProjectID { get; set; }
        [ForeignKey(nameof(User))]
        public long UserID { get; set; }
        [ForeignKey(nameof(ProjectRole))]
        public long ProjectRoleID { get; set; }
        public Project Project
        {
            get => _lazyLoader.Load(this, ref _project);
            set => _project = value;
        }
        public User User
        {
            get => _lazyLoader.Load(this, ref _user);
            set => _user = value;
        }
        public ProjectRole ProjectRole
        {
            get => _lazyLoader.Load(this, ref _projectRole);
            set => _projectRole = value;
        }
        #endregion
    }
}
