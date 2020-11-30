using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code2Gether_Discord_Bot.Library.Models
{
    [Table("PROJECT_MEMBER")]
    public class ProjectMember
    {
        #region Fields
        // Allows the Entity Framework to load a value only when its called (not immediately)
        private readonly ILazyLoader _lazyLoader;
        private Project _project;
        private Member _member;
        #endregion

        #region Properties
        [Column("PROJECT_ID")]
        [ForeignKey(nameof(Project))]
        public int ProjectID { get; set; }
        [Column("MEMBER_ID")]
        [ForeignKey(nameof(Member))]
        public int MemberID { get; set; }
        public Project Project
        {
            get => _lazyLoader.Load(this, ref _project);
            set
            {
                _project = value;
                ProjectID = _project.ID;
            }
        }
        public Member Member
        {
            get => _lazyLoader.Load(this, ref _member);
            set
            {
                _member = value;
                MemberID = _member.ID;
            }
        }
        #endregion

        #region Constructor
        public ProjectMember() { }

        public ProjectMember(ILazyLoader lazyLoader) : this()
        {
            _lazyLoader = lazyLoader;
        }
        #endregion
    }
}
